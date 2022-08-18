using Assets.Code.Components.Commands;

namespace Assets.Code.Interfaces
{
    interface ICommandAnimationController
    {
        bool IsReady(int entity);

        ref IsReadyToGetCommandComponent GetReadyComponent(int entity);
    }
}
