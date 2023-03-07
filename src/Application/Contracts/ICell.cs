namespace Application.Contracts;

public interface ICell
{
    ICell Transition(int liveNeighborsCount);
}
