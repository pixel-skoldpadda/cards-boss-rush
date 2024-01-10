using Ui.Windows;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "_WINDOW_ITEM", menuName = "Items/Window")]
    public class WindowItem : ScriptableObject
    {
        [SerializeField] private WindowType type;
        [SerializeField] private GameObject prefab;

        public WindowType Type => type;
        public GameObject Prefab => prefab;
    }
}
