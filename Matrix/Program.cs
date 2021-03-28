using System;
using System.Threading;

namespace Matrix
{
	public class Program
	{
		public const int size = 100;


		private static int[,] firstMatrix = InitializationMatrix();
		private static int[,] secondMatrix = InitializationMatrix();
		public static int[,] resultMatrix = new int[size, size];
		public static int[,] resultMatrix2 = new int[size, size];


		public static void Main(string[] args)
		{		

			int firstIndex = 0;
			
			Thread[] threads = new Thread[size];
			var watch = System.Diagnostics.Stopwatch.StartNew();
			for (int thredIndex = size - 1; thredIndex >= 0; --thredIndex)
            {
				int lastIndex = firstIndex + size;
				if (thredIndex == 0)
                {
					lastIndex = size * size;
                }
				threads[thredIndex] = new Thread(new ParameterizedThreadStart(SetMartixThread));
				threads[thredIndex].Start(new MatrixThread(firstMatrix, secondMatrix, firstIndex, lastIndex, size));
				firstIndex = lastIndex;
            }

			foreach (var item in threads)
            {
				item.Join();
            }
			
			watch.Stop();
			var elapsedMs = watch.ElapsedMilliseconds;
			Console.WriteLine("Время вычисления в многопоточном режиме: " + elapsedMs);


			resultMatrix2 = MultiplicationMatrix();
			
			Console.WriteLine("Совпадение двух матриц: ");
			bool flag = true;
			
			for (int row = 0; row < size; row++)
			{
				for (int col = 0; col < size; col++)
				{
					if (resultMatrix[row, col] != resultMatrix2[row, col])
                    {
						flag = false;
                    }
				}
			}
			
			Console.WriteLine(flag);

			Console.ReadLine();
		}

		public static void SetMartixThread(object items)
		{
			MatrixThread item = (MatrixThread) items;
			item.RUN();
		}

		private static int[,] InitializationMatrix()
		{
			var matrix = new int[size, size];
			var rand = new Random();

			for (int i = 0; i < size; i++)
			{
				for (int j = 0; j < size; j++)
				{
					matrix[i, j] = rand.Next(1, 10);
					//Console.Write(matrix[i, j] + ", ");
				}
				//Console.WriteLine();
			}

			return matrix;
		}

		private static int[,] MultiplicationMatrix()
		{
			var watch = System.Diagnostics.Stopwatch.StartNew();

			int[,] results = new int[size, size];
		
			for (int i = 0; i < firstMatrix.GetLength(0); i++)
			{
				for (int j = 0; j < secondMatrix.GetLength(1); j++)
				{
					for (int k = 0; k < secondMatrix.GetLength(0); k++)
					{
						results[i, j] += firstMatrix[i, k] * secondMatrix[k, j];
					}
				}
			}

			watch.Stop();
			var elapsedMs = watch.ElapsedMilliseconds;
			Console.WriteLine("Время вычисления в однопоточном режиме: " + elapsedMs);
		
			return results;
		}

	}
}
