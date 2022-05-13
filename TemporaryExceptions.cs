using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DALMSSQLSERVER
{
    public class TemporaryExceptions : Exception
    {
        public string errorMessage { get; set; }
        public string? TemporaryErrorMessage { get; set; }
        //optionele error message als 2e in de constructor

        public TemporaryExceptions(string errormessage, string? temporaryErrorMessage = null) : base(errormessage)
        {
            this.errorMessage = errormessage;
            this.TemporaryErrorMessage = temporaryErrorMessage;
        }

        public string GetFullError()
        {
            return $"{errorMessage} {TemporaryErrorMessage}";
        }
        //To be continued..
    }
}
