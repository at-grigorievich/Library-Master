using System;
using UnityEngine;

namespace ATG.LevelControl
{
    public class LineLevelBehaviour: ICreateLevelBehaviour
    {
        public T[] InstantiateBlocks<T,K>(K[] blocks, GameObject blocksParent) 
            where T : ILevelBlock<MonoBehaviour>
        {
            var arr = new T[blocks.Length];
            
            Vector3 lastPos = Vector3.zero;
            
            for (int i = 0; i < blocks.Length; i++)
            {
                var blockPos = lastPos;

                if (blocks[i] is MonoBehaviour beh)
                {
                    var b = GameObject.Instantiate(beh);

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
                else
                {
                    throw new ArgumentException("blocks array is not spawnable !");
                }
            }

            return arr;
        }
    }
}