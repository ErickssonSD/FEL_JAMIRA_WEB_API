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
using System.Net.Mail;

namespace FEL_JAMIRA_API.Controllers
{
    public class FaleConoscoController : GenericController<FaleConosco>
    {
        [Authorize]
        public async Task<ResponseViewModel<bool>> EnviarEmail(string email, string titulo, string corpo)
        {
            try
            {
                //Conta de email para fazer o envio...
                string Conta = "fel.jamira.brasil@gmail.com";
                string Senha = "j@mira123";

                //Montar o email...
                MailMessage msg = new MailMessage(Conta, email);
                //de->para
                msg.Subject = titulo; //assunto da mensagem
                msg.IsBodyHtml = false;
                msg.Body = corpo; //corpo da mensagem

                //Enviar o email...
                //SMTP (Simple Mail Transfer Protocol)
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(Conta, Senha);
                smtp.EnableSsl = true; //Security Socket de Layer

                //autenticação
                smtp.Send(msg); //enviando a mensagem

                return new ResponseViewModel<bool>
                {
                    Data = true,
                    Mensagem = "Mensagem enviada com sucesso!",
                    Serializado = true,
                    Sucesso = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseViewModel<bool>
                {
                    Data = true,
                    Mensagem = "Não foi possivel enviar a mensagem. " + ex.Message,
                    Serializado = true,
                    Sucesso = true
                };
            }
        }
    }
}
