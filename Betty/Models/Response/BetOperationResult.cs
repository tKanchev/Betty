namespace Betty.Models.Response
{
    internal class BetOperationResult : OperationResult
    {
        internal int WinAmount { get; set; }

        internal decimal NewBalance { get; set; }
    }
}
