using Assets.Code.ECS.Components;

namespace Assets.Code.Interfaces
{
    public interface ISceneConfig
    {
        GameObjectData[] Platforms { get; set; }
    }
}
