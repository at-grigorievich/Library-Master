using System;
using UnityEngine;
using Zenject;

namespace ATG.LevelControl
{
    public class ZenjectStaticLevelBehaviour<K> : ICreateLevelBehaviour
        where K: MonoBehaviour
    {
        private readonly PlaceholderFactory<GameObject, K> _factory;

        public ZenjectStaticLevelBehaviour(PlaceholderFactory<GameObject, K> factory)
        {
            _factory = factory;
        }


        public T[] InstantiateBlocks<T, K1>(K1[] blocks, GameObject blocksParent) where T : ILevelBlock<MonoBehaviour>
        {
            var arr = new T[blocks.Length];

            for (int i = 0; i < blocks.Length; i++)
            {
                if (blocks[i] is MonoBehaviour beh)
                {
                    var spawnedBlock = _factory.Create(beh.gameObject);

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