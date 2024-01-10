using Data;

namespace Infrastructure.Services.State
{
    public interface IGameStateService
    {
        GameState State { get; set; }
    }
}