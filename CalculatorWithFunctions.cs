double ReadDouble(ref bool Exit, bool ZeroAllowed)
{
    bool success = false;
    while (!success)
    {
        string inp = Console.ReadLine();
        if (inp == "exit") { Exit = true; break; }
        bool isANumber = double.TryParse(inp, out double A);
        if (isANumber && (ZeroAllowed || A != 0))
        {  return A; }
        else { Console.WriteLine($" '{inp}' is not a valid number!, reenter a valid number to continue"); }
    }
    return 0;
}


void PrintMenu()
{
    Console.WriteLine("=== CALCULATOR ===");
    Console.WriteLine("valid operators: [+] [-] [*] [/]");
    Console.WriteLine("write [exit] to quit the program");
    Console.WriteLine("Note: Use [,] as floating point");
    Console.WriteLine("==================");
    Console.WriteLine(" ");
}



string ReadOperation(ref bool Exit)
{
    while (true)
    {

        Console.WriteLine("valid operators: [+] [-] [*] [/]");
        Console.WriteLine("select operation: ");
        string operation = Console.ReadLine();
        Console.WriteLine(" ");

        if (operation == "exit") { Exit = true; break; }

        switch (operation)
        {
            case "+":
            case "-":
            case "*":
            case "/":

                return operation;

            default:
                Console.WriteLine("Operator selection failiure, reenter a valid operator to continue");
                break;
        }
    }
    return "";
}



double Compute(string operation, double A, double B)
{
    double result = operation switch
    {
        "+" => A + B,
        "-" => A - B,
        "*" => A * B,
        "/" => A / B,
        _ => throw new InvalidOperationException($"{operation} compute error")
    };
    return result;
}



void PrintResult(string operation, double A, double B, double result)
{
    Console.WriteLine($"result: {A} {operation} {B} = {result}");
}






void calculator()
{
    bool Exit = false;
    PrintMenu();

    while (!Exit)
    {
        string operation = ReadOperation(ref Exit);
        if (Exit) { break; }

        bool ZeroAllowed = true;
        if (operation == "/") { ZeroAllowed = false; }

        Console.WriteLine("select 1st number:");
        double A = ReadDouble(ref Exit, true);
        if (Exit) { break; }

        Console.WriteLine("Select 2nd number");

        double B = ReadDouble(ref Exit, ZeroAllowed);
        if (Exit) { break; }

        // Console.WriteLine(" ");
        Console.WriteLine("Numbers selected");
        Console.Clear();
        Console.WriteLine(" ");

        double result = Compute(operation, A, B);
        PrintResult(operation, A, B, result);

        Console.WriteLine(" ");

    }
    Console.WriteLine(" ");
    Console.WriteLine(" ");
    Console.Beep();
    Console.WriteLine("Closing program... ");
}
calculator();
