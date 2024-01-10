using Zenject;

namespace Infrastructure.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly DiContainer _diContainer;

        [Inject]
        public GameFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }
    }
}