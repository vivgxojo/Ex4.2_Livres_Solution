using Ex4._2_Livres_Solution;

namespace TestProjectLivres
{
    public class TestsLivres
    {
        [Fact]
        public void Constructeur_Valide_AssigneValeursCorrectement()
        {
            // Arrange
            string isbn = "1234567890";
            string titre = "C# Avancé";
            string auteur = "Jean Dupont";
            float prix = 25.99f;

            // Act
            Livre livre = new Livre(isbn, titre, auteur, prix);

            // Assert
            Assert.Equal(isbn, livre.ISBN);
            Assert.Equal(titre, livre.Titre);
            Assert.Equal(auteur, livre.Auteur);
            Assert.Equal(prix, livre.Prix);
        }

        // Tests de propriétés valides

        Livre livre = new Livre("1234567890", "Titre", "Auteur", 10, 100, 2015);

        [Fact]
        public void ISBN_Valide_AssigneCorrectement()
        {
            string isbn = "978-3-16-148410-0";
            livre.ISBN = isbn;
            Assert.Equal(isbn, livre.ISBN);
        }

        // Tests avec jeux d'essais

        [Theory]
        [InlineData(0.01f)]
        [InlineData(999.99f)]
        public void Prix_Valide_AssigneCorrectement(float prixValide)
        {
            livre.Prix = prixValide;
            Assert.Equal(prixValide, livre.Prix);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(1000)]
        public void NombrePages_Valide_AssigneCorrectement(int nombrePagesValide)
        {
            livre.NombrePages = nombrePagesValide;
            Assert.Equal(nombrePagesValide, livre.NombrePages);
        }

        [Theory]
        [InlineData(1450)]  // Premier livre imprimé
        [InlineData(2025)]
        public void AnneePublication_Valide_AssigneCorrectement(int anneePublicationValide)
        {
            livre.AnneePublication = anneePublicationValide;
            Assert.Equal(anneePublicationValide, livre.AnneePublication);
        }

        // Tests d'exception

        [Theory]
        [InlineData("12345678900000")]      // Trop long
        [InlineData("12345-1234")]          // Trop court
        [InlineData("abcdefghij")]          // Contient des lettres
        public void ISBN_Invalide_DeclencheException(string isbnInvalide)
        {
            // Vérifier que le constructeur utilise la validation
            Assert.Throws<ArgumentException>(() => new Livre(isbnInvalide, "Titre", "Auteur", 10));
        }

        [Fact]
        public void Prix_Negatif_DeclencheException()
        {
            // Vérifier que le set fait la validation.
            Assert.Throws<ArgumentException>(() => livre.Prix = -0.01f);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-50)]
        public void NombrePages_Invalide_DeclencheException(int nombrePagesInvalide)
        {
            Assert.Throws<ArgumentException>(() => livre.NombrePages = nombrePagesInvalide);
        }

        [Fact]
        public void ToString_RetourneFormatAttendu()
        {
            // Arrange
            Livre livre = new Livre("1234567890", "C# Avancé", "Jean Dupont", 25.99f, 30, 2025);

            // Act
            string resultat = livre.ToString();

            // Assert
            Assert.Equal("Livre: C# Avancé | Auteur: Jean Dupont | ISBN: 1234567890 | Prix: 25,99$ | Pages: 350 | Année: 2025", resultat);
        }
    }
}