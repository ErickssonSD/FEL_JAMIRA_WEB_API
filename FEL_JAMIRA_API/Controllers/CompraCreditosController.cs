using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FEL_JAMIRA_WEB_API.Models;
using FEL_JAMIRA_API.Models.Clientes;

namespace FEL_JAMIRA_API.Controllers
{
    public class CompraCreditosController : GenericController<CompraCreditos>
    {
        /// <summary>
        /// Método para Buscar os recebimentos do usuários.
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("~/api/CompraCreditos/GetTransacoesDoCliente")]
        public async Task<ResponseViewModel<List<TransacoesDoCliente>>> GetTransacoesDoCliente(string idUsuario)
        {
            try
            {
                int valor = int.Parse(idUsuario);
                List<TransacoesDoCliente> solicitacao = await db.CompraCreditos
                                            .Where(x => x.IdCliente.Equals(valor))
                                            .Select(cl => new TransacoesDoCliente
                                            {
                                                DataTransacao = cl.DataTransacao,
                                                Valor = cl.Credito
                                            }).ToListAsync();
                ResponseViewModel<List<TransacoesDoCliente>> response = new ResponseViewModel<List<TransacoesDoCliente>>
                {
                    Data = solicitacao,
                    Mensagem = "Dados retornados com sucesso!",
                    Serializado = true,
                    Sucesso = true
                };

                return response;
            }
            catch (Exception e)
            {
                return new ResponseViewModel<List<TransacoesDoCliente>>()
                {
                    Data = null,
                    Serializado = true,
                    Sucesso = false,
                    Mensagem = "Não foi possivel atender a sua solicitação: " + e.Message
                };
            }
        }
    }
}
