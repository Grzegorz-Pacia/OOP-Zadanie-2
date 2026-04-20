class Player:
    def __init__(self, playerName, playerSymbol):
        self.__playerName = playerName
        self.__playerSymbol = playerSymbol
    
    def getPlayerName(self):
        return self.__playerName
    
    def getPlayerSymbol(self):
        return self.__playerSymbol

class Board:
    def __init__(self):
        self.__boardContents = [[' ', ' ', ' '], [' ', ' ', ' '], [' ', ' ', ' ']]
    
    def GetTileContent(self, x, y):
        return self.__boardContents[x, y]
    
    def SetTileContent(self, x, y, symbol):
        self.__boardContents[x, y] = symbol
    
    def CheckWinCondition(self):
        possibleWinner = [' ', ' ', ' ']

        if (self.__boardContents[0, 0] == self.__boardContents[1, 1] and self.__boardContents[1, 1] == self.__boardContents[2, 2]) or\
           (self.__boardContents[0, 2] == self.__boardContents[1, 1] and self.__boardContents[1, 1] == self.__boardContents[2, 0]) or\
           (self.__boardContents[0, 1] == self.__boardContents[1, 1] and self.__boardContents[1, 1] == self.__boardContents[2, 1]) or\
           (self.__boardContents[1, 0] == self.__boardContents[1, 1] and self.__boardContents[1, 1] == self.__boardContents[1, 2]):
            possibleWinner[0] = self.__boardContents[1, 1]
        elif (self.__boardContents[0, 0] == self.__boardContents[0, 1] and self.__boardContents[0, 1] == self.__boardContents[0, 2]) or\
             (self.__boardContents[0, 0] == self.__boardContents[1, 0] and self.__boardContents[1, 0] == self.__boardContents[2, 0]):
            possibleWinner[1] = self.__boardContents[0, 0]
        elif (self.__boardContents[2, 2] == self.__boardContents[2, 1] and self.__boardContents[2, 1] == self.__boardContents[2, 0]) or\
             (self.__boardContents[2, 2] == self.__boardContents[1, 2] and self.__boardContents[1, 2] == self.__boardContents[0, 2]):
            possibleWinner[2] = self.__boardContents[2, 2]
        
        for symbol in possibleWinner:
            if symbol != ' ':
                return symbol
        
        for i in range(3):
            for j in range(3):
                if self.__boardContents[i, j] == ' ':
                    return ' '
        
        return 'T'
    
    def DisplayBoardContents(self):
        for i in range(3):
            for j in range(3):
                print(self.__boardContents[i, j])
                if j != 2:
                    print("|")
            print()
            if i != 2:
                print("-+-+-")
    
    