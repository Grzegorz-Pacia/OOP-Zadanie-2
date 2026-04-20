import os

class Player:
    def __init__(self, playerName, playerSymbol):
        self.__playerName = playerName
        self.__playerSymbol = playerSymbol
    
    def GetPlayerName(self):
        return self.__playerName
    
    def GetPlayerSymbol(self):
        return self.__playerSymbol

class Board:
    def __init__(self):
        self.__boardContents = [[' ', ' ', ' '], [' ', ' ', ' '], [' ', ' ', ' ']]
    
    def GetTileContent(self, x, y):
        return self.__boardContents[x][y]
    
    def SetTileContent(self, x, y, symbol):
        self.__boardContents[x][y] = symbol
    
    def CheckWinCondition(self):
        possibleWinner = [' ', ' ', ' ']

        if (self.__boardContents[0][0] == self.__boardContents[1][1] and self.__boardContents[1][1] == self.__boardContents[2][2]) or\
           (self.__boardContents[0][2] == self.__boardContents[1][1] and self.__boardContents[1][1] == self.__boardContents[2][0]) or\
           (self.__boardContents[0][1] == self.__boardContents[1][1] and self.__boardContents[1][1] == self.__boardContents[2][1]) or\
           (self.__boardContents[1][0] == self.__boardContents[1][1] and self.__boardContents[1][1] == self.__boardContents[1][2]):
            possibleWinner[0] = self.__boardContents[1][1]
        elif (self.__boardContents[0][0] == self.__boardContents[0][1] and self.__boardContents[0][1] == self.__boardContents[0][2]) or\
             (self.__boardContents[0][0] == self.__boardContents[1][0] and self.__boardContents[1][0] == self.__boardContents[2][0]):
            possibleWinner[1] = self.__boardContents[0][0]
        elif (self.__boardContents[2][2] == self.__boardContents[2][1] and self.__boardContents[2][1] == self.__boardContents[2][0]) or\
             (self.__boardContents[2][2] == self.__boardContents[1][2] and self.__boardContents[1][2] == self.__boardContents[0][2]):
            possibleWinner[2] = self.__boardContents[2][2]
        
        for symbol in possibleWinner:
            if symbol != ' ':
                return symbol
        
        for i in range(3):
            for j in range(3):
                if self.__boardContents[i][j] == ' ':
                    return ' '
        
        return 'T'
    
    def DisplayBoardContents(self):
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
        self.__board = Board()
        self.__players = [Player(player1Name, 'O'), Player(player2Name, 'X')]
        self.__currentPlayer = 0
    
    def NextTurn(self):
        print('Tura gracza ' + self.__players[self.__currentPlayer].GetPlayerName() + ".\n")
        self.__board.DisplayBoardContents()
        print('\nPodaj pole, na którym chcesz umieścić symbol \"' + self.__players[self.__currentPlayer].GetPlayerSymbol() + '\", w postaci \"x y\",\n' +\
              'gdzie x to numer wiersza, a y to numer kolumny:')
        while True:
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
        
        if self.__board.CheckWinCondition() == self.__players[self.__currentPlayer].GetPlayerSymbol():
            print()
            self.__board.DisplayBoardContents()
            print('\nZwycięża gracz ' + self.__players[self.__currentPlayer].GetPlayerName() + '!')
            return False
        elif self.__board.CheckWinCondition() == 'T':
            print()
            self.__board.DisplayBoardContents()
            print('\nRemis!')
            return False
        self.__currentPlayer = abs(self.__currentPlayer - 1)
        return True

def main():
    game1 = Game('Gracz 1', 'Gracz 2')
    playing = True
    while(playing):
        playing = game1.NextTurn()

if __name__ == "__main__":
    main()