namespace Nim
{
    internal class Program
    {
        // Initializing the sticks to print
        public static int[,] DisplaySticks { get; set; } =
        {{ 0,0,0,1,0,0,0 },
         { 0,0,1,1,1,0,0 },
         { 0,1,1,1,1,1,0 },
         { 1,1,1,1,1,1,1 }, };
        // Initializing the sticks as a regular list (couldn't figure out how to calculate nim sum with only the 2d array)
        public static List<int> Sticks { get; set; } = new List<int> { 1, 3, 5, 7 };
        // To make AI wait for players' turn
        public static bool HasMoved { get; set; } = false;
        static void Main()
        {
            if (HasMoved)
            {
                PCMove();
            }
            Console.Clear();
            // Printing the game state on console
            Console.WriteLine("Hvis AI tager de(n) sidste tændstik(ker), taber du \n");
            for (int i = 0; i < DisplaySticks.GetLength(0); i++)
            {
                Console.Write($"{i + 1}: ");
                for (int j = 0; j < DisplaySticks.GetLength(1); j++)
                {
                    if (DisplaySticks[i, j] == 0)
                    {
                        Console.Write(" ");
                    }
                    else if (DisplaySticks[i, j] == 1)
                    {
                        Console.Write("|");
                    }
                }
                Console.WriteLine();
            }
            // If there's no more sticks left reinitialize (only AI can win)
            if (HasWon())
            {
                Console.WriteLine("\n AI vandt!");
                Console.ReadKey();
                Sticks = new List<int> { 1, 3, 5, 7 };
                DisplaySticks = new int[,] {{ 0,0,0,1,0,0,0 },
                                            { 0,0,1,1,1,0,0 },
                                            { 0,1,1,1,1,1,0 },
                                            { 1,1,1,1,1,1,1 }, };
                Main();
            }
            Console.Write("\n Fra hvilken række vil du fjerne tændstikker?: ");
            string Input = Console.ReadLine()!;
            Console.WriteLine();

            // Checking which row the player wants to remove from (empty rows are illegal)
            try
            {
                switch (int.Parse(Input))
                {
                    case 1:
                        if (IsEmpty(0)) { Main(); }
                        FirstRow();
                        break;
                    case 2:
                        if (IsEmpty(1)) { Main(); }
                        SecondRow();
                        break;
                    case 3:
                        if (IsEmpty(2)) { Main(); }
                        ThirdRow();
                        break;
                    case 4:
                        if (IsEmpty(3)) { Main(); }
                        FourthRow();
                        break;
                    default:
                        Main();
                        break;
                }
            }
            catch { Main(); }

        }

        // The player removing the first row does not need to specify how many to remove
        static void FirstRow()
        {
            RemoveSticksFromRow(0, 1);
            Main();
        }
        // The player can specify how many sticks they want to remove from this row, so check if the desired amount is legal, then remove it
        // (Repeating for ThirdRow() and FourthRow())
        static void SecondRow()
        {
            Console.Write("Hvor mange tændstikker vil du fjerne?: ");
            string Input = Console.ReadLine()!;
            if (int.TryParse(Input, out int value))
            {
                if (value > 0 && value <= SticksInRow(1))
                {
                    RemoveSticksFromRow(1, value);
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
                    RemoveSticksFromRow(2, value);
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
                    RemoveSticksFromRow(3, value);
                    Main();
                }
                else { Main(); }
            }
            else { Main(); }
        }

        // Gets the amount of sticks in a given row
        static int SticksInRow(int Row)
        {
            int count = 0;
            for (int i = 0; i < DisplaySticks.GetLength(1); i++)
            {
                if (DisplaySticks[Row, i] == 1) { count++; }
            }
            return count;
        }

        // Checks if given row is empty
        static bool IsEmpty(int Row)
        {
            return Sticks[Row] == 0;
        }

        static bool HasWon()
        {
            int SticksLeft = 0;
            foreach (int count in Sticks)
            {
                SticksLeft += count;
            }
            if (SticksLeft == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        // Calculating the nimsum by XOR operation
        static int CalculateNimSum()
        {
            int NimSum = 0;
            foreach (int count in Sticks)
            {
                NimSum ^= count;
            }
            return NimSum;
        }
        // Remove given sticks from given row, update the DisplaySticks to reflect the change on console output
        static void RemoveSticksFromRow(int row, int OnesToRemove)
        {
            HasMoved = true;
            Sticks[row] -= OnesToRemove;
            for (int i = 0; i < DisplaySticks.GetLength(1) && OnesToRemove > 0; i++)
            {
                if (DisplaySticks[row, i] == 1)
                {
                    DisplaySticks[row, i] = 0;
                    OnesToRemove--;
                }
            }
        }

        // AI to make the move by considering nims sum by getting desired sticks by exponent
        static void PCMove()
        {
            int NimSum = CalculateNimSum();
            bool moveMade = false;

            if (NimSum != 0)
            {
                for (int row = 0; row < Sticks.Count; row++)
                {
                    int desiredSticks = Sticks[row] ^ NimSum;
                    if (desiredSticks < Sticks[row])
                    {
                        int sticksToRemove = Sticks[row] - desiredSticks;
                        if (row == 0 && sticksToRemove == 1)
                        {
                            RemoveSticksFromRow(row, sticksToRemove);
                            moveMade = true;
                            break;
                        }
                        else if (Sticks.Sum() - sticksToRemove != 1)
                        {
                            RemoveSticksFromRow(row, sticksToRemove);
                            moveMade = true;
                            break;
                        }
                    }
                }
            }

            if (!moveMade) // If no optimal move was found, make a random move
            {
                RandomMove();
            }

            HasMoved = false;
        }

        // Making a random move switch to winning configuration
        static void RandomMove()
        {
            Random random = new();
            int randomRow;
            do
            {
                randomRow = random.Next(0, Sticks.Count);
            } while (IsEmpty(randomRow));
            RemoveSticksFromRow(randomRow, 1);
        }

    }
}