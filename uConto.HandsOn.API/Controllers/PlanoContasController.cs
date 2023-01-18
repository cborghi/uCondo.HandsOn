using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System;
using uCondo.HandsOn.Service.Interfaces;
using System.Linq;
using System.Web.Http;
using uCondo.HandsOn.Domain;
using System.Collections.Generic;
using uCondo.HandsOn.Service.TemplateService;
using uCondo.HandsOn.Domain.Entities;

namespace uCondo.HandsOn.API.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/HandsOn")]
    public class PlanoContasController : Controller
    {
        private readonly IPlanoContasService _planoContasService;
        private readonly IConfiguration _config;

        public PlanoContasController(IPlanoContasService planoContasService, IConfiguration config)
        {
            _planoContasService = planoContasService;
            _config = config;
        }

        /// <summary>
        /// Lista todos os planos de conta
        /// </summary>
        /// <returns>List<PlanoContaEntity></returns>
        [Microsoft.AspNetCore.Mvc.HttpGet()]
        [Microsoft.AspNetCore.Mvc.Route("ListarPlanoContas")]
        public async Task<IActionResult> Listar([FromUri] string filtro = null, Guid? idPlanoConta = null)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(filtro))
                {
                    var retorno = await _planoContasService.Listar();
                    return Ok(retorno.Where(a => a.COD_PLANO_CONTAS.Contains(filtro) || a.NOME_CONTAS.Contains(filtro)));
                    
                }
                else if(idPlanoConta != null)
                {
                    var retorno = await _planoContasService.Listar();
                    return Ok(retorno.Where(a => a.ID_PLANO_CONTAS == idPlanoConta));
                }
                else
                {
                    var retorno = await _planoContasService.Listar();
                    return Ok(retorno);
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = "error", message = ex.Message });
            }
        }

        /// <summary>
        /// Lista todos os planos de conta Pai
        /// </summary>
        /// <returns>List<PlanoContaEntity></returns>
        [Microsoft.AspNetCore.Mvc.HttpGet()]
        [Microsoft.AspNetCore.Mvc.Route("ListarPlanoContasPai")]
        public async Task<IActionResult> ListarPai()
        {
            try
            {                
                var retorno = await _planoContasService.Listar();
                return Ok(retorno.Where(a => !a.ACEITA_LANCAMENTOS));
            }
            catch (Exception ex)
            {
                return Json(new { status = "error", message = ex.Message });
            }
        }

        /// <summary>
        /// Busca a filha seguinte ao plano de conta pai escolhido
        /// </summary>
        /// <returns>string</returns>
        [Microsoft.AspNetCore.Mvc.HttpGet()]
        [Microsoft.AspNetCore.Mvc.Route("BuscarFilhaSeguinte")]
        public async Task<IActionResult> BuscarFilhaSeguinte([FromUri] string codPai)
        {
            try
            {
                Dictionary<string, string> retorno = await _planoContasService.BuscarFilha(codPai);
                return Json (new { codigoPai = retorno["pai"], codigoFilha = retorno["filha"] });
            }
            catch (Exception ex)
            {
                return Json(new { status = "error", message = ex.Message });
            }
        }

        /// <summary>
        /// Busca a filha seguinte ao plano de conta pai escolhido
        /// </summary>
        /// <returns>string</returns>
        [Microsoft.AspNetCore.Mvc.HttpPost()]
        [Microsoft.AspNetCore.Mvc.Route("InserirPlanoContas")]
        public async Task<IActionResult> Inserir([Microsoft.AspNetCore.Mvc.FromBody] PlanoContaEntity planoContas)
        {
            try
            {
                await _planoContasService.Inserir(planoContas);
                return Ok();
            }
            catch (Exception ex)
            {
                return Json(new { status = "error", message = ex.Message });
            }
        }
    }
}
