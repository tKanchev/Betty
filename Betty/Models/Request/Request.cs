using Betty.Extensions;

namespace Betty.Models.Request
{
    internal class Request
    {
        //private readonly string[] ValidOperations = new string[] { "bet", "deposit", "withdrawal" };
        internal string Operation { get; set; }

        internal int AmountInCents { get; set; }

        //internal bool IsValid { get; set; }

        public Request(string operation, string amount)
        {
            Operation = operation;
            AmountInCents = (int)decimal.Parse(amount).ToCentsValue();

            //if (IsEmptyInput(rawInput))
            //{
            //    IsValid = false;
            //    return;
            //}

            //string[] inputSplitted = rawInput.Trim().Split(' ');

            //if (!IsValidSplittedInput(inputSplitted))
            //{
            //    IsValid = false;
            //    return;
            //}

            //try
            //{
            //    if (IsValidOperation(inputSplitted[0]))
            //    {
            //        Operation = inputSplitted[0];
            //        AmountInCents = (int)decimal.Parse(inputSplitted[1]).ToCentsValue();
            //        IsValid = true;
            //    }
            //    else
            //    {
            //        IsValid = false;
            //        return;
            //    }
            //}
            //catch (Exception)
            //{
            //    IsValid = false;
            //}
        }

        //private bool IsValidOperation(string operation)
        //    => ValidOperations.Any(x => x == operation);

        //private bool IsEmptyInput(string rawInput)
        //    => string.IsNullOrEmpty(rawInput);

        //private bool IsValidSplittedInput(string[] inputSplitted)
        //    => inputSplitted.Length == 2;
    }
}
