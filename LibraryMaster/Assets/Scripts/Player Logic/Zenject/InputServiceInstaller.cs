using PlayerLogic;
using UnityEngine;
using Zenject;

public class InputServiceInstaller : MonoInstaller
{
    [SerializeField] private PlayerInputService _inputService;
    
    public override void InstallBindings()
    {
        Container
            .Bind<IInputable>()
            .To<PlayerInputService>()
            .FromInstance(_inputService)
            .AsSingle()
            .NonLazy();
    }
}