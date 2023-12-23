namespace blazorSnake
{
    public class Field
    {
        public BoxValues[,] field;

        public Field(int w, int h)
        {
            field = new BoxValues[w, h];
        }
    }
}
