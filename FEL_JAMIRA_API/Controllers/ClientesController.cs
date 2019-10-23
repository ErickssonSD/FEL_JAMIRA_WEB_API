using FEL_JAMIRA_API.Models.Cadastros;
using FEL_JAMIRA_API.Util;
using FEL_JAMIRA_WEB_API.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FEL_JAMIRA_API.Controllers
{
    public class ClientesController : GenericController<Cliente>
    {
        // POST: Generic/Cadastrar
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Método para cadastrar um novo registro.
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("~/api/Clientes/CadastrarCliente")]
        public async Task<ResponseViewModel<Usuario>> CadastrarCliente(CadastroCliente cadastroCliente)
        {
            try
            {
                Usuario existente = new Usuario();
                Pessoa existente1 = new Pessoa();

                Task.Run(async () => {
                    var valor = db.Usuarios.Where(x => x.Login == cadastroCliente.Email).FirstOrDefault();
                    existente = valor;

                    var valor2 = db.Pessoas.Where(x => x.CPF == cadastroCliente.CPF).FirstOrDefault();
                    existente1 = valor2;
                }).Wait();

                if (existente != null)
                    throw new Exception("O Email já existe.");

                if (existente1 != null)
                    throw new Exception("O CPF já existe.");

                UsuariosController usuariosController = new UsuariosController();

                string auxSenha = Helpers.GenerateRandomString();

                Usuario usuario = new Usuario
                {
                    Login = cadastroCliente.Email,
                    AuxSenha = auxSenha,
                    Senha = Helpers.CriarSenha(cadastroCliente.Senha, auxSenha),
                    Level = 2,
                    Nome = cadastroCliente.Nickname ?? "",
                    Pessoa = new Cliente
                    {
                        Nome = cadastroCliente.Nome,
                        Nascimento = cadastroCliente.Nascimento,
                        CPF = cadastroCliente.CPF ?? "",
                        RG = cadastroCliente.RG ?? "",
                        Nickname = cadastroCliente.Nickname,
                        DataCriacao = DateTime.Now,
                        Saldo = 0,
                        EnderecoPessoa = new Endereco
                        {
                            Rua = cadastroCliente.Rua ?? "",
                            Numero = cadastroCliente.Numero,
                            Bairro = cadastroCliente.Bairro ?? "",
                            CEP = cadastroCliente.CEP ?? "",
                            Complemento = cadastroCliente.Complemento ?? "",
                            IdCidade = cadastroCliente.IdCidade,
                            IdEstado = cadastroCliente.IdEstado
                        }
                    }
                };

                db.Usuarios.Add(usuario);

                Task.Run(async () => {
                    await db.SaveChangesAsync();
                }).Wait();

                ResponseViewModel<Usuario> response = new ResponseViewModel<Usuario> {
                    Data = usuario,
                    Sucesso = true,
                    Serializado = true,
                    Mensagem = "Cadastro Realizado com Sucesso!"
                };

                return response;
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        [System.Web.Http.Authorize]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("~/api/Clientes/BuscarCliente/{id}")]
        public async Task<ResponseViewModel<Cliente>> BuscarCliente(int? id)
        {
            try
            {
                if (id != null)
                {
                    Cliente entidade =
                        db.Clientes.Include("EnderecoPessoa").Where(x => x.Id == id).FirstOrDefault();

                    return new ResponseViewModel<Cliente>()
                    {
                        Data = entidade,
                        Serializado = true,
                        Sucesso = true,
                        Mensagem = "Busca realizada com sucesso"
                    };
                }
                else
                {
                    return new ResponseViewModel<Cliente>()
                    {
                        Data = null,
                        Serializado = true,
                        Sucesso = true,
                        Mensagem = "Sem filtro para busca do cliente."
                    };
                }
            }
            catch (Exception e)
            {
                return new ResponseViewModel<Cliente>()
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