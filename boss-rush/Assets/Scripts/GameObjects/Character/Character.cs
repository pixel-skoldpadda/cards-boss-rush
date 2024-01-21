using Items;
using Ui.Hud;
using UnityEngine;

namespace GameObjects.Character
{
    public abstract class Character : MonoBehaviour
    {
        [SerializeField] protected HealthBar healthBar;

        protected CharacterItem item;
        protected int health;

        protected CardsDeck cardsDeck;

        protected void Construct(CharacterItem characterItem)
        {
            item = characterItem;
            health = characterItem.MaxHealth;
            
            CreateCardsDeck();
        }

        protected abstract void CreateCardsDeck();
    }
}