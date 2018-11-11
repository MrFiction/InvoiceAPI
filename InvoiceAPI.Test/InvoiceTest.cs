using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;


namespace InvoiceAPI.Test
{
    public class InvoiceTest
    {
        [Theory]
        [ClassData(typeof(IndexOfData))]
        public void Generate_ServiceProviderDoesNotPayVAT(Models.Client client, Models.ServiceProvider provider, Models.Invoice expectedInvoice)
        {
            Models.Invoice actualInvoice = Models.Invoice.generateInvoice(client, provider);

            Assert.Equal(actualInvoice, expectedInvoice);
        }
        public class IndexOfData : IEnumerable<object[]>
        {
            private readonly List<object[]> _data = new List<object[]>
            {
                 new object[]
                {
                    new Models.Client("nameOfClient1",123456789, "qqqaaa123456",1000, 21,"Lithuania",false,true,true),
                    new Models.ServiceProvider("NameOfProvider",987654321,19,"Poland",false,true),
                    new Models.Invoice
                    (
                        new Models.Client("nameOfClient1",123456789, "qqqaaa123456",1000, 21,"Lithuania",false,true,true),
                        new Models.ServiceProvider("NameOfProvider",987654321,19,"Poland",false,true),
                        1000,1000,0
                    )
                },
                  new object[]
                {
                    new Models.Client("nameOfClient2",123456789, "qqqaaa123456",1000, 21,"Lithuania",false,true,true),
                    new Models.ServiceProvider("NameOfProvider",987654321,21,"Lithuania",false,true),
                    new Models.Invoice
                    (
                        new Models.Client("nameOfClient2",123456789, "qqqaaa123456",1000, 21,"Lithuania",false,true,true),
                        new Models.ServiceProvider("NameOfProvider",987654321,21,"Lithuania",false,true),
                        1000,1000,0
                    )
                }

            };

            public IEnumerator<object[]> GetEnumerator()
            { return _data.GetEnumerator(); }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

        }
        [Theory]
        [ClassData(typeof(IndexOfData2))]
        public void Generate_ClientOutsideEU(Models.Client client, Models.ServiceProvider provider, Models.Invoice expectedInvoice)
        {
            Models.Invoice actualInvoice = Models.Invoice.generateInvoice(client, provider);

            Assert.Equal(actualInvoice, expectedInvoice);
        }
        public class IndexOfData2 : IEnumerable<object[]>
        {
            private readonly List<object[]> _data = new List<object[]>
            {
                new object[]
                {
                    new Models.Client("nameOfClient1",123456789, "qqqaaa123456",500, 17,"China",false,false,false),
                    new Models.ServiceProvider("NameOfProvider",987654321,19,"Poland",true,true),
                    new Models.Invoice
                    (
                        new Models.Client("nameOfClient1",123456789, "qqqaaa123456",500, 17,"China",false,false,false),
                        new Models.ServiceProvider("NameOfProvider",987654321,19,"Poland",true,true),
                        500,500,0
                    )
                },
                new object[]
                {
                    new Models.Client("nameOfClient2",123456789, "qqqaaa123456",500, 16,"Mexico",true,true,false),
                    new Models.ServiceProvider("NameOfProvider",987654321,19,"Poland",true,true),
                    new Models.Invoice
                    (
                        new Models.Client("nameOfClient2",123456789,"qqqaaa123456",500, 16,"Mexico",true,true,false),
                        new Models.ServiceProvider("NameOfProvider",987654321,19,"Poland",true,true),
                        500,500,0
                    )
                }
            };

            public IEnumerator<object[]> GetEnumerator()
            { return _data.GetEnumerator(); }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

        }
        [Theory]
        [ClassData(typeof(IndexOfData3))]
        public void Generate_ServiceProviderInEU_DiferentCountrysClientNoVat(Models.Client client, Models.ServiceProvider provider, Models.Invoice expectedInvoice)
        {
            Models.Invoice actualInvoice = Models.Invoice.generateInvoice(client, provider);

            Assert.Equal(actualInvoice, expectedInvoice);
        }
        public class IndexOfData3 : IEnumerable<object[]>
        {
            private readonly List<object[]> _data = new List<object[]>
            {
                new object[]
                {
                    new Models.Client("nameOfClient1",123456789, "qqqaaa123456",1000, 21,"Lithuania",false,true,true),
                    new Models.ServiceProvider("NameOfProvider",987654321,19,"Poland",true,true),
                    new Models.Invoice
                    (
                        new Models.Client("nameOfClient1",123456789, "qqqaaa123456",1000, 21,"Lithuania",false,true,true),
                        new Models.ServiceProvider("NameOfProvider",987654321,19,"Poland",true,true),
                        1000,1210,21
                    )
                },
                new object[]
                {
                    new Models.Client("nameOfClient1",123456789, "qqqaaa123456",20000, 21,"Lithuania",false,false,true),
                    new Models.ServiceProvider("NameOfProvider",987654321,19,"Poland",true,true),
                    new Models.Invoice
                    (
                        new Models.Client("nameOfClient1",123456789, "qqqaaa123456",20000, 21,"Lithuania",false,false,true),
                        new Models.ServiceProvider("NameOfProvider",987654321,19,"Poland",true,true),
                        20000,24200,21
                    )
                }

            };

            public IEnumerator<object[]> GetEnumerator()
            { return _data.GetEnumerator(); }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

        }
        [Theory]
        [ClassData(typeof(IndexOfData4))]
        public void Generate_ServiceProviderInEU_DiferentCountrysClientPaysVat(Models.Client client, Models.ServiceProvider provider, Models.Invoice expectedInvoice)
        {
            Models.Invoice actualInvoice = Models.Invoice.generateInvoice(client, provider);

            Assert.Equal(actualInvoice, expectedInvoice);
        }
        public class IndexOfData4 : IEnumerable<object[]>
        {
            private readonly List<object[]> _data = new List<object[]>
            {
                new object[]
                {
                    new Models.Client("nameOfClient1",123456789, "qqqaaa123456",1000, 21,"Lithuania",true,true,true),
                    new Models.ServiceProvider("NameOfProvider",987654321,19,"Poland",true,true),
                    new Models.Invoice
                    (
                        new Models.Client("nameOfClient1",123456789, "qqqaaa123456",1000, 21,"Lithuania",true,true,true),
                        new Models.ServiceProvider("NameOfProvider",987654321,19,"Poland",true,true),
                        1000,1000,0
                    )
                },

            };

            public IEnumerator<object[]> GetEnumerator()
            { return _data.GetEnumerator(); }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

        }
        [Theory]
        [ClassData(typeof(IndexOfData5))]
        public void Generate_ServiceProviderInEU_SameCountry(Models.Client client, Models.ServiceProvider provider, Models.Invoice expectedInvoice)
        {
            Models.Invoice actualInvoice = Models.Invoice.generateInvoice(client, provider);

            Assert.Equal(actualInvoice, expectedInvoice);
        }
        public class IndexOfData5 : IEnumerable<object[]>
        {
            private readonly List<object[]> _data = new List<object[]>
            {
                new object[]
                {
                    new Models.Client("nameOfClient1",123456789, "qqqaaa123456",1000, 21,"Lithuania",false,true,true),
                    new Models.ServiceProvider("NameOfProvider",987654321,21,"Lithuania",true,true),
                    new Models.Invoice
                    (
                        new Models.Client("nameOfClient1",123456789, "qqqaaa123456",1000, 21,"Lithuania",false,true,true),
                        new Models.ServiceProvider("NameOfProvider",987654321,21,"Lithuania",true,true),
                        1000,1210,21
                    )
                }
            };

            public IEnumerator<object[]> GetEnumerator()
            { return _data.GetEnumerator(); }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

        }


    }
}
