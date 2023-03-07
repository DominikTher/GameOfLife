using Application.Contracts;

namespace Application;

public struct DeadCell : ICell
{
    public ICell Transition(int liveNeighborsCount)
        => liveNeighborsCount is 3 ? new LiveCell() : new DeadCell();
}
