using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace OFXSharp.Tests
{
    [TestFixture]
    public class CanParser
    {
        [Test]
        public void CanParserItau()
        {
            var parser = new OFXDocumentParser();
            var ofxDocument = parser.Import(new FileStream(@"itau.ofx", FileMode.Open));

            Assert.AreEqual(ofxDocument.Account.AccountID, "9999 99999-9");
            Assert.AreEqual(ofxDocument.Account.BankID, "0341");

            Assert.AreEqual(3, ofxDocument.Transactions.Count());
            CollectionAssert.AreEqual(ofxDocument.Transactions.Select(x => x.Memo.Trim()).ToList(), new[] { "RSHOP", "REND PAGO APLIC AUT MAIS", "SISDEB" });
        }

        [Test]
        public void CanParserSantander()
        {
            var parser = new OFXDocumentParser();
            var ofxDocument = parser.Import(new FileStream(@"santander.ofx", FileMode.Open));

            Assert.IsNotNull(ofxDocument);
        }
    }
}
