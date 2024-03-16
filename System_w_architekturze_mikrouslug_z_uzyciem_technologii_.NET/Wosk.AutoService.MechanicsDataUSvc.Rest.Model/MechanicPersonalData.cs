namespace Wosk.AutoService.MechanicsDataUSvc.Rest.Model
{
    public class MechanicPersonalData
    {
        public string name { get; set; }
        public string surname { get; set; }

        public string pesel { get; set; }

        public MechanicPersonalData() { }

        public MechanicPersonalData(string name, string surname, string pesel)
        {
            this.name = name;
            this.surname = surname;
            this.pesel = pesel;
        }

        public override string ToString()
        {
            return "Name: " + this.name + " Surname: " + this.surname + " Pesel: " + this.pesel;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (obj.GetType() != this.GetType()) return false;
            MechanicPersonalData other = (MechanicPersonalData)obj;
            return (this.pesel == other.pesel);
        }
    }
}
