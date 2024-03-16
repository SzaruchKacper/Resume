using Wosk.AutoService.MechanicsDataUSvc.Rest.Model;

namespace Wosk.AutoService.MechanicsDataUSvc.Rest.Services
{
    public interface IMechanicsController
    {
        public int[] GetAssignedRepairsIds(string pesel);
        public MechanicData GetMechanicData(string pesel);
        public MechanicData[] GetMechanicsData();
        public MechanicPersonalData GetMechanicPersonalData(string pesel);
        public void AssignNewRepair(int repairId);
    }
}
