using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace ATG.LevelControl
{
    public class ATGLevelSystem: ILevelSystem, IInitializable
    {
        private const string LevelInfoRef = "Level_ID";

        [Inject] private LevelDataList _levelDataList;
        [Inject] private PlayerData.PlayerData _playerData;
        [Inject] private ILevelStatus _levelStatus;

        [Inject] private EnvironmentBlock.Factory _blockFactory;
        
        public int CurrentLevelId => PlayerPrefs.HasKey(LevelInfoRef) ? PlayerPrefs.GetInt(LevelInfoRef) : 1;
        
        public ILevel CurrentLevel => _levelDataList.FindLevelData(CurrentLevelId);
        public EnvironmentBlock[] BlockInstances { get; private set; }
        

        [Inject]
        public virtual void Initialize()
        {
            _levelStatus.OnCompleteLevel += (sender, args) =>
            {
                SaveLevel(CurrentLevelId + 1);
                _playerData.UpdateLevel();
            };
        }
        
        
        public virtual void LoadLevel()
        {
            ICreateLevelBehaviour levelBehaviour = null;
            switch (CurrentLevel.TypeOfLevel)
            {
                case LevelType.Line : 
                    levelBehaviour = new LineLevelBehaviour();
                    break;
                case LevelType.Matrix :
                    levelBehaviour = new MatrixLevelBehaviour();
                    break;
                case LevelType.Static:
                    levelBehaviour = new StaticLevelBehaviour();
                    break;

                case LevelType.ZenjectLine :
                    levelBehaviour = new ZenjectLineLevelBehaviour<EnvironmentBlock>(_blockFactory);
                    break;
                case LevelType.ZenjectMatrix :
                    levelBehaviour = new ZenjectMatrixLevelBehaviour<EnvironmentBlock>(_blockFactory);
                    break;
                case LevelType.ZenjectStatic :
                    levelBehaviour = new ZenjectStaticLevelBehaviour<EnvironmentBlock>(_blockFactory);
                    break;
            }
            
            BlockInstances = CurrentLevel.GetAnsInstantiateLevelBlocks<EnvironmentBlock>(levelBehaviour);
        }
        
        
        public virtual void LoadNextLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        
        public void ReloadLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
       
        public void SaveLevel(int level)
        {
            int savedLevel = level > 0 && level <= _levelDataList.LevelsCount ? level : 1;
            PlayerPrefs.SetInt(LevelInfoRef,savedLevel);
        }
    }
}