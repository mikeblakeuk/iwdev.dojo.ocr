using System.Linq;

namespace IwDev.Dojo.Ocr.Tests
{
    public class AccountValidator
    {
        //(d1+2*d2+3*d3 +..+9*d9) mod 11 = 0
        public bool IsValid(string number)
        {
            // Could guess the number by padding the front with zeros
            if (number == null)
                return false;

            var ints = new int[number.Length];
            for (int i = 0; i < number.Length; i++)
            {
                int result;
                if (!int.TryParse(number[i].ToString(), out result))
                    return false;
                ints[i] = result;
            }
            return IsValid(ints);
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