namespace ShipWar3
{
    struct PlayerProfile
    {
        public string name;
        public PlayerType typeOfPlayer;
        public bool? areShipsHidden;
        public int wins;
        public PlayerProfile(string name,PlayerType typeOfPlayer,bool? areShipsHidden,int wins)
        {
            this.name = name;
            this.typeOfPlayer = typeOfPlayer;
            this.areShipsHidden = areShipsHidden;
            this.wins = wins;
            if (typeOfPlayer == PlayerType.ai)
            {
                this.name = "Bot";
            }
        }
    }
}