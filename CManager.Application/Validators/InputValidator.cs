
/*
  A method to validate user input in the Console app. Uses "string FieldName" as an input parameter.
  By using this as an input parameter, i don't need to declare a variable before calling the method. 
  "FieldName" automatically declares a variable that can be used within the method.
*/
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
