using UnityEngine;
using Zenject;

namespace ATG.LevelControl
{
    public class ATGLevelLoader : MonoBehaviour
    {
        [SerializeField] private bool IsClearPlayerPrefs;
        
        [Inject] private ILevelSystem _levelSystem;
        
        private void Awake()
        {
#if UNITY_EDITOR
            if (IsClearPlayerPrefs)
            {
                PlayerPrefs.DeleteAll();
                UnityEditor.EditorApplication.isPlaying = false;
            }
#endif
            
            _levelSystem.LoadLevel();
        }
    }
}
