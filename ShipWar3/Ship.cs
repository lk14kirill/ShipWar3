using System;

namespace ShipWar3
{
    public struct Ship
    {
        public int xCoord;
        public int yCoord;
        public int size;
        public ShipState state;

        public Ship(int size, int yCoord, int xCoord, ShipState state)
        {
            this.size = size;
            this.yCoord = yCoord;
            this.xCoord = xCoord;
            this.state = state;
        }
        public Ship[] GenerateShipsWithCoordinates(int shipQuantity)
        {
            Ship[] ships = new Ship[shipQuantity];
            for (int i = 0; i < shipQuantity; i++)
            {
                int yCoord = GenerateCoordinates();
                int xCoord = GenerateCoordinates();
                if (CanCreateShip(xCoord, yCoord, i, ships))
                {
                    ships[i] = new Ship(1, yCoord, xCoord, ShipState.alive);
                }
                else
                {
                    ships[i] = new Ship(1, yCoord, xCoord, ShipState.unspawned);
                }
            }
            return ships;
        }
        public static bool CanCreateShip(int xCoord, int yCoord, int number, Ship[] ships)
        {
            for (int i = 0; i < number; i++)
            {
                if (ships[number].xCoord == xCoord || ships[number].yCoord == yCoord)
                {
                    return false;
                }
            }
            return true;
        }
        public static int GenerateCoordinates()
        {
            Random rand = new Random();
            int coord = rand.Next(1, 10);

            return coord;
        }
    }
}