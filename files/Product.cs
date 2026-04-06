namespace CrudApp
{
    /// <summary>
    /// Stelt een product voor in de applicatie.
    /// Dit is het datamodel — pas dit aan naar jouw eigen onderwerp.
    /// </summary>
    public class Product
    {
        public int Id { get; set; }
        public string Naam { get; set; } = string.Empty;
        public double Prijs { get; set; }

        // Handig voor weergave in de lijst
        public override string ToString() => $"{Id} - {Naam} (€{Prijs:F2})";
    }
}
