using UnityEditor;
using UnityEngine;

namespace Code.Saves.Utils
{
    public static class SaveUtils
    {
#if UNITY_EDITOR
        [MenuItem("Tools/Save/DeleteSave")]
        public static void DeleteSave()
        {
            PlayerPrefs.DeleteAll();

            if (EditorApplication.isPlaying)
            {
                EditorApplication.ExitPlaymode();
            }
        }
#endif
    }
}