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


        //function to check if the surrounding area has the number trying to be inputted in their
        //It would break one of the rules of a 3 by 3, same col or row.
        public bool warningCheck(int row, int col, string inputs){

            bool safe = false;
            for(int j = 0; j < board.GetLength(0); j++){
                //checks the row to see if number is present their
                if(board[row, j].Equals(inputs)){
                    Console.WriteLine(board[row,j]);
                    safe = true;
                }
                //checks the col to see if number is present their
                if(board[j, col].Equals(inputs)){
                    safe = true;
                }
                //checks the 3 by 3 location of the associated row
                int checkRowBox = row - row % 3;
                int checkColBox = col - col % 3;
                for(int k = checkRowBox; k < checkRowBox + 3; k++ ){
                    for(int l = checkColBox; l < checkColBox + 3; l++){
                        if(board[k,l].Equals(inputs)){
                            safe = true;
                        }
                    }
                }
            }
            return safe;
        }

//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        //this board will update based on the user input to actually play the game.
        //all outputs will be offset by 1, so the border and inputs start at 1.
        public string[,] inputBoard(string[,] board, ArrayList moves){

            //parses user input format
            Console.WriteLine("Please enter your move as numbers: (Ex. 1 1 3)");
            bool canInput = true;
            string user = Console.ReadLine();
            bool check;
            Console.WriteLine();
            while(true){
                
                string[] inputs = user.Split(' ');

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
                //makes sure the last input parsed is a number
                if(IsNumeric(inputs[2])){

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
                    }
                    catch(FormatException){
                        Console.WriteLine("Wrong Format entered. Format is X Y Number between 1-9");
                        Console.WriteLine("Press any key to continue.");
                        Console.ReadKey(true);
                        Console.WriteLine();
                    }
                }
                else{

                        Console.WriteLine("Wrong Format entered. Format is X Y Number between 1-9");
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
                Console.WriteLine("Please enter your move: (Ex. 1(x) 1(y) 3(number):");
                //try-catch for input make sure it is valid
                user = Console.ReadLine();

            }

            return board;

        }

//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


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
            Then play the game.
        */
        private string[,] generateRandomBoard(string[,] board, int choice){

            var number = new Random();
            var rand = new Random();
            int count = 0;
            int amountofNumbers = 0;
            if(choice == 1 | choice == 2 | choice == 3){
                for(int i = 0; i < board.Length*2; i++){
                    //random location on the board for row and col
                    int row = rand.Next(0,9);
                    int col = rand.Next(0,9);
                    //valid to keep going since location is empty
                    if(board[row, col] == "-"){
                        //now check if number generated is possible in that row, column or 3 by 3
                        int numToPlace = rand.Next(1,10);
                        //check rows
                        for(int j = 0; j < board.GetLength(0); j++){
                            //checks the row to see if number is present their
                            if(board[row, j].Equals(numToPlace.ToString())){
                                count = count + 1;
                            }
                            //checks the col to see if number is present their
                            if(board[j, col].Equals(numToPlace.ToString())){
                                count = count + 1;
                            }
                            //checks the 3 by 3 location of the associated row
                            int checkRowBox = row - row % 3;
                            int checkColBox = col - col % 3;
                            for(int k = checkRowBox; k < checkRowBox + 3; k++ ){
                                for(int l = checkColBox; l < checkColBox + 3; l++){
                                    if(board[k,l].Equals(numToPlace.ToString())){
                                        count = count + 1;
                                    }
                                }
                            }
                        }

                        //count of 0 means that number can be placed in that location.
                        //reset count afterwards
                        if(count == 0){
                            board[row,col] = numToPlace.ToString();
                            amountofNumbers = amountofNumbers + 1;
                            //hard version will have 20 numbers on the board (choice == 3)
                            //medium version will have 30 numbers on the board(choice == 2)
                            //easy version will have 40 numbers on the board(choice == 1)
                            if(choice == 3 && amountofNumbers == 20){
                                return board;
                            }
                            else if(choice == 2 && amountofNumbers == 25){
                                return board;
                            }
                            else if(choice == 1 && amountofNumbers == 30){
                                return board;
                            }

                        }
                        count = 0;
                    }
                }
            }

            return board;
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
       
        private bool IsNumeric(string value)
        {
            return value.All(char.IsNumber);
        }
        

    }
}
