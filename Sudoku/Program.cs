using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Sudoku
{
    class Program
    {
        static public int[,] _sudoku;
        const int _size = 9;
        static bool IsSafe(int x, int y, int num)
        {
            int[] r = GetRow<int>(_sudoku, x);
            int[] c = GetColumn<int>(_sudoku, y);
            int[] b = GetBlock<int>(_sudoku, x, y);

            if (Array.IndexOf(r, num) == -1 &&
                Array.IndexOf(c, num) == -1 &&
                Array.IndexOf(b, num) == -1)
                return true;

            return false;
        }

        static bool IsComplete()
        {
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                    if (_sudoku[i, j] == 0) return false;
            }
            return true;
        }

        static bool Helper(int x, int y)
        {
            if (IsComplete()) return true;

            int nextX = x + (y + 1)/_size;
            int nextY = (y + 1)%_size;

            if (_sudoku[x, y] != 0) return Helper(nextX, nextY);

            for (int i = 1; i <= 9; i++)
            {
                if (IsSafe(x, y, i))
                {
                    _sudoku[x, y] = i;
                    Console.WriteLine("[" + x + ", " + y + "] : " + i);
                    if (Helper(nextX, nextY))
                        return true;
                    else
                    {
                        _sudoku[x, y] = 0;
                        Console.WriteLine("[" + x + ", " + y + "] : " + 0);
                    }
                }
            }

            return false;
        }

        /*
        public static T[] GetRow<T>(this T[,] array, int row)
        {
            if (!typeof(T).IsPrimitive)
                throw new InvalidOperationException("Not supported for managed types.");

            if (array == null)
                throw new ArgumentNullException("array");

            int cols = array.GetUpperBound(1) + 1;
            T[] result = new T[cols];

            int size;

            if (typeof(T) == typeof(bool))
                size = 1;
            else if (typeof(T) == typeof(char))
                size = 2;
            else
                size = Marshal.SizeOf<T>();

            Buffer.BlockCopy(array, row * cols * size, result, 0, cols * size);

            return result;
        }
        */

        public static T[] GetColumn<T>(T[,] matrix, int columnNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(0))
                    .Select(x => matrix[x, columnNumber])
                    .ToArray();
        }

        public static T[] GetRow<T>(T[,] matrix, int rowNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(1))
                    .Select(x => matrix[rowNumber, x])
                    .ToArray();
        }

        public static T[] GetBlock<T>(T[,] matrix, int x, int y)
        {
            int blockSize = (int) Math.Sqrt(_size);
            int blockX = x / blockSize;
            int blockY = y / blockSize;

            T[] block = new T[blockSize * blockSize];

            for (int i = 0; i < blockSize; i++)
            {
                for (int j = 0; j < blockSize; j++)
                {
                    block[blockSize * i + j] = matrix[blockSize * blockX + i, blockSize * blockY + j] ;
                }
            }
            
            return block;
        }

        static void Main(string[] args)
        {
            _sudoku = new int[_size, _size]{{3, 0, 6, 5, 0, 8, 4, 0, 0}, 
                      {5, 2, 0, 0, 0, 0, 0, 0, 0}, 
                      {0, 8, 7, 0, 0, 0, 0, 3, 1}, 
                      {0, 0, 3, 0, 1, 0, 0, 8, 0}, 
                      {9, 0, 0, 8, 6, 3, 0, 0, 5}, 
                      {0, 5, 0, 0, 9, 0, 6, 0, 0}, 
                      {1, 3, 0, 0, 0, 0, 2, 5, 0}, 
                      {0, 0, 0, 0, 0, 0, 0, 7, 4}, 
                      {0, 0, 5, 2, 0, 6, 3, 0, 0}}; 

            if (Helper(0, 0))
            {
                for (int i = 0; i < _size; i++)
                {
                    for (int j = 0; j < _size; j++)
                    {
                        Console.Write(_sudoku[i, j] + ", ");
                    }
                    Console.Write("\n");
                }
            }
        }
    }
}
