using System;
using System.Collections.Generic;
using System.Linq;

public class RandomFilledArray {
    private int[,] array;
    private double fillRatio;
    private List<Tuple<int, int>> remainingPositions;
    private Random random;

    public RandomFilledArray(int rows, int columns, double initialFillRatio = 20) {
        array = new int[rows, columns];
        fillRatio = initialFillRatio / 100.0;
        random = new Random();

        remainingPositions = Enumerable.Range(0, rows)
            .SelectMany(r => Enumerable.Range(0, columns).Select(c => new Tuple<int, int>(r, c)))
            .ToList();

        FillArray(fillRatio);
    }

    private void FillArray(double ratio) {
        int numElements = array.GetLength(0) * array.GetLength(1);
        int numFilledElements = (int)(numElements * ratio);

        ShufflePositions(remainingPositions);

        foreach (var position in remainingPositions.Take(numFilledElements)) {
            array[position.Item1, position.Item2] = 1;
        }

        remainingPositions = remainingPositions.Skip(numFilledElements).ToList();
    }

    private void ShufflePositions(List<Tuple<int, int>> positions) {
        int n = positions.Count;
        while (n > 1) {
            n--;
            int k = random.Next(n + 1);
            (positions[n], positions[k]) = (positions[k], positions[n]);
        }
    }

    public void AddFillRatio(double newFillRatioPercentage) {
        double newFillRatio = newFillRatioPercentage / 100.0;
        double additionalFillRatio = newFillRatio - fillRatio;

        if (additionalFillRatio > 0) {
            FillArray(additionalFillRatio);
            fillRatio = newFillRatio;
        }
    }

    public int[,] Array {
        get { return array; }
    }
}