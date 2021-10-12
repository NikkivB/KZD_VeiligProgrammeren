using Microsoft.VisualStudio.TestTools.UnitTesting;
using VeiligProgrammeren2122;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeiligProgrammeren2122.Tests
{
    [TestClass()]
    public class StaticMethodsTests
    {
        [TestMethod()]
        public void ShiftCypherTest1()
        {
            string text = "HALLO";
            string encrypted = StaticMethods.ShiftCypher(text, 2);

            Assert.AreEqual("JCNNQ", encrypted);
        }

        [TestMethod()]
        public void UnShiftCypherTest1()
        {
            string text = "JCNNQ";
            string decrypted = StaticMethods.UnShiftCypher(text, 2);

            Assert.AreEqual("HALLO", decrypted);
        }

        [TestMethod()]
        public void ShiftCypherTest2()
        {
            string text = "Hallo123";
            string encrypted = StaticMethods.ShiftCypher(text, 2);

            Assert.AreEqual("Jcnnq123", encrypted);
        }
    }
}