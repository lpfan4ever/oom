using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lesson3
{
    [TestFixture]
    class Exchangeratetest
    {
        [Test]
        public void ExchangeRateTest1()
        {
            var x = ExchangeRates.Get(Currency.EUR, Currency.EUR);
           // var y = ExchangeRates.Get(Currency.GBP, Currency.USD);
            //var z = ExchangeRates.Get(Currency.USD, Currency.GBP);
            Assert.IsTrue(x == 1);
        }
    }
}
