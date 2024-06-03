using Microsoft.VisualStudio.TestTools.UnitTesting;
using IonMod;
using System.Diagnostics;

namespace IonModTest
{
    [TestClass]
    public class GetIonZoneTest
    {
        //
        private static IonToken Token = new IonToken(TestIonParameters.PublicPrefix, TestIonParameters.Secret);
        private GetIonZone client = new GetIonZone(Token);
        //
        //
        // Get-IonZone - All
        [TestMethod]
        public void GetIonZoneTestAll()
        {
            client.Run();
        }
        //
        // Get-IonZone - One
        [TestMethod]
        public void GetIonZoneTestOne()
        {
            client.Run(TestIonParameters.ZoneId);
        }
        //
        // Get-IonZone - One (Should fail with 401)
        // Any typos or errors on the URL path will cause a 401. Only malformed body will return 400.
        [TestMethod]
        public void GetIonZoneTestOneFail()
        {
            try
            {
                client.Run(TestIonParameters.ZoneId + "z");
            }
            catch (IonUnauthorizedException)
            {
                Debug.Assert(true);
            }

        }

    }
}