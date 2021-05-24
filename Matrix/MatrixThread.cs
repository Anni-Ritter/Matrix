using System;
using System.Threading;

namespace Matrix
{
    public class MatrixThread
    {
        private int[,] FirstMatrix;
        private int[,] SecondMatrix;
        private int Size;


        public MatrixThread(int[,] firstMatrix, int[,] secondMatrix, int size)
        {
            this.FirstMatrix = firstMatrix;
            this.SecondMatrix = secondMatrix;
            this.Size = size;
        }

        public void CalculateValue(int row, int col)
        {
            Program.ResultMatrix[row, col] = 0;
            for (int i = 0; i < Size; ++i)
            {
                Program.ResultMatrix[row, col] += FirstMatrix[row, i] * SecondMatrix[i, col];
            }
        }
        
        public void MultipleMatrixThread(int firstIndex, int lastIndex)
        {
        
            for (int index = firstIndex; index < lastIndex; ++index)
            {
                CalculateValue(index / Size, index % Size);
            }
            
        }
    }    
}
