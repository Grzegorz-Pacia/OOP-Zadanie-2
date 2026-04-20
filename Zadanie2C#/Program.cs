using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie2C_
{
    class Player
    {
        private string playerName { get; set; } //nazwa gracza
        private char playerSymbol { get; set; } //symbol gracza (X lub O)

        public Player(string playerName, char playerSymbol) //ustawia nazwę i symbol gracza
        {
            this.playerName = playerName;
            this.playerSymbol = playerSymbol;
        }

        public string getPlayerName() { return playerName; }

        public char getPlayerSymbol() { return playerSymbol; }
    }

    class Board
    {
        private char[,] boardContents; //tablica, której elementy stanowią pola planszy

        public Board() //tworzy tablicę 3x3 reprezentującą planszę i wypełnia ją spacjami
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

        public char GetTileContent(int x, int y) //zwraca zawartość danego pola
        {
            return boardContents[x, y];
        }

        public void SetTileContent(int x, int y, char symbol) //ustawia podany symbol jako zawartość danego pola
        {
                boardContents[x, y] = symbol;
        }

        public char CheckWinCondition() //sprawdza, czy któryś gracz zwyciężył, doszło do remisu lub można dalej gracz
        {
            char[] possibleWinner = { ' ', ' ', ' ' }; //przechowuje wartości pól [1, 1], [0, 0] oraz [2, 2] jeśli zostanie wykryte zwyciężające ustawienie znaków. Zwycięstwo zostanie wykryte również, gdy będą to 3 spacje, więc są to tylko potencjalne zwycięstwa.

            if ((boardContents[0, 0] == boardContents[1, 1] && boardContents[1, 1] == boardContents[2, 2]) || //wykrywa zwycięstwa zawierające pole [1, 1]
                (boardContents[0, 2] == boardContents[1, 1] && boardContents[1, 1] == boardContents[2, 0]) ||
                (boardContents[0, 1] == boardContents[1, 1] && boardContents[1, 1] == boardContents[2, 1]) ||
                (boardContents[1, 0] == boardContents[1, 1] && boardContents[1, 1] == boardContents[1, 2]))
            {
                possibleWinner[0] = boardContents[1, 1];
            }
            else if ((boardContents[0, 0] == boardContents[0, 1] && boardContents[0, 1] == boardContents[0, 2]) || //wykrywa zwycięstwa zawierające pole [0, 0]
                (boardContents[0, 0] == boardContents[1, 0] && boardContents[1, 0] == boardContents[2, 0]))
            {
                possibleWinner[1] = boardContents[0, 0];
            }
            else if ((boardContents[2, 2] == boardContents[2, 1] && boardContents[2, 1] == boardContents[2, 0]) || //wykrywa zwycięstwa zawierające pole [2, 2]
                (boardContents[2, 2] == boardContents[1, 2] && boardContents[1, 2] == boardContents[0, 2]))
            {
                possibleWinner[2] = boardContents[2, 2];
            }

            foreach (char symbol in possibleWinner) //jeśli któreś potencjalne zwycięstwo nie jest spacją - zwraca odpowiadający mu znak
            {
                if (symbol != ' ')
                {
                    return symbol;
                }
            }

            for (int i = 0; i < 3; i++) //wyszukuje spacje w tablicy. Jeśli jakąś znajdzie, zwraca spację, co oznacza, że można dalej grać.
            {
                for (int j = 0; j < 3; j++)
                {
                    if (boardContents[i, j] == ' ')
                    {
                        return ' ';
                    }
                }
            }

            return 'T'; //jeśli żaden ze wcześniejszych warunków nie został spełniony, zwraca T sygnalizujące remis
        }

        public void DisplayBoardContents() //wypisuje w konsoli zawartość tablicy wraz z liniami oddzielającymi pola
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
        private Board board; //przechowuje planszę do danej gry
        private Player[] players; //przechowuje obu graczy danej gry
        private int currentPlayer; //który gracz w tablicy będzie wykonywał następną turę

        public Game(string player1Name, string player2Name) //tworzy planszę, tablicę graczy i umieszcza w niej dwóch graczy o podanych nazwach i odpowiednich symbolach, ustawia pierwszego z nich jako wykonującego następną turę
        {
            this.board = new Board();
            this.players = new Player[2];
            this.players[0] = new Player(player1Name, 'O');
            this.players[1] = new Player(player2Name, 'X');
            this.currentPlayer = 0;
        }

        public bool NextTurn() //przeprowadza kolejną turę gry
        {
            Console.Clear();
            Console.WriteLine("Tura gracza " + players[currentPlayer].getPlayerName() + ".\n");
            board.DisplayBoardContents();
            Console.WriteLine("\nPodaj pole, na którym chcesz umieścić symbol \"" + players[currentPlayer].getPlayerSymbol() + "\", w postaci \"x y\",\n" +
                              "gdzie x to numer wiersza, a y to numer kolumny:");
            do //powtarza się do momentu, gdy obecny gracz w poprawny sposób poda niezajęte pole
            {
                string targetTile = Console.ReadLine();
                if (targetTile.Length == 3 && targetTile[1] == ' '
                   && (targetTile[0] == '1' || targetTile[0] == '2' || targetTile[0] == '3')
                   && (targetTile[2] == '1' || targetTile[2] == '2' || targetTile[2] == '3')
                   && (board.GetTileContent((targetTile[0]-'1'), (targetTile[2]-'1')) == ' '))
                {
                    board.SetTileContent((targetTile[0]-'1'), (targetTile[2]-'1'), players[currentPlayer].getPlayerSymbol()); //odjęcie '1' od wartości char daje tu poprawną wartość int pola w tablicy (indexowanie od 0)
                    break;
                }
                else
                {
                    Console.WriteLine("Podano pole docelowe w nieprawidłowej postaci lub podano pole, które jest już zajęte.\n" +
                                      "Spróbuj ponownie:");
                }
            } while(true);

            if (board.CheckWinCondition() == players[currentPlayer].getPlayerSymbol()) //sprawdza, czy obecny gracz wygrał i kończy grę
            {
                Console.Clear();
                board.DisplayBoardContents();
                Console.WriteLine("\nZwycięża gracz " + players[currentPlayer].getPlayerName() + "!");
                return false; //gra się zakończyła
            }
            else if (board.CheckWinCondition() == 'T') //sprawdza, czy doszło do remisu i kończy grę
            {
                Console.Clear();
                board.DisplayBoardContents();
                Console.WriteLine("\nRemis!");
                return false; //gra się zakończyła
            }
            currentPlayer = Math.Abs(currentPlayer - 1); //zamienia obecnego gracza
            return true; //gra jest kontynuowana
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Game game1 = new Game("Gracz 1", "Gracz 2"); //tworzy obiekt gry, którego konstruktor tworzy dwa obiekty graczy (z podanymi nazwami) i obiekt planszy
            while(game1.NextTurn()); //powtarza się do momentu, gdy metoda NextTurn zwróci false, czyli momentu do zakończenia się gry
        }
    }
}
