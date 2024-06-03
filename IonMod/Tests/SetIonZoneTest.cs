using Microsoft.VisualStudio.TestTools.UnitTesting;
using IonMod;
using System.Diagnostics;

namespace IonModTest
{
    //
    [TestClass]
    public class SetIonZoneTest
    {
        //
        private static IonToken Token = new IonToken(TestIonParameters.PublicPrefix, TestIonParameters.Secret);
        private GetIonZone gClient = new GetIonZone(Token);
        //
        [TestMethod]
        public void SetIonZoneTest200()
        {
            IonZone? tempZone = gClient.Run(TestIonParameters.ZoneId);
            Debug.Assert(tempZone != null);
            SetIonZone sClient = new SetIonZone(Token, tempZone);
            sClient.Run();
        }
    }
    //
}