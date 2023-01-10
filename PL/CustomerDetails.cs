using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public class CustomerDetails
    {
        public string name { get; set; }
        public string address { get; set; }
        public string email { get; set; }

      
        public CustomerDetails(string name, string address, string email)
        {
            this.name = name;
            this.address = address;
            this.email = email;
        }
    }
}
