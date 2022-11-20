namespace EvidenceHodinWeb.Models
{
    public class Uzivatel
    {
        public int UzivatelId { get; set; }
        public string? OwnerID { get; set; } // user ID from AspNetUser table.
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public UserState Status { get; set; }

    }

    public enum UserState
    {
        Active, // aktivovan pro pouzivani
        Inactive, // jen se registroval, jeste nema pristup
        Retired // odesel pryc
    }
}
