namespace ShipWar3
{
    public struct PlayerProfile
    {
        public string name;
        public PlayerType typeOfPlayer;
        public bool? areShipsHidden;
        public int wins;
        public string? login;
        public string? password;
        public PlayerProfile(string name,PlayerType typeOfPlayer,bool? areShipsHidden,int wins,string? login,string? password)
        {
            this.name = name;
            this.typeOfPlayer = typeOfPlayer;
            this.areShipsHidden = areShipsHidden;
            this.wins = wins;
            this.login = login;
            this.password = password;
            if (typeOfPlayer == PlayerType.ai)
            {
                this.name = "Bot";
            }
        }
    }
}