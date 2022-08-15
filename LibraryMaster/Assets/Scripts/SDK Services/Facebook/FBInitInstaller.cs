using UnityEngine;
using Zenject;

public class FBInitInstaller : MonoInstaller
{
    [SerializeField] private FacebookInit _fbInitService;
    
    public override void InstallBindings()
    {
        Container
            .Bind<FacebookInit>()
            .FromComponentInNewPrefab(_fbInitService)
            .AsSingle()
            .NonLazy();
    }
}