using UnityEngine;

namespace Items
{
    public abstract class Item : ScriptableObject
    {
        [SerializeField] protected string itemName;
        [SerializeField] protected string description;
        
        public string ItemName => itemName;
        public string Description => description;
    }
}