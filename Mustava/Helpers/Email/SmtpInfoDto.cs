namespace Mustava.Helpers.Email
{
    public class SmtpInfoDto
    {
        public string SmtpServer { get; set; }

        public string Frm { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public int Port { get; set; }
    }
}