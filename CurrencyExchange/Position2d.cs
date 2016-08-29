namespace CurrencyExchange
{
    using System;

    public class Position2D : ICloneable
    {
        public Position2D(int row, int col)
        {
            this.Row = row;
            this.Col = col;
        }

        public int Row { get; set; }

        public int Col { get; set; }

        public object Clone()
        {
            return new Position2D(this.Row, this.Col);
        }
    }
}
