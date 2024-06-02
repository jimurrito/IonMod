using Microsoft.VisualStudio.TestTools.UnitTesting;
using IonMod;
using System.Diagnostics;

namespace IonModTest
{
    [TestClass]
    public class GetIonZoneTest
    {
        //
        private IonToken Token;
        //
        public GetIonZoneTest()
        {
            Token = new IonToken(TestIonParameters.PublicPrefix, TestIonParameters.Secret);
        }
        //
        //
        // Get-IonZone - All
        [TestMethod]
        public void GetIonZoneTestAll(){
            GetIonZone client = new GetIonZone(Token);
            client.Run();
        }
        //
        // Get-IonZone - One
        [TestMethod]
        public void GetIonZoneTestOne(){
            GetIonZone client = new GetIonZone(Token);
            var result = client.Run(TestIonParameters.ZoneId);
            Debug.Assert(result != null);
        }

    }
}