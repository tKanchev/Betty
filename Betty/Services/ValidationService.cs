using Betty.Constants;
using System.Linq;

namespace Betty.Services
{
    internal class ValidationService
    {
        internal static bool HasSufficientFunds(int amount)
        {
            int balance = WalletService.GetBalanceInCents();

            if (amount > balance || amount < 0)
            {
                return false;
            }

            return true;
        }

        internal static bool IsInputValid(string rawInput)
        {
            if (IsEmptyInput(rawInput))
            {
                return false;
            }

            string[] inputSplitted = rawInput.Trim().Split(' ');
            if (inputSplitted.Length != 2)
            {
                return false;
            }

            string operation = inputSplitted[0];
            string amount = inputSplitted[1];

            string[] validOperations = new string[]
            {
                OperationConstants.Bet,
                OperationConstants.Deposit,
                OperationConstants.Withdrawal
            };

            if (!validOperations.Any(x => x == operation))
            {
                return false;
            }

            if (!IsAmountValid(amount))
            {
                return false;
            }

            return true;
        }

        private static bool IsEmptyInput(string rawInput)
            => string.IsNullOrEmpty(rawInput);

        private static bool IsAmountValid(string amount)
            => decimal.TryParse(amount, out _);
    }
}
