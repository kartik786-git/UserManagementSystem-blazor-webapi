namespace BlazorAppSecure.Model
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public List<string> Roles { get; set; } = new List<string>();
    }
}
