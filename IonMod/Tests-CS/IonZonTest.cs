using Microsoft.VisualStudio.TestTools.UnitTesting;
using IonMod;
using System.Diagnostics;

namespace IonModTest
{
    //
    [TestClass]
    public class IonZoneTest
    {
        List<IonZone> TestZones;
        //
        // Init test with login
        public IonZoneTest()
        {
            TestIonParameters.LoginTest();
            // get all zones to ensure records are not uninstantized
            TestZones = GetIonZone.Run();
        }
        //
        //
        [TestMethod]
        public void IonZoneTestUnInitRecError()
        {
            try
            {
                // pull one zone
                IonZone TestZone = TestZones[0];
                TestZone.Records.ToString(); // should fail
                Debug.Assert(false);
            }
            // success means we catch here
            catch (IonUninitException)
            {
                return;
            }
            Debug.Assert(false);
        }
    }
    //
}