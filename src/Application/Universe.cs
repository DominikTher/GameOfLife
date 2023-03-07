using Application.Contracts;

namespace Application;

public class Universe
{
    private IDictionary<string, Coordinates> universe;
    private readonly Random random;

    public int X { get; private set; }
    public int Y { get; private set; }

    public Universe(int x, int y)
    {
        X = x;
        Y = y;
        universe = new Dictionary<string, Coordinates>(X * Y);
        random = new Random();
    }

    public void AddLiving(int x, int y)
    {
        universe.Remove(GetKey(x, y));
        universe.Add(GetKey(x, y), new Coordinates(x, y, new LiveCell()));
    }

    public void AddDead(int x, int y)
    {
        universe.Remove(GetKey(x, y));
        universe.Add(GetKey(x, y), new Coordinates(x, y, new DeadCell()));
    }

    public ICell GetCell(int x, int y)
        => universe[GetKey(x, y)].Cell;

    public void InitializeRandom()
    {
        Initialize(() => random.Next(0, 2) == 0 ? new DeadCell() : new LiveCell());
    }

    public void InitializeDead()
    {
        Initialize(() => new DeadCell());
    }

    public void Initialize(Func<ICell> cellInitialization)
    {
        universe = new Dictionary<string, Coordinates>(X * Y);

        for (int row = 0; row < X; row++)
        {
            for (int column = 0; column < Y; column++)
            {
                universe.Add(GetKey(row, column), new Coordinates(row, column, cellInitialization()));
            }
        }
    }

    public void NewGeneration()
    {
        var newUniverseCells = universe
            .Select(coordinates => (Coordinates: coordinates.Value, LiveNeighborsCount: coordinates.Value.GetLiveNeighborsCount(universe, X, Y)))
            .Select(data => data.Coordinates with { Cell = data.Coordinates.Cell.Transition(data.LiveNeighborsCount) })
            .ToDictionary(coordinates => GetKey(coordinates.X, coordinates.Y), coordinates => coordinates);

        universe = newUniverseCells!;
    }

    private static string GetKey(int x, int y)
        => $"{x}:{y}";
}