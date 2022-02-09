using System;

namespace ATG.LevelControl
{
    public class ATGLevelStatus: ILevelStatus
    {
        public event EventHandler OnLevelStart;
        public event EventHandler OnCompleteLevel;
        public event EventHandler OnFailedLevel;
        
        public void StartLevel()
        {
            OnLevelStart?.Invoke(this,null);
        }

        public void CompleteLevel()
        {
            OnCompleteLevel?.Invoke(this,null);
        }

        public void FailedLevel()
        {
            OnFailedLevel?.Invoke(this,null);
        }
    }
}