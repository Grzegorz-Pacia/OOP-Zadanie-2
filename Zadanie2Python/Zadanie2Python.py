import os

class Player:
    def __init__(self, playerName, playerSymbol):
        self.__playerName = playerName #nazwa gracza
        self.__playerSymbol = playerSymbol #symbol gracza (X lub O)
    
    def GetPlayerName(self):
        return self.__playerName
    
    def GetPlayerSymbol(self):
        return self.__playerSymbol

class Board:
    def __init__(self):
        self.__boardContents = [[' ', ' ', ' '], [' ', ' ', ' '], [' ', ' ', ' ']] #tablica, której elementy stanowią pola planszy
    
    def GetTileContent(self, x, y): #zwraca zawartość danego pola
        return self.__boardContents[x][y]
    
    def SetTileContent(self, x, y, symbol): #ustawia podany symbol jako zawartość danego pola
        self.__boardContents[x][y] = symbol
    
    def CheckWinCondition(self): #sprawdza, czy któryś gracz zwyciężył, doszło do remisu lub można dalej gracz
        possibleWinner = [' ', ' ', ' '] #przechowuje wartości pól [1, 1], [0, 0] oraz [2, 2] jeśli zostanie wykryte zwyciężające ustawienie znaków. Zwycięstwo zostanie wykryte również, gdy będą to 3 spacje, więc są to tylko potencjalne zwycięstwa.

        if (self.__boardContents[0][0] == self.__boardContents[1][1] and self.__boardContents[1][1] == self.__boardContents[2][2]) or\
           (self.__boardContents[0][2] == self.__boardContents[1][1] and self.__boardContents[1][1] == self.__boardContents[2][0]) or\
           (self.__boardContents[0][1] == self.__boardContents[1][1] and self.__boardContents[1][1] == self.__boardContents[2][1]) or\
           (self.__boardContents[1][0] == self.__boardContents[1][1] and self.__boardContents[1][1] == self.__boardContents[1][2]): #wykrywa zwycięstwa zawierające pole [1, 1]
            possibleWinner[0] = self.__boardContents[1][1]
        elif (self.__boardContents[0][0] == self.__boardContents[0][1] and self.__boardContents[0][1] == self.__boardContents[0][2]) or\
             (self.__boardContents[0][0] == self.__boardContents[1][0] and self.__boardContents[1][0] == self.__boardContents[2][0]): #wykrywa zwycięstwa zawierające pole [0, 0]
            possibleWinner[1] = self.__boardContents[0][0]
        elif (self.__boardContents[2][2] == self.__boardContents[2][1] and self.__boardContents[2][1] == self.__boardContents[2][0]) or\
             (self.__boardContents[2][2] == self.__boardContents[1][2] and self.__boardContents[1][2] == self.__boardContents[0][2]): #wykrywa zwycięstwa zawierające pole [2, 2]
            possibleWinner[2] = self.__boardContents[2][2]
        
        for symbol in possibleWinner: #jeśli któreś potencjalne zwycięstwo nie jest spacją - zwraca odpowiadający mu znak
            if symbol != ' ':
                return symbol
        
        for i in range(3): #wyszukuje spacje w tablicy. Jeśli jakąś znajdzie, zwraca spację, co oznacza, że można dalej grać.
            for j in range(3):
                if self.__boardContents[i][j] == ' ':
                    return ' '
        
        return 'T' #jeśli żaden ze wcześniejszych warunków nie został spełniony, zwraca T sygnalizujące remis
    
    def DisplayBoardContents(self): #wypisuje w konsoli zawartość tablicy wraz z liniami oddzielającymi pola
        for i in range(0, 3):
            for j in range(0, 3):
                print(self.__boardContents[i][j], end='')
                if j != 2:
                    print('|', end='')
            print()
            if i != 2:
                print('-+-+-')
    
class Game:
    def __init__(self, player1Name, player2Name):
        self.__board = Board() #przechowuje planszę do danej gry
        self.__players = [Player(player1Name, 'O'), Player(player2Name, 'X')] #przechowuje obu graczy danej gry
        self.__currentPlayer = 0 #który gracz w tablicy będzie wykonywał następną turę
    
    def NextTurn(self): #przeprowadza kolejną turę gry
        print('Tura gracza ' + self.__players[self.__currentPlayer].GetPlayerName() + ".\n")
        self.__board.DisplayBoardContents()
        print('\nPodaj pole, na którym chcesz umieścić symbol \"' + self.__players[self.__currentPlayer].GetPlayerSymbol() + '\", w postaci \"x y\",\n' +\
              'gdzie x to numer wiersza, a y to numer kolumny:')
        while True: #powtarza się do momentu, gdy obecny gracz w poprawny sposób poda niezajęte pole
            targetTile = input()
            if len(targetTile) == 3 and targetTile[1] == ' '\
            and (targetTile[0] == '1' or targetTile[0] == '2' or targetTile[0] == '3')\
            and (targetTile[2] == '1' or targetTile[2] == '2' or targetTile[2] == '3')\
            and (self.__board.GetTileContent((int(targetTile[0])-1), (int(targetTile[2])-1)) == ' '):
                self.__board.SetTileContent((int(targetTile[0])-1), (int(targetTile[2])-1), self.__players[self.__currentPlayer].GetPlayerSymbol())
                break
            else:
                print('Podano pole docelowe w nieprawidłowej postaci lub podano pole, które jest już zajęte.\n' +\
                      'Spróbuj ponownie:')
        
        if self.__board.CheckWinCondition() == self.__players[self.__currentPlayer].GetPlayerSymbol(): #sprawdza, czy obecny gracz wygrał i kończy grę
            print()
            self.__board.DisplayBoardContents()
            print('\nZwycięża gracz ' + self.__players[self.__currentPlayer].GetPlayerName() + '!')
            return False #gra się zakończyła
        elif self.__board.CheckWinCondition() == 'T': #sprawdza, czy doszło do remisu i kończy grę
            print()
            self.__board.DisplayBoardContents()
            print('\nRemis!')
            return False #gra się zakończyła
        self.__currentPlayer = abs(self.__currentPlayer - 1) #zamienia obecnego gracza
        print()
        return True #gra jest kontynuowana

def main():
    game1 = Game('Gracz 1', 'Gracz 2') #tworzy obiekt gry, którego konstruktor tworzy dwa obiekty graczy (z podanymi nazwami) i obiekt planszy
    playing = True
    while(playing): #powtarza się do momentu, gdy metoda NextTurn zwróci false, czyli do zakończenia się gry
        playing = game1.NextTurn()

if __name__ == "__main__":
    main()