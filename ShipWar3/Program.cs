using System;

namespace ShipWar3
{
    class Program
    {
        static void Main(string[] args)
        {
            var gameCycle = new GameCycle();
            gameCycle.StartGame();
        }
    }
    public enum ShipState
    {
        alive,
        destroyed,
        unspawned
    }
    public struct Consts
    {
        public const int width = 11;
        public const int height = 11;
    }
    public struct Ship
    {
        public int xCoord;
        public int yCoord;
        public int size;
        public ShipState state;
        
        public Ship(int size, int yCoord, int xCoord, ShipState state)
        {
            this.size = size;
            this.yCoord = yCoord;
            this.xCoord = xCoord;
            this.state = state;
        }
        public Ship[] GenerateShipsWithCoordinates(int shipQuantity)
        {
            Ship[] ships = new Ship[shipQuantity];
            for (int i = 0; i < shipQuantity; i++)
            {
                int yCoord = GenerateCoordinates();
                int xCoord = GenerateCoordinates();
                if (CanCreateShip(xCoord, yCoord, i, ships))
                {
                    ships[i] = new Ship(1, yCoord, xCoord, ShipState.alive);
                }
                else
                {
                    ships[i] = new Ship(1, yCoord, xCoord, ShipState.unspawned);
                }
            }
            return ships;
        }
        public static bool CanCreateShip(int xCoord, int yCoord, int number, Ship[] ships)
        {
            for (int i = 0; i < number; i++)
            {
                if(ships[number].xCoord == xCoord || ships[number].yCoord == yCoord)
                {
                    return false;
                }
            }
            return true;
        }
        public static int GenerateCoordinates()
        { 
            Random rand = new Random();
            int coord = rand.Next(1, 10);

            return coord;
        }
    }
    public class FrontendField
    {
        public char[,] field = new char[Consts.height, Consts.width];
        public void GenerateField()
        {
            for (int y = 0; y < Consts.height; y++)
            {
                for (int x = 0; x < Consts.width; x++)
                {
                    field[y, x] = '.';
                }
            }
        }
    }
    public class Draw            //Method is doing drawing of this game
    {
        public char symbol;
        public void MakeIndent()
        {
            Console.WriteLine();
        }
        private int[][] TransformShipCoordInInts(Ship[] shipsArray)
        {
            int[][] intArray = new int[shipsArray.Length][];
            for (int i = 0; i < shipsArray.Length; i++)
            {
                int xCoords = shipsArray[i].xCoord;
                int yCoords = shipsArray[i].yCoord;
                intArray[i] = new int[2] { xCoords, yCoords };
            }
            return intArray;
        }
        private int[] TransformShipStatesInArray (Ship[] shipsArray)
        {
            int[] shipsArrayInt = new int[shipsArray.Length];
            for (int i = 0; i < shipsArrayInt.Length; i++)
            {
                if (shipsArray[i].state == ShipState.alive)
                {
                    shipsArrayInt[i] = 0;
                }
                if (shipsArray[i].state == ShipState.destroyed)
                {
                    shipsArrayInt[i] = 1;
                }
                if (shipsArray[i].state == ShipState.unspawned)
                {
                    shipsArrayInt[i] = 2;
                }
            }
            return shipsArrayInt;
        }
        public void DrawFieldWithoutShips(ref FrontendField field)
        {
            int numberToChar = 63;
            int number = 47;
            for (int y = 0; y < Consts.height - 1; y++)
            {
                number++;
                for (int x = 0; x < Consts.width; x++)
                {
                    numberToChar++;

                    symbol = field.field[y, x];
                    if (y == 0 && x >= 1)
                    {
                        symbol = (char)numberToChar;
                    }
                    if (y >= 1 && x == 0)
                    {
                        symbol = (char)number;
                    }
                    if (symbol == '.' || symbol == '^')
                    {
                        field.field[y, x] = symbol;
                    }
                    Console.Write(symbol);
                }
            Console.WriteLine();
            }
        }
        public void DrawFieldWithShips(Ship[] ships,ref FrontendField field)
        {
            
            int[] shipStates = TransformShipStatesInArray(ships);
            int[][] shipCoords = TransformShipCoordInInts(ships);
            int numberToChar = 63;
            int number = 47;
            int f = 0;
            for (int y = 0; y < Consts.height - 1; y++) 
            {
                number++;
                for (int x = 0; x < Consts.width; x++)
                {
                    numberToChar++;

                    symbol = field.field[y,x];
                    if (y == 0 && x >= 1)
                    {
                        symbol = (char)numberToChar;
                    }
                    if (y >= 1 && x == 0)
                    {
                        symbol = (char)number;
                    }
                    for (int i = 0; i < ships.Length; i++)
                    {
                        if (shipCoords[i][0] == x && shipCoords[i][1] == y)
                        {
                            if(shipStates[i] == 0)
                                symbol = '!';
                            if (shipStates[i] == 1)
                                symbol = '#';
                            if (shipStates[i] == 2)
                                symbol = '.';
                        }
                    }
                    if (symbol == '.' || symbol == '^')
                    {
                        field.field[y, x] = symbol;
                    }
                    Console.Write(symbol);
                }
                Console.WriteLine();
            }
        }

    }
    class UI
    {
        public void GameSettings()
        {
            WriteASentence(ConsoleColor.Cyan, "Welcome to ships war!");
            WriteASentence(ConsoleColor.Cyan, "You can play agains our bot.Here some instructions:");
            WriteASentence(ConsoleColor.Cyan, "^ - means you missed");
            WriteASentence(ConsoleColor.Cyan, "# - means you hit");
            WriteASentence(ConsoleColor.Cyan, "There are no stable quantity of ships,it can either 3,nor 5.But not more than 11");
            WriteASentence(ConsoleColor.Cyan, "Every ship is single-deck");
            WriteASentence(ConsoleColor.Cyan, "First,who destroyed all of ships - wins!");
            Console.WriteLine();
            WriteASentence(ConsoleColor.Cyan, "Type quantity of ships to start the game");
        }
        public void WriteASentence(ConsoleColor color, string text)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        public (int,int)AskForCoordToShoot()
        {
            int attackHeight = int.Parse(Console.ReadLine());
            char letter = char.Parse(Console.ReadLine());
            int attackWidth = (int)letter - 64;

            return (attackWidth, attackHeight);
        }
        public void End()
        {
            WriteASentence(ConsoleColor.Cyan, "Thanks for playing");    
        }
    }
    class GameCycle
    {
        private Ship shipReference = new Ship();
        private UI uiReference = new UI();
        private Draw drawClassReference = new Draw();

        public static bool isgameEnded = false;
        public int shipQuantity;
        private int counter = 0;

        public Ship[] playerShips;
        public Ship[] AIShips;

        public FrontendField playerField;
        public FrontendField AIField;
        private void Menu()
        {
            uiReference.GameSettings();
            int start = int.Parse(Console.ReadLine());
            shipQuantity = start;
        }
        public  void StartGame()
        {
            Menu();
            Console.Clear();

            playerShips = shipReference.GenerateShipsWithCoordinates(shipQuantity);
            playerField = new FrontendField();
            playerField.GenerateField();

            AIShips = shipReference.GenerateShipsWithCoordinates(shipQuantity);
            AIField = new FrontendField();
            AIField.GenerateField();

            DrawingFieldLocalFunction();

            GameProcess();
        }
        private void DrawingFieldLocalFunction()                                                   //this Method calls drawing methods from drawclass
        {
            Console.Clear();
            drawClassReference.DrawFieldWithShips(playerShips, ref playerField);
            drawClassReference.MakeIndent();
            drawClassReference.DrawFieldWithoutShips(ref AIField);
        }
        public void GameProcess()
        {
            while (!isGameEnded(ref isgameEnded))
            {
                Cycle();
            }
            uiReference.End();
        }

        private void Cycle()
        {
            if (counter == 0)
            {
                System.Threading.Thread.Sleep(1500);
                (int x,int y) = uiReference.AskForCoordToShoot();
                CheckForHit(ref AIShips,ref AIField, x, y);
                DrawingFieldLocalFunction();
                CheckForWinner(AIShips,"You ");
                counter++;
            }
            else
            {
                WaitForBot();
                (int x, int y) = GenerateAttackCoords();
                CheckForHit(ref playerShips, ref playerField, x, y);
                DrawingFieldLocalFunction();
                CheckForWinner(playerShips,"Enemy ");
                counter--;
            }
        }
        private void WaitForBot()
        {
            uiReference.WriteASentence(ConsoleColor.Cyan, "Wait,enemy is attacking you!");
            System.Threading.Thread.Sleep(1500);
        }
        private void CheckForHit(ref Ship[] ships,ref FrontendField field,int x,int y)
        {
            for (int i = 0; i < ships.Length; i++)
            {
                if(ships[i].xCoord == x && ships[i].yCoord == y)
                {
                    ships[i].state = ShipState.destroyed;
                    field.field[y, x] = '!';
                    uiReference.WriteASentence(ConsoleColor.Cyan, "Hit!");
                    if (counter == 1)
                    {
                        counter = 2;
                    }
                    else                                                              //Making counter +1 or -1 to start playerCycle/aiCycle again when hit
                    {
                        counter = -1;
                    }
                    return;
                }
            }
            uiReference.WriteASentence(ConsoleColor.Cyan, "Miss!");
            field.field[y, x] = '^';
        }
        private (int x,int y) GenerateAttackCoords()
        {
            Random rand = new Random();
            int x = rand.Next(1, Consts.width);
            int y = rand.Next(1, Consts.height);
            return (x, y);
        }
        public  bool isGameEnded(ref bool _isGameEnded)
        {
            return _isGameEnded;
        }
        private void CheckForWinner(Ship[] ships,string winner)
        {
            for (int i = 0; i < ships.Length; i++)
            {
                if (ships[i].state == ShipState.alive)
                    return;
            }
            uiReference.WriteASentence(ConsoleColor.Cyan, winner + "became winner");
            isgameEnded = true;
        }
    }
}
