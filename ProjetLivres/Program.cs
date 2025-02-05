using Ex4._2_Livres_Solution;

List<Livre> bibliotheque = new List<Livre>();


InitialiserLivres();
MenuPrincipal();


void InitialiserLivres()
{
    bibliotheque.Add(new Livre("1234567890", "C# Avancé", "Jean Dupont", 25.99f, 350, 2020));
    bibliotheque.Add(new Livre("2345678901", "Python Facile", "Alice Martin", 19.99f, 250, 2018));
    bibliotheque.Add(new Livre("3456789012", "Java en Action", "Bob Durand", 29.99f, 400, 2022));
}

void MenuPrincipal()
{
    while (true)
    {
        Console.WriteLine("\n--- Menu Bibliothèque ---");
        Console.WriteLine("1. Ajouter un livre");
        Console.WriteLine("2. Modifier un livre");
        Console.WriteLine("3. Supprimer un livre");
        Console.WriteLine("4. Rechercher un livre");
        Console.WriteLine("5. Sauvegarder la liste");
        Console.WriteLine("6. Quitter");
        Console.Write("Votre choix : ");

        string choix = Console.ReadLine();
        switch (choix)
        {
            case "1": AjouterLivre(); break;
            case "2": ModifierLivre(); break;
            case "3": SupprimerLivre(); break;
            case "4": RechercherLivre(); break;
            case "5": SauvegarderCSV(); break;
            case "6": return;
            default: Console.WriteLine("Choix invalide, réessayez."); break;
        }
    }
}

void AjouterLivre()
{ 
    try
    {
        Console.Write("ISBN : ");
        string isbn = Console.ReadLine();
        Console.Write("Titre : ");
        string titre = Console.ReadLine();
        Console.Write("Auteur : ");
        string auteur = Console.ReadLine();
        float prix = -1;
        do {
            try
            {
                Console.Write("Prix : ");
                prix = float.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Le prix doit être un nombre.");
            }
        } while (prix == -1);
        Console.Write("Nombre de pages : ");
        int pages = int.Parse(Console.ReadLine());
        Console.Write("Année de publication : ");
        int annee = int.Parse(Console.ReadLine());

        bibliotheque.Add(new Livre(isbn, titre, auteur, prix, pages, annee));
        Console.WriteLine("Livre ajouté avec succès !");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erreur : {ex.Message}");
    }
}

void ModifierLivre()
{
    Console.Write("Entrez l'ISBN du livre à modifier : ");
    string isbn = Console.ReadLine();
    Livre livre = bibliotheque.FirstOrDefault(l => l.ISBN == isbn);

    if (livre != null)
    {
        Console.Write("Nouveau titre : ");
        livre.Titre = Console.ReadLine();
        Console.Write("Nouvel auteur : ");
        livre.Auteur = Console.ReadLine();
        Console.Write("Nouveau prix : ");
        livre.Prix = float.Parse(Console.ReadLine());
        Console.Write("Nouveau nombre de pages : ");
        livre.NombrePages = int.Parse(Console.ReadLine());
        Console.Write("Nouvelle année de publication : ");
        livre.AnneePublication = int.Parse(Console.ReadLine());

        Console.WriteLine("Livre modifié avec succès !");
    }
    else
    {
        Console.WriteLine("Livre non trouvé.");
    }
}

void SupprimerLivre()
{
    /*
    Console.Write("Entrez l'ISBN du livre à supprimer : ");
    string isbn = Console.ReadLine();
    Livre livre = bibliotheque.FirstOrDefault(l => l.ISBN == isbn);
    */

    Livre livre = RechercherLivre()[0];
    Console.WriteLine("Voulez-vous vraiment suppprimer " + livre.Titre + "(o/n)");
    string choix = Console.ReadLine();
    if (livre != null && choix == "o")
    {
        bibliotheque.Remove(livre);
        Console.WriteLine("Livre supprimé avec succès !");
    }
    else
    {
        Console.WriteLine("Livre non supprimé.");
    }
}

List<Livre> RechercherLivre()
{
    Console.Write("Rechercher par (auteur/titre/prix) : ");
    string critere = Console.ReadLine().ToLower();
    List<Livre> resultats = new List<Livre>();

    //TODO : Corriger la fonction de recherche

    switch (critere)
    {
        case "auteur":
            Console.Write("Nom de l'auteur : ");
            string auteur = Console.ReadLine().ToLower();
            resultats = bibliotheque.FindAll(l => l.Auteur.ToLower().Contains(auteur));
            break;
        case "titre":
            Console.Write("Titre du livre : ");
            string titre = Console.ReadLine().ToLower();
            foreach(Livre livre in bibliotheque)
            {
                if (livre.Titre.ToLower().Contains(titre))
                {
                    resultats.Add(livre);
                }
            }
            break;
        case "prix":
            Console.Write("Prix minimum : ");
            float prixMin = float.Parse(Console.ReadLine());
            Console.Write("Prix maximum : ");
            float prixMax = float.Parse(Console.ReadLine());
            foreach(Livre l in bibliotheque)
            {
                if (l.Prix >= prixMin && l.Prix <= prixMax)
                    resultats.Add(l);
            }
            break;
        default:
            Console.WriteLine("Critère invalide.");
            return null;
    }

    Console.WriteLine("\nRésultats :");
    foreach (var livre in resultats)
    {
        Console.WriteLine(livre);
    }
    return resultats;
}

void SauvegarderCSV()
{
    Console.Write("Nom du fichier (ex: livres.csv) : ");
    string fichier = Console.ReadLine();

    using (StreamWriter writer = new StreamWriter(fichier))
    {
        writer.WriteLine("ISBN;Titre;Auteur;Prix;Pages;AnneePublication");
        foreach (var livre in bibliotheque)
        {
            writer.WriteLine($"{livre.ISBN};{livre.Titre};{livre.Auteur};{livre.Prix};{livre.NombrePages};{livre.AnneePublication}");
        }
    }

    Console.WriteLine("Liste sauvegardée !");
}
