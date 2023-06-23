namespace MES.Models
{
    public class MasterUser
    {
        public List<masteruser> MasterList { get; set; }
    }

    public class masteruser
    {
        public int ID_User { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public int Station_ID { get; set; }

        public int User_Level { get; set; }
    }
}
