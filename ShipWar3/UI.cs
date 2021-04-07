using System;

namespace ShipWar3
{
    class UI
    {
        public void GameSettings()
        {
            WriteASentence(ConsoleColor.Cyan, "Welcome to ships war!");
            WriteASentence(ConsoleColor.Cyan, "You can play agains our bot.Here some instructions:");
            WriteASentence(ConsoleColor.Cyan, "^ - means you missed");
            WriteASentence(ConsoleColor.Cyan, "# - means you hit");
            WriteASentence(ConsoleColor.Cyan, "Every ship is single-deck");
            WriteASentence(ConsoleColor.Cyan, "First,who destroyed all of ships - wins!");
            Console.WriteLine();
            WriteASentence(ConsoleColor.Cyan, "Type quantity of ships to start the game");
        }
        public void WriteASentence(ConsoleColor color, string text)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        public (int, int) AskForCoordToShoot()
        {
            int attackHeight;
            char letter;
            while(!int.TryParse(Console.ReadLine(),out  attackHeight) || attackHeight > 10)
                WriteASentence(ConsoleColor.Cyan, "Write a number within the field!");
            while(!char.TryParse(Console.ReadLine(),out  letter) || (int) letter -64 > 10)
                WriteASentence(ConsoleColor.Cyan, "Write a letter within the field!");
            int attackWidth = (int)letter - 64;

            return (attackWidth, attackHeight);
        }
        public void End()
        {
            WriteASentence(ConsoleColor.Cyan, "Thanks for playing");
        }
        public void TakeInfoAboutPlayers(out PlayerType player1Type, out PlayerType player2Type, out string? player1Name, out string? player2Name)
        {
            string player1Input;
            string player2Input;
            WriteASentence(ConsoleColor.Cyan, "Now choose who ll play:human or bot?");

            do
            {
                WriteASentence(ConsoleColor.Cyan, "Please,enter 'human' or 'bot'");
                player1Input = Console.ReadLine();
            }
            while (player1Input != "human" && player1Input != "bot");
            ConvertStringAndDefineType(out player1Type, player1Input, out player1Name);
            WriteASentence(ConsoleColor.Cyan, "Enter second player");
            do
            {
                WriteASentence(ConsoleColor.Cyan, "Please,enter 'human' or 'bot'");
                player2Input = Console.ReadLine();
            }
            while (player2Input != "human" && player2Input != "bot");
            ConvertStringAndDefineType(out player2Type, player2Input, out player2Name);
        }
        public void ConvertStringAndDefineType(out PlayerType playerType, string input, out string? name)
        {
            playerType = PlayerType.ai;
            name = null;
            switch (input)
            {
                case "human":
                    playerType = PlayerType.human;
                    WriteASentence(ConsoleColor.Cyan, "Write your name please");
                    name = Console.ReadLine();

                    break;
                case "bot":
                    playerType = PlayerType.ai;
                    name = null;
                    break;
            }
        }
    }
}