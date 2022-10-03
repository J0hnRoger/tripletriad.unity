#if (UNITY_EDITOR)
using UnityEditor;
#endif

namespace RogerFamily.Editor
{
    #if (UNITY_EDITOR)
    [FilePath("Assets/SceneSelectorTool/Editor/SceneSelectorSettings.asset", FilePathAttribute.Location.ProjectFolder)]
    public class SceneSelectorSettings : ScriptableSingleton<SceneSelectorSettings>
    {
        public string PreviousScenePath;

        void OnDestroy()
        {
            Save(true);
        }
    }
    #endif
}