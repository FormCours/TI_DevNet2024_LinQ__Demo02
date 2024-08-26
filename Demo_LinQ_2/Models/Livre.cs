namespace Demo_LinQ_2.Models
{
    public class Livre
    {
        public Livre(int id, string titre, int annee, int auteurId)
        {
            Id = id;
            Titre = titre;
            Annee = annee;
            AuteurId = auteurId;
        }

        public int Id { get; set; }
        public string Titre { get; set; }
        public int Annee { get; set; }

        public int AuteurId { get; set; }

        
    }
}
