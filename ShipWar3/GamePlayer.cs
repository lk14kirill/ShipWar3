namespace ShipWar3
{
    struct GamePlayer
    {
        public PlayerType typeOfPlayer;
        public bool? areShipsHidden;
        public Ship[]? ships;
        public FrontendField? field;
        public string name;
        public int wins;
        public GamePlayer(PlayerType typeOfPlayer, Ship[] ships, FrontendField field,bool? areShipsHidden, string name,  int wins)
        {
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