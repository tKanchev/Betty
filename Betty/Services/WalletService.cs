using Betty.Models;
using System;

namespace Betty.Services
{
    internal static class WalletService
    {
        internal static int Balance = 0;

        internal static int GetBalanceInCents()
            => Balance;

        internal static decimal GetBalanceInDollars()
            => Math.Round((decimal)Balance, 2) / 100;

        internal static int UpdateBalance(int amount)
        {
            Balance -= amount;

            return Balance;
        }

        internal static WalletOperationResult Deposit(int amount)
        {
            if (amount <= 0)
            {
                return new WalletOperationResult
                {
                    IsSucces = false,
                    NewBalance = GetBalanceInDollars()
                };
            }

            Balance += amount;

            return new WalletOperationResult
            {
                IsSucces = true,
                NewBalance = GetBalanceInDollars()
            };
        }

        internal static WalletOperationResult Withdrawal(int amount)
        {
            if (amount <= 0 || amount > Balance)
            {
                return new WalletOperationResult
                {
                    IsSucces = false,
                    NewBalance = GetBalanceInDollars()
                };
            }

            Balance -= amount;

            return new WalletOperationResult
            {
                IsSucces = true,
                NewBalance = GetBalanceInDollars()
            };
        }
    }
}
