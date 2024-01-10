using Data;

namespace Infrastructure.Services.State
{
    public class GameStateService : IGameStateService
    {
        public GameState State { get; set; }
    }
}