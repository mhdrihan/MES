namespace MES.Models
{
    public class MasterOrder
    {
        public List<masterorder> OrderList { get; set; }
    }

    public class masterorder
    {
        public string? Work_Order { get; set; }

        public string? Refference_Name { get; set; }

        public string? Work_Plan { get; set; }

        public int? Qty_Order { get; set; }

        public int? Qty_Launching { get; set; }

        public string? Status_Order { get; set; }

        public DateTime? Date_Order { get; set; }

        public DateTime? Date_Complete { get; set; }

        public string? Transact_By { get; set; }

        public string? Username { get; set; }

        public string? WO_Comment { get; set; }

        public int? Priority_WO { get; set; }

        public int? Station_ID { get; set; }

        public int? Station_Suffix { get; set; }

    }
}
