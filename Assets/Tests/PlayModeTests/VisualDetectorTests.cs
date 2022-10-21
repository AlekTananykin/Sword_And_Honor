using Asserts.Code;
using NUnit.Framework;
using UnityEngine;

public class VisualDetectorTests
{
    private GameObject CreateParticipant(float xPos, float yPos, float zPos)
    {
        GameObject participant = new GameObject();
        participant.transform.position = new Vector3(xPos, yPos, zPos);
        var enemyCollider = participant.AddComponent<CapsuleCollider2D>();
        enemyCollider.bounds.size.Set(0.2f, 1f, 0f);
        enemyCollider.bounds.center.Set(0f, 0.5f, 0f);

        return participant;
    }

    [Test]
    public void EnemySeePlayerOnTheRight()
    {
        var playerPosition = new Vector3(0.5f, 0f, 0f);
        GameObject player = CreateParticipant(
            playerPosition.x, playerPosition.y, playerPosition.z);
        player.layer = GameLayers.PlayerLayer;

        GameObject enemy = CreateParticipant(0.0f, 0f, 0f);
        enemy.layer = GameLayers.EnemyLayer;

        var visualDetector = enemy.AddComponent<VisualDetector>();
        visualDetector.VisionDistance = 1f;
        visualDetector.VisionRayPosition = new Vector3(0f, 0.5f, 0f);

        Vector3 targetPosition = new Vector3();

        const bool isPlayerOnTheRight = true;
        bool result = visualDetector.ToSee(isPlayerOnTheRight, 
            ref targetPosition);

        Assert.IsTrue(result);
        Assert.AreEqual(playerPosition, targetPosition);
    }

    [Test]
    public void EnemySeePlayerOnTheLeft()
    {
        var playerPosition = new Vector3(-0.5f, 0f, 0f);
        GameObject player = CreateParticipant(
            playerPosition.x, playerPosition.y, playerPosition.z);
        player.layer = GameLayers.PlayerLayer;

        GameObject enemy = CreateParticipant(0.0f, 0f, 0f);
        enemy.layer = GameLayers.EnemyLayer;

        var visualDetector = enemy.AddComponent<VisualDetector>();
        visualDetector.VisionDistance = 1f;
        visualDetector.VisionRayPosition = new Vector3(0f, 0.5f, 0f);

        Vector3 targetPosition = new Vector3();

        const bool isPlayerOnTheRight = false;
        bool result = visualDetector.ToSee(isPlayerOnTheRight,
            ref targetPosition);

        Assert.IsTrue(result);
        Assert.AreEqual(playerPosition, targetPosition);
    }

    [Test]
    public void PlayerOnTheRightIsTooFar()
    {
        var playerPosition = new Vector3(2f, 0f, 0f);
        GameObject player = CreateParticipant(
            playerPosition.x, playerPosition.y, playerPosition.z);
        player.layer = GameLayers.PlayerLayer;

        GameObject enemy = CreateParticipant(0.0f, 0f, 0f);
        enemy.layer = GameLayers.EnemyLayer;

        var visualDetector = enemy.AddComponent<VisualDetector>();
        visualDetector.VisionDistance = 1f;
        visualDetector.VisionRayPosition = new Vector3(0f, 0f, 0f);

        Vector3 targetPosition = new Vector3();
        bool result = visualDetector.ToSee(true, ref targetPosition);

        Assert.IsFalse(result);
    }

    [Test]
    public void PlayerOnTheLeftIsTooFar()
    {
        var playerPosition = new Vector3(2f, 0f, 0f);
        GameObject player = CreateParticipant(
            playerPosition.x, playerPosition.y, playerPosition.z);
        player.layer = GameLayers.PlayerLayer;

        GameObject enemy = CreateParticipant(0.0f, 0f, 0f);
        enemy.layer = GameLayers.EnemyLayer;

        var visualDetector = enemy.AddComponent<VisualDetector>();
        visualDetector.VisionDistance = 1f;
        visualDetector.VisionRayPosition = new Vector3(0f, 0f, 0f);

        Vector3 targetPosition = new Vector3();
        bool result = visualDetector.ToSee(true, ref targetPosition);

        Assert.IsFalse(result);
    }
}
