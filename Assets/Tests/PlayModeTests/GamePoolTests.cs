using Assets.Code.Interfaces;
using Assets.Code.Pools;
using Moq;
using NUnit.Framework;
using UnityEngine;

public class GamePoolTests
{
    [Test]
    public void GamePoolTestsSimplePasses()
    {
        GameObject go = new GameObject();

        IGameObjectFabric gameOBjectFabric = Mock.Of<IGameObjectFabric>(
            d => d.Load(It.IsAny<string>()) == go);

        SimplePool pool = new SimplePool(
            gameOBjectFabric, "path to prefab GameObject");

        var poolGameObject = pool.GetGameObject();
        Assert.AreEqual(go, poolGameObject);
    }
}
