using System.Text.RegularExpressions;

namespace CManager.Application.Validators;

public class EmailValidator
{
    public static string ValidateEmail(string fieldName)
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

            var pattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
            var result = Regex.IsMatch(input, pattern);

            if (result)
                return input;

            Console.WriteLine("Invalid email..");
            Console.WriteLine("Use name@example.com");
            Console.WriteLine("");
            Console.WriteLine("Press any key to continue....");
            Console.ReadKey();
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("");
        }
    }


}
