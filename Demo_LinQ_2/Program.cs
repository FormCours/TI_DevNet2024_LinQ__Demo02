using Demo_LinQ_2.Models;

IEnumerable<Vehicule> vehicules = [
    new Voiture(1, "Golf", "VW", Voiture.CarburantEnum.Diesel, 5),
    new Voiture(2, "2101", "Lada", Voiture.CarburantEnum.Essence, 5, true),
    new Voiture(3, "Logan", "Dacia", Voiture.CarburantEnum.Hybride, 4),
    new Avion(4, "Concorde", "Sud-Aviation", 100, false),
    new Voiture(5, "Riva", "Lada", Voiture.CarburantEnum.Diesel, 4),
    new Voiture(6, "1310", "Dacia", Voiture.CarburantEnum.Essence, 5),
    new Avion(7, "A320", "Airbus", 150, false),
    new Voiture(8, "E-tron", "Audi", Voiture.CarburantEnum.Electrique, 3),
    new Voiture(10, "Niva SUV", "Lada", Voiture.CarburantEnum.Essence, 2),
    new Avion(11, "A300-600ST", "Airbus", 265, true)
];

IEnumerable<Voiture> voitures = [
    new Voiture(1, "Golf", "VW", Voiture.CarburantEnum.Diesel, 5),
    new Voiture(2, "2101", "Lada", Voiture.CarburantEnum.Essence, 5, true),
    new Voiture(3, "Logan", "Dacia", Voiture.CarburantEnum.Hybride, 4),
    new Voiture(5, "Riva", "Lada", Voiture.CarburantEnum.Diesel, 4),
    new Voiture(6, "1310", "Dacia", Voiture.CarburantEnum.Essence, 5),
    new Voiture(8, "E-tron", "Audi", Voiture.CarburantEnum.Electrique, 3),
    new Voiture(10, "Niva SUV", "Lada", Voiture.CarburantEnum.Essence, 2),
];


// Cast VS OfType
// **************

// - Cast
// Le code plante si le type est incompatible !
{
    // IEnumerable<Avion> result1 = vehicules.Cast<Avion>();
    // IEnumerable<Avion> result2 = from Avion a in vehicules
    //                              select a;

    // On peut le combiné à un where
    IEnumerable<Avion> result1 = vehicules.Where(v => v is Avion).Cast<Avion>();

    foreach (Avion a in result1)
    {
        Console.WriteLine(a.Id);
    }
}

// - OfType
// Cast uniquement les éléments compatible (Ne plante donc pas ♥)
{
    IEnumerable<Voiture> result1 = vehicules.OfType<Voiture>();

    foreach (Voiture a in result1)
    {
        Console.WriteLine(a.Id);
    }
}


// Where
// *****
{
    IEnumerable<Voiture> result1 = voitures.Where(v => v.Carburant == Voiture.CarburantEnum.Diesel && v.NbPorte >= 4);

    IEnumerable<Voiture> result3 = from v in voitures
                                   where v.Carburant == Voiture.CarburantEnum.Diesel && v.NbPorte >= 4
                                   select v;

    // La clause where n'est pas obligatoirement unique
    IEnumerable<Voiture> result2 = voitures.Where(v => v.Carburant == Voiture.CarburantEnum.Diesel)
                                           .Where(v => v.NbPorte >= 4);

    IEnumerable<Voiture> result4 = from v in voitures
                                   where v.Carburant == Voiture.CarburantEnum.Diesel
                                   where v.NbPorte >= 4
                                   select v;
}

// Select
{
    IEnumerable<string> result1 = vehicules.OfType<Voiture>()
                                           .Where(v => v.NbPorte == 3)
                                           .Select(v => $"{v.Marque} {v.Modele}");

    IEnumerable<string> result2 = from Voiture v in vehicules.OfType<Voiture>()
                                  where v.NbPorte == 3
                                  select $"{v.Marque} {v.Modele}";

    // Rappel => Utiliser le mot clef "var" quand le type est annonyme !
    var result3 = vehicules.OfType<Voiture>()
                           .Where(v => v.Decapotable)
                           .Select(v => new { Nom = $"{v.Marque} {v.Modele}", NbPorte = v.NbPorte });

    var result4 = from Voiture v in vehicules.OfType<Voiture>()
                  where v.Decapotable
                  select new { Nom = $"{v.Marque} {v.Modele}", NbPorte = v.NbPorte };

    foreach (var item in result4)
    {
        Console.WriteLine(item.Nom);
    }
}

// Single/SingleOrDefault
// First/FirstOrDefault
// Last/LastOrDefault
{
    // La méthode « Single » plante s'il y a plus d'un resultat, contrairement aux « First » et « Last »
    // Les méthodes précédé de "...OrDefault" ne plante pas quand il n'y a pas de resultat !
    Voiture result1 = voitures.Where(v => v.Marque == "Audi").Single();
    Voiture result2 = voitures.Single(v => v.Marque == "VW");
    Voiture? result3 = voitures.LastOrDefault(v => v.NbPorte > 6);
}
Console.WriteLine();

// Order
{
    IEnumerable<Voiture> result1 = vehicules.OfType<Voiture>()
                                            .OrderBy(v => v.Carburant.ToString().ToLower());

    Console.WriteLine("Order 1 : ");
    foreach (var item in result1)
    {
        Console.WriteLine(" - " + item.Marque + " " + item.Modele + " " + item.Carburant);
    }

    IEnumerable<Voiture> result2 = vehicules.OfType<Voiture>()
                                            .OrderByDescending(v => v.Marque)
                                            .ThenBy(v => v.Modele);

    IEnumerable<Voiture> result3 = from v in vehicules.OfType<Voiture>()
                                   orderby v.Marque descending, v.Modele
                                   select v;

    Console.WriteLine("Order 2 : ");
    foreach (var item in result2)
    {
        Console.WriteLine(" - " + item.Marque + " " + item.Modele + " " + item.Carburant);
    }
}



// Count
{
    int total1 = vehicules.OfType<Voiture>()
                          .Count(v => v.NbPorte > 3);

    int total2 = (from voiture in vehicules.OfType<Voiture>()
                  where voiture.NbPorte > 3
                  select voiture).Count();

    int total3 = (from voiture in vehicules.OfType<Voiture>() select voiture).Count(v => v.NbPorte > 3);

    Console.WriteLine($"Total 1 : {total1}");
    Console.WriteLine($"Total 2 : {total2}");
    Console.WriteLine($"Total 3 : {total3}");


    int nbPlaceAll1 = vehicules.OfType<Avion>()
                           .Sum(a => a.NbPlace);

    int nbPlaceAll2 = vehicules.OfType<Avion>()
                            .Select(a => a.NbPlace)
                            .Sum();

    Console.WriteLine($"PlaceAll 1 : {nbPlaceAll1}");
    Console.WriteLine($"PlaceAll 2 : {nbPlaceAll2}");


    double nbPlaceAvg1 = vehicules.OfType<Avion>()
                                  .Average(a => a.NbPlace);

    double nbPlaceAvg2 = vehicules.OfType<Avion>()
                                  .Select(a => a.NbPlace)
                                  .Average();

    Console.WriteLine($"PlaceAvg 1 : {nbPlaceAvg1}");
    Console.WriteLine($"PlaceAvg 2 : {nbPlaceAvg2}");
}
Console.WriteLine();


// Group By
{
    // -> La liste des voitures groupé par marque

    //IEnumerable<IGrouping<string, Voiture>> grp1 = voitures.GroupBy(v => v.Marque);

    IEnumerable<IGrouping<string, Voiture>> grp1 = from v in voitures
                                                   group v by v.Marque;

    foreach(IGrouping<string, Voiture> element in grp1)
    {
        Console.WriteLine(" - " + element.Key);

        foreach(Voiture v in element)
        {
            Console.WriteLine($"{v.Modele} {v.Carburant}");
        }
    }
    Console.WriteLine();

    //---------------------------------------------------------------------------

    // La liste des voiture avec les informations suivantes : Nom (Marque + Modele) et le nombre porte.
    // Le tout groupé par type de carburant.

    var grp2 = voitures.Select(v => new
                        {
                            Nom = $"{v.Marque} {v.Modele}",
                            Porte = v.NbPorte,
                            Carburant = v.Carburant,
                        })
                    .GroupBy(v => v.Carburant);

    foreach(var element in grp2)
    {
        Console.WriteLine(" - " + element.Key);

        foreach(var voiture in element)
        {
            Console.WriteLine(voiture);
        }
    }
    Console.WriteLine();

    var grp3 = voitures.GroupBy(v => v.Carburant)
                       .Select(g => new
                       {
                           Clef = g.Key,
                           Valeurs = g.Select(el => new {
                               Nom = $"{el.Marque} {el.Modele}",
                               Porte = el.NbPorte
                           })
                       });

    foreach (var element in grp3)
    {
        Console.WriteLine(" - " + element.Clef);

        foreach (var voiture in element.Valeurs)
        {
            Console.WriteLine(voiture);
        }
    }
    Console.WriteLine();


    var grp4 = from v in voitures
               group new { Nom = $"{v.Marque} {v.Modele}", Porte = v.NbPorte } by v.Carburant;
               //    ↑ Les données qui seront présente dans le groupe

    foreach (var element in grp4)
    {
        Console.WriteLine(" - " + element.Key);

        foreach (var v in element)
        {
            Console.WriteLine(v);
        }
    }

}

//##########################################################################################################
//##########################################################################################################
//##########################################################################################################

IEnumerable<Auteur> auteurs = [
    new Auteur(1, "Douglas", "Adams"),
    new Auteur(2, "Carl", "Barks"),
    new Auteur(3, "René", "Barjavel"),
    new Auteur(4, "Don", "Rosa"),
    new Auteur(5, "Clélia", "Rosso")
];

IEnumerable<Livre> livres = [
    new Livre(1, "Le Guide du voyageur galactique", 1978, 1),
    new Livre(2, "Donald et le trésor du pirate", 1974, 2),
    new Livre(3, "La Chasse au canard...", 1988, 4),
    new Livre(4, "Le chapeau rouge", 1957, 2),
    new Livre(5, "La nuit des temps", 1968, 3),
    new Livre(6, "Le Retour des trois Caballeros", 2000, 4),
    new Livre(7, "Un avion à réaction !", 1955, 2),
    new Livre(8, "Trois bons petits canards", 2000, 2),
    new Livre(9, "Ravage", 1943, 3),
    new Livre(10, "L'Enchanteur", 1984, 3),
    new Livre(11, "Un baraqué débusqué !", 1955, 2),
    new Livre(12, "Citrouille carabinée", 1988, 4)
];

{
    Console.WriteLine("Inner Join");
    // La liste des livres avec leur auteurs

    var join01 = from l in livres
                 join a in auteurs on l.AuteurId equals a.Id
                 select new
                 {
                     Titre = l.Titre,
                     Auteur = $"{a.Prenom} {a.Nom}"
                 };

    foreach (var item in join01)
    {
        Console.WriteLine(item);
    }
    Console.WriteLine();

    var join02 = livres.Join(
                            auteurs,            // Collection à joindre
                            l => l.AuteurId,    // La clef de la collection de base
                            a => a.Id,          // La clef de la collection jointe
                            (livre, auteur) => new
                            {
                                Titre = livre.Titre,
                                Auteur = $"{auteur.Prenom} {auteur.Nom}"
                            }
                        );

    foreach (var item in join02)
    {
        Console.WriteLine(item);
    }
    Console.WriteLine();
}

{
    Console.WriteLine("Left Join");
    // La liste des auteurs avec leurs oeuvres

    var join03 = auteurs.GroupJoin(
                            livres,           // La collection à joindre
                            a => a.Id,        // La clef dans la collection de base
                            l => l.AuteurId,  // La clef dans la collection de jointe,
                            (auteur, listLivre) => new
                            {
                                Nom = $"{auteur.Prenom} {auteur.Nom}",
                                Livres = listLivre.Select(l => new { l.Titre, l.Annee })
                            }
                        );

    foreach(var item in join03)
    {
        Console.WriteLine(" - " + item.Nom);

        foreach (var livre in item.Livres)
        {
            Console.WriteLine($"{livre.Titre} ({livre.Annee})");
        }

        if(item.Livres.Count() == 0)
        {
            Console.WriteLine("Pas encore de livre :o");
        }
    }
    Console.WriteLine();


    var join04 = from a in auteurs
                 join l in livres on a.Id equals l.AuteurId into listLivre
                 select new
                 {
                     Nom = $"{a.Prenom} {a.Nom}",
                     Livres = listLivre.Select(l => new { l.Titre, l.Annee })
                 };

    foreach (var item in join04)
    {
        Console.WriteLine(" - " + item.Nom);

        foreach (var livre in item.Livres)
        {
            Console.WriteLine($"{livre.Titre} ({livre.Annee})");
        }

        if (item.Livres.Count() == 0)
        {
            Console.WriteLine("Pas encore de livre :o");
        }
    }
}


{
    Console.WriteLine("Cross Join");
    var join05 = from l in livres
                 from a in auteurs
                 select new
                 {
                     Auteur = $"{a.Prenom} {a.Nom}",
                     Livre = l.Titre
                 };

    foreach (var item in join05)
    {
        Console.WriteLine(item);
    }
    Console.WriteLine();

    Console.Clear();
    Console.WriteLine("Cross Join + Where");
    var join06 = from l in livres
                 from a in auteurs
                 where a.Id == l.AuteurId   // Condition de la jointure
                 select new
                 {
                     Auteur = $"{a.Prenom} {a.Nom}",
                     Livre = l.Titre
                 };

    foreach (var item in join06)
    {
        Console.WriteLine(item);
    }
}