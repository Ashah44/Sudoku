using System;
using System.Collections;

// This is the Game class. The purpose of this class is to have the outlay of the game.
// Through the rules and it's ability to choose which level of diffuculty to the user wants to play.
// This class explains the rules and acts like a type of menu.
//TODO:
// save and load sudoku files so user can resume whenever


namespace Sudoku
{
    class Game{
        
        private string user = "";
        private int choice;

//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //function that represents the menu that the user can choice from.
        public int menu(string[,] board){

            Console.WriteLine();
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
            //to check if input is castable to an int, if not then that means input was incorrect and not a possible choice, so try again.
            try{
                choice = Convert.ToInt32(user);
            }
            catch (FormatException)
            {
                choice = 0;
            }

            if (choice <= 0 | choice > 6){

                Console.WriteLine("Incorrect Choice, please choose again.");
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey(true);
                Console.WriteLine();
                
            }

            return choice;
        }

//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        //function to just show the user instructions on how to play the game.
        public void Instructions(){

            //featurees of the game
            Console.WriteLine();
            Console.WriteLine("Basic Features of the game:");
            Console.Write("  This game has three different types of difficulties. Easy, medium and Hard.");
            Console.WriteLine("  You can choose from either of the three and it will generate a board that can be played on.");
            Console.WriteLine("  If wanted the user can also load in a game based on a format.");
            Console.WriteLine("  The format will be a text file that reads in line by line. The format is:");
            Console.WriteLine("      Row Col Number");
            Console.WriteLine("      Row Col Number");
            Console.WriteLine("      Row Col Number");
            Console.WriteLine("      etc etc  etc..");
            Console.Write("  The row and col are the X and Y for the board.");
            Console.WriteLine("  The number is the one associated with that position.");
            Console.WriteLine("  Three text files will be associated to download");
            Console.WriteLine("Press any key to learn the rules.");
            Console.ReadKey(true);

            Console.WriteLine();
            
            // how to play the game
            Console.WriteLine("How to play:");
            Console.WriteLine("  To play their are 3 basic rules:");
            Console.WriteLine("   1. Every Row must have a unique number from 1-9");
            Console.WriteLine("   2. Every Col must have a unique number from 1-9");
            Console.Write("   3. Every 3 by 3 box must be unique starting from the top left. So the 3 by 3 box that is made by the interesection of");
            Console.WriteLine(" Rows 1-3 and Cols 1-3 must be unique. Another example would be, 3-6 for both Rows and Cols. That results in the middle box of the board.");
            Console.WriteLine("  In this version the player will input an X and Y coordinate along with the number they believe to be in their.");
            Console.WriteLine("  The input will be the same way the load in files is setup with Row Col Number.");
            Console.WriteLine("  You will be able to save your game when the feature is ready.");
            Console.WriteLine();
            Console.WriteLine("Press any key to go back to the menu");
            Console.ReadKey(true);  
            Console.WriteLine();             

        }

//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public void load(){

            Console.WriteLine();
            Console.WriteLine("This feature is not implemented yet.");
            Console.WriteLine();
            Console.WriteLine("Press any key to go back to the menu");
            Console.ReadKey(true);  
            Console.WriteLine(); 

        }

//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    }
}