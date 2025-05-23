using System;

namespace MatrixApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Matrix m1 = new Matrix(2, 2);
            m1[0, 0] = 1; m1[0, 1] = 2;
            m1[1, 0] = 3; m1[1, 1] = 4;

            Matrix m2 = new Matrix(2, 2);
            m2[0, 0] = 5; m2[0, 1] = 6;
            m2[1, 0] = 7; m2[1, 1] = 8;

            Console.WriteLine("Матрица 1:");
            Console.WriteLine(m1);

            Console.WriteLine("Матрица 2:");
            Console.WriteLine(m2);

            Matrix sum = m1 + m2;
            Console.WriteLine("Сумма матриц:");
            Console.WriteLine(sum);

            Matrix diff = m1 - m2;
            Console.WriteLine("Разность матриц:");
            Console.WriteLine(diff);

            Matrix product = m1 * m2;
            Console.WriteLine("Произведение матриц:");
            Console.WriteLine(product);

            Matrix scaled = m1 * 2;
            Console.WriteLine("Умножение матрицы на число:");
            Console.WriteLine(scaled);

            Matrix m3 = new Matrix(2, 2);
            m3[0, 0] = 1; m3[0, 1] = 2;
            m3[1, 0] = 3; m3[1, 1] = 4;

            Console.WriteLine("\nПроверка на равенство:");
            Console.WriteLine($"m1 == m3: {m1 == m3}");
            Console.WriteLine($"m1 != m2: {m1 != m2}");

            Console.WriteLine($"m1.Equals(m3): {m1.Equals(m3)}");
            Console.WriteLine($"m1.GetHashCode() == m3.GetHashCode(): {m1.GetHashCode() == m3.GetHashCode()}");
        }
    }

    public class Matrix
    {
        private int rows;
        private int cols;
        private double[,] data;

        public Matrix(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
            data = new double[rows, cols];
        }

        public int Rows
        {
            get => rows;
            private set
            {
                if (value <= 0) throw new ArgumentException("Число строк должно быть положительным.");
                rows = value;
            }
        }

        public int Cols
        {
            get => cols;
            private set
            {
                if (value <= 0) throw new ArgumentException("Число столбцов должно быть положительным.");
                cols = value;
            }
        }

        public double this[int i, int j]
        {
            get
            {
                if (i < 0 || i >= Rows || j < 0 || j >= Cols)
                    throw new IndexOutOfRangeException("Индексы выходят за границы матрицы.");
                return data[i, j];
            }
            set
            {
                if (i < 0 || i >= Rows || j < 0 || j >= Cols)
                    throw new IndexOutOfRangeException("Индексы выходят за границы матрицы.");
                data[i, j] = value;
            }
        }

        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            if (m1.Rows != m2.Rows || m1.Cols != m2.Cols)
                throw new InvalidOperationException("Матрицы должны иметь одинаковые размеры для сложения.");

            Matrix result = new Matrix(m1.Rows, m1.Cols);
            for (int i = 0; i < m1.Rows; i++)
                for (int j = 0; j < m1.Cols; j++)
                    result[i, j] = m1[i, j] + m2[i, j];

            return result;
        }

        public static Matrix operator -(Matrix m1, Matrix m2)
        {
            if (m1.Rows != m2.Rows || m1.Cols != m2.Cols)
                throw new InvalidOperationException("Матрицы должны иметь одинаковые размеры для вычитания.");

            Matrix result = new Matrix(m1.Rows, m1.Cols);
            for (int i = 0; i < m1.Rows; i++)
                for (int j = 0; j < m1.Cols; j++)
                    result[i, j] = m1[i, j] - m2[i, j];

            return result;
        }

        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            if (m1.Cols != m2.Rows)
                throw new InvalidOperationException("Количество столбцов первой матрицы должно совпадать с количеством строк второй.");

            Matrix result = new Matrix(m1.Rows, m2.Cols);
            for (int i = 0; i < m1.Rows; i++)
                for (int j = 0; j < m2.Cols; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < m1.Cols; k++)
                        sum += m1[i, k] * m2[k, j];
                    result[i, j] = sum;
                }

            return result;
        }

        public static Matrix operator *(Matrix m, double scalar)
        {
            Matrix result = new Matrix(m.Rows, m.Cols);
            for (int i = 0; i < m.Rows; i++)
                for (int j = 0; j < m.Cols; j++)
                    result[i, j] = m[i, j] * scalar;

            return result;
        }

        public static bool operator ==(Matrix m1, Matrix m2)
        {
            if (ReferenceEquals(m1, m2)) return true;
            if (ReferenceEquals(m1, null) || ReferenceEquals(m2, null)) return false;

            if (m1.Rows != m2.Rows || m1.Cols != m2.Cols) return false;

            for (int i = 0; i < m1.Rows; i++)
                for (int j = 0; j < m1.Cols; j++)
                    if (m1[i, j] != m2[i, j])
                        return false;

            return true;
        }

        public static bool operator !=(Matrix m1, Matrix m2)
        {
            return !(m1 == m2);
        }

        public override bool Equals(object obj)
        {
            if (obj is Matrix other)
                return this == other;
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                for (int i = 0; i < Rows; i++)
                    for (int j = 0; j < Cols; j++)
                        hash = hash * 23 + data[i, j].GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            string result = "";
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    result += $"{data[i, j],5:F2}";
                }
                result += "\n";
            }
            return result;
        }
    }
}