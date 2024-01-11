using Items;
using Items.Boss;
using Ui.Windows;

namespace Infrastructure.Services.Items
{
    public interface IItemsService
    {
        void LoadAllItems();
        WindowItem GetWindowItem(WindowType type);
        BossEnemyItem GetBossEnemyItem(BossType type);
        PlayerItem PlayerItem { get; }
    }
}