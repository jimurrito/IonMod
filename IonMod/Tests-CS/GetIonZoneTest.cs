using Microsoft.VisualStudio.TestTools.UnitTesting;
using IonMod;
using System.Diagnostics;

namespace IonModTest
{
    [TestClass]
    public class GetIonZoneTest
    {   
        // Init test with login
        public GetIonZoneTest()
        {
            TestIonParameters.LoginTest();
        }
        //
        // Get-IonZone - All
        [TestMethod]
        public void GetIonZoneTestAll()
        {
            //TestIonParameters.LoginTest();
            Debug.Assert(GetIonZone.Run() is List<IonZone>);
        }
        //
        // Get-IonZone - One
        [TestMethod]
        public void GetIonZoneTestOne()
        {
            //TestIonParameters.LoginTest();
            Debug.Assert(GetIonZone.Run(TestIonParameters.ZoneId) is IonZone);
        }
        //
        // Get-IonZone - One (Should fail with 401)
        // Any typos or errors on the URL path will cause a 401. Only malformed body will return 400.
        [TestMethod]
        public void GetIonZoneTestOneFail()
        {
            //TestIonParameters.LoginTest();
            try { GetIonZone.Run("failure-test"); Debug.Assert(false); }
            catch (IonUnauthorizedException) { }
        }
    }
}