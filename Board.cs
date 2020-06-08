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
                board = generateDifficultyBoard(board, choice);

            }
            else if(choice == 2){
                Console.WriteLine("Here is the medium difficulty board.");
                board = generateDifficultyBoard(board, choice);
            }
            else if(choice == 3){
                Console.WriteLine("Here is the hard difficulty board.");
                board = generateDifficultyBoard(board, choice);

            }
            else{
                //Console.WriteLine("Incorrect input.");
                board = buildEmptyboard(board);
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

        //function name: inputBoard
        //this board will update based on the user input to actually play the game.
        //all outputs will be offset by 1, so the border and inputs start at 1.
        public string[,] inputBoard(string[,] board, ArrayList moves){

            //parses user input format
            Console.WriteLine();
            Console.WriteLine("Please enter your move as numbers: (Ex. 1 1 3)");
            bool canInput = true;
            bool winner;
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

                            } //end of try
                            catch(FormatException){ //makes sure the input matches formation
                                Console.WriteLine("Wrong Format entered. Format is X Y Number between numbers 1-9.");
                                Console.WriteLine("Press any key to continue.");
                                Console.ReadKey(true);
                                Console.WriteLine();
                            } //end of catch
                            catch(IndexOutOfRangeException){ //makes sure input was not out of range
                                Console.WriteLine("Inputs need to be in between numbers 1-9");
                                Console.WriteLine("Press any key to continue.");
                                Console.ReadKey(true);
                                Console.WriteLine();
                            } //end of catch
                        } //end of if statement
                        else{ //error message
                            Console.WriteLine("The value at that position must be in between 1-9.");
                            Console.WriteLine("Press any key to continue.");
                            Console.ReadKey(true);
                            Console.WriteLine();
                        } //end of else
                    } //end of if statement
                    else{ //error message
                        Console.WriteLine("Wrong Format entered. Format is X Y Number between 1-9.");
                        Console.WriteLine("Press any key to continue.");
                        Console.ReadKey(true);
                        Console.WriteLine();
                    } //end of else
                } //end of try block
                catch(IndexOutOfRangeException){ //make sure input is valid not just a bunch of random stuff
                    Console.WriteLine("Invalid input.");
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey(true);
                    Console.WriteLine();
                } //end of catch
                

                //shows the positions on the board that can't be changed
                displayExistingMoves(moves);
                displayBoard(board);

                Console.WriteLine();
                canInput = true;

                //still openSpace to be put on board
                if(openSpace(board)== false){
                    winner = checkSolvedBoard(board);
                    if(winner){
                        Console.WriteLine("Congratulations, you win.");
                        break;
                    }
                    else{
                        Console.WriteLine("Looks like you made a mistake.");
                        Console.WriteLine("Try going through again and fixing your mistake.");
                        Console.WriteLine();
                    }
                }
                
                //ask for userinput
                Console.WriteLine("Please enter your move: (Ex. 1 1 3):");
                Console.WriteLine("To exit the game type save or quit.");
                //try-catch for input make sure it is valid
                user = Console.ReadLine();
                Console.WriteLine();

            } //end of while loop

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
                    } //end of if statement
                } //end of for loop
            } //end of for loop

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
                } //end of for loop
                Console.WriteLine();
            } //end of if statement
            else if(moves.Count % 5  == 0){
                for(int i = 0; i< moves.Count; i = i + 6){
                    Console.Write(" (" + moves[i] + "," + moves[i+1] + ")");
                    Console.WriteLine(" (" + moves[i+2] + "," + moves[i+3] + ")");
                } //end of for loop
                Console.WriteLine();
            } //end of else if statement
        }

//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //function name: checkSolvedBoard
        //this function will check if the board is solved.
        //If needed the user can see the solvedBoard - needs implementing
        //doesn't use backtracing
        private bool checkSolvedBoard(string[,] board){

            //check if every row,col and 3 by 3 on the board is equal to the sum of 1-9.
            int sum = 0;
            int target = 45;
            bool solved = false;
            int counter = 0;

            //row checker
            for(int i = 0; i < board.GetLength(0); i++){
                for(int j = 0; j < board.GetLength(1); j++){
                    sum = sum + Convert.ToInt32(board[i,j]);
                    if(sum == target){
                        counter = counter + 1;
                        sum = 0;
                    }
                }
            }

            //col checker
            for(int i = 0; i < board.GetLength(0); i++){
                for(int j = 0; j < board.GetLength(1); j++){
                    sum = sum + Convert.ToInt32(board[j,i]);
                    if(sum == target){
                        counter = counter + 1;
                        sum = 0;
                    }
                }
            }

            //3 by 3 checker
            for(int i = 0; i < board.GetLength(0); i = i + 3 ){
                for(int j = 0; j < board.GetLength(1); j = j + 3){
                //checks the 3 by 3 location of the associated row
                    int checkRowBox = i - i % 3;
                    int checkColBox = j - j % 3;
                    for(int k = checkRowBox; k < checkRowBox + 3; k++ ){
                        for(int l = checkColBox; l < checkColBox + 3; l++){
                            sum = sum + Convert.ToInt32(board[k,l]);
                            if(sum == target){
                                counter = counter + 1;
                                sum = 0;
                            }
                            
                        }
                    }
                }
            }

            //if counter has hit 27 that means it passed all 3 checks and you win.
            if(counter == 27){
                solved = true;
            }
            
            return solved;
        }
//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        // //function name: checkSolvedBoard
        // //this function will check if the board is solved.
        // //If needed the user can see the solvedBoard - needs implementing
        // //users backtracing.
        // public bool checkSolvedBoard(string[,] board){

        //     return false;
        // }

//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //function name: generateDifficultyBoard
        //generates board for three different difficulties.
        
        //hard version will have 20 numbers on the board (choice == 3)
        //medium version will have 25 numbers on the board(choice == 2)
        //easy version will have 30 numbers on the board(choice == 1)
        private string[,] generateDifficultyBoard(string[,] board, int choice){


            //generates a fully solved board
            //now strip away some numbers to let user solve
            board = generateRandomBoard(board);
            var rand = new Random();
            int deleteNumber = 0;
            while(deleteNumber != 500){
                int row = rand.Next(0, 9);
                int col = rand.Next(0, 9);

                if(!board[row, col].Equals("-")){
                    board[row, col] = "-";
                    deleteNumber = deleteNumber + 1;
                }

                if(choice == 3 && deleteNumber == 61){
                    return board;
                }
                else if(choice == 2 && deleteNumber == 56){
                    return board;
                }
                else if(choice == 1 && deleteNumber == 51){
                    return board;
                }
            }

            return board;
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
        private string[,] generateRandomBoard(string[,] board){

            bool check;
            int[] numToPlace = new int[9]{1, 2, 3, 4, 5, 6, 7 , 8, 9};
            int row = -1;
            int col = -1;

            for(int i = 0; i < 81; i++){
                //random location on the board for row and col
                row = i/9;
                col = i%9;
                //valid to keep going since location is empty
                if(board[row, col].Equals("-")){
                    //shuffle array of 1-9 to randomize board.
                    numToPlace = shuffleArray(numToPlace);
                    for(int p = 0; p < numToPlace.Length; p++){
                        check = warningCheck(row, col, numToPlace[p].ToString());
                        //check is false than that means that number can be placed in that location.
                        if(check == false){
                            board[row,col] = numToPlace[p].ToString();

                        } //end of if statement
                    }
                } //end of if statement
                
            } //end of for loop

            //check to see if board is solve, if not then destroy current board and try again
            if(openSpace(board) == false){
                return board;
            } //end of if statement
            else{
                board = buildEmptyboard(board);
                generateRandomBoard(board);
            } //end of else statement

            return board;
        }

//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //function name: randArray
        //randomize the array from values 1-9
        private int[] shuffleArray(int[] numToPlace){

            var rand = new Random();

            //loop through the array that has 1-9 and then shuffle the position randomly
            for (int i = 0; i < numToPlace.Length; i++) { 
              
                // Pick a random index from the array to use
                int randPosition = rand.Next(i, numToPlace.Length); 
                
                // Swap the current position with the random position 
                int temp = numToPlace[i]; 
                numToPlace[i] = numToPlace[randPosition]; 
                numToPlace[randPosition] = temp; 
            } //end of for loop

            //checks the arranged values to make sure they work
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

            //has open space
            for(int i = 0; i < board.GetLength(0); i++){
                for(int j = 0; j < board.GetLength(0); j++){
                    if(board[i,j].Equals("-")){
                        return true;
                    }
                }
            }

            // no more opens spaces check for win
            return false;
        }

//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


    }
}
