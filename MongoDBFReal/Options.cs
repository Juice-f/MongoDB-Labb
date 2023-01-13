using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBFReal
{
    class Input
    {
        public static int SelectInt(string[] choices, bool showChoices)
        {
            if (showChoices)
                for (int i = 0; i < choices.Length; i++)
                {
                    Console.WriteLine(i + " " + choices[i]);
                }

            bool choiceMade = false;
            int choice = -1;
            while (!choiceMade)
            {

                try
                {
                    choice = int.Parse(Console.ReadLine());
                    return choice;
                }
                catch
                {
                    Console.WriteLine($"Enter a number between {0} and {choices.Length-1}");
                }
                if (choice < choices.Length && choice > 0)
                {
                    choiceMade = true;
                }
                Console.WriteLine($"Enter a number between {0} and {choices.Length-1}");

            }
            return -1;
        }

        public static int ReadInt()
        {
            int input = 0;
            bool validInt = false;
            while (!validInt)
            {
                try
                {
                    input = int.Parse(Console.ReadLine());
                    validInt = true;
                    return input;
                }
                catch
                {
                    Console.WriteLine("Enter an integer");
                }
            }

            return input;
        }

    }

}
