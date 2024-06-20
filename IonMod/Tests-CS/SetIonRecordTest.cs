using Microsoft.VisualStudio.TestTools.UnitTesting;
using IonMod;
using System.Diagnostics;


namespace IonModTest
{
    //
    [TestClass]
    public class SetIonRecordTest
    {
        IonRecord TestRecord;
        //
        // Init test with login
        public SetIonRecordTest()
        {
            TestIonParameters.LoginTest();
            TestRecord = GetIonRecord.Run(TestIonParameters.ZoneId, TestIonParameters.RecordId);
        }
        //
        //
        [TestMethod]
        public void SetIonRecordTestSuccess()
        {
            Debug.Assert(SetIonRecord.Run(TestIonParameters.ZoneId, TestRecord) is IonRecord);
        }

    }

}