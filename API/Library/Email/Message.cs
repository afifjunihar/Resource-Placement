using API.Models.ViewModels;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Library.Email
{
    public class Message
    {
		public string To { get; set; }
		public string Subject { get; set; }
		public EmailVM Content { get; set; }

		public Message(string to, string subject, EmailVM content)
		{
			this.To = to;
			this.Subject = subject;
			this.Content = content;
		}
	}
}
