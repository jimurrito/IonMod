using Microsoft.VisualStudio.TestTools.UnitTesting;
using IonMod;

namespace IonModTest
{
    //
    [TestClass]
    public class SetIonZoneTest
    {
        IonZone TestZone;
        //
        // Init test with login
        public SetIonZoneTest()
        {
            TestIonParameters.LoginTest();
            TestZone = GetIonZone.Run(TestIonParameters.ZoneId);
        }
        //
        //
        [TestMethod]
        public void SetIonZoneTestSuccess()
        {
            SetIonZone.Run(TestIonParameters.ZoneId, TestZone.Records);
        }
    }
    //
}