using FEL_JAMIRA_API.Models.Cadastros;
using FEL_JAMIRA_API.Models.Estacionamentos;
using FEL_JAMIRA_API.Util;
using FEL_JAMIRA_WEB_API.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FEL_JAMIRA_API.Controllers
{
    public class EstacionamentosController : GenericController<Estacionamento>
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
        [System.Web.Http.Route("~/api/Estacionamentos/CadastrarFornecedor")]
        public async Task<ResponseViewModel<Usuario>> CadastrarFornecedor(CadastroFornecedor cadastroFornecedor)
        {
            try
            {
                Usuario existente = new Usuario();
                Task.Run(async () => {
                    var valor = db.Usuarios.Where(x => x.Login == cadastroFornecedor.Email).FirstOrDefault();
                    existente = valor;

                }).Wait();

                if (existente != null)
                    throw new Exception("O Email já existe.");

                UsuariosController usuariosController = new UsuariosController();
                EstacionamentosController estacionamentosController = new EstacionamentosController();

                string auxSenha = Helpers.GenerateRandomString();

                Usuario usuario = new Usuario
                {
                    Login = cadastroFornecedor.Email,
                    AuxSenha = auxSenha,
                    Senha = Helpers.CriarSenha(cadastroFornecedor.Senha, auxSenha),
                    Level = 1,
                    Nome = cadastroFornecedor.Nickname ?? "",
                    Pessoa = new Pessoa
                    {
                        Nome = cadastroFornecedor.NomeProprietario,
                        Nascimento = cadastroFornecedor.Nascimento,
                        CPF = cadastroFornecedor.CPF ?? "",
                        RG = cadastroFornecedor.RG ?? "",
                        DataCriacao = DateTime.Now,
                        EnderecoPessoa = new Endereco
                        {
                            Rua = cadastroFornecedor.Rua ?? "",
                            Numero = cadastroFornecedor.Numero,
                            Bairro = cadastroFornecedor.Bairro ?? "",
                            CEP = cadastroFornecedor.CEP ?? "",
                            Complemento = cadastroFornecedor.Complemento ?? "",
                            IdCidade = cadastroFornecedor.IdCidade,
                            IdEstado = cadastroFornecedor.IdEstado
                        }
                    }
                };

                db.Usuarios.Add(usuario);

                Task.Run(async () => {
                    await db.SaveChangesAsync();
                }).Wait();

                
                Estacionamento estacionamento = new Estacionamento
                {
                    NomeEstacionamento = cadastroFornecedor.NomeEstacionamento ?? "",
                    CNPJ = cadastroFornecedor.CNPJ ?? "",
                    InscricaoEstadual = cadastroFornecedor.InscricaoEstadual ?? "",
                    IdEnderecoEstabelecimento = null,
                    IdPessoa = usuario.IdPessoa
                };

                db.Estacionamentos.Add(estacionamento);

                Task.Run(async () => {
                    await db.SaveChangesAsync();
                }).Wait();


                ResponseViewModel<Usuario> responseUser = new ResponseViewModel<Usuario>
                {
                    Mensagem = "Cadastro Realizado com Sucesso!",
                    Serializado = true,
                    Sucesso = true,
                    Data =  usuario
                };
                return responseUser;
            }
            catch (Exception e)
            {
                return new ResponseViewModel<Usuario>()
                {
                    Data = null,
                    Serializado = true,
                    Sucesso = false,
                    Mensagem = "Não foi possivel atender a sua solicitação: " + e.Message
                };
            }
        }

        [System.Web.Http.Authorize]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("~/api/Estacionamentos/EstacionamentoPorPessoa")]
        public async Task<ResponseViewModel<Estacionamento>> GetEstacionamentoPorPessoa(int IdPessoa)
        {
            try
            {
                Estacionamento entidade =
                    db.Estacionamentos.Include("EnderecoEstacionamento").Include("Proprietario").Include("Proprietario.EnderecoPessoa").Where(x => x.IdPessoa.Equals(IdPessoa)).FirstOrDefault();

                return new ResponseViewModel<Estacionamento>()
                {
                    Data = entidade,
                    Serializado = true,
                    Sucesso = true,
                    Mensagem = "Busca realizada com sucesso"
                };
            }
            catch (Exception e)
            {
                return new ResponseViewModel<Estacionamento>()
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
