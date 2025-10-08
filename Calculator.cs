double NumberInputChecked(ref bool Exit)
{
    bool success = false;
    while (!success)
    {
        string inp = Console.ReadLine();
        if (inp == "exit") { Exit = true; break; }
        bool isANumber = double.TryParse(inp, out double A);
        if (isANumber)
        {
            isANumber = true;
            return A;
        }
        else { Console.WriteLine($" '{inp}' is not a number!, reenter valid number to continue"); }
    }
    return 0;
}


void calculator()
{   bool Exit = false;
    Console.WriteLine("=== CALCULATOR ===");
    Console.WriteLine("write [exit] to quit the program");
    Console.WriteLine("Note: Use [,] as floating point");
    while (!Exit)
    {
        
        Console.WriteLine("select numbers:");
        double A = NumberInputChecked(ref Exit);
        if (Exit) { break; }
        double B = NumberInputChecked(ref Exit);
        if (Exit) { break; }
        Console.WriteLine(" ");
        Console.WriteLine("Numbers selected");
        Console.WriteLine(" ");

        bool success = false;
        while (!success)
        {
            success = true;
            Console.WriteLine("valid operators: [+] [-] [*] [/]");
            Console.WriteLine("select operation: ");
            string selectedOperation = Console.ReadLine();
            Console.WriteLine(" ");
            if (selectedOperation == "exit") { Exit = true; break; }
            if (selectedOperation == "+")
            {
                Console.WriteLine("addition selected");
                Console.WriteLine($"{A} + {B} = {A + B}");
            }
            else if (selectedOperation == "-")
            {
                Console.WriteLine("substraction selected");
                Console.WriteLine($"{A} - {B} = {A - B}");
            }
            else if (selectedOperation == "*")
            {
                Console.WriteLine("multiplication selected");
                Console.WriteLine($"{A} * {B} = {A * B}");
            }
            else if (selectedOperation == "/")
            {
                Console.WriteLine("division selected");
                Console.WriteLine($"{A} / {B} = {A / B}");
            }
            else
            {
                Console.WriteLine("operator selection failiure, reenter valid operator to continue");
               // Console.WriteLine("tip: enter operators just as * or others ");

                success = false;
            }
        }
    }
    Console.WriteLine(" ");
    Console.WriteLine(" ");
    Console.Beep();
    Console.WriteLine("Closing program... ");
}

calculator();
