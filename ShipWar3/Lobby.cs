using System;

namespace ShipWar3
{
    class Lobby
    {
        private UI ui = new UI();
        private XML xml = new XML();
        private MatchMaker matchMaker = new MatchMaker();
        private PlayerProfileList profiles = new PlayerProfileList();
        private PlayerProfile player1;
        private PlayerProfile player2;
        private bool continueGame= false;
        public void Start()
        {
            do
            {
                profiles.Load();
                ui.GameSettings();
                player1 = ui.LogInOrRegisterAccount(profiles);
                player2 = ui.LogInOrRegisterAccount(profiles);
                profiles.Save();

                Console.Clear();

                matchMaker.DefinePlayers(player1.typeOfPlayer, player2.typeOfPlayer, player1.name, player2.name);                // Setting parameters for players
                matchMaker.PlayGames(1);

                (int firstPlayerWins, int secondPlayerWins) = matchMaker.GetResults();
                profiles.ChangeWinsOfProfile(player1.login, firstPlayerWins);
                profiles.ChangeWinsOfProfile(player2.login, secondPlayerWins);
                profiles.Save();
            } while (continueGame);

        }
       
    }
}
