using Application.Contracts;

namespace Application;

public struct LiveCell : ICell
{
    public ICell Transition(int liveNeighborsCount)
        => liveNeighborsCount is 2 or 3 ? new LiveCell() : new DeadCell();
}
