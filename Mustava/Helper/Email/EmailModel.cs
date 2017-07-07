using System.Collections.Generic;

namespace Mustava.Helper.Email
{
    public class EmailModel
    {
        public string To { get; set; }

        public List<string> CarbonCopies { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; } 
    }
}