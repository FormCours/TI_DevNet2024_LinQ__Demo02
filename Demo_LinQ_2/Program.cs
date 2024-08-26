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