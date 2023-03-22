namespace NumberRecognition.NeuralNetwork;

public class Matrix {
    private static readonly Random Random = new Random(DateTime.Now.Millisecond);

    private readonly int rows;
    private readonly int cols;
    private readonly float[,] data;

    public Matrix(int rows, int cols) {
        this.rows = rows;
        this.cols = cols;
        data = new float[rows, cols];
        InitializeArray();
    }

    private Matrix(Matrix matrix) {
        this.data = new float[matrix.data.GetLength(0), matrix.data.GetLength(1)];
        InitializeArray(matrix);
    }

    public Matrix Copy() {
        return new Matrix(this);
    }

    public static Matrix FromArray(byte[] array) {
        Matrix result = new Matrix(array.Length, 1);
        for (int i = 0; i < array.Length; i++)
            result.data[i, 0] = array[i];
        return result;
    }

    public static Matrix Subtract(Matrix a, Matrix b) {
        Matrix result = new Matrix(a.data.GetLength(0), a.data.GetLength(1));
        if (MatrixCompatible(a, b))
            for (int i = 0; i < a.rows; i++)
                for (int j = 0; j < a.cols; j++)
                    result.data[i, j] = a.data[i, j] - b.data[i, j];
        return result;
    }

    public void Add(Matrix matrix) {
        if (MatrixCompatible(this, matrix))
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    data[i, j] += matrix.data[i, j];
    }

    public static Matrix Multiply(Matrix a, Matrix b) {
        Matrix result = new Matrix(a.data.GetLength(0), a.data.GetLength(1));
        if (MatrixCompatible(a, b))
            for (int i = 0; i < a.rows; i++)
                for (int j = 0; j < a.cols; j++)
                    result.data[i, j] = a.data[i, j] * b.data[i, j];
        return result;
    }

    public void Multiply(Matrix matrix) {
        if (MatrixCompatible(this, matrix))
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    data[i, j] *= matrix.data[i, j];
    }

    public void Multiply(float value) {
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                data[i, j] *= value;
    }

    public static Matrix Map(Matrix matrix, Func<float, float> func) {
        Matrix result = new Matrix(matrix.rows, matrix.cols);
        for (int i = 0; i < matrix.rows; i++)
            for (int j = 0; j < matrix.cols; j++)
                result.data[i, j] = func(result.data[i, j]);
        return result;
    }

    public void Map(Func<float, float> func) {
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                data[i, j] = func(data[i, j]);
    }

    public float[] ToArray() {
        float[] result = new float[data.Length];
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                result[i * rows + j] = data[i, j];
        return result;
    }

    public void Randomize() {
        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < cols; j++) {
                data[i, j] = RandomValue();
            }
        }
    }

    public static Matrix Transpose(Matrix matrix) {
        Matrix result = new Matrix(matrix.rows, matrix.cols);
        for (int i = 0; i < matrix.rows; i++)
            for (int j = 0; j < matrix.cols; j++)
                result.data[i, j] = matrix.data[j, i];
        return result;
    }

    #region Private Methods

    private float RandomValue()
        => (float)Random.NextDouble() * (1 - -1) + -1;

    private static bool MatrixCompatible(Matrix a, Matrix b)
        => a.rows == b.rows && a.cols == b.cols;

    private void InitializeArray() {
        for (int i = 0; i < data.GetLength(0); i++) {
            for (int j = 0; j < data.GetLength(1); j++) {
                data[i, j] = 2;
            }
        }
    }

    private void InitializeArray(Matrix matrix) {
        for (int i = 0; i < data.GetLength(0); i++) {
            for (int j = 0; j < data.GetLength(1); j++) {
                data[i, j] = matrix.data[i, j];
            }
        }
    }

    #endregion
}