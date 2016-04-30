/*
Server          Name          SMTP  Address Port SSL
Yahoo!  smtp.mail.yahoo.com   587        Yes
GMail   smtp.gmail.com        587        Yes
Hotmail smtp.live.com         587        Yes
*/

bool enviou = sendMail("smtp.live.com", 587, "remetente@hotmail.com", "remetentePassword", "bob", new string[] { "destinatario@algo.com" }, null, "assunto", "corpo email");

protected bool sendMail(string smtpServer, int smtpPort, string emailRemetente, string senhaRemetente, string nomeRemetente, string[] mailTo, string[] mailCc, string subject, string body)
{
	try
	{
		string to = mailTo != null ? string.Join(",", mailTo) : null;
		string cc = mailCc != null ? string.Join(",", mailCc) : null;

		using (SmtpClient client = new SmtpClient(smtpServer, smtpPort))
		{
			client.Credentials = new NetworkCredential(emailRemetente, senhaRemetente);
			client.EnableSsl = true;

			MailMessage mail = new MailMessage();
			mail.From = new MailAddress(emailRemetente, nomeRemetente);
			mail.To.Add(to);
			mail.Subject = subject;
			mail.Body = body.Replace(Environment.NewLine, "<BR>");
			mail.IsBodyHtml = true;

			if (cc != null)
				mail.CC.Add(cc);

			client.Send(mail);
			return true;
		}
	}
	catch (Exception ex)
	{
		Response.Write("Ocorreram problemas no envio do e-mail. Erro = " + ex.Message);
		return false;
	}
}