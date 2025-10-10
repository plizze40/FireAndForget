using System;

namespace Core.Entities
{
    public struct Position : IEquatable<Position>
    {
        public int X;
        public int Y;

        public Position(int x = 0, int y = 0)
        {
            X = x;
            Y = y;
        }

        public static Position Zero => new(0, 0);

        public override string ToString() => $"({X}, {Y})";

        public static Position operator +(Position a, Position b) => new(a.X + b.X, a.Y + b.Y);
        public static Position operator -(Position a, Position b) => new(a.X - b.X, a.Y - b.Y);
        public static bool operator ==(Position a, Position b) => a.X == b.X && a.Y == b.Y;
        public static bool operator !=(Position a, Position b) => !(a == b);

        public override bool Equals(object obj) => obj is Position other && this == other;
        public override int GetHashCode() => X.GetHashCode() ^ Y.GetHashCode();

        public bool Equals(Position other)
        {
            return X == other.X && Y == other.Y;
        }
    }
}