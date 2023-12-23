using System.Drawing;

namespace blazorSnake
{
    public class Snake
    {
        private bool Alive = true;
        private int Length;
        private Point HeadPos;
        private Point TailPos;
        private List<Point> SnakePositions;
        public Snake(Point start) 
        {
            HeadPos = start;
            TailPos = new(start.X, start.Y+1);
            Length = 2;
            SnakePositions = new List<Point>() { TailPos, HeadPos };
        }

        public bool IsAlive()
        {
            return Alive;
        }

        public int GetLength()
        {
            return Length;
        }
        public Field Move(Moves move, Field field)
        {
            Point direction = MakeDirection(move);
            Point newHeadPos = new Point((HeadPos.X + direction.X), (HeadPos.Y + direction.Y));
            if (IsOutSideField(newHeadPos, field))
            {
                Alive = false;
                return field;
            }

            if (IsCannibal(newHeadPos, field))
            {
                Alive = false;
            }

            field.field[newHeadPos.X, newHeadPos.Y] = BoxValues.Snake;
            SnakePositions.Add(newHeadPos);

            if(IsFood(newHeadPos, field))
            {
                Length++; 
                return field;
            }

            field.field[TailPos.X, TailPos.Y] = BoxValues.Empty;
            SnakePositions = SnakePositions[1..];

            return field;
        }

        private Point MakeDirection(Moves move)
        {
            switch (move)
            {
                case Moves.Left:
                    return new(-1, 0);
                case Moves.Right:
                    return new(1, 0);
                case Moves.Up:
                    return new(0, -1);
                case Moves.Down:
                    return new(0, 1);
                default:
                    throw new NotImplementedException("Move not supported!");
            }
        }

        private bool IsOutSideField(Point point, Field field)
        {
            return point.X > field.field.GetLength(0) - 1 || point.Y > field.field.GetLength(1) - 1;
        }

        private bool IsCannibal(Point point, Field field)
        {
            return field.field[point.X, point.Y] == BoxValues.Snake;
        }

        private bool IsFood(Point point, Field field)
        {
            return field.field[point.X, point.Y] == BoxValues.Food;
        }

        public enum Moves
        {
            Left,
            Right,
            Up,
            Down,
        }
    }
}
