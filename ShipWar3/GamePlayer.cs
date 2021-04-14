namespace ShipWar3
{
    class GamePlayer
    {
        public Ship shipReference = new Ship();
        public PlayerType typeOfPlayer;
        public bool? areShipsHidden;
        public Ship[]? ships;
        public FrontendField? field;
        public string name;
        public int wins;
        public int shipQuantity;
        public GamePlayer(PlayerType typeOfPlayer, Ship[] ships, FrontendField field,bool? areShipsHidden, string name,  int wins,int shipQuantity)
        {
            this.typeOfPlayer = typeOfPlayer;
            this.ships = ships;
            this.field = field;
            this.name = name;
            this.areShipsHidden = areShipsHidden;
            this.wins = wins;
            this.shipQuantity = shipQuantity;
        }
        public void GeneratePlayerStructures()                   //Generates ships and field
        {
            this.ships = shipReference.GenerateShipsWithCoordinates(shipQuantity);
            this.field = new FrontendField();
            this.field.GenerateField();
        }
    }
}