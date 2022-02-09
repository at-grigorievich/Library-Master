using UnityEngine;

namespace ATG.LevelControl
{
    public interface ICreateLevelBehaviour
    {
        T[] InstantiateBlocks<T, K>(K[] blocks, GameObject blocksParent)
            where T : ILevelBlock<MonoBehaviour>;
    }
}