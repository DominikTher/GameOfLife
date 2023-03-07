using Application.Contracts;

namespace Application;

public struct Coordinates
{
    public int X { get; set; }
    public int Y { get; set; }
    public ICell Cell { get; set; }

    public Coordinates(int x, int y, ICell cell)
    {
        X = x;
        Y = y;
        Cell = cell;
    }

    public int GetLiveNeighborsCount(IDictionary<string, Coordinates> universe, int x, int y)
    {
        var neighborsCount = 0;

        for (int row = -1; row < 2; row++)
        {
            for (int column = -1; column < 2; column++)
            {
                var r = (X + row + x) % x;
                var c = (Y + column + y) % y;

                if (sameCell(X, Y, r, c))
                {
                    continue;
                }

                var cell = universe[$"{r}:{c}"].Cell;

                if (cell is LiveCell)
                {
                    neighborsCount++;
                }
            }
        }

        return neighborsCount;

        static bool sameCell(int x, int y, int row, int column)
            => row == x && column == y;
    }
}
