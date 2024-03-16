using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wosk.AutoService.MechanicsDataUSvc.Tests
{
    using Wosk.AutoService.MechanicsDataUSvc.Rest.Client;
    using Wosk.AutoService.MechanicsDataUSvc.Rest.Model;

    [TestClass]
    public class MechanicsDataServiceClient
    {
        [TestMethod]
        public void GetMechanicsData_GetsAllMechanicsData_ThereIsOneEntry()
        {
            MockMechanicsDataServiceClient serviceClient = new();
            MechanicData[] mechanicsData = serviceClient.GetMechanicsData();

            int expectedCount = 1;
            int actualCount = mechanicsData.Length;

            Assert.AreEqual(expectedCount, actualCount, "Entries count is {0} should be {1}", actualCount, expectedCount);
        }

        [TestMethod]

        public void GetMechanicData_GetAllDataOfMechanicWithSpecificPESEL_MatchExists()
        {
            MockMechanicsDataServiceClient serviceClient = new();
            MechanicData mechanicData = serviceClient.GetMechanicData("12345678912");

            string expectedName = "Kacper";
            string expectedSurname = "Szaruch";
            string expectedPesel = "12345678912";
            int[] expectedRepairsIds = {1,2,3};

            string actualName = mechanicData.name;
            string actualSurname = mechanicData.surname;
            string actualPesel = mechanicData.pesel;
            int[] actualRepairsIds = mechanicData.repairsIds;

            Assert.AreEqual(expectedName, actualName, "Name is {1} shoudld be {0}", actualName, expectedName);
            Assert.AreEqual(expectedSurname, actualSurname, "Surname is {1} should be {0}", actualSurname, expectedSurname);
            Assert.AreEqual(expectedPesel, actualPesel, "Pesel is {1} should be {0}", actualPesel, expectedPesel);
            CollectionAssert.AreEqual(expectedRepairsIds, actualRepairsIds, "Repairs Ids are {1} should be {0}", actualRepairsIds, expectedRepairsIds);
        }

        [TestMethod]

        public void GetMechanicData_GetAllDataOfMechanicWithSpecificPESEL_MatchDoesNotExist()
        {
            MockMechanicsDataServiceClient serviceClient = new();
            MechanicData mechanicData = serviceClient.GetMechanicData("0");

            string expectedName = null;
            string expectedSurname = null;
            string expectedPesel = null;
            int[] expectedRepairsIds = null;

            string actualName = mechanicData.name;
            string actualSurname = mechanicData.surname;
            string actualPesel = mechanicData.pesel;
            int[] actualRepairsIds = mechanicData.repairsIds;

            Assert.AreEqual(expectedName, actualName, "Name is {1} shoudld be null", actualName);
            Assert.AreEqual(expectedSurname, actualSurname, "Surname is {1} should be null", actualSurname);
            Assert.AreEqual(expectedPesel, actualPesel, "Pesel is {1} should be null", actualPesel);
            CollectionAssert.AreEqual(expectedRepairsIds, actualRepairsIds, "Repairs Ids are {1} should be null", actualRepairsIds);
        }

        [TestMethod]

        public void GetMechanicPersonalData_GetMechanicPersonalDataOfMechanicWithSpecificPESEL_MatchExists()
        {
            MockMechanicsDataServiceClient serviceClient = new();
            MechanicPersonalData mechanicPersonalData = serviceClient.GetMechanicPersonalData("12345678912");

            string expectedName = "Kacper";
            string expectedSurname = "Szaruch";
            string expectedPesel = "12345678912";

            string actualName = mechanicPersonalData.name;
            string actualSurname = mechanicPersonalData.surname;
            string actualPesel = mechanicPersonalData.pesel;

            Assert.AreEqual(expectedName, actualName, "Name is {1} shoudld be {0}", actualName, expectedName);
            Assert.AreEqual(expectedSurname, actualSurname, "Surname is {1} should be {0}", actualSurname, expectedSurname);
            Assert.AreEqual(expectedPesel, actualPesel, "Pesel is {1} should be {0}", actualPesel, expectedPesel);
        }

        [TestMethod]
        public void GetMechanicPersonalData_GetMechanicPersonalDataOfMechanicWithSpecificPESEL_MatchDoesNotExist()
        {
            MockMechanicsDataServiceClient serviceClient = new();
            MechanicPersonalData mechanicPersonalData = serviceClient.GetMechanicPersonalData("0");

            string expectedName = null;
            string expectedSurname = null;
            string expectedPesel = null;

            string actualName = mechanicPersonalData.name;
            string actualSurname = mechanicPersonalData.surname;
            string actualPesel = mechanicPersonalData.pesel;

            Assert.AreEqual(expectedName, actualName, "Name is {1} shoudld be null", actualName);
            Assert.AreEqual(expectedSurname, actualSurname, "Surname is {1} should be null", actualSurname);
            Assert.AreEqual(expectedPesel, actualPesel, "Pesel is {1} should be null", actualPesel);
        }

        [TestMethod]

        public void AssignNewRepair_AssignNewRepaitoLeastBusyMechanic_AssignSuccessful()
        {
            MockMechanicsDataServiceClient serviceClient = new();   
            
            bool expectedResult = true;
            string expectedName = "Kacper";
            string expectedSurname = "Szaruch";
            string expectedPesel = "12345678912";
            int[] expectedRepairsIds = { 1, 2, 3, 4};


            bool actualResult = serviceClient.AssignNewRepair(4);
            MechanicData mechanicData = serviceClient.GetMechanicData("12345678912");

            string actualName = mechanicData.name;
            string actualSurname = mechanicData.surname;
            string actualPesel = mechanicData.pesel;
            int[] actualRepairsIds = mechanicData.repairsIds;

            Assert.AreEqual(expectedResult, actualResult, "Result is {1} shoudld be {0}", actualResult, expectedResult);
            Assert.AreEqual(expectedName, actualName, "Name is {1} shoudld be {0}", actualName, expectedName);
            Assert.AreEqual(expectedSurname, actualSurname, "Surname is {1} should be {0}", actualSurname, expectedSurname);
            Assert.AreEqual(expectedPesel, actualPesel, "Pesel is {1} should be {0}", actualPesel, expectedPesel);
            CollectionAssert.AreEqual(expectedRepairsIds, actualRepairsIds, "Repairs Ids are {1} should be {0}", actualRepairsIds, expectedRepairsIds);
        }

        [TestMethod]

        public void AssignNewRepair_AssignNewRepaitoLeastBusyMechanic_AssignUnsuccessful()
        {
            MockMechanicsDataServiceClient serviceClient = new();

            bool expectedResult = false;

            bool actualResult = serviceClient.AssignNewRepair(3);

            Assert.AreEqual(expectedResult, actualResult, "Result is {1} shoudld be {0}", actualResult, expectedResult);
        }
    }
}
