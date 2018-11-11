using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceAPI.Models
{
    public class Client : IEquatable<Client>
    {
        public string name { get; set; }
        public int id { get; set; }
        public string orderID { get; set; }
        public decimal amountToPay { get; set; }
        public int vatInCountryOfOrigin { get; set; }
        public string country { get; set; }
        public bool paysVAT { get; set; }
        public bool isJuridicalPerson { get; set; }
        public bool inEU { get; set; }

        public Client()
        {
        }

        public Client(string name, int id, string orderID, decimal amountToPay, int vatInCountryOfOrigin, string country, bool paysVAT, bool isJuridicalPerson, bool inEU)
        {
            this.name = name;
            this.id = id;
            this.orderID = orderID;
            this.amountToPay = amountToPay;
            this.vatInCountryOfOrigin = vatInCountryOfOrigin;
            this.country = country;
            this.paysVAT = paysVAT;
            this.isJuridicalPerson = isJuridicalPerson;
            this.inEU = inEU;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Client);
        }

        public bool Equals(Client other)
        {
            return other != null &&
                   name == other.name &&
                   id == other.id &&
                   vatInCountryOfOrigin == other.vatInCountryOfOrigin &&
                   country == other.country &&
                   paysVAT == other.paysVAT &&
                   isJuridicalPerson == other.isJuridicalPerson &&
                   inEU == other.inEU;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(name, id, vatInCountryOfOrigin, country, paysVAT, isJuridicalPerson, inEU);
        }
    }
}
