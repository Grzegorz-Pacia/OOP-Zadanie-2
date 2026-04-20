using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie2C_
{
    class Player
    {
        private string playerName { get; set; }
        private char playerSymbol { get; set; }

        public Player(string playerName, char playerSymbol)
        {
            this.playerName = playerName;
            this.playerSymbol = playerSymbol;
        }

        public string getPlayerName() { return playerName; }

        public char getPlayerSymbol() { return playerSymbol; }
    }

    class Board
    {
        private char[,] boardContents;

        public Board()
        {
            this.boardContents = new char[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    this.boardContents[i, j] = ' ';
                }
            }
        }

        public char GetTileContent(int x, int y)
        {
            return boardContents[x, y];
        }

        public bool SetTileContent(int x, int y, char symbol)
        {
            if (boardContents[x, y] == 'X' || boardContents[x, y] == 'O')
            {
                return false;
            }
            else
            {
                boardContents[x, y] = symbol;
                return true;
            }
        }

        public char CheckWinCondition()
        {
            char[] possibleWinner = { ' ', ' ', ' ' };

            if ((boardContents[0, 0] == boardContents[1, 1] && boardContents[1, 1] == boardContents[2, 2]) ||
                (boardContents[0, 2] == boardContents[1, 1] && boardContents[1, 1] == boardContents[2, 0]) ||
                (boardContents[0, 1] == boardContents[1, 1] && boardContents[1, 1] == boardContents[2, 1]) ||
                (boardContents[1, 0] == boardContents[1, 1] && boardContents[1, 1] == boardContents[1, 2]))
            {
                possibleWinner[0] = boardContents[1, 1];
            }
            else if ((boardContents[0, 0] == boardContents[0, 1] && boardContents[0, 1] == boardContents[0, 2]) ||
                (boardContents[0, 0] == boardContents[1, 0] && boardContents[1, 0] == boardContents[2, 0]))
            {
                possibleWinner[1] = boardContents[0, 0];
            }
            else if ((boardContents[2, 2] == boardContents[2, 1] && boardContents[2, 1] == boardContents[2, 0]) ||
                (boardContents[2, 2] == boardContents[1, 2] && boardContents[1, 2] == boardContents[0, 2]))
            {
                possibleWinner[2] = boardContents[2, 2];
            }

            foreach (char symbol in possibleWinner)
            {
                if (symbol != ' ')
                {
                    return symbol;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (boardContents[i, j] == ' ')
                    {
                        return ' ';
                    }
                }
            }

            return 'T';
        }

        public void DisplayBoardContents()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(boardContents[i, j]);
                    if (j != 2)
                    {
                        Console.Write("|");
                    }
                }
                Console.WriteLine();
                if (i != 2) 
                {
                    Console.WriteLine("-+-+-");
                }
            }
        }
    }

    class Game
    {
        private Board board;
        private Player[] players;
        private int currentPlayer;

        public Game(string player1Name, string player2Name)
        {
            this.board = new Board();
            this.players = new Player[2];
            this.players[0] = new Player(player1Name, 'O');
            this.players[1] = new Player(player2Name, 'X');
            this.currentPlayer = 0;
        }

        public bool nextTurn()
        {
            Console.Clear();
            Console.WriteLine("Tura gracza " + players[currentPlayer].getPlayerName() + ".");
            board.DisplayBoardContents();
            Console.WriteLine("Podaj pole, na którym chcesz umieścić symbol \"" + players[currentPlayer].getPlayerSymbol() + "\" w postaci \"x y\",\n" +
                              "gdzie x to numer wiersza, a y to numer kolumny:");
            do
            {
                string targetTile = Console.ReadLine();
                if (targetTile.Length == 3 && targetTile[1] == ' '
                   && (targetTile[0] == '1' || targetTile[0] == '2' || targetTile[0] == '3')
                   && (targetTile[2] == '1' || targetTile[2] == '2' || targetTile[2] == '3')
                   && (board.GetTileContent((targetTile[0]-'1'), (targetTile[2]-'1')) == ' '))
                {
                    board.SetTileContent((targetTile[0]-'1'), (targetTile[2]-'1'), players[currentPlayer].getPlayerSymbol());
                    break;
                }
                else
                {
                    Console.WriteLine("Podano pole docelowe w nieprawidłowej postaci lub podano pole, które jest już zajęte.\n" +
                                      "Spróbuj ponownie:");
                }
            } while(true);

            if (board.CheckWinCondition() == players[currentPlayer].getPlayerSymbol())
            {
                Console.Clear();
                board.DisplayBoardContents();
                Console.WriteLine("Zwyciężą gracz " + players[currentPlayer].getPlayerName() + "!");
                return false;
            }
            else if (board.CheckWinCondition() == 'T')
            {
                Console.Clear();
                board.DisplayBoardContents();
                Console.WriteLine("Remis!");
                return false;
            }
            currentPlayer = Math.Abs(currentPlayer - 1);
            return true;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Game game1 = new Game("Gracz 1", "Gracz 2");
            while(game1.nextTurn());
        }
    }
}
