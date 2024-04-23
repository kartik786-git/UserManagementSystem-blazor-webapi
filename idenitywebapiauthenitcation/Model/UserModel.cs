namespace idenitywebapiauthenitcation.Model
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public List<string> Roles { get; set; }
    }
}
