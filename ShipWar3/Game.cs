using System;

namespace ShipWar3
{
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
        public void GeneratePlayerStructures(ref Player player)                   //Generates ships and field
        {
            player.ships = shipReference.GenerateShipsWithCoordinates(_shipQuantity);
            player.field = new FrontendField();
            player.field.GenerateField();
        }
        public void Start()
        {
            isgameEnded = false;
            cachedWinsPlayer1 = _player1.wins;
            cachedWinsPlayer2 = _player2.wins;
            Console.Clear();
            uiReference.WriteASentence(ConsoleColor.Cyan, "Warming ships...");
            System.Threading.Thread.Sleep(3000);

            GeneratePlayerStructures(ref _player1);
            GeneratePlayerStructures(ref _player2);
            ChangeShipShowingStatesForHumans(ref _player1, ref _player2);

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
        private void DrawMethodWithDefiningToShowShipsOrNot(Player playerToDraw)                                                   //Method which defines player type and drawing field depending on type
        {
            drawClassReference.DrawField(playerToDraw.ships, playerToDraw.field, playerToDraw.areShipsHidden);
            Console.WriteLine(playerToDraw.areShipsHidden);
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
                ChangeShipShowingStatesForHumans(ref _player2, ref _player1);
                DrawFields();
                CheckForWinner(_player2.ships, ref _player1);
                counter++;
            }
            else
            {
                uiReference.WriteASentence(ConsoleColor.Cyan, _player2.name + " s turn!");
                (int x, int y) = Attack(_player2.typeOfPlayer);
                CheckForHit(_player1.ships, _player1.field, x, y);
                ChangeShipShowingStatesForHumans(ref _player1, ref _player2);
                DrawFields();
                  
                CheckForWinner(_player1.ships, ref _player2);
                counter--;
            }
        }
        private void ChangeShipShowingStatesForHumans(ref Player player1,ref Player player2)
        {
            if (player1.typeOfPlayer == PlayerType.human && player2.typeOfPlayer == PlayerType.human)
            {
                player1.areShipsHidden = false;
                player2.areShipsHidden = true;
            }
        }
        private void CheckForHit(Ship[] ships, FrontendField field, int x, int y)
        {
            for (int i = 0; i < ships.Length; i++)
            {
                if (ships[i].xCoord == x && ships[i].yCoord == y)
                {
                    ships[i].state = ShipState.destroyed;
                    field.field[y, x] = '#';
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
}