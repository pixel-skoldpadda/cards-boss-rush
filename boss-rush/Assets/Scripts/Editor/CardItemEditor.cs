using Items.Card;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(CardItem))]
    public class CardItemEditor : UnityEditor.Editor
    {
        private CardItem _cardItem;

        private void OnEnable()
        {
            _cardItem = (CardItem) target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (_cardItem.CardIcon == null)
            {
                return;
            }

            Texture2D texture2D = AssetPreview.GetAssetPreview(_cardItem.CardIcon);
            GUILayout.Label("", GUILayout.Height(104), GUILayout.Width(74));
            GUI.DrawTexture( GUILayoutUtility.GetLastRect(), texture2D);
        }
    }
}