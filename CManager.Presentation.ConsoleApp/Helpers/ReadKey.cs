
// Created a method so that i don't have to repeat the same code every time i want the program to wait for a "key input" from the user to continue. (In the menu controller)

namespace CManager.Presentation.ConsoleApp.Helpers;

public static class ReadKey
{
    public static void SystemHolder()
    {
        Console.WriteLine("");
        Console.WriteLine("Press any key to continue....");
        Console.ReadKey();
    }
}
