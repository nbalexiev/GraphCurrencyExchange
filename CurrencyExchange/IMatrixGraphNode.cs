namespace CurrencyExchange
{
    public interface IMatrixGraphNode<T> : IGraphNode<T>
    {
        Position2D Position { get; set; }

        //In the matrix each node must hold a reference to the graph it's contained in in order to implement the IGraph interface.
        MatrixWorld Graph { get; set; }
    }
}
