namespace Wosk.AutoService.MechanicsDataUSvc.Rest.Model
{
    public class MechanicData
    {
        public string name { get; set; }
        public string surname { get; set; }
        public string pesel { get; set; }   
        public int[] repairsIds { get; set; }        

        public MechanicData() { }

        public MechanicData(string name, string surname, string pesel, int[] repairsIds)
        {
            this.name = name;
            this.surname = surname;
            this.repairsIds = repairsIds;
            this.pesel = pesel;
        }

        public override string ToString()
        {
            string reapirsArray = "";
            if (repairsIds != null)
            {
                foreach (int id in repairsIds)
                {
                    reapirsArray += id + " ";
                }

            }

            return "Name: " + this.name + " Surname: " + this.surname + " Pesel: " + this.pesel + " RepairsIds: " + reapirsArray;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (obj.GetType() != this.GetType()) return false;
            MechanicData other = (MechanicData)obj;
            return (this.pesel == other.pesel);
        }
    }
}
