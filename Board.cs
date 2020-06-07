using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;


//This is the game board class. This class will intialize and build the board for the user.
//It updates the board and 
//As of right now it is an empty board 9 by 9 with dashes
//TODO:
//error check the randomize to make sure it is solvable
//allow user to have a little guess dictionary
namespace Sudoku{
    class Board{

        private string[,] board;

//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        
        //function name: buildEmptyBoard
        //builds an empty sudoku board 9 by 9
        
        private static string[,] buildEmptyboard(string[,] board){
        
           for(int i = 0; i< board.GetLength(0); i++){
               for(int j = 0; j < board.GetLength(1); j++){
                   board[i,j] = "-";        
               }
           }

           return board; 
        }

//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //function name: displayBoard
        //displays the board (9 by 9)
        public void displayBoard(string[,] board){

            Console.Write("     1   ");
            Console.Write("2   ");
            Console.Write("3   ");
            Console.Write("4   ");
            Console.Write("5   ");
            Console.Write("6   ");
            Console.Write("7   ");
            Console.Write("8   ");
            Console.Write("9   ");
            Console.WriteLine();
            Console.WriteLine();
            for(int i = 0; i< board.GetLength(0); i++){
                Console.Write((i+1).ToString() + "    ");
                for(int j = 0; j < board.GetLength(1); j++){
                    Console.Write(board[i,j] + "   ");
                }
                Console.Write("\n");
            }
        }

//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //function name: choiceBoard
        //this function generates the board based on the choice given.
        public string[,] choiceBoard(string[,] board, int choice){

            Console.WriteLine();
            board = buildEmptyboard(board);
            
            //based on user choice a board will be printed.
            if(choice == 1){
                Console.WriteLine("Here is the easy difficulty board.");
                board = generateRandomBoard(board, choice);

            }
            else if(choice == 2){
                Console.WriteLine("Here is the medium difficulty board.");
                board = generateRandomBoard(board,choice);

            }
            else if(choice == 3){
                Console.WriteLine("Here is the hard difficulty board.");
                board = generateRandomBoard(board,choice);

            }
            else{
                //Console.WriteLine("Incorrect input.");
                board = generateRandomBoard(board,choice);
            }

            return board;
        }

//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //function name: warningCheck
        //function to check if the surrounding area has the number trying to be inputted in their
        //It would break one of the rules of a 3 by 3, same col or row.
        private bool warningCheck(int row, int col, string inputs){

            bool safe = false;
            for(int j = 0; j < board.GetLength(0); j++){
                //checks the col to see if number is present their
                if(board[row, j].Equals(inputs)){
                    safe = true;
                    return safe;
                }
                //checks the row to see if number is present their
                if(board[j, col].Equals(inputs)){
                    safe = true;
                    return safe;
                }
                //checks the 3 by 3 location of the associated row
                int checkRowBox = row - row % 3;
                int checkColBox = col - col % 3;
                for(int k = checkRowBox; k < checkRowBox + 3; k++ ){
                    for(int l = checkColBox; l < checkColBox + 3; l++){
                        if(board[k,l].Equals(inputs)){
                            safe = true;
                            return safe;
                        }
                    }
                }
            }
            return safe;
        }

//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // private bool warningCheck(int row, int col, int numToPlace){

        //     bool safe = false;
        //     for(int j = 0; j < board.GetLength(0); j++){
        //         //checks the col to see if number is present their
        //         if(board[row, j].Equals(inputs)){
        //             safe = true;
        //             return safe;
        //         }
        //         //checks the row to see if number is present their
        //         if(board[j, col].Equals(inputs)){
        //             safe = true;
        //             return safe;
        //         }
        //         //checks the 3 by 3 location of the associated row
        //         int checkRowBox = row - row % 3;
        //         int checkColBox = col - col % 3;
        //         for(int k = checkRowBox; k < checkRowBox + 3; k++ ){
        //             for(int l = checkColBox; l < checkColBox + 3; l++){
        //                 if(board[k,l].Equals(inputs)){
        //                     safe = true;
        //                     return safe;
        //                 }
        //             }
        //         }
        //     }
        //     return safe;
        // }

//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //function name: inputBoard
        //this board will update based on the user input to actually play the game.
        //all outputs will be offset by 1, so the border and inputs start at 1.
        public string[,] inputBoard(string[,] board, ArrayList moves){

            //parses user input format
            Console.WriteLine();
            Console.WriteLine("Please enter your move as numbers: (Ex. 1 1 3)");
            bool canInput = true;
            string user = Console.ReadLine();
            bool check;
            Console.WriteLine();
            while(true){
                
                string[] inputs = user.Split(' ');

                if(user.ToUpper().Equals("QUIT") | user.ToUpper().Equals("SAVE")){
                    break;
                }
                //checks for inputs from user against already placed values to make sure they can't be overwritten
                for(int i = 0; i < moves.Count; i = i + 2){
                    if(inputs[0].Equals(moves[i]) && inputs[1].Equals(moves[i+1])){
                        canInput = false;
                        Console.WriteLine("That number can not be changed.");
                        Console.WriteLine("Press any key to continue.");
                        Console.ReadKey(true);
                        Console.WriteLine();
                    }
                }

                //can update bored as that value is changeable
                //after parsing input check if inputs are valid, if they are then update board.
                //Makes sure the input entered are within range (1-9)
                //makes sure the last input parsed is a number
                try{ //try to make sure input is actually in the right format
                    if(IsNumeric(inputs[2])){ //checks if input value is a number
                        if(Convert.ToInt32(inputs[2]) > 0 && Convert.ToInt32(inputs[2]) < 10){  //make sure value inputted  is between 1 and 9
                            try{
                                    //checks if value being placed is already in the row, col or 3 by 3
                                    check = warningCheck(Convert.ToInt32(inputs[0])-1, Convert.ToInt32(inputs[1])-1, inputs[2]);

                                    //give user a message about number breaking the rules
                                    if(check){
                                        Console.WriteLine("Remember that number exists in the row, col or 3x3");
                                        Console.WriteLine("Press any key to continue.");
                                        Console.ReadKey(true);
                                        Console.WriteLine();
                                    }
                                    
                                    if(canInput){
                                        board[(Convert.ToInt32(inputs[0])-1), (Convert.ToInt32(inputs[1])-1)] = inputs[2];
                                    }
                                    if(openSpace(board)){
                                        Console.WriteLine("hello");

                                    }
                            }
                            catch(FormatException){ //makes sure the input matches formation
                                Console.WriteLine("Wrong Format entered. Format is X Y Number between numbers 1-9.");
                                Console.WriteLine("Press any key to continue.");
                                Console.ReadKey(true);
                                Console.WriteLine();
                            }
                            catch(IndexOutOfRangeException){ //makes sure input was not out of range
                                Console.WriteLine("Inputs need to be in between numbers 1-9");
                                Console.WriteLine("Press any key to continue.");
                                Console.ReadKey(true);
                                Console.WriteLine();
                            }
                        }
                        else{ //error message
                            Console.WriteLine("The value at that position must be in between 1-9.");
                            Console.WriteLine("Press any key to continue.");
                            Console.ReadKey(true);
                            Console.WriteLine();
                        }
                    }
                    else{ //error message
                        Console.WriteLine("Wrong Format entered. Format is X Y Number between 1-9.");
                        Console.WriteLine("Press any key to continue.");
                        Console.ReadKey(true);
                        Console.WriteLine();
                    }
                }
                catch(IndexOutOfRangeException){ //make sure input is valid not just a bunch of random stuff
                    Console.WriteLine("Invalid input.");
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey(true);
                    Console.WriteLine();
                }
                

                //shows the positions on the board that can't be changed
                displayExistingMoves(moves);
                displayBoard(board);

                Console.WriteLine();
                canInput = true;

                //ask for userinput
                Console.WriteLine("Please enter your move: (Ex. 1 1 3):");
                Console.WriteLine("To exit the game type save or quit.");
                //try-catch for input make sure it is valid
                user = Console.ReadLine();
                Console.WriteLine();

            }

            return board;

        }

//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //function name: checkExistingMoves
        //a function that holds all current positions on the board
        //will be used to help make sure the original board is not altered
        //offset by one because the board boarders start at 1, so easier for user to type in
        public ArrayList checkExistingMoves(string[,] board){

            ArrayList storeExistingMoves = new ArrayList();

            for(int i = 0; i < board.GetLength(0); i++){
                for(int j = 0; j < board.GetLength(1); j++){
                    if(!board[i,j].Equals("-")){
                        storeExistingMoves.Add((i+1).ToString());
                        storeExistingMoves.Add((j+1).ToString());
                    }
                }
            }

            return storeExistingMoves;
        }

//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //function name: displayExistingMoves
        //function that displays all the exiting moves that can be used.
        private void displayExistingMoves(ArrayList moves){

            Console.WriteLine("The Positions that can't be changed:");

            if(moves.Count % 10 == 0){
                for(int i = 0; i< moves.Count; i = i + 10){
                    Console.Write(" (" + moves[i] + "," + moves[i+1] + ")");
                    Console.Write(" (" + moves[i+2] + "," + moves[i+3] + ")");
                    Console.Write(" (" + moves[i+4] + "," + moves[i+5] + ")");
                    Console.Write(" (" + moves[i+6] + "," + moves[i+7] + ")");
                    Console.WriteLine(" (" + moves[i+8] + "," + moves[i+9] + ")");
                }
                Console.WriteLine();
            }
            else if(moves.Count % 5  == 0){
                for(int i = 0; i< moves.Count; i = i + 6){
                    Console.Write(" (" + moves[i] + "," + moves[i+1] + ")");
                    Console.WriteLine(" (" + moves[i+2] + "," + moves[i+3] + ")");
                } 
            }
        }

//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //function name: solveBoard
        //this function will solve and tell the player if the board is solvable. 
        //If needed the user can see the solvedBoard
        public void solveBoard(string[,] board){


        }

//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //generates a board for different diffuculty.
        // simple backtracking algorithim
        /*Start with an empty board.
            loop through board, generate a random spot for the row and col. If spot is empty then proceed.
            Now generate number that will be placed their. 
            The number generated must pass the three cases:
                1. row must not have the number generated in it
                2. col must not have the number generated in it
                3. the 3 by 3 box must not have the number generated in it
            Then place number their and continue until it loops through entire board.
            The end of the function the board will be a solved board of sudoku.
        */
        //use recursion in case it doesn't fill all the spots.
        private string[,] generateRandomBoard(string[,] board, int choice){

            bool check;
            int[] numToPlace = new int[9]{1, 2, 3, 4, 5, 6, 7 , 8, 9};
            int row = -1;
            int col = -1;
            if(choice == 1 | choice == 2 | choice == 3){
                for(int i = 0; i < 81; i++){
                    //random location on the board for row and col
                    row = i/9;
                    col = i%9;
                    //valid to keep going since location is empty
                    if(board[row, col].Equals("-")){
                        //shuffle array of 1-9 to ranomize board.
                        numToPlace = shuffleArray(numToPlace);
                        for(int p = 0; p < numToPlace.Length; p++){
                            check = warningCheck(row, col, numToPlace[p].ToString());
                            //check is false than that means that number can be placed in that location.
                            //reset count afterwards
                            if(check == false){
                                board[row,col] = numToPlace[p].ToString();

                            } //end of if statement
                        }
                    } //end of if statement
                    
                } //end of for loop
                
            } //end of if statement

            if(openSpace(board) == false){
                return board;
            }
            else{
                board = buildEmptyboard(board);
                generateRandomBoard(board, choice);
            }

            return board;
        }

//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //function name: randArray
        //randomize the array from values 1-9
        private int[] shuffleArray(int[] numToPlace){

            var rand = new Random();

            //loop that starts from the back and iterates to the beginning
            for (int i = numToPlace.Length - 1; i > 0; i--) { 
              
                // Pick a random index 
                // from 0 to i 
                int randPosition = rand.Next(0, i+1); 
                
                // Swap the current position with the random position 
                int temp = numToPlace[i]; 
                numToPlace[i] = numToPlace[randPosition]; 
                numToPlace[randPosition] = temp; 
            }

            // for (int i = 0; i < numToPlace.Length; i++) {
            //     Console.Write(numToPlace[i] + " ");
            // }
            //     Console.WriteLine();

            return numToPlace;
        }
//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        //setter for gameboard
        public void setBoard(string[,] board){
            this.board = board;
        }

//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //getter for the board
        public string[,] getBoard(){
            return board;
        }

//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //function name: Isnumeric
        //checks if the string input is a number
        private bool IsNumeric(string value)
        {
            return value.All(char.IsNumber);
        }
        
//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //function name: openspace
        // checks if the board has has open spot
        private bool openSpace(string[,] board){

            for(int i = 0; i < board.GetLength(0); i++){
                for(int j = 0; j < board.GetLength(0); j++){
                    if(board[i,j].Equals("-")){
                        return true;
                    }
                }
            }
            return false;
        }

//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


    }
}
