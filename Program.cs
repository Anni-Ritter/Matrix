using System;
using System.Threading;

namespace Matrix
{
	public class Program
	{
		public const int Size = 100;


		private static int[,] FirstMatrix = InitializationMatrix();
		private static int[,] SecondMatrix = InitializationMatrix();
		private static MatrixThread Matrix;
		public static int[,] ResultMatrix = new int[Size, Size];
		public static int[,] ResultMatrix2 = new int[Size, Size];


		public static void Main(string[] args)
		{

			int optimalThreads = Size * Size / Environment.ProcessorCount;
			Thread[] threads = new Thread[Environment.ProcessorCount];
			Matrix = new MatrixThread(FirstMatrix, SecondMatrix, Size);

			for (int i = 0; i < threads.Length; i++)
            {
				threads[i] = new Thread(SetMartixThread);

				int[] indexes = { i * optimalThreads, (i + 1) * optimalThreads };
				threads[i].Start(indexes);
			}

			var watch = System.Diagnostics.Stopwatch.StartNew();
			foreach (var item in threads)
            {
				item.Join();
            }
			
			watch.Stop();
			var elapsedMs = watch.Elapsed;
			Console.WriteLine("Время вычисления в многопоточном режиме: " + elapsedMs);


			ResultMatrix2 = MultiplicationMatrix();
			
			Console.WriteLine("Совпадение двух матриц: ");
			bool flag = true;
			
			for (int row = 0; row < Size; row++)
			{
				for (int col = 0; col < Size; col++)
				{
					if (ResultMatrix[row, col] != ResultMatrix2[row, col])
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
			int[] index = (int[])items;
			Matrix.MultipleMatrixThread(index[0], index[1]);
		}

		private static int[,] InitializationMatrix()
		{
			var matrix = new int[Size, Size];
			var rand = new Random();

			for (int i = 0; i < Size; i++)
			{
				for (int j = 0; j < Size; j++)
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

			int[,] results = new int[Size, Size];
		
			for (int i = 0; i < FirstMatrix.GetLength(0); i++)
			{
				for (int j = 0; j < SecondMatrix.GetLength(1); j++)
				{
					for (int k = 0; k < SecondMatrix.GetLength(0); k++)
					{
						results[i, j] += FirstMatrix[i, k] * SecondMatrix[k, j];
					}
				}
			}

			watch.Stop();
			var elapsedMs = watch.Elapsed;
			Console.WriteLine("Время вычисления в однопоточном режиме: " + elapsedMs);
		
			return results;
		}

	}
}
