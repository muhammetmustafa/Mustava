using System.Collections.Generic;

namespace Mustava.Helpers.Email
{
    public class EmailModel
    {
        public string To { get; set; }

        public List<string> CarbonCopies { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; } 
    }
}