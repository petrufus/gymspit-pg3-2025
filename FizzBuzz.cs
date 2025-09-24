void FizzBuzz(int number)
{
    if ((number % 3 != 0 && number % 5 != 0) | number == 0)
    {
        Console.WriteLine(number);
    }
    else if (number % 3 == 0 && number % 5 != 0)
    {
        Console.WriteLine("Fizz");
    }
    else if (number % 3 != 0 && number % 5 == 0)
    {
        Console.WriteLine("Buzz");
    }
    else if (number % 3 == 0 && number % 5 == 0)
    {
        Console.WriteLine("FizzBuzz");
    }
}



for (int i = 0; i <= 100; i++)
{
    FizzBuzz(i);
}


Console.WriteLine("input number to check if it Fizzles or buzzles fr");

while (true)
{
    string wr = Console.ReadLine();
    int.TryParse(wr, out int number);
    FizzBuzz(number);
}

