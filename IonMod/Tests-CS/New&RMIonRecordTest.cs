using Microsoft.VisualStudio.TestTools.UnitTesting;
using IonMod;
using System.Diagnostics;


namespace IonModTest
{
    //
    [TestClass]
    public class NewAndRMIonRecordTest
    {
        IonRecord TestRecord;
        //
        // Init test class
        public NewAndRMIonRecordTest()
        {
            TestIonParameters.LoginTest();
            IonZone TestZone = GetIonZone.Run(TestIonParameters.ZoneId);
            TestRecord = new("test2" + "." + TestZone.Name, "1.1.0.0");
        }
        //
        [TestMethod]
        public void NewAndRMIonRecordTestSuccess()
        {
            List<IonRecord> TestRecordList = new List<IonRecord> { TestRecord };
            Debug.Assert(TestRecordList[0] is IonRecord);
            List<IonRecord> CreatedList = NewIonRecord.Run(TestIonParameters.ZoneId, TestRecordList);
            Debug.Assert(CreatedList[0] is IonRecord);
            RmIonRecord.Run(TestIonParameters.ZoneId,CreatedList[0]);
        }
    }

}