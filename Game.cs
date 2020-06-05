using System;
using System.Collections;

// This is the Game class. The purpose of this class is to have the outlay of the game.
// Through the rules and it's ability to choose which level of diffuculty to the user wants to play.
// This class explains the rules and acts like a type of menu.
//TODO:
// Implement a type of game design where it asks user for diffuculty.
// Implement a rules screen.
// save and load sudoku files so user can resume whenever


namespace Sudoku
{
    class Game{
        
        private string user = "";
        private int choice;
        public int menu(string[,] board){

            Console.WriteLine("Welcome to my Sudoku Game.");
            Console.WriteLine("Please Select one of the options to continue.");
            Console.WriteLine("1. Easy Board.");
            Console.WriteLine("2. Medium Board.");
            Console.WriteLine("3. Hard Board");
            Console.WriteLine("4. Load Game");
            Console.WriteLine("5. Instructions.");
            Console.WriteLine("6. Exit");
            Console.WriteLine();
            Console.WriteLine("Enter user input below.");
            user = Console.ReadLine();
            try{
                choice = Convert.ToInt32(user);
            }
            catch(FormatException e){
                choice = 0;
            }

            if (choice <= 0 | choice > 6){

                Console.WriteLine("Incorrect Choice, please choose again.");
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
                Console.WriteLine();
                
            }

            return choice;
        }

        public void Instructions(){

            Console.WriteLine("Basic Features of the game:");
            Console.Write("  This game has three different types of difficulties. Easy, medium and Hard.");
            Console.WriteLine("  You can choose from either of the three and it will generate a board that can be played on.");
            Console.WriteLine("  If wanted the user can also load in a game based on a format.");
            Console.WriteLine("  The format will be text file that reads in line by line. The format is:");
            Console.WriteLine("      Row Col Number");
            Console.WriteLine("      Row Col Number");
            Console.WriteLine("      Row Col Number");
            Console.WriteLine("      etc etc  etc..");
            Console.Write("  The row and col are the X and Y for the board.");
            Console.WriteLine("  The number is the one associated with that position.");
            Console.WriteLine("  Three text files will be associated to download");
            Console.WriteLine();

            Console.WriteLine("Press any key to go back to the menu");


        }

    }
}