using System.Drawing;

namespace blazorSnake.Client
{
    public class Snake
    {
        private bool Alive = true;
        private int Length;
        private Point HeadPos;
        private Point TailPos;
        private List<Point> SnakePositions;
        private Moves LastMove = Moves.Up;
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

            HeadPos= newHeadPos;
            SnakePositions.Add(newHeadPos);

            if(IsFood(newHeadPos, field))
            {
                field.field[newHeadPos.X, newHeadPos.Y] = BoxValues.Snake;
                Length++;
                field.PlaceFood();
                return field;
            }
            field.field[newHeadPos.X, newHeadPos.Y] = BoxValues.Snake;

            field.field[TailPos.X, TailPos.Y] = BoxValues.Empty;
                SnakePositions = SnakePositions[1..];
                TailPos = SnakePositions.First();
                return field;
            
        }

        private Point MakeDirection(Moves move)
        {
            Point newMove;

            switch (move)
            {
                case Moves.Left:
                    if (LastMove == Moves.Right) 
                    {
                        return MakeDirection(Moves.Right);
                    }
                    newMove = new(-1, 0);
                    break;
                case Moves.Right:
                    if (LastMove == Moves.Left)
                    {
                        return MakeDirection(Moves.Left);
                    }
                    newMove = new(1, 0);
                    break;
                case Moves.Up:
                    if (LastMove == Moves.Down)
                    {
                        return MakeDirection(Moves.Down);
                    }
                    newMove = new(0, -1);
                    break;
                case Moves.Down:
                    if (LastMove == Moves.Up)
                    {
                        return MakeDirection(Moves.Up);
                    }
                    newMove = new(0, 1);
                    break;
                default:
                    throw new NotImplementedException("Move not supported!");
            }
            LastMove = move;
            return newMove;
        }

        private bool IsOutSideField(Point point, Field field)
        {
            bool xOut = point.X < field.field.GetLength(0) - 1;
            bool yOut = point.Y < field.field.GetLength(1) - 1;

            return (point.X > field.field.GetLength(0) - 1 || point.Y > field.field.GetLength(1) - 1 || point.Y < 0 || point.X < 0);
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
