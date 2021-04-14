using System;

namespace ShipWar3
{
    class Game
    {
        private Ship shipReference = new Ship();
        private UI uiReference = new UI();
        private Draw drawClassReference = new Draw();

        public  bool isgameEnded;
        private int _shipQuantity;
        private int counter = 0;

        private GamePlayer _player1;
        private GamePlayer _player2;
        public Game(PlayerProfile player1,PlayerProfile player2,int shipQuantity)
        {
            _player1 = new GamePlayer(player1.typeOfPlayer, null,null,player1.areShipsHidden,player1.name,player1.wins,shipQuantity); 
            _player2 = new GamePlayer(player2.typeOfPlayer, null, null, player2.areShipsHidden, player2.name, player2.wins,shipQuantity);
            _shipQuantity = shipQuantity;
        }
        public (int wins1,int wins2) GetResults()
        {
            return (_player1.wins, _player2.wins);
        }
        public void Start()
        {
            isgameEnded = false;

            Console.Clear();
            uiReference.WriteASentence(ConsoleColor.Cyan, "Warming ships...");
            System.Threading.Thread.Sleep(3000);

            _player1.GeneratePlayerStructures();
            _player2.GeneratePlayerStructures();
            ChangeShipShowingStatesForHumans( _player1,  _player2);

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
        private void DrawMethodWithDefiningToShowShipsOrNot(GamePlayer playerToDraw)                                                   //Method which defines player type and drawing field depending on type
        {
            drawClassReference.DrawField(playerToDraw.ships, playerToDraw.field, playerToDraw.areShipsHidden);
        }
        public void GameProcess()
        {
            while (!isGameEnded( isgameEnded))
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
                ChangeShipShowingStatesForHumans( _player2,  _player1);
                DrawFields();
                CheckForWinner(_player2.ships,  _player1);
                counter++;
            }
            else
            {
                uiReference.WriteASentence(ConsoleColor.Cyan, _player2.name + " s turn!");
                (int x, int y) = Attack(_player2.typeOfPlayer);
                CheckForHit(_player1.ships, _player1.field, x, y);
                ChangeShipShowingStatesForHumans( _player1,  _player2);
                DrawFields();
                  
                CheckForWinner(_player1.ships,  _player2);
                counter--;
            }
        }
        private void ChangeShipShowingStatesForHumans( GamePlayer player1, GamePlayer player2)
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
        public bool isGameEnded( bool _isGameEnded)
        {
            return _isGameEnded;
        }
        private void CheckForWinner(Ship[] ships,  GamePlayer player)
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