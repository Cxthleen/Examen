using System.Collections.Generic;
using System.Linq;

namespace CrudApp
{
    /// <summary>
    /// Beheert de CRUD-operaties voor producten.
    /// In een echte app vervang je de List door een database (bijv. SQLite of SQL Server).
    /// </summary>
    public class ProductService
    {
        // In-memory lijst als vervanging voor een database
        private readonly List<Product> _producten = new()
        {
            new Product { Id = 1, Naam = "Laptop",    Prijs = 999.99 },
            new Product { Id = 2, Naam = "Muis",      Prijs = 24.95  },
            new Product { Id = 3, Naam = "Toetsenbord", Prijs = 49.99 }
        };

        private int _volgendeId = 4;

        // CREATE
        public void Toevoegen(Product product)
        {
            product.Id = _volgendeId++;
            _producten.Add(product);
        }

        // READ – alle producten
        public List<Product> AlleProducten() => _producten.ToList();

        // READ – één product op id
        public Product? ZoekOpId(int id) =>
            _producten.FirstOrDefault(p => p.Id == id);

        // UPDATE
        public bool Bijwerken(Product gewijzigd)
        {
            var bestaand = ZoekOpId(gewijzigd.Id);
            if (bestaand == null) return false;

            bestaand.Naam  = gewijzigd.Naam;
            bestaand.Prijs = gewijzigd.Prijs;
            return true;
        }

        // DELETE
        public bool Verwijderen(int id)
        {
            var product = ZoekOpId(id);
            if (product == null) return false;

            _producten.Remove(product);
            return true;
        }
    }
}
