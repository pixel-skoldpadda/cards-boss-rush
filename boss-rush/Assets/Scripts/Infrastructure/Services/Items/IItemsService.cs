using Items;
using Ui.Windows;

namespace Infrastructure.Services.Items
{
    public interface IItemsService
    {
        void LoadAllItems();
        WindowItem GetWindowItem(WindowType type);
    }
}