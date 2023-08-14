// I DIDN'T MANAGE TO FINISH THE ASSIGNMENT IN TIME (14/08/23)
// TODO:
// 1. CHECK IF USER CAN ACTUALLY REMOVE ANY MORE STICKS FROM A ROW
// 2. COMPUTER MUST TAKE TURN (XOR OPERATION)
// 3. GAME STATE CHECKING TO SEE IF THERE'S ONLY ONE STICK LEFT AND ASSIGN A WINNER


namespace Nim
{
    internal class Program
    {
        public static int[,] Sticks { get; set; } =
        {{ 0,0,0,1,0,0,0 },
         { 0,0,1,1,1,0,0 },
         { 0,1,1,1,1,1,0 },
         { 1,1,1,1,1,1,1 }, };
        
        static void Main()
        {
            Console.Clear();
            for (int i = 0; i < Sticks.GetLength(0); i++)
            {
                Console.Write($"{i+1}: ");
                for (int j = 0; j < Sticks.GetLength(1); j++)
                {
                    if (Sticks[i,j] == 0)
                    {
                        Console.Write(" ");
                    }
                    else if (Sticks[i, j] == 1)
                    {
                        Console.Write("|");
                    }
                }
                Console.WriteLine();
            }

            Console.Write("Fra hvilken række vil du fjerne tændstikker?: ");
            string Input = Console.ReadLine()!;
            Console.WriteLine();

            try 
            {
                switch(int.Parse(Input))
                {
                    case 1:
                        FirstRow();
                        break;
                    case 2:
                        SecondRow();
                        break;
                    case 3:
                        ThirdRow();
                        break;
                    case 4:
                        FourthRow();
                        break;
                    default: 
                        Main();
                        break;
                }
            } 
            catch { Main(); }

        }

        static void FirstRow()
        {
            for (int i = 0; i < Sticks.GetLength(0); i++)
            {
                if (Sticks[0, i] == 1)
                {
                    Sticks[0, i] = 0;
                }
            }
            Main();
        }
        static void SecondRow()
        {
            Console.Write("Hvor mange tændstikker vil du fjerne?: ");
            string Input = Console.ReadLine()!;
            if (int.TryParse(Input, out int value))
            {
                if (value > 0 && value <= SticksInRow(1))
                {
                    int OnesToRemove = value;
                    for (int i = 0; i < Sticks.GetLength(1) && OnesToRemove > 0; i++)
                    {
                        if (Sticks[1, i] == 1)
                        {
                            Sticks[1, i] = 0;
                            OnesToRemove--;
                        }
                    }
                    Main();
                }
                else { Main(); }
            }
            else { Main(); }
        }
        static void ThirdRow()
        {
            Console.Write("Hvor mange tændstikker vil du fjerne?: ");
            string Input = Console.ReadLine()!;
            if (int.TryParse(Input, out int value))
            {
                if (value > 0 && value <= SticksInRow(2))
                {
                    int OnesToRemove = value;
                    for (int i = 0; i < Sticks.GetLength(1) && OnesToRemove > 0; i++)
                    {
                        if (Sticks[2, i] == 1)
                        {
                            Sticks[2, i] = 0;
                            OnesToRemove--;
                        }
                    }
                    Main();
                }
                else { Main(); }
            }
            else { Main(); }
        }
        static void FourthRow()
        {
            Console.Write("Hvor mange tændstikker vil du fjerne?: ");
            string Input = Console.ReadLine()!;
            if (int.TryParse(Input, out int value))
            {
                if (value > 0 && value <= SticksInRow(3))
                {
                    int OnesToRemove = value;
                    for (int i = 0; i < Sticks.GetLength(1) && OnesToRemove > 0; i++)
                    {
                        if (Sticks[3, i] == 1)
                        {
                            Sticks[3, i] = 0;
                            OnesToRemove--;
                        }
                    }
                    Main();
                }
                else { Main(); }
            }
            else { Main(); }
        }

        static int SticksInRow(int Row)
        {
            int count = 0;
            for (int i = 0; i < Sticks.GetLength(1); i++)
            {
                if (Sticks[Row, i] == 1) { count++; }
            }
            return count;
        }
    }
}