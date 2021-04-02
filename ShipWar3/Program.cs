using System;

namespace ShipWar3
{
    class Program
    {
        static void Main(string[] args)
        {
            var lobby = new Lobby();
            lobby.Start();
        }
    }
    public enum ShipState
    {
        alive,
        destroyed,
        unspawned
    }
    public enum PlayerType
    {
        human,
        ai
    }
    struct Player
    {
        public PlayerType typeOfPlayer;
        public Ship[]? ships;
        public FrontendField? field;
        public string name;
        public int wins;
        public Player(PlayerType typeOfPlayer, Ship[] ships, FrontendField field, string name,  int wins)
        {
            this.typeOfPlayer = typeOfPlayer;
            this.ships = ships;
            this.field = field;
            this.name = name;
            this.wins = wins;
            if (typeOfPlayer == PlayerType.ai)
            {
                this.name = "Bot";
            }
        }
    }
    public struct Constants
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
                if (ships[number].xCoord == xCoord || ships[number].yCoord == yCoord)
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
        public char[,] field = new char[Constants.height, Constants.width];
        public void GenerateField()
        {
            for (int y = 0; y < Constants.height; y++)
            {
                for (int x = 0; x < Constants.width; x++)
                {
                    field[y, x] = '.';
                }
            }
        }
    }
    public class Draw            //Method is doing drawing of this game
    {
        public char symbol;

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
        private int[] TransformShipStatesInArray(Ship[] shipsArray)
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
        public void DrawField(Ship[] ships, FrontendField field, bool areShipsHidden)
        {

            int[] shipStates = TransformShipStatesInArray(ships);
            int[][] shipCoords = TransformShipCoordInInts(ships);
            int numberToChar = 63;
            int number = 47;
            int f = 0;
            for (int y = 0; y < Constants.height - 1; y++)
            {
                number++;
                for (int x = 0; x < Constants.width; x++)
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
                    if (!areShipsHidden)
                        for (int i = 0; i < ships.Length; i++)
                        {
                            if (shipCoords[i][0] == x && shipCoords[i][1] == y)
                            {
                                if (shipStates[i] == 0)
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
        public (int, int) AskForCoordToShoot()
        {
            int attackHeight;
            char letter;
            while(!int.TryParse(Console.ReadLine(),out  attackHeight) || attackHeight > 10)
                WriteASentence(ConsoleColor.Cyan, "Write a number within the field!");
            while(!char.TryParse(Console.ReadLine(),out  letter) || (int) letter -64 > 10)
                WriteASentence(ConsoleColor.Cyan, "Write a letter within the field!");
            int attackWidth = (int)letter - 64;

            return (attackWidth, attackHeight);
        }
        public void End()
        {
            WriteASentence(ConsoleColor.Cyan, "Thanks for playing");
        }
        public void TakeInfoAboutPlayers(out PlayerType player1Type, out PlayerType player2Type, out string? player1Name, out string? player2Name)
        {
            string player1Input;
            string player2Input;
            WriteASentence(ConsoleColor.Cyan, "Now choose who ll play:human or bot?");

            do
            {
                WriteASentence(ConsoleColor.Cyan, "Please,enter 'human' or 'bot'");
                player1Input = Console.ReadLine();
            }
            while (player1Input != "human" && player1Input != "bot");
            ConvertStringAndDefineType(out player1Type, player1Input, out player1Name);
            WriteASentence(ConsoleColor.Cyan, "Enter second player");
            do
            {
                WriteASentence(ConsoleColor.Cyan, "Please,enter 'human' or 'bot'");
                player2Input = Console.ReadLine();
            }
            while (player2Input != "human" && player2Input != "bot");
            ConvertStringAndDefineType(out player2Type, player2Input, out player2Name);
        }
        public void ConvertStringAndDefineType(out PlayerType playerType, string input, out string? name)
        {
            playerType = PlayerType.ai;
            name = null;
            switch (input)
            {
                case "human":
                    playerType = PlayerType.human;
                    WriteASentence(ConsoleColor.Cyan, "Write your name please");
                    name = Console.ReadLine();

                    break;
                case "bot":
                    playerType = PlayerType.ai;
                    name = null;
                    break;
            }
        }
    }
    class Lobby
    {
        private UI ui = new UI();
        private GameSettings gameSettings = new GameSettings();
        private PlayerType _player1Type;
        private PlayerType _player2Type;
        private string? _player1Name;
        private string? _player2Name;
        public void Start()
        {
            gameSettings.Menu();
            Console.Clear();
            BeginSettings();
        }
        public void BeginSettings()
        {
            ui.TakeInfoAboutPlayers(out _player1Type, out _player2Type, out _player1Name, out _player2Name);   // Takes info about player
            gameSettings.DefinePlayers(_player1Type, _player2Type, _player1Name, _player2Name);                // Setting parameters for players

            gameSettings.PlayGames(3);
        }
    }
    class Game
    {
        private Ship shipReference = new Ship();
        private UI uiReference = new UI();
        private Draw drawClassReference = new Draw();

        public static bool isgameEnded;
        private int _shipQuantity;
        private int counter = 0;
        private int cachedWinsPlayer1;
        private int cachedWinsPlayer2;

        private Player _player1;
        private Player _player2;
        public Game(Player player1,Player player2,int shipQuantity)
        {
            _player1 = player1;
            _player2 = player2;
            _shipQuantity = shipQuantity;
        }
        public void CalculateWinner(ref Player player1,ref Player player2)
        {
            if (_player1.wins > cachedWinsPlayer1)
                player1.wins = _player1.wins;
            if (_player2.wins > cachedWinsPlayer2)
                player2.wins = _player2.wins;
        }
        public void Start()
        {
            isgameEnded = false;
            cachedWinsPlayer1 = _player1.wins;
            cachedWinsPlayer2 = _player2.wins;
            Console.Clear();
            uiReference.WriteASentence(ConsoleColor.Cyan, "Warming ships...");
            System.Threading.Thread.Sleep(3000);

            _player1.ships = shipReference.GenerateShipsWithCoordinates(_shipQuantity);
            _player1.field = new FrontendField();
            _player1.field.GenerateField();

            _player2.ships = shipReference.GenerateShipsWithCoordinates(_shipQuantity);
            _player2.field = new FrontendField();
            _player2.field.GenerateField();

            DrawFields();

            GameProcess();
        }
        private void DrawFields()
        {
            Console.Clear();
            DrawMethodWithDefiningToShowShipsOrNot(_player1);
            Console.WriteLine();
            DrawMethodWithDefiningToShowShipsOrNot(_player2);
        }
        private void DrawMethodWithDefiningToShowShipsOrNot(Player player)                                                   //Method which defines player type and drawing field depending on type
        {
            if (player.typeOfPlayer == PlayerType.ai)
                drawClassReference.DrawField(player.ships, player.field, true);
            else
                drawClassReference.DrawField(player.ships, player.field, false);
        }
        public void GameProcess()
        {
            while (!isGameEnded(ref isgameEnded))
            {
                Cycle();
            }
            uiReference.End();
        }
        private (int x, int y) Attack(PlayerType playertype)
        {
            int x;
            int y;
            if (playertype == PlayerType.human)
                (x, y) = uiReference.AskForCoordToShoot();
            else
                (x, y) = GenerateAttackCoords();
            return (x, y);
        }
        private void Cycle()
        {
            if (counter == 0)
            {
                uiReference.WriteASentence(ConsoleColor.Cyan, _player1.name + " s turn!");
                System.Threading.Thread.Sleep(1500);
                (int x, int y) = Attack(_player1.typeOfPlayer);
                CheckForHit(_player2.ships, _player2.field, x, y);
                DrawFields();
                CheckForWinner(_player2.ships, ref _player1);
                counter++;
            }
            else
            {
                uiReference.WriteASentence(ConsoleColor.Cyan, _player2.name + " s turn!");
                (int x, int y) = Attack(_player2.typeOfPlayer);
                CheckForHit(_player1.ships, _player1.field, x, y);
                DrawFields();
                CheckForWinner(_player1.ships, ref _player2);
                counter--;
            }
        }
        private void CheckForHit(Ship[] ships, FrontendField field, int x, int y)
        {
            for (int i = 0; i < ships.Length; i++)
            {
                if (ships[i].xCoord == x && ships[i].yCoord == y)
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
        private (int x, int y) GenerateAttackCoords()
        {
            Random rand = new Random();
            int x = rand.Next(1, Constants.width);
            int y = rand.Next(1, Constants.height);
            return (x, y);
        }
        public bool isGameEnded(ref bool _isGameEnded)
        {
            return _isGameEnded;
        }
        private void CheckForWinner(Ship[] ships, ref Player player)
        {
            for (int i = 0; i < ships.Length; i++)
            {
                if (ships[i].state == ShipState.alive)
                    return;
            }
            uiReference.WriteASentence(ConsoleColor.Cyan, player.name + " became winner");
            player.wins += 1;
            System.Threading.Thread.Sleep(1500);
            isgameEnded = true;
        }
    }
    class GameSettings
    {
        Player player1;
        Player player2;
        int shipQuantity;
        private UI uiReference = new UI();
        public void DefinePlayers(PlayerType player1Type, PlayerType player2Type, string player1Name, string player2Name)
        {
            player1 = new Player(player1Type, null, null, player1Name, 0); 
            player2 = new Player(player2Type, null, null, player2Name, 0);
        }
        public void Menu()
        {
            uiReference.GameSettings();
            while (!int.TryParse(Console.ReadLine(),out shipQuantity))
               uiReference.WriteASentence(ConsoleColor.Cyan, "Write a number!");
        }
        public void PlayGames(int quantity)
        {
            while (player1.wins != quantity && player2.wins != quantity) 
            {
                Game game = new Game(player1, player2, shipQuantity);                                                  //creates new game and launches it
                game.Start();
                game.CalculateWinner(ref player1, ref player2);
                uiReference.WriteASentence(ConsoleColor.Cyan, player1.name + ":" + player1.wins.ToString());           //showing results of game
                uiReference.WriteASentence(ConsoleColor.Cyan, player2.name + ":" + player2.wins.ToString());
                System.Threading.Thread.Sleep(1500);
            }     
        }

    }
}