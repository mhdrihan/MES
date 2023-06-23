namespace MES.Models
{
    public class UserModel
    {
        public List<user> UserList { get; set; }
    }

    public class user
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string role { get; set; }
    }
}
