using System;
using System.Threading;

namespace Matrix
{
    public class MatrixThread
    {
        private int[,] firstMatrix;
        private int[,] secondMatrix;
        private int firstIndex;
        private int lastIndex;
        private int size;


        public MatrixThread(int[,] firstMatrix, int[,] secondMatrix, int firstIndex, int lastIndex, int size)
        {
            this.firstMatrix = firstMatrix;
            this.secondMatrix = secondMatrix;
            this.firstIndex = firstIndex;
            this.lastIndex = lastIndex;
            this.size = size;
        }

        public void CalculateValue(int row, int col)
        {

            int sum = 0;
            for (int i = 0; i < size; ++i)
            {
                sum += firstMatrix[row, i] * secondMatrix[i, col];
            }
            Program.resultMatrix[row, col] = sum;
        }

        public void RUN()
        {

            for (int index = firstIndex; index < lastIndex; ++index)
            {
                CalculateValue(index / size, index % size);
            }
        }
    }
}
