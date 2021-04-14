using System;

namespace ShipWar3
{
    class MatchMaker
    {
        PlayerProfile player1;
        PlayerProfile player2;
        int shipQuantity;
        private UI uiReference = new UI();
        public void DefineShowingOfShips(ref PlayerProfile player1,ref PlayerProfile player2)
        {
            if (player1.typeOfPlayer == PlayerType.ai && player2.typeOfPlayer == PlayerType.human)
            {
                player1.areShipsHidden = true;
                player2.areShipsHidden = false;
            }
            if (player1.typeOfPlayer == PlayerType.ai && player2.typeOfPlayer == PlayerType.ai)
            {
                player1.areShipsHidden = false;
                player2.areShipsHidden = false;
            }
           else
            {
                player1.areShipsHidden = false;
                player2.areShipsHidden = true;
            }
        }
        public void DefinePlayers(PlayerType player1Type, PlayerType player2Type, string player1Name, string player2Name)
        {
            player1 = new PlayerProfile(player1Name, player1Type,null, 0);
            player2 = new PlayerProfile(player2Name, player2Type,null, 0);
            DefineShowingOfShips(ref player1, ref player2);
        }
        public void Menu()
        {
            uiReference.GameSettings();
            while (!int.TryParse(Console.ReadLine(), out shipQuantity))
                uiReference.WriteASentence(ConsoleColor.Cyan, "Write a number!");
        }
        private void CalculateWinner(int resultedWinsPlayer1,int resultedWinsPlayer2)
        {
            if (resultedWinsPlayer1 > player1.wins)
                player1.wins += 1;
            if (resultedWinsPlayer2 > player2.wins)
                player2.wins += 1;
        }
        public void PlayGames(int quantity)
        {
            while (player1.wins != quantity && player2.wins != quantity) 
            {
                Game game = new Game(player1, player2, shipQuantity);                                                  //creates new game and launches it
                game.Start();      
                (int resultedWinsPlayer1,int resultedWinsPlayer2) = game.GetResults();
                CalculateWinner(resultedWinsPlayer1,resultedWinsPlayer2);
                uiReference.WriteASentence(ConsoleColor.Cyan, player1.name + ":" + player1.wins.ToString());           //showing results of game
                uiReference.WriteASentence(ConsoleColor.Cyan, player2.name + ":" + player2.wins.ToString());
                System.Threading.Thread.Sleep(1500);
            }     
        }

    }
}