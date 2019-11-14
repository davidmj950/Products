using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace David.Products.Common.Models
{
    public class ReCaptchaResponse
    {
        public bool Success;
        public string ChallengeTs;
        public string Hostname;
        public object[] ErrorCodes;
    }
}
