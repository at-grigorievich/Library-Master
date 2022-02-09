using UnityEngine;
using Zenject;

namespace ATG.LevelControl
{
    public class EnvironmentBlock: MonoBehaviour, ILevelBlock<MonoBehaviour>
    {
        [SerializeField] private Vector3 size;

        public Vector3 Size => size;
        
        public class Factory: PlaceholderFactory<GameObject,EnvironmentBlock>{}
    }
}