using System;

namespace ShipWar3
{
    class Lobby
    {
        private UI ui = new UI();
        private MatchMaker gameSettings = new MatchMaker();
        private PlayerType _player1Type;
        private PlayerType _player2Type;
        private string _player1Name;
        private string _player2Name;
        public void Start()
        {
            gameSettings.Menu();
            Console.Clear();
            BeginSettings();

            gameSettings.PlayGames(3);
        }
        public void BeginSettings()
        {
            ui.TakeInfoAboutPlayers(out _player1Type, out _player2Type, out _player1Name, out _player2Name);   // Takes info about player
            gameSettings.DefinePlayers(_player1Type, _player2Type, _player1Name, _player2Name);                // Setting parameters for players
        }
    }
}