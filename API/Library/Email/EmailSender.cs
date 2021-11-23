using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Library.Email
{
    public class EmailSender : IEmailSender
    {
      private readonly EmailConfiguration _emailConfig;

		public EmailSender(EmailConfiguration emailConfig)
		{
         this._emailConfig = emailConfig;
		}

      public async Task SendEmailAsync(Message message)
		{
         var emailMessage = CreateEmailMessage(message);

         await SendAsync(emailMessage);
		}

      private MimeMessage CreateEmailMessage(Message message)
		{
         var emailMessage = new MimeMessage();
         emailMessage.From.Add(new MailboxAddress(message.Content.Sender_Alias,_emailConfig.From));
         emailMessage.To.Add(new MailboxAddress(message.To));
         emailMessage.Subject = message.Subject;
         // Body Builder
         var bodyBuilder = new BodyBuilder();
         string ActionEmail = message.Content.Interview_Action;

			if (ActionEmail == "Diterima")
			{
            bodyBuilder.HtmlBody = string.Format($"<div style='max-width:370px;border:1px solid #000;padding:2.1em'><div><h4>Dear <i>{message.Content.Nama}</i>,</h4><p style=text-align:justify>     Dengan email ini kami informasikan bahwa saudara/saudari <b><i>{ActionEmail}</i></b> pada project <span>{message.Content.Project_Name}</span> sebagai <span>{message.Content.Jobs}</span>. Demikianlah informasi ini kami sampaikan, untuk kelengkapan berkas & informasi lebih lanjut silahkan hubungi <i>HR Metrodata</i>.<p><b>Terima kasih.</b><p>{message.Content.Sender_Alias}</div><hr><div style=margin-top:2em;font-weight:700;color:#4169e1;font-size:.77em><p>PT. Mitra Integrasi Informatika<p>APL Tower, 37th Floor Jl.<p>Letjen S. Parman Kav. 28 Jakarta 11470</div></div>");
            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
			}
			else if (ActionEmail == "Ditolak")
			{
            bodyBuilder.HtmlBody = string.Format($"<div style='max-width:370px;border:1px solid #000;padding:2.1em'><div><h4>Dear <i>{message.Content.Nama}</i>,</h4><p style=text-align:justify>     Dengan berat hati kami informasikan bahwa saudara/saudari <b><i>{ActionEmail}</i></b> pada project <span>{message.Content.Project_Name}</span> sebagai <span>{message.Content.Jobs}</span>, dikarenakan {message.Content.Note}. Demikianlah informasi ini kami sampaikan. semoga kedepannya saudara/saudari bisa menjadi lebih baik.<p><b>Terima kasih.</b><p>{message.Content.Sender_Alias}</div><hr><div style=margin-top:2em;font-weight:700;color:#4169e1;font-size:.77em><p>PT. Mitra Integrasi Informatika<p>APL Tower, 37th Floor Jl.<p>Letjen S. Parman Kav. 28 Jakarta 11470</div></div>");
            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
         }
         bodyBuilder.HtmlBody = string.Format($"<div style='max-width:370px;border:1px solid #000;padding:2.1em'><div><h4>Dear<i> {message.Content.Nama}</i>,</h4><p style=text-align:justify>     Sehubungan dengan telah berakhirnya MCC, Maka selanjutnya candidate akan ditempatkan pada client. Menindaklanjuti hal tersebut kami telah menetapkan jadwal interview dengan client yang dilaksanakan pada :<div><ul style=list-style:none;padding:.3em><li style='margin:.37em 0'>Tangal : {message.Content.Tanggal}<li style='margin:.37em 0'>Project : {message.Content.Project_Name}<li style='margin:.37em 0'>Jobdesk : {message.Content.Jobs}</ul></div><p style=text-align:justify>Silahkan join as Guest menggunakan web browser dengan menggunakan link di bawah ini, Mohon untuk sudah standby 10 menit sebelum dilaksanakan.<p><b>Terima kasih.</b></div><hr><p><span>Google Meet :</span><a href='https://meet.google.com/?pli=1'target=_blank>click this link to join</a><hr><div style=margin-top:2em;font-weight:700;color:#4169e1;font-size:.77em><p>PT. Mitra Integrasi Informatika<p>APL Tower, 37th Floor Jl.<p>Letjen S. Parman Kav. 28 Jakarta 11470</div></div>");
         emailMessage.Body = bodyBuilder.ToMessageBody();
         return emailMessage;
		}

      private async Task SendAsync(MimeMessage mailMessage)
		{
         using(var client = new SmtpClient())
			{
            try
            {
               await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true);
               client.AuthenticationMechanisms.Remove("XOAUTH2");
               await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);

               await client.SendAsync(mailMessage);
            }
            catch
            {
               throw;
            }
            finally
            {
               await client.DisconnectAsync(true);
               client.Dispose();
            }
         }
		}
    }
}
