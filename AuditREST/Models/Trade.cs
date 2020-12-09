namespace AuditREST.Models
{
    public class Trade
    {
        public int TradeId { get; set; }
        public string Name { get; set; }

        public Trade()
        {
        }

        public Trade(string name)
        {
            Name = name;
        }
    }
}