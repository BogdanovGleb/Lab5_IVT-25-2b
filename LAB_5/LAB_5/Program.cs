using System;
using System.Collections.Generic;
using System.Text;

public class Program
{
    private static Random random = new Random();

    private static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        int choice = -1;

        while (choice != 0)
        {
            Console.WriteLine("\n=== ГЛАВНОЕ МЕНЮ ===");
            Console.WriteLine("1. Двумерный массив (добавить строку в конец)");
            Console.WriteLine("2. Рваный массив (удалить строки с нулями)");
            Console.WriteLine("3. Обработка строки (самый длинный идентификатор)");
            Console.WriteLine("0. Выход");
            Console.Write("Ваш выбор: ");

            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Ошибка! Введите целое число.");
                choice = -1;
                continue;
            }

            if (choice == 1)
            {
                MatrixTask();
            }
            else if (choice == 2)
            {
                JaggedTask();
            }
            else if (choice == 3)
            {
                StringTask();
            }
            else if (choice == 0)
            {
                Console.WriteLine("Программа завершена.");
            }
            else
            {
                Console.WriteLine("Такого пункта меню нет.");
            }
        }
    }

    private static int ReadInt(string message)
    {
        int number;

        while (true)
        {
            Console.Write(message);

            if (int.TryParse(Console.ReadLine(), out number))
            {
                return number;
            }

            Console.WriteLine("Ошибка! Введите целое число.");
        }
    }

    private static int ReadPositiveInt(string message)
    {
        int number;

        do
        {
            number = ReadInt(message);

            if (number <= 0)
            {
                Console.WriteLine("Число должно быть больше нуля.");
            }
        }
        while (number <= 0);

        return number;
    }

    private static int ReadMode()
    {
        int mode;

        do
        {
            Console.WriteLine("1. Заполнить вручную");
            Console.WriteLine("2. Заполнить случайными числами");

            mode = ReadInt("Ваш выбор: ");

            if (mode != 1 && mode != 2)
            {
                Console.WriteLine("Введите 1 или 2.");
            }
        }
        while (mode != 1 && mode != 2);

        return mode;
    }

    private static void MatrixTask()
    {
        Console.WriteLine("\n--- Двумерный массив ---");

        int rowCount = ReadPositiveInt("Количество строк: ");
        int columnCount = ReadPositiveInt("Количество столбцов: ");

        var matrix = new int[rowCount, columnCount];
        int mode = ReadMode();

        for (var i = 0; i < rowCount; i++)
        {
            for (var j = 0; j < columnCount; j++)
            {
                if (mode == 1)
                {
                    matrix[i, j] = ReadInt("Введите элемент: ");
                }
                else
                {
                    matrix[i, j] = random.Next(1, 100);
                }
            }
        }

        PrintArray(matrix, "Исходная матрица");

        var newRow = new int[columnCount];
        Console.WriteLine("\nЗаполнение новой строки:");

        for (var j = 0; j < columnCount; j++)
        {
            if (mode == 1)
            {
                newRow[j] = ReadInt("Введите элемент новой строки: ");
            }
            else
            {
                newRow[j] = random.Next(1, 100);
            }
        }

        var newMatrix = AddRowToEnd(matrix, newRow);
        PrintArray(newMatrix, "Матрица после добавления строки в конец");
    }

    private static int[,] AddRowToEnd(int[,] matrix, int[] row)
    {
        var rowCount = matrix.GetLength(0);
        var columnCount = matrix.GetLength(1);

        var newMatrix = new int[rowCount + 1, columnCount];

        for (var i = 0; i < rowCount; i++)
        {
            for (var j = 0; j < columnCount; j++)
            {
                newMatrix[i, j] = matrix[i, j];
            }
        }

        for (var j = 0; j < columnCount; j++)
        {
            newMatrix[rowCount, j] = row[j];
        }

        return newMatrix;
    }

    private static void PrintArray(int[,] matrix, string title = "Массив")
    {
        Console.WriteLine("\n" + title + ":");

        if (matrix.Length == 0)
        {
            Console.WriteLine("Массив пуст.");
            return;
        }

        for (var i = 0; i < matrix.GetLength(0); i++)
        {
            for (var j = 0; j < matrix.GetLength(1); j++)
            {
                Console.Write(matrix[i, j] + "\t");
            }

            Console.WriteLine();
        }
    }

    private static void JaggedTask()
    {
        Console.WriteLine("\n--- Рваный массив ---");

        int rowCount = ReadPositiveInt("Количество строк: ");
        var jaggedArray = new int[rowCount][];
        int mode = ReadMode();

        for (var i = 0; i < rowCount; i++)
        {
            int columnCount = ReadPositiveInt(
                "Количество элементов в строке " + (i + 1) + ": ");

            jaggedArray[i] = new int[columnCount];

            for (var j = 0; j < columnCount; j++)
            {
                if (mode == 1)
                {
                    jaggedArray[i][j] = ReadInt("Введите элемент: ");
                }
                else
                {
                    jaggedArray[i][j] = random.Next(0, 10);
                }
            }
        }

        PrintArray(jaggedArray, "Исходный рваный массив");
        jaggedArray = RemoveRowsWithZeros(jaggedArray);
        PrintArray(jaggedArray, "Массив после удаления строк с нулями");
    }

    private static int[][] RemoveRowsWithZeros(int[][] jaggedArray)
    {
        var keptRows = new List<int[]>();

        foreach (var row in jaggedArray)
        {
            if (!HasZero(row))
            {
                keptRows.Add(row);
            }
        }

        return keptRows.ToArray();
    }

    private static bool HasZero(int[] row)
    {
        foreach (var item in row)
        {
            if (item == 0)
            {
                return true;
            }
        }

        return false;
    }

    private static void PrintArray(int[][] jaggedArray, string title = "Массив")
    {
        Console.WriteLine("\n" + title + ":");

        if (jaggedArray.Length == 0)
        {
            Console.WriteLine("Массив пуст.");
            return;
        }

        for (var i = 0; i < jaggedArray.Length; i++)
        {
            for (var j = 0; j < jaggedArray[i].Length; j++)
            {
                Console.Write(jaggedArray[i][j] + "\t");
            }

            Console.WriteLine();
        }
    }

    private static void StringTask()
    {
        Console.WriteLine("\n--- Поиск идентификатора ---");

        var text = "static void PrintUpper string info12346: WriteLine ToUpper info, 1234info.";

        Console.WriteLine("Исходная строка:");
        Console.WriteLine(text);

        var longestIdentifiers = FindLongestIdentifiers(text);

        if (longestIdentifiers.Count == 0)
        {
            Console.WriteLine("Идентификаторы не найдены.");
        }
        else
        {
            Console.WriteLine("\nРезультат: " + string.Join(", ", longestIdentifiers));
        }
    }

    private static List<string> FindLongestIdentifiers(string text)
    {
        var identifiers = new List<string>();
        int maxLength = 0;
        int i = 0;

        while (i < text.Length)
        {
            if (!char.IsLetter(text[i]) && text[i] != '_')
            {
                i++;
                continue;
            }

            int start = i;

            while (i < text.Length && (char.IsLetterOrDigit(text[i]) || text[i] == '_'))
            {
                i++;
            }

            string word = text.Substring(start, i - start);

            if (IsKeyword(word))
            {
                continue;
            }

            if (identifiers.Contains(word))
            {
                continue;
            }

            if (word.Length > maxLength)
            {
                maxLength = word.Length;
                identifiers.Clear();
                identifiers.Add(word);
            }
            else if (word.Length == maxLength)
            {
                identifiers.Add(word);
            }
        }

        return identifiers;
    }

    private static bool IsKeyword(string word)
    {
        string[] keywords =
        {
            "static", "void", "string", "int", "class", "if", "else",
            "for", "while", "return", "public", "private", "protected"
        };

        foreach (var keyword in keywords)
        {
            if (word == keyword)
            {
                return true;
            }
        }

        return false;
    }
}