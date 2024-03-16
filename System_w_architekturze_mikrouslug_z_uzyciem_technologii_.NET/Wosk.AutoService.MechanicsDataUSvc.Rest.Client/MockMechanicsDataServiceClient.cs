using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wosk.AutoService.MechanicsDataUSvc.Rest.Client
{
    using Wosk.AutoService.MechanicsDataUSvc.Rest.Model;
    public class MockMechanicsDataServiceClient : IMechanicsService
    {
        private MechanicData[] mechanicsData = new MechanicData[]{
            new MechanicData("Kacper", "Szaruch", "12345678912", new int[] {1, 2, 3})
        };

        private MechanicPersonalData[] mechanicsPersonalData = new MechanicPersonalData[]{
            new MechanicPersonalData("Kacper", "Szaruch", "12345678912")
        };


        public bool AssignNewRepair(int repairId)
        {
            bool isSuccess = true;
                MechanicData leastBusy = mechanicsData.First();
                foreach (MechanicData mechanic in mechanicsData)
                {
                    if (mechanic.repairsIds.Contains<int>(repairId))
                    {
                        isSuccess = false;
                        return isSuccess;
                    }
                    else if (leastBusy.repairsIds.Length > mechanic.repairsIds.Length)
                    {
                        leastBusy = mechanic;
                    }
                }
                List<int> newRepairsIds = new List<int>(leastBusy.repairsIds);
                newRepairsIds.Add(repairId);
                leastBusy.repairsIds = newRepairsIds.ToArray();
                return isSuccess;
            }

        public int[] GetAssignedRepairsIds(string pesel)
        {
            foreach (MechanicData mechanic in mechanicsData)
            {
                if (mechanic.pesel.Equals(pesel))
                {
                    return mechanic.repairsIds;
                }
            }
            return new int[] {};
        }

        public MechanicData GetMechanicData(string pesel)
        {
            foreach (MechanicData mechanic in mechanicsData)
            {
                if (mechanic.pesel.Equals(pesel))
                {
                    return mechanic;
                }
            }
            return new();
        }

        public MechanicPersonalData GetMechanicPersonalData(string pesel)
        {
            foreach (MechanicPersonalData mechanic in mechanicsPersonalData)
            {
                if (mechanic.pesel.Equals(pesel))
                {
                    return mechanic;
                }
            }
            return new();
        }

        public MechanicData[] GetMechanicsData()
        {
            return mechanicsData;
        }
    }
}
