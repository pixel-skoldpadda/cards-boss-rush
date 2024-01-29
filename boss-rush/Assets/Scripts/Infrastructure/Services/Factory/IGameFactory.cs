using Infrastructure.States;
using Items.Boss;

namespace Infrastructure.Services.Factory
{
    public interface IGameFactory
    {
        void CreateBossEnemy(BossEnemyItem item, IGameStateMachine stateMachine);
    }
}