using System;

namespace Chips
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            if (input.StartsWith("chips:") && input.EndsWith("]"))
            {
                int bracket = input.IndexOf('[');
                input = input.Substring(bracket + 1, input.Length - bracket - 2);
            }
            else
            {
                return;
            }

            string[] chipsStrs = input.Split(',');
            int[] chips = new int[chipsStrs.Length];
            for (int i = 0; i < chipsStrs.Length; i++)
            {
                try
                {
                    chips[i] = Convert.ToInt16(chipsStrs[i]);
                }
                catch
                {
                    return;
                }
            }

            Calc calc = new Calc(chips);
            calc.arrange();
            int steps = calc.getSteps();

            Console.WriteLine(steps);
            Console.Read();
        }
    }

    class Calc
    {
        int[] chips;
        int  middle;
        int steps;

        public Calc(int[] _chips)
        {
            chips = _chips;
        }

        public void arrange()
        {
            steps = 0;
            calcMiddle();
            replace();
        }

        public int getSteps()
        {
            return steps;
        }

        private int maxIndex()
        {
            int maxIndex = 0;
            int max = 0;
            for (int i = 0; i < chips.Length; i++)
            {
                if (chips[i] > max)
                {
                    max = chips[i];
                    maxIndex = i;
                }
            }
            return maxIndex;
        }

        private void replace()
        {
            int max = maxIndex();

            if (chips[max] == middle)
            {
                return;
            }
            else
            {
                int left = leftIndex(max);
                int maxRight = maxRightIndex(max);
                int right = rightIndex(maxRight);

                if (chips[left] < chips[right])
                {
                    chips[left]++;
                    chips[max]--;
                }
                else
                {
                    chips[right]++;
                    chips[maxRight]--;
                }
            }
            steps++;

            replace();
        }

        private int maxRightIndex(int max)         //Правая граница последовательности максимальных значений
        {
            int maxRight = max;
            for (int i = max + 1; i < chips.Length ; i++)
            {
                if (chips[i] == chips[max])
                {
                    maxRight++;
                }
            }

            return maxRight;
        }

        private int leftIndex(int maxIndex)
        {
            if (maxIndex > 0)
            {
                return maxIndex - 1;
            }
            else
            {
                return chips.Length-1;
            }
        }

        private int rightIndex(int maxIndex)
        {
            if (maxIndex < chips.Length-1)
            {
                return maxIndex + 1;
            }
            else
            {
                return 0;
            }
        }

        private void calcMiddle()
        {
            int sum = 0;
            foreach (int chip in chips)
            {
                sum += chip;
            }
            middle = sum / chips.Length;
        }
    }
}
