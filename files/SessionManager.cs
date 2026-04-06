namespace CrudApp
{
    /// <summary>
    /// Houdt bij welke gebruiker momenteel is ingelogd.
    /// </summary>
    public static class SessionManager
    {
        /// <summary>
        /// De gebruikersnaam van de ingelogde gebruiker.
        /// Is null als niemand ingelogd is.
        /// </summary>
        public static string? HuidigeGebruiker { get; set; }

        /// <summary>
        /// Logt de huidige gebruiker uit.
        /// </summary>
        public static void Uitloggen()
        {
            HuidigeGebruiker = null;
        }
    }
}
