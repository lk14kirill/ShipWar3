using System;
using System.Collections.Generic;

namespace ShipWar3
{
    class Lobby
    {
        private UI ui = new UI();
        private XML xml = new XML();
        private MatchMaker matchMaker = new MatchMaker();
        public List<PlayerProfile> playerProfiles = new List<PlayerProfile>();
        private PlayerProfile player1;
        private PlayerProfile player2;
        public void Start()
        {
            xml.Load(ref playerProfiles);
            ui.GameSettings();
            player1 = ui.LogInOrRegisterAccount(playerProfiles);
            player2 = ui.LogInOrRegisterAccount(playerProfiles);
            xml.Save(playerProfiles);

            Console.Clear();
            
            matchMaker.DefinePlayers(player1.typeOfPlayer, player2.typeOfPlayer, player1.name, player2.name) ;                // Setting parameters for players
            matchMaker.PlayGames(3);

            (int wins, int wins2) = matchMaker.GetResults();
            FindAndChangeWinsOfPlayerProfilesList(player1.login, wins);
            FindAndChangeWinsOfPlayerProfilesList(player2.login, wins2);
            xml.Save(playerProfiles);
        }
        private void FindAndChangeWinsOfPlayerProfilesList(string login,int wins)
        {

             playerProfiles.FindAll(profile => profile.login == login).ForEach(profileComponent => profileComponent.wins += wins);
        }
    }
}