using UnityEngine;

namespace Items
{
    public abstract class Item : ScriptableObject
    {
        [SerializeField] protected string itemName;
        
        [TextArea]
        [SerializeField] protected string description;
        
        public string ItemName => itemName;
        public string Description => description;
    }
}