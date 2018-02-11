namespace TradeCaretaker.Messages
{
    public class BackFillStageCompleted
    {
        public string Asset { get; set; }
        public int Current { get; set; }
        public int TotalNumber { get; set; }
    }
}