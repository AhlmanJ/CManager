namespace CManager.Business.Validators;

public class InputValidator
{
    public static string ValidateInput(string FieldName) 
    {
        while (true) 
        {
            Console.Write($"{FieldName}");
            var input = Console.ReadLine()!.ToLower().Trim();

            if (!string.IsNullOrEmpty(input))
            {
                return input;
            }

            
            Console.WriteLine($"{FieldName} cannot be empty!");
            Console.WriteLine("");
            Console.WriteLine("Press any key to continue and try again.");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
