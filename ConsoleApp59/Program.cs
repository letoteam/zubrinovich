using System;
using System.IO;

public class Program
{
    public static void Main(string[] args)
    {
        var lines = File.ReadAllLines("data.txt");
        Console.WriteLine($"Прочитано {lines.Length} строк из файла:");

        foreach (var line in lines)
        {
            Console.WriteLine(line);
        }

        var matrix = new double[lines.Length, lines.Length + 1];

        for (int i = 0; i < lines.Length; i++)
        {
            var values = lines[i].Split(' ');
            for (int j = 0; j < values.Length; j++)
            {
                matrix[i, j] = double.Parse(values[j]);
            }
        }

        var solution = Solve(matrix, 1000, 0.0001);

        Console.WriteLine("Решение:");

        for (int i = 0; i < solution.Length; i++)
        {
            Console.WriteLine($"x{i+1} = {solution[i]}");
        }

        Console.ReadKey();
    }

    public static double[] Solve(double[,] matrix, int maxIterations, double epsilon)
    {
        var x = new double[matrix.GetLength(0)];
        var prevX = new double[x.Length];
        var iteration = 0;

        do
        {
            Console.WriteLine($"Итерация {iteration + 1}:");
            for (int i = 0; i < x.Length; i++)
            {
                Console.WriteLine($"x[{i+1}] = {x[i]}");
            }

            prevX = (double[])x.Clone();
            Iterate(matrix, x);
            iteration++;
        } while (iteration < maxIterations && !HasConverged(x, prevX, epsilon));

        return x;
    }

    public static void Iterate(double[,] matrix, double[] x)
    {
        for (int i = 0; i < x.Length; i++)
        {
            x[i] = CalculateNewApproximation(matrix, x, i);
        }
    }

    public static double CalculateNewApproximation(double[,] matrix, double[] x, int i)
    {
        double sum = 0;

        for (int j = 0; j < matrix.GetLength(1) - 1; j++)
        {
            if (j != i)
            {
                sum += matrix[i, j] * x[j];
            }
        }

        return (matrix[i, matrix.GetLength(1) - 1] - sum) / matrix[i, i];
    }

    public static bool HasConverged(double[] x, double[] prevX, double epsilon)
    {
        for (int i = 0; i < x.Length; i++)
        {
            if (Math.Abs(x[i] - prevX[i]) > epsilon)
            {
                return false;
            }
        }

        return true;
    }
}