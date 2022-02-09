using System;
using UnityEngine;

namespace ATG.LevelControl
{
    public class StaticLevelBehaviour: ICreateLevelBehaviour
    {
        public T[] InstantiateBlocks<T, K>(K[] blocks, GameObject blocksParent) 
            where T : ILevelBlock<MonoBehaviour>
        {
            var arr = new T[blocks.Length];

            for (int i = 0; i < blocks.Length; i++)
            {
                if (blocks[i] is MonoBehaviour beh)
                {
                    var spawnedBlock = GameObject.Instantiate(beh);

                    if (spawnedBlock is T lvlBlock)
                    {
                        spawnedBlock.transform.position = lvlBlock.Size;
                        spawnedBlock.transform.rotation = Quaternion.identity;

                        arr[i] = lvlBlock;
                    }
                    else
                    {
                        throw new ArgumentException($"{spawnedBlock.transform.name} hasn't environment block type!");
                    }
                    
                }
                else
                {
                    throw new ArgumentException("blocks array is not spawnable !");
                }
            }

            return arr;
        }
    }
}