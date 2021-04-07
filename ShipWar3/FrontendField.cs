namespace ShipWar3
{
    public class FrontendField
    {
        public char[,] field = new char[Constants.height, Constants.width];
        public void GenerateField()
        {
            for (int y = 0; y < Constants.height; y++)
            {
                for (int x = 0; x < Constants.width; x++)
                {
                    field[y, x] = '.';
                }
            }
        }
    }
}