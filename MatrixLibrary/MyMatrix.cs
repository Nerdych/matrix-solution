using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixLibrary
{
    public class MyMatrix<T>
    {
        public dynamic[,] numbers;
        public int xSize;
        public int ySize;

        public MyMatrix(int x, int y)
        {
            xSize = x;
            ySize = y;
            numbers = new dynamic[xSize, ySize];
        }

        public dynamic this[int i, int j]
        {
            get
            {
                return numbers[i, j];
            }
            set
            {
                numbers[i, j] = value;
            }
        }

        public static MyMatrix<T> operator +(MyMatrix<T> matrix1, MyMatrix<T> matrix2)
        {
            if (matrix1.xSize != matrix2.xSize || matrix1.ySize != matrix2.ySize) throw new ArgumentException("Матрицы нельзя сложить");

            MyMatrix<T> resultMatrix = new MyMatrix<T>(matrix1.xSize, matrix2.ySize);

            for (int i = 0; i < matrix1.xSize; i++)
            {
                for (int j = 0; j < matrix2.ySize; j++)
                {
                    resultMatrix[i, j] = matrix1[i, j] + matrix2[i, j];
                }
            }

            return resultMatrix;
        }

        public static MyMatrix<T> operator *(MyMatrix<T> matrix1, MyMatrix<T> matrix2)
        {
            if (matrix1.xSize != matrix2.ySize) throw new ArgumentException("Матрицы нельзя перемножить");

            MyMatrix<T> resultMatrix = new MyMatrix<T>(matrix2.xSize, matrix1.ySize);

            for (int i = 0; i < matrix1.ySize; i++)
            {
                for (int j = 0; j < matrix2.xSize; j++)
                {
                    resultMatrix[j, i] = 0;
                    for (int k = 0; k < matrix2.ySize; k++)
                    {
                        resultMatrix[j, i] += matrix1[k, i] * matrix2[j, k];
                    }
                }
            }

            return resultMatrix;
        }

        public static MyMatrix<T> GenerateMatrix(int rows, int columns, Func<int, int, Random, T> f)
        {
            if (rows == 0 || columns == 0) throw new ArgumentException("Матрица не может быть нулевого размера!");
            MyMatrix<T> newMatrix = new MyMatrix<T>(rows, columns);
            Random rnd = new Random();

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                {
                    newMatrix[j, i] = f(j, i, rnd);
                }

            return newMatrix;
        }

        public void WriteMatrix()
        {
            for (int i = 0; i < ySize; i++)
            {
                for (int j = 0; j < xSize; j++)
                {
                    Console.Write(numbers[j, i] + "\t");
                }

                Console.WriteLine();
            }
        }
    }
}