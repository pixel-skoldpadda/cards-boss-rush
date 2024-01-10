using UnityEditor;
using UnityEngine;

namespace Editor
{
    public abstract class Tools
    {
        [MenuItem("Tools/Game state/Clear")]
        public static void ClearPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}