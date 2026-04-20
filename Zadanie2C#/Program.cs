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
    }

    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
