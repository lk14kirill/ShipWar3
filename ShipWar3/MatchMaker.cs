using System;

namespace ShipWar3
{
    class MatchMaker
    {
        Player player1;
        Player player2;
        int shipQuantity;
        private UI uiReference = new UI();
        public void DefineShowingOfShips(ref Player player1,ref Player player2)
        {
            if (player1.typeOfPlayer == PlayerType.ai && player2.typeOfPlayer == PlayerType.human)
            {
                player1.areShipsHidden = true;
                player2.areShipsHidden = false;
            }
            if (player1.typeOfPlayer == PlayerType.human && player2.typeOfPlayer == PlayerType.ai)
            {
                player1.areShipsHidden = false;
                player2.areShipsHidden = true;
            }
            if (player1.typeOfPlayer == PlayerType.ai && player2.typeOfPlayer == PlayerType.ai)
            {
                player1.areShipsHidden = false;
                player2.areShipsHidden = false;
            }
            if(player1.typeOfPlayer == PlayerType.human && player2.typeOfPlayer == PlayerType.human)
            {
                player1.areShipsHidden = false;
                player2.areShipsHidden = true;
            }
        }
        public void DefinePlayers(PlayerType player1Type, PlayerType player2Type, string player1Name, string player2Name)
        {
            player1 = new Player(0,player1Type, null, null,null, player1Name, 0); 
            player2 = new Player(1,player2Type, null, null,null, player2Name, 0);
            DefineShowingOfShips(ref player1, ref player2);
        }
        public void Menu()
        {
            uiReference.GameSettings();
            while (!int.TryParse(Console.ReadLine(),out shipQuantity))
               uiReference.WriteASentence(ConsoleColor.Cyan, "Write a number!");
        }
        public void PlayGames(int quantity)
        {
            while (player1.wins != quantity && player2.wins != quantity) 
            {
                Game game = new Game(player1, player2, shipQuantity);                                                  //creates new game and launches it
                game.Start();
                game.CalculateWinner(ref player1, ref player2);
                uiReference.WriteASentence(ConsoleColor.Cyan, player1.name + ":" + player1.wins.ToString());           //showing results of game
                uiReference.WriteASentence(ConsoleColor.Cyan, player2.name + ":" + player2.wins.ToString());
                System.Threading.Thread.Sleep(1500);
            }     
        }

    }
}