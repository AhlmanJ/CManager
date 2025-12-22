using System.Text.RegularExpressions;

namespace CManager.Business.Validators;

public class PhoneNrValidator
{
    public static string ValidatePhoneNr(string fieldName)
    {
        while (true)
        {
            Console.Write($"{fieldName}");
            var input = Console.ReadLine()!;

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine($"{fieldName} is required. Press any key to try again...");
                Console.ReadKey();
                continue;
            }

            var pattern = @"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$";
            var result = Regex.IsMatch(input, pattern);

            if (result)
                return input;

            Console.WriteLine("Invalid phone number..");
            Console.WriteLine("Must be a valid phone number, with or without + at the beginning. ");
            Console.WriteLine("");
            Console.WriteLine("Press any key to continue....");
            Console.ReadKey();
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("");
        }
    }
}