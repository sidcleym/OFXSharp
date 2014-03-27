using System.IO;
using System.Linq;
using System.Text;
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

        [Test]
        public void CanParserBancoDoBrasil()
        {
            var parser = new OFXDocumentParser();
            var ofxDocument = parser.Import(new FileStream(@"bb.ofx", FileMode.Open), Encoding.GetEncoding("ISO-8859-1"));

            Assert.AreEqual(ofxDocument.Account.AccountID, "99999-9");
            Assert.AreEqual(ofxDocument.Account.BranchID, "9999-9");
            Assert.AreEqual(ofxDocument.Account.BankID, "1");

            Assert.AreEqual(3, ofxDocument.Transactions.Count());
            CollectionAssert.AreEqual(ofxDocument.Transactions.Select(x => x.Memo.Trim()).ToList(), new[] { "Transferência Agendada", "Compra com Cartão", "Saque" });
            
            Assert.IsNotNull(ofxDocument);
        }
    }
}
