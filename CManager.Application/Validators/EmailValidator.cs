using CManager.Infrastructure.Repositories;
using System.Text.RegularExpressions;

namespace CManager.Business.Validators;

public class EmailValidator
{
    public static string ValidateEmail(string fieldName)
    {
        CustomerRepository customerRepository = new CustomerRepository();

        while (true)
        {
            var customers = customerRepository.GetAllCustomers();

            Console.Write($"{fieldName}");
            var input = Console.ReadLine()!;

            var exists = customers.FirstOrDefault(c => c.Email == input);

            if (exists != null)
            {
                string answer = "";
                do
                {
                    Console.Clear();
                    Console.WriteLine("");
                    Console.WriteLine($"Customer with email address: {input} , already exists!");
                    Console.WriteLine("");
                    Console.WriteLine("Do you want to continue with this email address or enter a different email address?");
                    Console.Write("Press (Y) to continue or (N) to enter a different email address:");
                    answer = Console.ReadLine().ToLower();

                    if (answer == "n")
                    {
                        Console.Clear();
                        Console.WriteLine("Please enter another Email.");
                        Console.Write($"{fieldName}");
                        input = Console.ReadLine()!;
                        break;
                    }

                    else if (answer == "y")
                    {
                        Console.Clear();
                        Console.WriteLine("Please continue!");
                        Console.WriteLine("");
                        break;
                    }
                    else if (answer != "y" || answer != "n")
                    {
                        Console.WriteLine("You must enter (Y) or (N)");
                        Console.ReadKey();
                    }

                }while (answer != "y" || answer != "n");
            }

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
