namespace ShipWar3
{
    struct Player
    {
        public PlayerType typeOfPlayer;
        public bool? areShipsHidden;
        public Ship[]? ships;
        public FrontendField? field;
        public string name;
        public int playerIndex;
        public int wins;
        public Player(int playerIndex,PlayerType typeOfPlayer, Ship[] ships, FrontendField field,bool? areShipsHidden, string name,  int wins)
        {
            this.playerIndex = playerIndex;
            this.typeOfPlayer = typeOfPlayer;
            this.ships = ships;
            this.field = field;
            this.name = name;
            this.areShipsHidden = areShipsHidden;
            this.wins = wins;
            if (typeOfPlayer == PlayerType.ai)
            {
                this.name = "Bot";
            }
        }
    }
}