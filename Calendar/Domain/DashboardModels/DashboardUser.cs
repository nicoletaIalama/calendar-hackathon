namespace Calendar.Domain.DashboardModels
{
    public class User
    {
        public int UserID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string Initials { get; set; } = string.Empty;
    }
}
