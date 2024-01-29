using System.Collections.Generic;
using Items;
using Items.Boss;
using Ui.Windows;

namespace Infrastructure.Services.Items
{
    public interface IItemsService
    {
        void LoadAllItems();
        WindowItem GetWindowItem(WindowType type);
        PlayerItem PlayerItem { get; }
        List<BossEnemyItem> BossEnemyItems { get; }
    }
}