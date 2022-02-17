using UnityEngine;

namespace ATG.LevelControl
{
    public interface ILevel
    {
        int Id { get; }
        string Name { get; }
        
        Color BackgroundColor { get; }
        
        LevelType TypeOfLevel { get; }
        
        T[] GetAnsInstantiateLevelBlocks<T>(ICreateLevelBehaviour _createLevel) 
            where T:ILevelBlock<MonoBehaviour>;
    }
}