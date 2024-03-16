using Wosk.AutoService.MechanicsDataUSvc.Model;

namespace Wosk.AutoService.MechanicsDataUSvc.Rest.Model
{
    public static class DataConverter
    {
        #region Converting from Mechanic to DTO classes
        public static MechanicData ConvertToMechanicData(Mechanic mechanic) 
        {
            return new MechanicData(mechanic.Name, mechanic.Surname,
                                    mechanic.Pesel, mechanic.RepairsIds);
        }

        public static MechanicPersonalData ConvertToMechanicPersonalData(Mechanic mechanic)
        {
            return new MechanicPersonalData(mechanic.Name, mechanic.Surname,
                                    mechanic.Pesel);
        }

        #endregion

        #region Converting from DTO classes to Mechanic
        public static Mechanic ConvertToMechanic(MechanicData mechanicData)
        {
            return new Mechanic(-1, mechanicData.name, mechanicData.surname, mechanicData.repairsIds, mechanicData.pesel);
        }

        #endregion
    }
}
