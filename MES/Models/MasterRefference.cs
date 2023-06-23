namespace MES.Models
{
    public class MasterRefference
    {
        public List<master_refference> RefferenceList { get; set; }
    }
    public class master_refference
    {
        public int Refference_ID { get; set; }

        public string Refference_Name { get; set; }

        public string Refference_Description { get; set; }

        public DateTime Last_Modify { get; set; }

        public string Transact_By { get; set; }
    }
}
