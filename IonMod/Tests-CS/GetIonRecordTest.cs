using Microsoft.VisualStudio.TestTools.UnitTesting;
using IonMod;
using System.Diagnostics;


namespace IonModTest
{
    //
    [TestClass]
    public class GetIonRecordTest
    {
        // Init test with login
        public GetIonRecordTest()
        {
            TestIonParameters.LoginTest();
        }
        //
        // get-ionrecord - Success
        [TestMethod]
        public void GetIonRecordTestSuccess()
        {
            Debug.Assert(GetIonRecord.Run(TestIonParameters.ZoneId, TestIonParameters.RecordId) is IonRecord);
        }
        //
        // get-ionrecord - fail
        [TestMethod]
        public void GetIonRecordTestFail()
        {
            try { GetIonRecord.Run("failure-test", "failure-test"); Debug.Assert(false); }
            catch (IonUnauthorizedException) { }
        }
    }
    //
}