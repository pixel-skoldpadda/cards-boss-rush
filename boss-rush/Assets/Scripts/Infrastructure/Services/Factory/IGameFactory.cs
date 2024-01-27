using Items.Boss;

namespace Infrastructure.Services.Factory
{
    public interface IGameFactory
    {
        void CreateBossEnemy(BossType type);
    }
}