using Items.Boss;

namespace Infrastructure.Services.Factory
{
    public interface IGameFactory
    {
        void CreatePlayer();
        void CreateBossEnemy(BossType type);
    }
}