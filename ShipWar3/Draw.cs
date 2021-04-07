using System;

namespace ShipWar3
{
    public class Draw            //Method is doing drawing of this game
    {
        public char symbol;

        private int[][] TransformShipCoordInInts(Ship[] shipsArray)
        {
            int[][] intArray = new int[shipsArray.Length][];
            for (int i = 0; i < shipsArray.Length; i++)
            {
                int xCoords = shipsArray[i].xCoord;
                int yCoords = shipsArray[i].yCoord;
                intArray[i] = new int[2] { xCoords, yCoords };
            }
            return intArray;
        }
        private int[] TransformShipStatesInArray(Ship[] shipsArray)
        {
            int[] shipsArrayInt = new int[shipsArray.Length];
            for (int i = 0; i < shipsArrayInt.Length; i++)
            {
                if (shipsArray[i].state == ShipState.alive)
                {
                    shipsArrayInt[i] = 0;
                }
                if (shipsArray[i].state == ShipState.destroyed)
                {
                    shipsArrayInt[i] = 1;
                }
                if (shipsArray[i].state == ShipState.unspawned)
                {
                    shipsArrayInt[i] = 2;
                }
            }
            return shipsArrayInt;
        }
        public void DrawField(Ship[] ships, FrontendField field, bool? areShipsHidden)
        {

            int[] shipStates = TransformShipStatesInArray(ships);
            int[][] shipCoords = TransformShipCoordInInts(ships);
            int numberToChar = 63;
            int number = 47;
            int f = 0;
            for (int y = 0; y < Constants.height - 1; y++)
            {
                number++;
                for (int x = 0; x < Constants.width; x++)
                {
                    numberToChar++;

                    symbol = field.field[y, x];
                    if (y == 0 && x >= 1)
                    {
                        symbol = (char)numberToChar;
                    }
                    if (y >= 1 && x == 0)
                    {
                        symbol = (char)number;
                    }
                    if ((bool)!areShipsHidden)
                        for (int i = 0; i < ships.Length; i++)
                        {
                            if (shipCoords[i][0] == x && shipCoords[i][1] == y)
                            {
                                if (shipStates[i] == 0)
                                    symbol = '!';
                                if (shipStates[i] == 1)
                                    symbol = '#';
                                if (shipStates[i] == 2)
                                    symbol = '.';
                            }
                        }
                    if (symbol == '.' || symbol == '^')
                    {
                        field.field[y, x] = symbol;
                    }
                    Console.Write(symbol);
                }
                Console.WriteLine();
            }
        }
    }
}