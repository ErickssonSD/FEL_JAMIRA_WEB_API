﻿using System;
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
    public class CarrosController : GenericController<Carro>
    {

        /// <summary>
        /// Método para buscar um registro em especifico.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Carros/Detalhes/5
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("~/api/Carros/BuscarCarroCliente/{id}")]
        public async Task<ResponseViewModel<CarroRetorno>> BuscarCarroCliente(int? id)
        {
            try
            {
                CarroRetorno carroRetorno = new CarroRetorno();
                Carro entidade = db.Carros.FirstOrDefault(x => x.IdCliente == id);
                if (entidade == null)
                    entidade = new Carro();

                entidade.Cliente = db.Clientes.Find(id);
                carroRetorno.IdMarca = entidade.IdMarca;
                carroRetorno.Level = 2;
                carroRetorno.Modelo = entidade.Modelo;
                carroRetorno.Nome = entidade.Cliente.Nome;
                carroRetorno.Placa = entidade.Placa;
                carroRetorno.Porte = entidade.Porte;
                carroRetorno.Alerta = !entidade.Cliente.TemCarro;

                return new ResponseViewModel<CarroRetorno>()
                {
                    Data = carroRetorno,
                    Serializado = true,
                    Sucesso = true,
                    Mensagem = "Busca realizada com sucesso"
                };
            }
            catch (Exception e)
            {
                return new ResponseViewModel<CarroRetorno>()
                {
                    Data = null,
                    Serializado = true,
                    Sucesso = false,
                    Mensagem = "Não foi possivel atender a sua solicitação: " + e.Message
                };
            }
        }

        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("~/api/Carros/Registrar")]
        //[ValidateAntiForgeryToken]
        public virtual async Task<ResponseViewModel<Carro>> RegistrarCarro(Carro carro)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (ClientesController clientesController = new ClientesController())
                    {
                        var retornoCarro = await Cadastrar(carro);

                        var cliente = await clientesController.Detalhes(carro.IdCliente);
                        cliente.Data.TemCarro = true;
                        var retornoCliente = await clientesController.Editar(cliente.Data);

                        var response = new ResponseViewModel<Carro>
                        {
                            Data = retornoCarro.Data,
                            Sucesso = true,
                            Mensagem = "Entidade Cadastrada com sucesso."
                        };
                        return response;
                    };
                }
                else
                {
                    var response = new ResponseViewModel<Carro>
                    {
                        Data = carro,
                        Sucesso = false,
                        Mensagem = "Erro na validação da entidade."
                    };
                    return response;
                }
            }
            catch (Exception e)
            {
                return new ResponseViewModel<Carro>()
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
