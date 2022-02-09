using System;
using UnityEngine;
using Zenject;

namespace ATG.LevelControl
{
    public class ZenjectLineLevelBehaviour<K>: ICreateLevelBehaviour
        where K: MonoBehaviour
    {
        private readonly PlaceholderFactory<GameObject, K> _factory;

        public ZenjectLineLevelBehaviour(PlaceholderFactory<GameObject, K> factory)
        {
            _factory = factory;
        }

        public T[] InstantiateBlocks<T,K>(K[] blocks, GameObject blocksParent) 
            where T : ILevelBlock<MonoBehaviour>
        {
            var arr = new T[blocks.Length];
            
            Vector3 lastPos = Vector3.zero;
            
            for (int i = 0; i < blocks.Length; i++)
            {
                if (blocks[i] is MonoBehaviour beh)
                {
                    var b = _factory.Create(beh.gameObject);
                    
                    if (b is T block)
                    {
                        arr[i] = block;

                        Vector3 middle = (i > 0 ? arr[i].Size / 2f : Vector3.zero);
                        
                        b.transform.position = lastPos + middle;
                        b.transform.rotation = Quaternion.identity;
                        b.transform.SetParent(blocksParent.transform);
                        
                        lastPos = b.transform.position + arr[i].Size/2f;
                    }
                    else
                    {
                        throw new ArgumentException($"{b.transform.name} hasn't environment block type!");
                    }
                }
            }

            return arr;
        }
    }
}