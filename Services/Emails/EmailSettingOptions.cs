using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryAppBackend.Services.Emails
{
    public class EmailSettingOptions
    {
        public String PrimaryDomain { get; set; }
        public String SecondaryDomain { get; set; }
        public String PrimaryPort { get; set; }
        public String SecondaryPort { get; set; }
        public String FromEmail { get; set; }
        public String FromPassword { get; set; }
    }
}
