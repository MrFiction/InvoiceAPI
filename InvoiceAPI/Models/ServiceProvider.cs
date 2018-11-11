using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceAPI.Models
{

    public class ServiceProvider : IEquatable<ServiceProvider>
    {
        public string name { get; set; }
        public int id { get; set; }
        public int vatInCountryOfOrigin { get; set; }
        public string country { get; set; }
        public bool paysVAT { get; set; }
        public bool inEU { get; set; }

        public ServiceProvider()
        {
        }

        public ServiceProvider(string name, int id, int vatInCountryOfOrigin, string country, bool paysVAT, bool inEU)
        {
            this.name = name;
            this.id = id;
            this.vatInCountryOfOrigin = vatInCountryOfOrigin;
            this.country = country;
            this.paysVAT = paysVAT;
            this.inEU = inEU;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ServiceProvider);
        }

        public bool Equals(ServiceProvider other)
        {
            return other != null &&
                   name == other.name &&
                   id == other.id &&
                   vatInCountryOfOrigin == other.vatInCountryOfOrigin &&
                   country == other.country &&
                   paysVAT == other.paysVAT &&
                   inEU == other.inEU;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(name, id, vatInCountryOfOrigin, country, paysVAT, inEU);
        }

    }
}
