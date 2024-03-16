using Wosk.AutoService.MechanicsDataUSvc.Model;

namespace Wosk.AutoService.MechanicsDataUSvc.Logic
{
    public class Mechanics:IMechanics
    {
        private static List<Mechanic>? mechanics;
        private static readonly object mechanicsLock = new object();
        private static MechanicsFileHandler? mechanicsFileHandler = new MechanicsFileHandler();

        private const string filename = "Data/mechanics.json";

        static Mechanics() {

            lock (mechanicsLock)
            {
                Mechanic[]? mechanicsArray = mechanicsFileHandler.ReadMechanics(filename);
                if(mechanicsArray == null) { mechanics = new List<Mechanic>(); }
                else { mechanics = new List<Mechanic>(mechanicsArray); }
            }

            
        }

        public Mechanic? GetMechanic(string pesel)
        {
            lock (mechanicsLock) 
            {
                return mechanics.Find(x => (x.Pesel.Equals(pesel)));
            }
            
        }

        public Mechanic? GetMechanic(int repairId)

        {   
            lock (mechanicsLock)
            {
                return mechanics.Find(x => (x.RepairsIds.Contains(repairId)));
            }         
        }

        public Mechanic[] GetMechanics()

        {
            lock (mechanicsLock) 
            {
                return mechanics.ToArray();
            }            
        }

        public bool AssignNewRepair(int repairId) 
        {
            bool success = true;
            lock (mechanicsLock)
            {
                Mechanic leastBusy = mechanics.First();
                foreach (Mechanic mechanic in mechanics)
                {
                    if (mechanic.RepairsIds.Contains<int>(repairId))
                    {
                        success = false;
                        return success;
                    }
                    else if (leastBusy.RepairsIds.Length > mechanic.RepairsIds.Length)
                    {
                        leastBusy = mechanic;
                    }
                }
                List<int> newRepairsIds = new List<int>(leastBusy.RepairsIds);
                newRepairsIds.Add(repairId);
                leastBusy.RepairsIds = newRepairsIds.ToArray();
                mechanicsFileHandler.WriteMechanics(filename, mechanics.ToArray());   
                
                return success;
            }
        }        
    }
}
