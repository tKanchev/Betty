using Betty.Models.Response;
using System;

namespace Betty.Services
{
    internal class GamingService
    {
        internal static BetOperationResult PlaceBet(int betAmount)
        {
            if (!ValidationService.HasSufficientFunds(betAmount))
            {
                return new()
                {
                    IsSucces = false,
                    NewBalance = WalletService.GetBalanceInDollars(),
                };
            }

            StatisticsService.IncreaseBetCount();

            double lossPercentage = StatisticsService.GetLossPercentage();
            Random random = new();
            bool isWin = random.Next(0, 100) % 2 == 0;

            if (isWin || lossPercentage > 50)
            {
                int winAmount = PlaceWin(betAmount);

                if (winAmount == 0)
                {
                    WalletService.UpdateBalance(betAmount);
                    StatisticsService.IncreaseLossCount();
                }

                return new()
                {
                    WinAmount = winAmount,
                    NewBalance = WalletService.GetBalanceInDollars(),
                    IsSucces = true
                };
            }
            else
            {
                WalletService.UpdateBalance(betAmount);
                StatisticsService.IncreaseLossCount();

                return new()
                {
                    WinAmount = 0,
                    NewBalance = WalletService.GetBalanceInDollars(),
                    IsSucces = true
                };
            }
        }

        private static int PlaceWin(int betAmount)
        {
            double normalWinsPercentageAchieved = StatisticsService
                .GetNormalWinPercentageReached();

            double bigWinsPercentageAcheived = StatisticsService
                .GetTotalBigWinPercentageReached();

            if (normalWinsPercentageAchieved >= 100 && bigWinsPercentageAcheived >= 100)
            {
                return 0;
            }

            if (normalWinsPercentageAchieved == 0
                && normalWinsPercentageAchieved == bigWinsPercentageAcheived)
            {
                bool isBigWin = new Random().Next(0, 100) % 2 == 0;

                return isBigWin
                    ? PlaceBigWin(betAmount)
                    : PlaceNormalWin(betAmount);
            }

            if (bigWinsPercentageAcheived > 100)
            {
                return PlaceNormalWin(betAmount);
            }
            else if (bigWinsPercentageAcheived > normalWinsPercentageAchieved)
            {
                return PlaceNormalWin(betAmount);
            }
            else if (normalWinsPercentageAchieved > bigWinsPercentageAcheived)
            {
                return PlaceBigWin(betAmount);
            }
            else
            {
                return PlaceNormalWin(betAmount);
            }
        }

        private static int PlaceNormalWin(int betAmount)
        {
            int maxWinAmount = betAmount * 2;
            Random random = new();

            int winAmount = random.Next(1, maxWinAmount);

            int balanceUpdateAmount = betAmount - winAmount;

            WalletService.UpdateBalance(balanceUpdateAmount);

            StatisticsService.IncreaseNormalWinCount();

            return winAmount;
        }

        private static int PlaceBigWin(int betAmount)
        {
            int minWinAmount = betAmount * 2;
            int maxWinAmount = betAmount * 10;
            Random random = new();

            int winAmount = random.Next(minWinAmount, maxWinAmount);

            int balanceUpdateAmount = betAmount - winAmount;

            WalletService.UpdateBalance(balanceUpdateAmount);

            StatisticsService.IncreaseBigWinCount();

            return winAmount;
        }
    }
}
