namespace IwDev.Dojo.Ocr.Tests
{
    public class AccountValidator
    {
        //(d1+2*d2+3*d3 +..+9*d9) mod 11 = 0
        public bool IsValid(string number)
        {
            // Could guess the number by padding the front with zeros
            if (number == null || number.Length != 9)
                return false;

            var total = 0;
            var offSet = 1;
            for (int i = number.Length - 1; i >= 1; i--)
            {
                int singleInt;
                if (!int.TryParse(number[i].ToString(), out singleInt))
                    return false;
            
                total *= singleInt + (offSet++);
            }
            total *= number[0];
            return total % 11 == 0;
        }
    }
}