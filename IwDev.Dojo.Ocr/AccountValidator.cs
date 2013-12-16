using System.Collections.Generic;

namespace IwDev.Dojo.Ocr
{
    public class AccountValidator
    {
        public bool IsValid(string accountNumer)
        {
            var ints = new List<int>();
            foreach (var singleChar in accountNumer)
            {
                int result;
                if (!int.TryParse(singleChar.ToString(), out result))
                    return false;
                ints.Add(result);
            }
            return IsValid(ints.ToArray());
        }

        //(d1+2*d2+3*d3 +..+9*d9) mod 11 = 0
        // Peek at https://code.google.com/p/danoncodekatas/source/browse/trunk/KataBankOCR/KataBankOCR/CheckSumValidator.cs
        public bool IsValid(int[] number)
        {
            // Could guess the number by padding the front with zeros
            if (number.Length != 9)
                return false;

            var sum = 0;
            for (var pos = 1; pos <= 9; pos++)
            {
                sum += number[9 - pos] * pos;
            }
            return (sum % 11) == 0;

        }
    }
}