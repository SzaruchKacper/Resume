namespace Wosk.AutoService.MechanicsDataUSvc.Model
{
    public class Mechanic
    {
        public int MechanicID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public  string Pesel { get;  set; }
        public int[] RepairsIds { get; set; }

        public Mechanic()
        {

        }

        public Mechanic(int mechanicId, string name, string surname, int[] repairsIds, string pesel)
        {
            MechanicID = mechanicId;
            Name = name;
            Surname = surname;
            RepairsIds = repairsIds;
            Pesel = pesel;
        }

        public override string ToString()
        {
            string reapirsArray = "";
            if (RepairsIds != null)
            {
                foreach (int id in RepairsIds)
                {
                    reapirsArray += id + " ";
                }
                
            }
            
            return "Id: " + this.MechanicID + " Name: " + this.Name + " Surname: " + this.Surname + " Pesel: "+this.Pesel+" RepairsIds: " + reapirsArray;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (obj.GetType() != this.GetType()) return false;
            Mechanic other = (Mechanic) obj;
            return (this.Pesel == other.Pesel);
        }

    }
}