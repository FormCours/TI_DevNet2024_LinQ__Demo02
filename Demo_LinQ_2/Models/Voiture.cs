namespace Demo_LinQ_2.Models
{
    public class Voiture : Vehicule
    {
        public Voiture(int id, string modele, string marque, CarburantEnum carburant, int nbPorte, bool decapotable = false) : base(id, modele, marque)
        {
            Carburant = carburant;
            NbPorte = nbPorte;
            Decapotable = decapotable;
        }

        public enum CarburantEnum
        {
            Essence,
            Diesel,
            Electrique,
            Hybride
        }

        public CarburantEnum Carburant { get; set; }
        public int NbPorte { get; set; }
        public bool Decapotable { get; set; }
    }
}
