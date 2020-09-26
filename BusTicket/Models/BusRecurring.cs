namespace BusTicket.Models
{
    public class BusRecurring
    {
        public int ID { get; set; }

        public int WeekDay { get; set; }

        public int BusID { get; set; }
        public Bus Bus { get; set; }
    }
}