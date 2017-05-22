using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void SimpleTest1()
        {
            Assert.IsTrue(1 == 1);
        }
        [Test]
        public void SimpleTest2()
        {
            var x = new customer("Firma 1",120, 0);
            Assert.IsTrue(x.Description == "Firma 1");
        }
        [Test]
        public void SimpleTest3()
        {
            Assert.Catch(() =>
            {
                var x = new customer(null, 120, 0);
            });
        }
        [Test]
        public void SimpleTest4()
        {
            Assert.Catch(() =>
            {
                var x = new customer("", 120, 0);
            });
        }
        [Test]
        public void SimpleTest5()
        {
            Assert.Catch(() =>
            {
                var x = new customer("Firma 1", -1, 0);
            });
        }
        [Test]
        public void SimpleTest6()
        {
            Assert.Catch(() =>
            {
                var x = new customer("Firma 1", 150, -1);
            });
        }
        [Test]
        public void SimpleTest7()
        {
            Assert.Catch(() =>
            {
                var x = new customer("Firma 1", 150, 0);
                x.UpdateNumber(-1);
            });
        }
        [Test]
        public void SimpleTest8()
        {
            var x = new customer("Firma 1", 150, 0);
            Assert.IsTrue(x.Name == "Firma 1");
        }
    }
}
