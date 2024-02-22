using System.Text;

namespace binaryClock
{
    internal class Program
    {
        static int[] intToIntArray(int input)
        {
            string stringInput = input.ToString();
            char[] charInput = stringInput.ToCharArray();

            Queue<int> inputStack = new Queue<int>();

            foreach (char piece in charInput)
            {
                inputStack.Enqueue((int)(piece - '0')); //weird ascii stuff that c# does
            }
            return inputStack.ToArray();
        }
        
                                   //H   H    M    M    S    S
        static char[,] clockFace={{' ', '0', ' ', '0', ' ', '0'},//8
                                  {' ', '0', '0', '0', '0', '0'},//4
                                  {'0', '0', '0', '0', '0', '0'},//2
                                  {'0', '0', '0', '0', '0', '0'} };//1
        
        static void changeState(int[]pos, bool state)
        {
            if (state)
            {
                clockFace[pos[0], pos[1]] = 'X';
            }
            else
            {
                clockFace[pos[0], pos[1]] = '0';
            }
        }
        static void Main(string[] args)
        {
            DateTime localDate = DateTime.Now;
            TimeSpan time = localDate.TimeOfDay;

            string timeString = time.ToString();
            string[] timeSplit = timeString.Split(":");

            int[] hours = intToIntArray(int.Parse(timeSplit[0]));

            int[] minutes = intToIntArray(int.Parse(timeSplit[1]));

            double secondsRounded = Math.Round(double.Parse(timeSplit[2]));
            int secondsInt = Convert.ToInt16(secondsRounded);
            int[] seconds = intToIntArray(secondsInt);

            int[][] timeArrays = { hours, minutes, seconds };

            var numbers = new Dictionary<int, int[]>
            {
                {1, [1]},
                {2, [2]},
                {3, [1,2]},
                {4, [4]},
                {5, [1,4]},
                {6, [4,2]},
                {7, [1,2,4] },
                {8, [8]},
                {9, [1,8]},
                {0, [0]},
            };
            var yValues = new Dictionary<int, int>
            {
                {8,0},
                {4,1},
                {2,2},
                {1,3},
            };
            int omg = 0;
            for (int j = 0; j < timeArrays.Length; j++) 
            {
                for (int i = 0; i < timeArrays[j].Length; i++)
                {
                    int[] f = numbers[timeArrays[j][i]];
                    foreach(int n in f)
                    {
                        if (n != 0)
                        {
                            changeState([yValues[n], omg], true);
                        }
                    }
                    omg++;
                }
                
            }
            StringBuilder resultBuilder = new StringBuilder();
            for (int x = 0; x < clockFace.GetLength(0); x++)
            {
                for(int y = 0; y<clockFace.GetLength(1); y++)
                {
                    resultBuilder.Append(clockFace[x,y]);
                    resultBuilder.Append(" ");
                }
                resultBuilder.AppendLine();
            }
            Console.WriteLine(time);
            string result = resultBuilder.ToString();
            Console.Write(result);
        }
    }
}
