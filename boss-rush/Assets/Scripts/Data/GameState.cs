using System;
using System.Collections.Generic;
using GameObjects.Character;
using GameObjects.Character.Player;
using Ui.Hud;

namespace Data
{
    [Serializable]
    public class GameState
    {
        [NonSerialized] private List<Character> _characters = new();

        [NonSerialized] private Character _activeCharacter;
        [NonSerialized] private int _characterIndex;

        [NonSerialized] private Hud _hud;
        
        [NonSerialized] private Action _onTurnStarted;
        [NonSerialized] private Action _onTurnFinished;
        
        public Character GetOpponentCharacter()
        {
            return _characterIndex + 1 > _characters.Count - 1 ? _characters[0] : _characters[_characterIndex + 1];
        }

        public Character ActiveCharacter
        {
            get => _activeCharacter;
            set => _activeCharacter = value;
        }

        public int CharacterIndex
        {
            get => _characterIndex;
            set => _characterIndex = value;
        }

        public Hud HUD
        {
            get => _hud;
            set => _hud = value;
        }

        public Action OnTurnStarted
        {
            get => _onTurnStarted;
            set => _onTurnStarted = value;
        }

        public Action OnTurnFinished
        {
            get => _onTurnFinished;
            set => _onTurnFinished = value;
        }

        public List<Character> Characters => _characters;

        public Player GetPlayer()
        {
            foreach (Character character in _characters)
            {
                if (character.IsPlayer())
                {
                    return (Player)character;
                }
            }
            return null;
        }
    }
}