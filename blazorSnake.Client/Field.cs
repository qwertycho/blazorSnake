using System.Drawing;

namespace blazorSnake.Client
{
    public class Field
    {
        public BoxValues[,] field;
        private Random random = new();

        public Field(int w, int h)
        {
            field = new BoxValues[w, h];
            PlaceFood();
        }

        public void PlaceFood()
        {

                int x = random.Next(0, field.GetLength(0));
                int y = random.Next(0, field.GetLength(1));
                if (field[x, y] != BoxValues.Snake) 
                {
                    field[x, y] = BoxValues.Food;
                return;
                }
            PlaceFood();
        }
    }
}
