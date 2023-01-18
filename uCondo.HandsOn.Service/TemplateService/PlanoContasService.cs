using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using uCondo.HandsOn.Domain.Entities;
using uCondo.HandsOn.Infrastructure.Interfaces;
using uCondo.HandsOn.Service.Interfaces;

namespace uCondo.HandsOn.Service.TemplateService
{
    public class PlanoContasService : IPlanoContasService
    {
        private readonly IPlanoContasRepository _planoContasRepository;
        private readonly IConfiguration _config;

        public PlanoContasService(IPlanoContasRepository planoContasRepository, IConfiguration config)
        {
            _planoContasRepository = planoContasRepository;
            _config = config;
        }

        public async Task<List<PlanoContaEntity>> Listar()
        {
            return (await _planoContasRepository.Listar()).ToList();
        }

        public async Task<Dictionary<string, string>> BuscarFilha(string codPai)
        {
            var ls = (await _planoContasRepository.Listar()).ToList();
            var listaFiltrada = ls.Where(a => a.COD_PAI_PLANO_CONTAS == codPai).ToList();

            var retorno = new Dictionary<string, string>();
            var lstInt = new List<int>();
            var tmn = 0;
            foreach (var item in listaFiltrada)
            {
                string[] splt = item.COD_PLANO_CONTAS.Split('.');
                tmn = splt.Length;
                int filha = Convert.ToInt32(splt[splt.Length - 1]);
                lstInt.Add(filha);
            }
            int next = lstInt.Max() + 1;

            if (next == 1000)
            {
                string[] splt = listaFiltrada[0].COD_PLANO_CONTAS.Split('.');
                if (splt.Length == 2)
                {
                    var listaFiltradaNext = ls.Where(a => a.COD_PAI_PLANO_CONTAS == (Convert.ToInt32(codPai) + 1).ToString()).ToList();
                    var lstIntNext = new List<int>();
                    foreach (var item in listaFiltradaNext)
                    {
                        string[] spltNext = item.COD_PLANO_CONTAS.Split('.');
                        tmn = spltNext.Length;
                        int filhaNext = Convert.ToInt32(spltNext[spltNext.Length - 1]);
                        lstIntNext.Add(filhaNext);
                    }
                    next = lstIntNext.Max() + 1;


                    var filha =  listaFiltradaNext[0].COD_PAI_PLANO_CONTAS + "." + next.ToString();

                    retorno.Add("filha", filha);
                    retorno.Add("pai", listaFiltradaNext[0].COD_PAI_PLANO_CONTAS);
                }
                else
                {
                    var lstIntNext = new List<int>();
                    var c = codPai.Split('.');
                    listaFiltrada = ls.Where(a => a.COD_PAI_PLANO_CONTAS == c[0]).ToList(); 
                    foreach (var item in listaFiltrada)
                    {
                        string[] spltNext = item.COD_PLANO_CONTAS.Split('.');
                        tmn = spltNext.Length;
                        int filha = Convert.ToInt32(spltNext[1]);
                        lstIntNext.Add(filha);
                    }
                    next = lstIntNext.Max() + 1;

                    var check = splt[0] + "." + next;
                    retorno.Add("filha", check);
                    retorno.Add("pai", splt[0]);
                }
            }
            else
            {
                string[] splt = listaFiltrada[0].COD_PLANO_CONTAS.Split('.');
                var r = "";
                for (int i = 0; i < tmn - 1; i++)
                {
                    r = r + splt[i] + ".";
                }
                var filha = r + next;
                retorno.Add("filha", filha);
                retorno.Add("pai", listaFiltrada[0].COD_PAI_PLANO_CONTAS);
            }

            return retorno;
        }

        public async Task Inserir(PlanoContaEntity planoContas)
        {
            await _planoContasRepository.Inserir(planoContas);
        }
    }
}
