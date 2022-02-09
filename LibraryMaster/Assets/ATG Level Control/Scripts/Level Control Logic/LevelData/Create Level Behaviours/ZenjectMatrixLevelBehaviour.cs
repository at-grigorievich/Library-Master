using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ATG.LevelControl
{
    public class ZenjectMatrixLevelBehaviour<K>: ICreateLevelBehaviour
        where K: MonoBehaviour
    {
        private readonly List<float> _lastVertical = new List<float>();
        private readonly PlaceholderFactory<GameObject, K> _factory;

        public ZenjectMatrixLevelBehaviour(PlaceholderFactory<GameObject, K> factory)
        {
            _factory = factory;
        }

        public T[] InstantiateBlocks<T, K>(K[] blocks, GameObject blocksParent) where T : ILevelBlock<MonoBehaviour>
        {
            var arr = new T[blocks.Length];
            
            Vector3 lastHorizontal = Vector3.zero;
            
            for (int i = 0; i < blocks.Length; i++)
            {
                if (blocks[i] is MonoBehaviour beh)
                {
                    var b = _factory.Create(beh.gameObject);

                    if (b is T block)
                    {
                        arr[i] = block;
                        
                        Vector3 middle = (i > 0 ? new Vector3(0f,arr[i].Size.y *2f,arr[i].Size.z) / 2f : Vector3.up*arr[i].Size.y);
                        
                        Vector3 vertical = _lastVertical.Count > i ? (_lastVertical[i] + arr[i].Size.x/2f  )* Vector3.right : Vector3.zero;

                        b.transform.position = lastHorizontal + middle + vertical;
                        b.transform.rotation = Quaternion.identity;
                        b.transform.SetParent(blocksParent.transform);

                        if (_lastVertical.Count >= i+1)
                        {
                            _lastVertical[i] = b.transform.position.x + arr[i].Size.x /2f;
                        }
                        else
                        {
                            _lastVertical.Add(b.transform.position.x + arr[i].Size.x /2f);
                        }
                        
                        lastHorizontal = new Vector3(0f,0f,b.transform.position.z) + new Vector3(0f,0f,arr[i].Size.z)/2f;
                        
                    }
                    else
                    {
                        throw new ArgumentException($"{b.transform.name} hasn't environment block type!");
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