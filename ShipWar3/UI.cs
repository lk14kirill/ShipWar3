using System;
using System.Collections.Generic;

namespace ShipWar3
{
    class UI
    {
        public void ShowInfo(PlayerProfile profile)
        {
            Console.WriteLine("Name:"+profile.name);
            Console.WriteLine("Login:"+profile.login);
            Console.WriteLine("Wins:" + profile.wins);
            System.Threading.Thread.Sleep(3000);
        }
        public PlayerProfile LogInOrRegisterAccount(List<PlayerProfile> playerProfiles)
        {
            Console.Clear();
            PlayerProfile profile;
            if (!IsBot())
            {          
                if (!IsThereAccount())
                {
                    TakeInfoAboutPlayer(out string name, out string login, out string password);
                    profile = CreateProfile(playerProfiles,name, login, password);
                }
                else
                {
                    profile = (PlayerProfile)LogIn(playerProfiles);
                }
                ShowInfo(profile);
            }
            else
            {
                 profile = new PlayerProfile(null, PlayerType.ai, null, 0, null, null);
            }
            return profile;
        }
        public PlayerProfile CreateProfile(List<PlayerProfile>playerProfiles,string name, string login, string password)
        {
            PlayerProfile profile = new PlayerProfile(name, PlayerType.human, null, 0, login, password);
            playerProfiles.Add(profile);
            return profile;
        }

        public bool IsBot()
        {
            string input;
            WriteASentence(ConsoleColor.Cyan, "Are you bot,or human?");
            do
            {
                WriteASentence(ConsoleColor.Cyan, "Please write 'bot' or 'human' ");
                input = Console.ReadLine();
            }
            while (input != "bot" && input != "human");
            return input == "bot" ? true : false;
        }
        public void GameSettings()
        {
            WriteASentence(ConsoleColor.Cyan, "Welcome to ships war!");
            WriteASentence(ConsoleColor.Cyan, "You can play agains our bot.Here some instructions:");
            WriteASentence(ConsoleColor.Cyan, "^ - means you missed");
            WriteASentence(ConsoleColor.Cyan, "# - means you hit");
            WriteASentence(ConsoleColor.Cyan, "Every ship is single-deck");
            WriteASentence(ConsoleColor.Cyan, "First,who destroyed all of ships - wins!");
            Console.WriteLine();
            WriteASentence(ConsoleColor.Cyan, "Type 'start' to start the game");
            Console.ReadLine();
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
            while(!char.TryParse(Console.ReadLine(),out  letter) || (int) letter -64 > 10);
                WriteASentence(ConsoleColor.Cyan, "Write a letter within the field!");
            int attackWidth = (int)letter - 64;

            return (attackWidth, attackHeight);
        }
        public void End()
        {
            WriteASentence(ConsoleColor.Cyan, "Thanks for playing");
        }
        public PlayerProfile? LogIn(List<PlayerProfile> profiles)
        {
            string login;
            string password;
            PlayerProfile? profile;
            profile = null;
            bool accountExists = false;
            WriteASentence(ConsoleColor.Cyan, "Write your login and password");
            do
            {
                WriteASentence(ConsoleColor.Cyan, "Please write login");
                login = Console.ReadLine();
                if (login == "return")
                    break;
                WriteASentence(ConsoleColor.Cyan, "Please write password");
                password = Console.ReadLine();
                if (password == "return")
                    break;
                
                profile = CheckForString(profiles, login, password);
                if (profile == null)
                {
                    accountExists = false;
                }
                else
                    accountExists = true;
            }
            while (!accountExists);
            return profile;
        }
        public PlayerProfile? CheckForString(List<PlayerProfile> profiles,string login,string password)
        {
            foreach(PlayerProfile profile in profiles)
            {
                if(profile.login == login && profile.password == password)
                {
                    return profile;
                }
            }
            return null;
        }
        public bool IsThereAccount()
        {
            string input;
            WriteASentence(ConsoleColor.Cyan, "Do you have account?");
            do
            {
                WriteASentence(ConsoleColor.Cyan, "Please write 'yes' or 'no' ");
                input = Console.ReadLine();
            }
            while (input != "yes" && input != "no");
            return input == "yes" ? true : false; 
        }
        public void TakeInfoAboutPlayer(out string? player1Name, out string login, out string password)
        {
            WriteASentence(ConsoleColor.Cyan, "Write your name");
            player1Name = Console.ReadLine();
            WriteASentence(ConsoleColor.Cyan, "Write your login");
            login = Console.ReadLine();
            WriteASentence(ConsoleColor.Cyan, "Write your password");
            password = Console.ReadLine();

        }
        public void QuantityOfShips(out int quantity)
        {
            WriteASentence(ConsoleColor.Cyan, "Write quantity of ships");
            while (!int.TryParse(Console.ReadLine(), out quantity) && quantity > 0 && quantity <11)
                WriteASentence(ConsoleColor.Cyan, "Write a number within the range~!(1,10)");
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