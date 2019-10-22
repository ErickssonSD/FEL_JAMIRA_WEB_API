﻿using FEL_JAMIRA_API.Models.Clientes;
using FEL_JAMIRA_API.Models.Estacionamentos;
using FEL_JAMIRA_WEB_API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace FEL_JAMIRA_API.Controllers
{
    //[Authorize]
    public class SolicitacaoController : GenericController<Solicitacao>
    {
        /// <summary>
        /// Método para Buscar os recebimentos do usuários.
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Authorize]
        [System.Web.Http.Route("~/api/Solicitacao/GetRecebimentos")]
        public async Task<ResponseViewModel<List<Recebimentos>>> GetRecebimentos(string idUsuario)
        {
            //90% da hora
            try
            {
                int valor = int.Parse(idUsuario);

                List<RecebimentosRetorno> filtroSolicitacao = await db.Solicitacoes.Include("Estacionamento")
                    .Where(x => x.Estacionamento.IdPessoa.Equals(valor) && x.Status.Equals(1))
                    .Select(cl => new RecebimentosRetorno
                    {
                        Data = cl.DataSaida,
                        Valor = cl.ValorTotalEstacionamento
                    }).ToListAsync();

                List<FiltragemRecebimentos> filtragemRecebimentos = new List<FiltragemRecebimentos>();

                foreach (var item in filtroSolicitacao)
                {
                    FiltragemRecebimentos f = new FiltragemRecebimentos();
                    DateTime date = Convert.ToDateTime(item.Data);
                    f.MesAno = date.Month + "-" + date.Year; 
                    f.Valor = item.Valor;
                    filtragemRecebimentos.Add(f);
                }

                List<Recebimentos> solicitacao = filtragemRecebimentos
                    .GroupBy(l =>l.MesAno)
                    .Select(cl => new Recebimentos
                    {
                        MesAno = cl.Key,
                        Valor = cl.Sum(c => c.Valor)
                    }).ToList();

                ResponseViewModel<List<Recebimentos>> response = new ResponseViewModel<List<Recebimentos>> {
                    Data = solicitacao,
                    Mensagem = "Dados retornados com sucesso!",
                    Serializado = true,
                    Sucesso = true
                };

                return response;
            }
            catch (Exception e)
            {
                return new ResponseViewModel<List<Recebimentos>>()
                {
                    Data = null,
                    Serializado = true,
                    Sucesso = false,
                    Mensagem = "Não foi possivel atender a sua solicitação: " + e.Message
                };
            }
        }

        /// <summary>
        /// Método para Buscar os Estacionamentos do Cliente.
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Authorize]
        [System.Web.Http.Route("~/api/Solicitacao/GetAllEstacionamentosDoCliente")]
        public async Task<ResponseViewModel<List<EstacionamentosDoCliente>>> GetAllEstacionamentosDoCliente(string idCliente)
        {
            try
            {
                int valor = int.Parse(idCliente);
                List<EstacionamentosDoCliente> solicitacao = await db.Solicitacoes.Include("Estacionamento")
                                            .Where(x => x.IdCliente.Equals(valor))
                                            .Select(cl => new EstacionamentosDoCliente
                                            {
                                                NomeEstacionamento = cl.Estacionamento.NomeEstacionamento,
                                                PeriodoDe = cl.DataEntrada,
                                                PeriodoAte = cl.DataSaida,
                                                ValorTotal = cl.ValorTotal
                                            }).ToListAsync();
                ResponseViewModel<List<EstacionamentosDoCliente>> response = new ResponseViewModel<List<EstacionamentosDoCliente>>
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
                return new ResponseViewModel<List<EstacionamentosDoCliente>>()
                {
                    Data = null,
                    Serializado = true,
                    Sucesso = false,
                    Mensagem = "Não foi possivel atender a sua solicitação: " + e.Message
                };
            }
        }

        /// <summary>
        /// Método para Buscar os Estacionamentos do Cliente.
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Authorize]
        [System.Web.Http.Route("~/api/Solicitacao/GetEstacionamentosDoCliente")]
        public async Task<ResponseViewModel<List<EstacionamentosDoCliente>>> GetEstacionamentosDoCliente(string idUsuario)
        {
            try
            {
                int valor = int.Parse(idUsuario);
                List<EstacionamentosDoCliente> solicitacao = await db.Solicitacoes.Include("Estacionamento")
                                            .Where(x => x.IdCliente.Equals(valor) && x.Status.Equals(1))
                                            .Select(cl => new EstacionamentosDoCliente
                                            {
                                                NomeEstacionamento = cl.Estacionamento.NomeEstacionamento,
                                                PeriodoDe = cl.DataEntrada,
                                                PeriodoAte = cl.DataSaida,
                                                ValorTotal = cl.ValorTotal
                                            }).ToListAsync();
                ResponseViewModel<List<EstacionamentosDoCliente>> response = new ResponseViewModel<List<EstacionamentosDoCliente>>
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
                return new ResponseViewModel<List<EstacionamentosDoCliente>>()
                {
                    Data = null,
                    Serializado = true,
                    Sucesso = false,
                    Mensagem = "Não foi possivel atender a sua solicitação: " + e.Message
                };
            }
        }


        /// <summary>
        /// Método para Buscar os Clientes do Estacionamento.
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Authorize]
        [System.Web.Http.Route("~/api/Solicitacao/GetUsuariosDoEstacionamento")]
        public async Task<ResponseViewModel<List<UsuariosDoEstacionamento>>> GetUsuariosDoEstacionamento(string idUsuario)
        {
            try
            {
                int valor = int.Parse(idUsuario);
                List<UsuariosDoEstacionamento> solicitacao = await db.Solicitacoes.Include("Cliente")
                                            .Where(x => x.IdEstacionamento.Equals(valor) && x.Status.Equals(1))
                                            .Select(cl => new UsuariosDoEstacionamento
                                            {
                                                NomeUsuario = cl.Cliente.Nome,
                                                PeriodoDe = cl.DataEntrada,
                                                PeriodoAte = cl.DataSaida,
                                                ValorGanho = cl.ValorTotalEstacionamento
                                            }).ToListAsync();
                ResponseViewModel<List<UsuariosDoEstacionamento>> response = new ResponseViewModel<List<UsuariosDoEstacionamento>>
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
                return new ResponseViewModel<List<UsuariosDoEstacionamento>>()
                {
                    Data = null,
                    Serializado = true,
                    Sucesso = false,
                    Mensagem = "Não foi possivel atender a sua solicitação: " + e.Message
                };
            }
        }

        /// <summary>
        /// Método para Buscar os atuais usuários que estão usando o estacionamento no momento.
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Authorize]
        [System.Web.Http.Route("~/api/Solicitacao/GetUsuariosAtivosSolicitacao")]
        public async Task<ResponseViewModel<List<Solicitantes>>> GetUsuariosAtivosSolicitacao(string idUsuario)
        {
            try
            {
                int valor = int.Parse(idUsuario);
                List<Solicitantes> solicitacao = new List<Solicitantes>();
                solicitacao = await db.Solicitacoes.Include("Cliente").Include("Estacionamento").Include("Estacionamento.Proprietario")
                                            .Where(x => x.Estacionamento.IdPessoa.Equals(valor) && x.Status.Equals(0))
                                            .Select(cl => new Solicitantes
                                            {
                                                InsereAlerta = !cl.Estacionamento.TemEstacionamento,
                                                Nickname = cl.Estacionamento.Proprietario.Nome,
                                                NomeCliente = cl.Cliente.Nome,
                                                IdCliente = cl.IdCliente,
                                                IdSolicitacao = cl.Id,
                                                Status = cl.Status,
                                                PeriodoDe = cl.DataEntrada,
                                                PeriodoAte = null
                                            }).ToListAsync();

                foreach (var item in solicitacao)
                {
                    Carro carro = CarroCliente(item.IdCliente);
                    item.Carro = carro.Modelo;
                    item.PlacaCarro = carro.Placa;

                }

                if (solicitacao.Count == 0)
                {
                    Estacionamento estacionamento = db.Estacionamentos.Include("Proprietario").FirstOrDefault(x => x.IdPessoa == valor);
                    Solicitantes solicitantes = new Solicitantes
                    {
                        InsereAlerta = !estacionamento.TemEstacionamento,
                        Nickname = estacionamento.Proprietario.Nome
                    };
                    solicitacao.Add(solicitantes);
                }

                ResponseViewModel<List<Solicitantes>> response = new ResponseViewModel<List<Solicitantes>>
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
                return new ResponseViewModel<List<Solicitantes>>()
                {
                    Data = null,
                    Serializado = true,
                    Sucesso = false,
                    Mensagem = "Não foi possivel atender a sua solicitação: " + e.Message
                };
            }
        }

        /// <summary>
        /// Método para Buscar os as solicitações em aberto dos estacionamentos.
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Authorize]
        [System.Web.Http.Route("~/api/Solicitacao/GetSolicitacoesEmAberto")]
        public async Task<ResponseViewModel<List<Solicitantes>>> GetSolicitacoesEmAberto(string idUsuario)
        {
            try
            {
                int valor = int.Parse(idUsuario);
                List<Solicitantes> solicitacao = new List<Solicitantes>();
                solicitacao = await db.Solicitacoes.Include("Cliente").Include("Estacionamento").Include("Estacionamento.Proprietario")
                                            .Where(x => x.Estacionamento.IdPessoa.Equals(valor) && x.Status.Equals(2))
                                            .Select(cl => new Solicitantes
                                            {
                                                InsereAlerta = !cl.Estacionamento.TemEstacionamento,
                                                Nickname = cl.Estacionamento.Proprietario.Nome,
                                                NomeCliente = cl.Cliente.Nome,
                                                IdCliente = cl.IdCliente,
                                                IdSolicitacao = cl.Id,
                                                Status = cl.Status,
                                                PeriodoDe = null,
                                                PeriodoAte = null
                                            }).ToListAsync();

                foreach (var item in solicitacao)
                {
                    Carro carro = CarroCliente(item.IdCliente);
                    item.Carro = carro.Modelo;
                    item.PlacaCarro = carro.Placa;

                }

                if (solicitacao.Count == 0)
                {
                    Estacionamento estacionamento = db.Estacionamentos.Include("Proprietario").FirstOrDefault(x => x.IdPessoa == valor);
                    Solicitantes solicitantes = new Solicitantes
                    {
                        InsereAlerta = !estacionamento.TemEstacionamento,
                        Nickname = estacionamento.Proprietario.Nome
                    };
                    solicitacao.Add(solicitantes);
                }

                ResponseViewModel<List<Solicitantes>> response = new ResponseViewModel<List<Solicitantes>>
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
                return new ResponseViewModel<List<Solicitantes>>()
                {
                    Data = null,
                    Serializado = true,
                    Sucesso = false,
                    Mensagem = "Não foi possivel atender a sua solicitação: " + e.Message
                };
            }
        }

        /// <summary>
        /// Método para Buscar as solicitações para serem finalizadas pelo estacionamento.
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Authorize]
        [System.Web.Http.Route("~/api/Solicitacao/GetSolicitacoesParaFinalizar")]
        public async Task<ResponseViewModel<List<Solicitantes>>> GetSolicitacoesParaFinalizar(string idUsuario)
        {
            try
            {
                int valor = int.Parse(idUsuario);
                List<Solicitantes> solicitacao = new List<Solicitantes>();
                solicitacao = await db.Solicitacoes.Include("Cliente").Include("Estacionamento").Include("Estacionamento.Proprietario")
                                            .Where(x => x.Estacionamento.IdPessoa.Equals(valor) && x.Status.Equals(3))
                                            .Select(cl => new Solicitantes
                                            {
                                                InsereAlerta = !cl.Estacionamento.TemEstacionamento,
                                                Nickname = cl.Estacionamento.Proprietario.Nome,
                                                NomeCliente = cl.Cliente.Nome,
                                                IdCliente = cl.IdCliente,
                                                IdSolicitacao = cl.Id,
                                                Status = cl.Status,
                                                PeriodoDe = null,
                                                PeriodoAte = null
                                            }).ToListAsync();

                foreach (var item in solicitacao)
                {
                    Carro carro = CarroCliente(item.IdCliente);
                    item.Carro = carro.Modelo;
                    item.PlacaCarro = carro.Placa;

                }
                if (solicitacao.Count == 0)
                {
                    Estacionamento estacionamento = db.Estacionamentos.Include("Proprietario").FirstOrDefault(x => x.IdPessoa == valor);
                    Solicitantes solicitantes = new Solicitantes
                    {
                        InsereAlerta = !estacionamento.TemEstacionamento,
                        Nickname = estacionamento.Proprietario.Nome
                    };
                    solicitacao.Add(solicitantes);
                }

                ResponseViewModel<List<Solicitantes>> response = new ResponseViewModel<List<Solicitantes>>
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
                return new ResponseViewModel<List<Solicitantes>>()
                {
                    Data = null,
                    Serializado = true,
                    Sucesso = false,
                    Mensagem = "Não foi possivel atender a sua solicitação: " + e.Message
                };
            }
        }

        /// <summary>
        /// Método para aprovar solicitação dos clientes.
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        [System.Web.Http.Authorize]
        [System.Web.Http.Route("~/api/Solicitacao/AprovarSolicitacao")]
        public async Task<ResponseViewModel<Solicitantes>> AprovarSolicitacao(int IdSolicitacao)
        {
            try
            {
                Solicitacao solicitacao = db.Solicitacoes.Include("Estacionamento").Include("Estacionamento.Proprietario").FirstOrDefault(x => x.Id == IdSolicitacao);

                solicitacao.Status = 0;
                solicitacao.DataEntrada = DateTime.Now;

                db.Entry<Solicitacao>(solicitacao).State = EntityState.Modified;
                db.SaveChanges();

                Solicitantes solicitantes = new Solicitantes
                {
                    Nickname = solicitacao.Estacionamento.Proprietario.Nome,
                    InsereAlerta = !solicitacao.Estacionamento.TemEstacionamento
                };

                ResponseViewModel<Solicitantes> response = new ResponseViewModel<Solicitantes>
                {
                    Data = solicitantes,
                    Mensagem = "Dados retornados com sucesso!",
                    Serializado = true,
                    Sucesso = true
                };

                return response;
            }
            catch (Exception e)
            {
                return new ResponseViewModel<Solicitantes>()
                {
                    Data = null,
                    Serializado = true,
                    Sucesso = false,
                    Mensagem = "Não foi possivel atender a sua solicitação: " + e.Message
                };
            }
        }

        /// <summary>
        /// Método para aprovar solicitação dos clientes.
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        [System.Web.Http.Authorize]
        [System.Web.Http.Route("~/api/Solicitacao/FinalizarSolicitacao")]
        public async Task<ResponseViewModel<Solicitantes>> FinalizarSolicitacao(int IdSolicitacao)
        {
            try
            {
                Solicitacao solicitacao = db.Solicitacoes.Include("Cliente").Include("Estacionamento").Include("Estacionamento.Proprietario").FirstOrDefault(x => x.Id == IdSolicitacao);
                Cliente cliente = solicitacao.Cliente;

                string tempo = "";
                solicitacao.Status = 1;
                solicitacao.DataSaida = DateTime.Now;
                var diferenca = (Convert.ToDateTime(solicitacao.DataSaida) - Convert.ToDateTime(solicitacao.DataEntrada));
                var minutosTotais = Convert.ToInt32((int)diferenca.TotalMinutes);
                var valorMinuto = solicitacao.Estacionamento.ValorHora/60.0;
                solicitacao.ValorTotal = valorMinuto * minutosTotais;
                solicitacao.ValorTotalEstacionamento = valorMinuto * minutosTotais * 0.9;
                solicitacao.ValorGanho = valorMinuto * minutosTotais * 0.1;
                cliente.Saldo -= solicitacao.ValorTotal;

                db.Entry<Cliente>(cliente).State = EntityState.Modified;
                db.SaveChanges();

                var dias = diferenca.Days;
                var horas = diferenca.Hours;
                var minutos = diferenca.Minutes;
                var segundos = diferenca.Seconds;
                if (dias > 0)
                    tempo = dias + " dia(s), ";
                if (horas > 0)
                    tempo += horas + " hora(s), ";

                tempo += minutos + " minuto(s) e " + segundos + " segundo(s)";
                solicitacao.TempoEstacionamento = tempo;

                db.Entry<Solicitacao>(solicitacao).State = EntityState.Modified;
                db.SaveChanges();

                Solicitantes solicitantes = new Solicitantes
                {
                    Nickname = solicitacao.Estacionamento.Proprietario.Nome,
                    InsereAlerta = !solicitacao.Estacionamento.TemEstacionamento
                };

                ResponseViewModel<Solicitantes> response = new ResponseViewModel<Solicitantes>
                {
                    Data = solicitantes,
                    Mensagem = "Dados retornados com sucesso!",
                    Serializado = true,
                    Sucesso = true
                };

                return response;
            }
            catch (Exception e)
            {
                return new ResponseViewModel<Solicitantes>()
                {
                    Data = null,
                    Serializado = true,
                    Sucesso = false,
                    Mensagem = "Não foi possivel atender a sua solicitação: " + e.Message
                };
            }
        }

        /// <summary>
        /// Método para Buscar os Clientes do Estacionamento.
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        [System.Web.Http.Authorize]
        [System.Web.Http.Route("~/api/Solicitacao/ReprovarSolicitacao")]
        public async Task<ResponseViewModel<Solicitantes>> ReprovarSolicitacao(int IdSolicitacao)
        {
            try
            {
                Solicitacao solicitacao = db.Solicitacoes.Include("Estacionamento").Include("Estacionamento.Proprietario").FirstOrDefault(x => x.Id == IdSolicitacao);

                solicitacao.Status = 4;

                db.Entry<Solicitacao>(solicitacao).State = EntityState.Modified;
                db.SaveChanges();

                Solicitantes solicitantes = new Solicitantes
                {
                    Nickname = solicitacao.Estacionamento.Proprietario.Nome,
                    InsereAlerta = !solicitacao.Estacionamento.TemEstacionamento
                };

                ResponseViewModel<Solicitantes> response = new ResponseViewModel<Solicitantes>
                {
                    Data = solicitantes,
                    Mensagem = "Dados retornados com sucesso!",
                    Serializado = true,
                    Sucesso = true
                };

                return response;
            }
            catch (Exception e)
            {
                return new ResponseViewModel<Solicitantes>()
                {
                    Data = null,
                    Serializado = true,
                    Sucesso = false,
                    Mensagem = "Não foi possivel atender a sua solicitação: " + e.Message
                };
            }
        }

        
        /// <summary>
        /// Método para cadastrar solicitação do fornecedor.
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        [System.Web.Http.Authorize]
        [System.Web.Http.Route("~/api/Solicitacao/CadastrarSolicitacao")]
        public async Task<ResponseViewModel<Solicitacao>> CadastrarSolicitacao(Solicitacao solicitacao)
        {
            try
            {
                if (solicitacao.Status == 2)
                {
                    List<Solicitacao> solicitacaos = db.Solicitacoes.Where(x => x.IdCliente == solicitacao.IdCliente && x.Status != 1 && x.Status != 4).ToList();
                    if (solicitacaos.Count > 0)
                    {
                        ResponseViewModel<Solicitacao> response = new ResponseViewModel<Solicitacao>
                        {
                            Data = solicitacao,
                            Mensagem = "O Cliente já se encontra em processo de avaliação.",
                            Serializado = false,
                            Sucesso = true
                        };
                        return response;
                    }
                    else
                    {
                        solicitacao.ValorGanho = 0;
                        solicitacao.ValorTotal = 0;
                        solicitacao.ValorTotalEstacionamento = 0;

                        db.Set<Solicitacao>().Add(solicitacao);

                        Task.Run(async () => {
                            await db.SaveChangesAsync();
                        }).Wait();

                        ResponseViewModel<Solicitacao> response = new ResponseViewModel<Solicitacao>
                        {
                            Data = solicitacao,
                            Mensagem = "Solicitação cadastrada com sucesso!",
                            Serializado = true,
                            Sucesso = true
                        };
                        return response;
                    }
                }
                else
                {
                    ResponseViewModel<Solicitacao> response = new ResponseViewModel<Solicitacao>
                    {
                        Data = solicitacao,
                        Mensagem = "Para Cadastrar a solicitação, é necessário que o status seja '2'!",
                        Sucesso = false,
                        Serializado = true
                    };
                    return response;
                }
            }
            catch (Exception e)
            {
                return new ResponseViewModel<Solicitacao>()
                {
                    Data = null,
                    Sucesso = true,
                    Serializado = false,
                    Mensagem = "Não foi possivel atender a sua solicitação: " + e.Message
                };
            }
        }
        
        public Carro CarroCliente(int idCliente)
        {
            Carro carro = db.Carros.FirstOrDefault(x => x.IdCliente == idCliente);
            return carro;
        }

    }
}
