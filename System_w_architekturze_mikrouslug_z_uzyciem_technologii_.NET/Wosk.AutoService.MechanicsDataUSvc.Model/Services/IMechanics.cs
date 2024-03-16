namespace Wosk.AutoService.MechanicsDataUSvc.Model
{
    public interface IMechanics
    {
        public Mechanic GetMechanic(string pesel);

        public Mechanic GetMechanic(int repairId);

        public Mechanic[] GetMechanics();

        public bool AssignNewRepair(int repairId);
    }
}
