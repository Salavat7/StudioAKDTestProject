using Mediators;
using PlayerInput;
using UnityEngine;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
    [SerializeField] private CharacterBehavior _character;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public override void InstallBindings()
    {
        Container.BindInterfacesTo<KeyboardMouseInput>().AsSingle();
        Container.Bind<CharacterBehavior>().FromInstance(_character).AsSingle();
        Container.Bind<CharacterInputMediator>().AsSingle().NonLazy();
    }
}
