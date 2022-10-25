using UnityEngine;

namespace Asserts.Code
{
    public static class Identifiers
    {
        public const string HorizontalAxis = "Horizontal";
        public const string Jump = "Jump";
        public const string Attack = "Attack";
        public const string Idle = "Idle";

        public const string PlayerPrefabName = "Knights/Knight";

        public const float UnitAnimationSpeed = 10.0f;

        public const int Damege = 5;

        public const string PlatformsConfigPath = 
            "Configs/SceneConfig";
        public const string EnemiesLocationConfigPath = 
            "Configs/EnemiesLocationConfig";
        public const string TrapsLocationConfigPath =
            "Configs/TrapsLocationConfig";
        public const string GiftsLocationConfigPath =
            "Configs/GiftsLocationConfig";

        public const string MinusFiveView =
            "MinusDigits/MinusFive";
    }

    public static class GameLayers
    {
        public const int EnemyLayer = 3;
        public const int PlayerLayer = 6;

        public static int GetTargetMaskByLayer(int layer)
        {
            switch (layer)
            {
                case PlayerLayer:
                    {
                        return EnemyLayerMask;
                    }
                case EnemyLayer:
                    {
                        return PlayerLayerMask;
                    }
                default:
                    {
                        Debug.LogError($"Unknown layer: {layer}");
                        return 0;
                    }
             }
        }

        public const int EnemyLayerMask = 1 << EnemyLayer;
        public const int PlayerLayerMask = 1 << PlayerLayer;
    }

    public static class PrefabPathes
    {
        public const string DragonPrefabPath = "Dragon";
        public const string KnightPrefabPath = "Knight";
        public const string ChestPrefabPath = "Chest";
    }

    public enum CharacterType { Dragon }
}
