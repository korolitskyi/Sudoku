using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class ValueGeneretor
    {
        public void Initialize(ref int[,] ValueArray)
        {
            ValueArray = new int[9, 9];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    ValueArray[i, j] = (i * 3 + i / 3 + j) % 9 + 1;
                }
            }
            ShuffGrid(ref ValueArray, 100);
        }

        void ChangeTwoCell(ref int[,] grid, int findValue1, int findValue2)
        {
            int xParam1, yParam1, xParam2, yParam2;
            xParam1 = xParam2 = yParam1 = yParam2 = 0;
            for (int i = 0; i < 9; i += 3)
            {
                for (int j = 0; j < 9; j += 3)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        for (int z = 0; z < 3; z++)
                        {
                            if (grid[i + k, j + z] == findValue1)
                            {
                                xParam1 = i + k;
                                yParam1 = j + z;
                            }
                            if (grid[i + k, j + z] == findValue2)
                            {
                                xParam2 = i + k;
                                yParam2 = j + z;
                            }
                        }
                    }
                    grid[xParam1, yParam1] = findValue2;
                    grid[xParam2, yParam2] = findValue1;
                }
            }
        }

        public void ShuffGrid(ref int[,] grid, int shufflvl)
        {
            for (int i = 0; i < shufflvl; i++)
            {
                Random rnd1 = new Random(Guid.NewGuid().GetHashCode());
                Random rnd2 = new Random(Guid.NewGuid().GetHashCode());

                ChangeTwoCell(ref grid, rnd1.Next(1, 10), rnd2.Next(1, 10));
            }
        }

        public void HideCells(ref int[,] grid, int minHideCells, int maxHideCells)
        {
            bool endcycle = false;

            Random rnd2 = new Random();

            int[] previousRand = new int[9];
            int numb = 0;

            int hideValue = 0;

            for (int i = 0; i < 9; i += 3)
            {
                for (int j = 0; j < 9; j += 3)
                {
                    int repeat = rnd2.Next(minHideCells, maxHideCells);
                    for (int r = 0; r < repeat; r++)
                    {
                        hideValue = noRepeatRand(previousRand, numb);
                        for (int k = 0; k < 3; k++)
                        {
                            for (int z = 0; z < 3; z++)
                            {
                                if ((k * 3 + k / 3 + z) % 9 + 1 == hideValue)
                                {
                                    grid[i + k, j + z] = 0;
                                    endcycle = true;
                                    break;
                                }
                            }
                            if (endcycle == true)
                            {
                                endcycle = false;
                                break;
                            }
                        }
                    }
                }
            }
        }

        int noRepeatRand(int[] previousRand, int numb)
        {
            int rand;
            Random rnd1 = new Random(Guid.NewGuid().GetHashCode());
            for (; ; )
            {
                rand = rnd1.Next(1, 10);
                if (previousRand.Contains(rand))
                    continue;
                previousRand[numb++] = rand;
                break;
            }
            return rand;
        }
    }
}
