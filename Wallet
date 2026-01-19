using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    string FilePath = "data.txt";

    const int MAX_LENGHT = 100;
    bool QUIT = false;

    int count = 0;
    int[] values = new int[MAX_LENGHT];
    string[] names = new string[MAX_LENGHT];
    string[] categories = new string[MAX_LENGHT];

    static void Main()
    {
        new Program().Run();
    }

    void Run()
    {
        LoadFile();

        while (!QUIT)
        {
            Console.WriteLine("\n1 - Print Wallet");
            Console.WriteLine("2 - Add Item");
            Console.WriteLine("3 - Edit Item");
            Console.WriteLine("4 - Delete Item");
            Console.WriteLine("5 - Print Filtered Wallet");
            Console.WriteLine("6 - Print Sorted");
            Console.WriteLine("0 - Exit");

            switch (Console.ReadLine())
            {
                case "1": PrintWallet(false, false); break;
                case "2": AddItem(); break;
                case "3": EditItem(); break;
                case "4": DeleteItem(); break;
                case "5": PrintWallet(true, false); break;
                case "6": PrintWallet(false, true); break;
                case "0":
                    SaveFile();
                    QUIT = true;
                    break;
            }
        }
    }


    void LoadFile()
    {
        if (!File.Exists(FilePath)) return;

        string[] lines = File.ReadAllLines(FilePath);
        for (int i = 0; i + 2 < lines.Length && count < MAX_LENGHT; i += 3)
        {
            values[count] = int.Parse(lines[i]);
            names[count] = lines[i + 1];
            categories[count] = lines[i + 2];
            count++;
        }
    }

    void SaveFile()
    {
        List<string> outp = new();
        for (int i = 0; i < count; i++)
        {
            outp.Add(values[i].ToString());
            outp.Add(names[i]);
            outp.Add(categories[i]);
        }
        File.WriteAllLines(FilePath, outp);
    }



    void AddItem()
    {
        if (count >= MAX_LENGHT) return;

        values[count] = ReadInt("Value: ");
        Console.Write("Name: ");
        names[count] = Console.ReadLine();
        Console.Write("Category: ");
        categories[count] = Console.ReadLine();

        count++;
    }

    void EditItem()
    {
        int i = ReadIndex();

        values[i] = ReadInt("New value: ");
        Console.Write("New name: ");
        names[i] = Console.ReadLine();
        Console.Write("New category: ");
        categories[i] = Console.ReadLine();
    }

    void DeleteItem()
    {
        int i = ReadIndex();

        for (int j = i; j < count - 1; j++)
        {
            values[j] = values[j + 1];
            names[j] = names[j + 1];
            categories[j] = categories[j + 1];
        }
        count--;
    }



    void PrintWallet(bool filter, bool sort)
    {
        int[] idx = new int[count];
        for (int i = 0; i < count; i++) idx[i] = i;

        if (sort)
            Array.Sort(idx, (a, b) => values[a].CompareTo(values[b]));

        Console.Write("\nFilter text (empty = none): ");
        string text = filter ? Console.ReadLine() : null;

        Console.WriteLine("\n{0,3} {1,8} {2,-20} {3,-15} {4,8}",
            "#", "Value", "Name", "Category", "Balance");

        int balance = 0;

        foreach (int i in idx)
        {
            balance += values[i];

            if (!filter || names[i].Contains(text, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("{0,3} {1,8} {2,-20} {3,-15} {4,8}",
                    i, values[i], names[i], categories[i], balance);
            }
        }

        PrintStats();
        PrintCategoryStats();
    }



    void PrintStats()
    {
        int inSum = 0, outSum = 0, inCnt = 0, outCnt = 0;
        int minIn = int.MaxValue, maxIn = int.MinValue;
        int minOut = int.MaxValue, maxOut = int.MinValue;

        for (int i = 0; i < count; i++)
        {
            if (values[i] >= 0)
            {
                inSum += values[i];
                inCnt++;
                minIn = Math.Min(minIn, values[i]);
                maxIn = Math.Max(maxIn, values[i]);
            }
            else
            {
                outSum += values[i];
                outCnt++;
                minOut = Math.Min(minOut, values[i]);
                maxOut = Math.Max(maxOut, values[i]);
            }
        }

        Console.WriteLine("\n--- STATISTICS ---");
        Console.WriteLine($"Income: {inCnt}, sum {inSum}, min {minIn}, max {maxIn}");
        Console.WriteLine($"Expense: {outCnt}, sum {outSum}, min {minOut}, max {maxOut}");
    }

    void PrintCategoryStats()
    {
        Dictionary<string, int> sums = new();

        for (int i = 0; i < count; i++)
        {
            if (!sums.ContainsKey(categories[i]))
                sums[categories[i]] = 0;

            sums[categories[i]] += values[i];
        }

        Console.WriteLine("\n--- CATEGORIES ---");
        foreach (var k in sums)
            Console.WriteLine($"{k.Key,-15}: {k.Value}");
    }

 

    int ReadInt(string text)
    {
        int x;
        while (true)
        {
            Console.Write(text);
            if (int.TryParse(Console.ReadLine(), out x))
                return x;
        }
    }

    int ReadIndex()
    {
        int i;
        do
        {
            Console.Write("Index: ");
        }
        while (!int.TryParse(Console.ReadLine(), out i) || i < 0 || i >= count);

        return i;
    }
}




