namespace Wosk.AutoService.MechanicsDataUSvc.Rest.Model
{
    public interface IMechanicsService
    {
        public int[] GetAssignedRepairsIds(string pesel);
        public MechanicData GetMechanicData(string pesel);
        public MechanicData[] GetMechanicsData();
        public MechanicPersonalData GetMechanicPersonalData(string pesel);        
        public bool AssignNewRepair(int repairId);
    }
}
