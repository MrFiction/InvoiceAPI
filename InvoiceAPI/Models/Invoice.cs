using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceAPI.Models
{
    /// <summary>
    /// Class and for holding invoice data & static methods to generate one
    /// </summary>
    public class Invoice : IEquatable<Invoice>
    {

        public Client client { get; private set; }
        public ServiceProvider provider { get; private set; }
        public string invoiceID { get; private set; }
        public DateTime date { get; private set; }
        public decimal preVATamount { get; private set; }
        public decimal total { get; private set; }
        public int vat { get; private set; }
        private static int invoiceCount;

        public Invoice()
        {
        }

        public Invoice(Client client, ServiceProvider provider,decimal preVATamount, decimal total, int vat)
        {
            this.client = client;
            this.provider = provider;
            this.invoiceID = generateId();
            this.date = DateTime.Now;
            this.preVATamount = preVATamount;
            this.total = total;
            this.vat = vat;
        }
        /// <summary>
        /// static method for returning an invoice depending on the client - provider relation
        /// (called from API POST. client from request in JSON. Service provider from JSON file in App_Data/serviceProvider.json  
        /// </summary>
        /// <param name="client">client data name,id etc.</param>
        /// <param name="provider">service provider data name,id etc.</param>
        /// <returns>invoice with the client data, provider data, pre VAT amount, total, calculated VAT</returns>
        public static Invoice generateInvoice(Client client, ServiceProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("No service provider");
            }
            ///if the provider is not a VAT payer VAT = 0

            if (!provider.paysVAT)
            {
                return new Invoice(client, provider, client.amountToPay, client.amountToPay, 0);
            }
            else
            {
                int vatForThisPair = calculateVAT(client, provider);
                decimal amountAfterVat = client.amountToPay / 100 * vatForThisPair + client.amountToPay;
                return new Invoice(client, provider, client.amountToPay,Decimal.Round(amountAfterVat,2), vatForThisPair);
            }

        }


        /// <summary>
        /// calculates VAT depending on client and provider country, if the client or provider pays VAT etc.
        ///
        ///It's assumed the provider is a VAT payer
        ///if the client is outside the EU(Europe Union) VAT = 0
        ///if the client is in the EU but is not a VAT payer VAT = VAT in his country
        ///if the client is in the EU and is VAT payer VAT = 0
        ///if the client and provider are in the same country VAT = VAT in that country 
        /// </summary>
        /// <param name="client">client data name,id etc.</param>
        /// <param name="provider">service provider data name,id etc.</param>
        /// <returns>VAT as int based on conditions above or -1(should not happen)</returns>
        private static int calculateVAT(Client client, ServiceProvider provider)
        {
            if (!client.inEU)
            {
                return 0;
            }
            if (!client.country.Equals(provider.country) && !client.paysVAT)
            {
                return client.vatInCountryOfOrigin;
            }
            if (!client.country.Equals(provider.country) && client.paysVAT)
            {
                return 0;
            }
            if (client.country.Equals(provider.country))
            {
                return provider.vatInCountryOfOrigin;
            }

            return -1;
        }

        /// <summary>
        /// Generates a unique invoice ID for every invoice
        /// result AA + year&month + 10 digit counter starting at 1
        /// example : "AA2018110000009999"
        /// </summary>
        /// <returns>string with the generated unique invoice ID</returns>
        private string generateId()
        {
            invoiceCount++;
            return "AA" + DateTime.Today.Year + DateTime.Today.Month + invoiceCount.ToString(new string ('0',10)) ;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Invoice);  
        }

        public bool Equals(Invoice other)
        {
            return other != null &&
                   EqualityComparer<Client>.Default.Equals(client, other.client) &&
                   EqualityComparer<ServiceProvider>.Default.Equals(provider, other.provider) &&
                   preVATamount == other.preVATamount &&
                   total == other.total &&
                   vat == other.vat;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(client, provider, invoiceID, date, preVATamount, total, vat);
        }
    }
}
