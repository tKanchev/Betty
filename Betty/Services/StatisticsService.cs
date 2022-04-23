namespace Betty.Services
{
    internal class StatisticsService
    {
        private const double MaxNormalWinsPercentage = 40;
        private const double MaxBigWinsPercentage = 10;

        private static int TotalBetCount;
        private static int TotalNormalWinCount;
        private static int TotalBigWinCount;
        private static int TotalLossCount;

        internal static double GetLossPercentage()
            => TotalLossCount / (double)TotalBetCount * 100;

        internal static double GetNormalWinPercentageReached()
            => GetTotalNormalWinPercentage() / MaxNormalWinsPercentage * 100;

        internal static double GetTotalBigWinPercentageReached()
            => GetTotalBigWinPercentage() / MaxBigWinsPercentage * 100;

        internal static void IncreaseBetCount()
            => TotalBetCount++;

        internal static void IncreaseNormalWinCount()
            => TotalNormalWinCount++;

        internal static void IncreaseBigWinCount()
            => TotalBigWinCount++;

        internal static void IncreaseLossCount()
            => TotalLossCount++;

        private static double GetTotalNormalWinPercentage()
            => TotalNormalWinCount / (double)TotalBetCount * 100;

        private static double GetTotalBigWinPercentage()
            => TotalBigWinCount / (double)TotalBetCount * 100;
    }
}
