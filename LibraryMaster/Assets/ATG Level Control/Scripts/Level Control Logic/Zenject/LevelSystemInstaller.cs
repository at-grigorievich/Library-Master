using Zenject;

namespace ATG.LevelControl
{
    public class LevelSystemInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindStatus();
            BindSystem();
        }

        private void BindSystem()
        {
            Container
                .Bind<ATG.LevelControl.ILevelSystem>()
                .To<ATG.LevelControl.ATGLevelSystem>()
                .AsSingle();
        }
        private void BindStatus()
        {
            Container
                .Bind<ILevelStatus>()
                .To<ATGLevelStatus>()
                .AsSingle();
        }
    }
}