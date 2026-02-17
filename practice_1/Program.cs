using System;
using System.IO;

class Program
{
    public static int[,] MatrixManualInput(int rows, int columns)
    {
        Console.WriteLine("Ручной ввод матрицы:");

        int[,] handled_matrix = new int[rows, columns];

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                Console.Write($"ввод элемента ({y + 1};{x + 1}): "); //добавлять +1 оффсет
                handled_matrix[y, x] = Convert.ToInt32(Console.ReadLine());
            }
        }

        return handled_matrix;
    }

    public static void MatrixPrint(int[,] input_matrix)
    {
        int rows = input_matrix.GetLength(0); //строки
        int columns = input_matrix.GetLength(1); //столбцы

        Console.WriteLine("Матрица:");

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                Console.Write($"{input_matrix[y, x]} ");
            }
            Console.WriteLine(""); //переход на следующую строку
        }
    }

    public static int[,] MatrixSumm(int[,] matrix_1, int[,] matrix_2)
    {
        int rows = matrix_1.GetLength(0); //строки
        int columns = matrix_1.GetLength(1); //столбцы

        //Console.WriteLine($"столбцы: {columns} , строки: {rows}\n");//проверка

        int[,] final_matrix = new int[rows, columns];

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                final_matrix[y, x] = matrix_1[y, x] + matrix_2[y, x];
            }
        }

        Console.WriteLine("Результат: ");

        return final_matrix;
    }

    public static int[,] MatrixSubstract(int[,] matrix_1, int[,] matrix_2)
    {
        int rows = matrix_1.GetLength(0); //строки
        int columns = matrix_1.GetLength(1); //столбцы

        int[,] final_matrix = new int[rows, columns];

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                final_matrix[y, x] = matrix_1[y, x] - matrix_2[y, x];
            }
        }

        Console.WriteLine("Результат: ");

        return final_matrix;
    }

    public static void ProcessThroughConsole()
    {
        Console.WriteLine("Кол-во столбцов: ");
        int columns = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Кол-во строк: ");
        int rows = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("");

        Console.WriteLine("Матрица №1");

        int[,] matrix_1 = MatrixManualInput(rows, columns);
        MatrixPrint(matrix_1);

        Console.WriteLine("Матрица №2");

        int[,] matrix_2 = MatrixManualInput(rows, columns);

        MatrixPrint(matrix_2);

        Console.WriteLine("\nвведите действие.\nсложение (\"+\")\nвычитание(\"-\")\n");
        string action = Console.ReadLine();

        int[,] final_matrix; //общая переменная результата и для сложения и для вычитания

        if (action == "+")
        {
            Console.WriteLine("Выбрано сложение.\n");
            final_matrix = MatrixSumm(matrix_1, matrix_2);
            MatrixPrint(final_matrix);
        }
        else if (action == "-")
        {
            Console.WriteLine("Выбрано вычитание.\n");
            final_matrix = MatrixSubstract(matrix_1, matrix_2);
            MatrixPrint(final_matrix);
        }
        else
        {
            Console.WriteLine("Команда не распознана");
        }
    }

    public static int[,] MatrixFromString(string input)
    {
        input = input.Trim(); //обрезка пустых строк

        char[] splitters_2 = { '\n', ',' };

        int rows = input.Split('\n', StringSplitOptions.RemoveEmptyEntries).Length; //кол-во строк + обрезка пробелов

        string[] elements = input.Split(splitters_2); //все эл-ты в массиве но строчном

        int columns = elements.Length / rows; //подсчёт колонок

        int[,] final_matrix = new int[rows, columns];

        int i = 0; //обращение к эл-ту elements

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                final_matrix[y, x] = Convert.ToInt32(elements[i]);
                i++;
            }
        }

        return final_matrix;
    }

    public static void MatrixInTxt(int[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int columns = matrix.GetLength(1);
        int elements = matrix.Length;

        string[] lined_matrix = new string[rows];

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                lined_matrix[y] += Convert.ToString(matrix[y, x] + " ");
            }
        }

        File.WriteAllLines("MatrixOutput.txt", lined_matrix);
        Console.WriteLine(
            "Результат вычислений записан в файл \"MatrixOutput.txt\"в корень программы."
        );
    }

    public static async Task ProcessThroughFile()
    {
        Console.WriteLine(
            "Располагайте строку каждой матрицы в новой строке.\nСтолбцы разделяйте запятой (\",\").\nМежду матрицами на отдельной строке напишите арифметический знак (\"+\"),(\"-\").\nБудьте внимательны при вводе, не ставьте лишних знаков.."
        );
        Console.WriteLine("Пример заполнения: ");
        Console.WriteLine("1,2,3\n4,5,6\n7,8,9\n-\n1,2,3\n4,5,6\n7,8,9");

        if (!File.Exists("MatrixInput.txt"))
        {
            Console.WriteLine("Был создан файл с примером.");
            //await
            File.WriteAllTextAsync(
                "MatrixInput.txt",
                "1,2,3\n4,5,6\n7,8,9\n-\n1,2,3\n4,5,6\n7,8,9"
            );
        }

        Console.WriteLine(
            "Оперируемый файл находится в корневой папке.\nНазвание файла: \"MatrixInput.txt\"\nНе забудьте сохранить изменения в файле.\nНажмите enter, как только файл будет готов к обработке.\n"
        );
        //await
        Console.ReadLine(); //ожидание подтверждения

        string all_text = File.ReadAllText("MatrixInput.txt");
        if (all_text.Length == 0)
        {
            Console.WriteLine("Файл пустой, проверьте содержимое.");
            return;
        }

        char[] splitters = { '+', '-' }; //разделители по действию

        string[] separated_string_matrixes = all_text.Split(splitters); //матрицы поделены на 2 эл-та в массиве

        string pre_matrix_1 = separated_string_matrixes[0];
        int[,] matrix_1 = MatrixFromString(pre_matrix_1); //обработали 1-ую матрицу в двумерный целочисленный массив
        Console.WriteLine("Матрица №1: ");
        MatrixPrint(matrix_1);

        string pre_matrix_2 = separated_string_matrixes[1];
        int[,] matrix_2 = MatrixFromString(pre_matrix_2); //и вторую тоже
        Console.WriteLine("Матрица №2: ");
        MatrixPrint(matrix_2);

        int[,] final_matrix = matrix_1; //общая переменная для исхода. Наследует размеры первой матрицы.

        if (all_text.Contains('+'))
        {
            final_matrix = MatrixSumm(matrix_1, matrix_2);
            MatrixPrint(final_matrix);
        }
        else if (all_text.Contains('-'))
        {
            final_matrix = MatrixSubstract(matrix_1, matrix_2);
            MatrixPrint(final_matrix);
        }
        else
        {
            Console.WriteLine("Знак действия не распознан.");
        }

        MatrixInTxt(final_matrix);
        Console.WriteLine("Выполнено.");
    }

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\nРабота через консоль : 1");
            Console.WriteLine("Работа через файлы : 2");
            Console.WriteLine("Выход: 3\n");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    ProcessThroughConsole();
                    break;

                case "2":
                    ProcessThroughFile();
                    break;

                case "3":
                    Console.WriteLine("Работа завершена.");
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Задача не распознана.");
                    break;
            }
        }
    }
}
