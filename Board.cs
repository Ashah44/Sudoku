using System.Collections;
using System;


//This is the game board class. This class will intialize and build the board for the user.
//As of right now it is an empty board 9 by 9 with dashes
//TODO:
//error check the randomize to make sure it is solvable
//Implement the updating of the board and gameplay.

namespace Sudoku{
    class Board{

        private string[,] board;

        //builds an empty sudoku board 9 by 9
        private static string[,] buildEmptyboard(string[,] board){
        
           for(int i = 0; i< board.GetLength(0); i++){
               for(int j = 0; j < board.GetLength(1); j++){
                   board[i,j] = "-";        
               }
           }

           return board; 
        }

        //displays an empty board (9 by 9)
        public void displayBoard(string[,] board, int choice){

            Console.WriteLine();
            board = buildEmptyboard(this.board);

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
            Console.Write("     1  ");
            Console.Write("2  ");
            Console.Write("3  ");
            Console.Write("4  ");
            Console.Write("5  ");
            Console.Write("6  ");
            Console.Write("7  ");
            Console.Write("8  ");
            Console.Write("9  ");
            Console.WriteLine();
            Console.WriteLine();
            for(int i = 0; i< board.GetLength(0); i++){
                Console.Write((i+1).ToString() + "    ");
                for(int j = 0; j < board.GetLength(1); j++){
                    Console.Write(board[i,j] + "  ");
                }
                Console.Write("\n");
            }

            Console.ReadKey();
        }
        
        private static void easyBoard(string[,] board){

            

        }

        //generate a board for different diffuculty.
        //TODO:
        //check the 3 by 3 we are in, check the row and column
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
                for(int i = 0; i < board.Length; i++){
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
                            else if(choice == 2 && amountofNumbers == 30){
                                return board;
                            }
                            else if(choice == 1 && amountofNumbers == 40){
                                return board;
                            }

                        }
                        count = 0;
                    }
                }
            }

            return board;
        }


        //setter for gameboard
        public void setBoard(string[,] board){
            this.board = board;
        }

        //getter for the board
        public string[,] getBoard(){
            return board;
        }
        

    }
}
