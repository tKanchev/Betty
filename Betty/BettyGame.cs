using Betty.Constants;
using Betty.Models;
using Betty.Models.Request;
using Betty.Models.Response;
using Betty.Services;
using System;

using static Betty.Constants.MessageConstants;

namespace Betty
{
    internal static class BettyGame
    {
        internal static void Start()
        {
            while (true)
            {
                Console.WriteLine(SubmitActionMessage);
                
                string rawInput = Console.ReadLine();

                if (rawInput.Trim() == OperationConstants.Exit)
                {
                    Console.WriteLine(ExitMessage);
                    break;
                }

                if (!ValidationService.IsInputValid(rawInput))
                {
                    Console.WriteLine(InvalidInputMessage);
                    continue;
                }
                string[] inputSplitted = rawInput.Split(' ');

                Request request = new(inputSplitted[0], inputSplitted[1]);           

                switch (request.Operation)
                {
                    case OperationConstants.Bet:
                        Play(request.AmountInCents);
                        break;
                    case OperationConstants.Deposit:
                        Deposit(request.AmountInCents);
                        break;
                    case OperationConstants.Withdrawal:
                        Withdrawal(request.AmountInCents);
                        break;
                    default:
                        throw new NotImplementedException();
                }

                Console.WriteLine();
            }
        }

        private static void Play(int amount)
        {
            BetOperationResult result = GamingService.PlaceBet(amount);

            if (result.IsSucces)
            {
                if (result.WinAmount > 0)
                {
                    Console.WriteLine(string.Format(WinMessage, FormatAmountToDollars(result.WinAmount), result.NewBalance));
                }
                else
                {
                    Console.WriteLine(string.Format(LossMessage, result.NewBalance));
                }
            }
            else
            {
                Console.WriteLine(string.Format(NotEnoughMoneyMessage, result.NewBalance));
            }
        }

        private static void Deposit(int amount)
        {
            WalletOperationResult depositresult = WalletService.Deposit(amount);

            if (depositresult.IsSucces)
            {
                Console.WriteLine(string.Format(SuccessfulDepositMessage, FormatAmountToDollars(amount), depositresult.NewBalance));
            }
            else
            {
                Console.WriteLine(string.Format(UnSuccessfulDepositMessage, FormatAmountToDollars(amount), depositresult.NewBalance));
            }
        }

        private static void Withdrawal(int amount)
        {
            WalletOperationResult withdrawalresult = WalletService.Withdrawal(amount);

            if (withdrawalresult.IsSucces)
            {
                Console.WriteLine(string.Format(SuccessfulWithdrawalMessage, FormatAmountToDollars(amount), withdrawalresult.NewBalance));
            }
            else
            {
                Console.WriteLine(string.Format(UnSuccessfulWithdrawalMessage, FormatAmountToDollars(amount), withdrawalresult.NewBalance));
            }
        }

        private static decimal FormatAmountToDollars(int amount)
            => Math.Round((decimal)amount, 2) / 100;
    }
}
