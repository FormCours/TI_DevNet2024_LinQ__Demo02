namespace Demo_LinQ_2.Models
{
    public abstract class Vehicule
    {
        protected Vehicule(int id, string modele, string marque)
        {
            Id = id;
            Modele = modele;
            Marque = marque;
        }

        public int Id { get; set; }
        public string Modele { get; set; }
        public string Marque { get; set; }


    }
}
