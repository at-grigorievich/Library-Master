using UnityEngine;

namespace ATG.LevelControl
{
    public interface ILevelBlock<T>
    {
        Vector3 Size { get; }
    }
}