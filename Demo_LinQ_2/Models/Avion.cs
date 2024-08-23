namespace Demo_LinQ_2.Models
{
    public class Avion : Vehicule
    {
        public Avion(int id, string modele, string marque, int nbPlace, bool cargo) : base(id, modele, marque)
        {
            NbPlace = nbPlace;
            Cargo = cargo;
        }

        public int NbPlace { get; set; }
        public bool Cargo { get; set; }
    }
}
