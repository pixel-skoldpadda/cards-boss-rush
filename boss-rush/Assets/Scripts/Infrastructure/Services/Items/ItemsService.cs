using System.Collections.Generic;
using System.Linq;
using Items;
using Items.Boss;
using Ui.Windows;
using UnityEngine;

namespace Infrastructure.Services.Items
{
    public class ItemsService : IItemsService
    {
        private Dictionary<WindowType, WindowItem> _windows;
        private Dictionary<BossType, BossEnemyItem> _bossEnemyItems;

        private PlayerItem _playerItem;

        public void LoadAllItems()
        {
            LoadWindowItems();
            LoadPlayerItem();
            LoadBossEnemiesItems();
        }

        private void LoadPlayerItem()
        {
            _playerItem = Resources.Load<PlayerItem>(ItemsPath.PlayerItemPath);
        }

        private void LoadBossEnemiesItems()
        {
            _bossEnemyItems = Resources.LoadAll<BossEnemyItem>(ItemsPath.BossEnemiesItemsPath).ToDictionary(
                k => k.Type, v => v);
        }

        private void LoadWindowItems()
        {
            _windows = Resources.LoadAll<WindowItem>(ItemsPath.WindowItemsPath).ToDictionary(
                k => k.Type, v => v);
        }

        public WindowItem GetWindowItem(WindowType type)
        {
            return _windows.TryGetValue(type, out WindowItem item) ? item : null;
        }

        public PlayerItem PlayerItem => _playerItem;

        public BossEnemyItem GetBossEnemyItem(BossType type)
        {
            return _bossEnemyItems.TryGetValue(type, out BossEnemyItem item) ? item : null;
        }
    }
}