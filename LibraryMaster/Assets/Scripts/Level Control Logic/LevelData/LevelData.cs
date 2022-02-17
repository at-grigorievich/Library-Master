using UnityEngine;

namespace ATG.LevelControl
{
    public enum LevelType
    {
        Line,
        Matrix,
        ZenjectLine,
        ZenjectMatrix,
        Static,
        ZenjectStatic
    }
    public enum LineLevelType
    {
        Line,
        ZenjectLine,
        Static,
        ZenjectStatic
    }

    public enum MatrixLevelType
    {
        Matrix,
        ZenjectMatrix
    }
    
    public abstract class LevelData : ScriptableObject, ILevel
    {
        [SerializeField] private int id;
        [SerializeField] private new string name;
        [SerializeField] private Color _backgroundColor;
        
        public int Id => id;
        public string Name => name;
        public Color BackgroundColor => _backgroundColor;

        public abstract LevelType TypeOfLevel { get;}


        public abstract T[] GetAnsInstantiateLevelBlocks<T>(ICreateLevelBehaviour createLevel)
            where T : ILevelBlock<MonoBehaviour>;

        protected GameObject CreateSceneDataObjects() => new GameObject("----SCENE----");
    }
}