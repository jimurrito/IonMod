using Microsoft.VisualStudio.TestTools.UnitTesting;
using IonMod;


namespace IonModTest
{
    //
    [TestClass]
    public class GetIonRecordTest
    {
        //
        private static IonToken Token = new IonToken(TestIonParameters.PublicPrefix, TestIonParameters.Secret);
        private GetIonRecord client = new GetIonRecord(Token, TestIonParameters.ZoneId, TestIonParameters.RecordId);

        
        [TestMethod]
        public void GetIonRecordTest200()
        {
            client.Run();
        }
        /*
        [TestMethod]
        public void TestTemplateMethod2()
        {

        }
        */
    }
    //
}