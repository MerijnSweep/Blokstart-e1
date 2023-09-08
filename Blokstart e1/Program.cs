namespace Blokstart_e1
{
    internal class Program
    {
        static void Main(string[] args)
        {
           bool IsRunning = true;
            
                Console.WriteLine("Welkom speler bij Dungeon Dude voer jouw naam in:");

                string pName = Console.ReadLine();

                Console.WriteLine($"{pName} welkom bij Dungeon Dude! ");
            while (IsRunning == true)
            {
                Console.WriteLine("How do you play?");
                Console.WriteLine("On the screen, a description will appear first of what is happening here, below which choices will appear in a numbered list.");
                Console.WriteLine("You can select these options by typing the corresponding number and pressing enter, which will perform an action.");
                Console.WriteLine("If the controls are no longer clear, you can type 'help,' and the instructions will be displayed again.");
                Console.WriteLine("1.   Continue");
                Console.WriteLine("2.   Stop playing");
                Console.WriteLine("Select your choice:");
                Console.ReadLine();
                string choice = Console.ReadLine();
                if (choice == "1")
                {
                    continue;
                }
                else if (choice == "2")
                {
                    IsRunning = false;
                }
                else
                {
                    return;
                }

            }
        }
    }
}