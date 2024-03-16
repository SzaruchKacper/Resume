using Wosk.AutoService.MechanicsDataUSvc.Logic;
using Wosk.AutoService.MechanicsDataUSvc.Model;
using Wosk.AutoService.MechanicsDataUSvc.Rest.Client;
using Wosk.AutoService.MechanicsDataUSvc.Rest.Model;

namespace Wosk.AutoSerwis.MechanicsDataUSvc.ConsoleApp
{

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Inner functions");
            InnerFunctionsTests();
            Console.WriteLine("\n---------------------------------------------------------\n");

            Console.WriteLine("MechanicsDataServiceClient");
            RestTest();
        }

        private static void InnerFunctionsTests()
        {
            Console.WriteLine("Reading mechanics from valid XML");
            Mechanic[] mechanicsTemp = new MechanicsFileHandler().ReadMechanics("mechanics.xml", "mechanics.xsd");
            foreach (Mechanic mechanic in mechanicsTemp)
            {
                Console.WriteLine(mechanic);
            }
            Console.WriteLine();
            Console.WriteLine("Reading mechanics from invalid XML");
            Mechanic[] mechanicsTemp2 = new MechanicsFileHandler().ReadMechanics("mechanics2.xml", "mechanics.xsd");
            Console.WriteLine();
            foreach (Mechanic mechanic in mechanicsTemp2)
            {
                Console.WriteLine(mechanic);
            }

            Console.WriteLine();
            Console.WriteLine("Reading mechanics from Json...");
            Mechanics mechanics = new Mechanics();
            foreach (Mechanic mechanic in mechanics.GetMechanics())
            {
                Console.WriteLine(mechanic);
            }            
        }

        private static void RestTest()
        {
            Console.WriteLine();
            Console.WriteLine("Reading mechanics...");

            MechanicsDataServiceClient restClient = new MechanicsDataServiceClient("localhost", 52080);
            MechanicData[] mechanicsData = restClient.GetMechanicsData();
            foreach (MechanicData mechanicData1 in mechanicsData)
            {
                Console.WriteLine(mechanicData1);
            }

            Console.WriteLine();
            string pesel = "12345678912";
            Console.WriteLine(String.Format("Reading mechanic data of mechanic with pesel = {0} ...", pesel));

            MechanicData mechanicData = restClient.GetMechanicData(pesel);
            Console.WriteLine(mechanicData.ToString());
            Console.WriteLine();


            Console.WriteLine(String.Format("Reading repairs data of mechanic with pesel = {0}...", pesel));

            int[] assignedReparisIds = restClient.GetAssignedRepairsIds(pesel);
            foreach (int assignedRepairId in assignedReparisIds)
            {
                Console.WriteLine(assignedRepairId);
            }



            Console.WriteLine();
            Console.WriteLine(String.Format("Reading mechanic personal data of mechanic with pesel = {0}...", pesel));

            MechanicPersonalData mechanicPersonalData = restClient.GetMechanicPersonalData(pesel);
            Console.WriteLine(mechanicPersonalData);
           
            int repairId = 99;

            Console.WriteLine();
            Console.WriteLine(String.Format("Assignig new repariId = {0} to least busy mechanic", repairId.ToString()));
            Console.WriteLine("First assign attempt");
            Console.WriteLine(restClient.AssignNewRepair(repairId));
            Console.WriteLine();

            Console.WriteLine("Second assign attempt");
            Console.WriteLine(restClient.AssignNewRepair(repairId));
            Console.WriteLine();

            Console.WriteLine("Updated data");
            MechanicData[] mechanicsDatas = restClient.GetMechanicsData();
            foreach (MechanicData mechanic in mechanicsDatas)
            {
                Console.WriteLine(mechanic);
            }

        }
    }
}