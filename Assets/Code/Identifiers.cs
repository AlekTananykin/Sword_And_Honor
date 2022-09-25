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
    }

    public static class PrefabPathes
    {
        public const string DragonPrefabPath = "Dragon";
        public const string KnightPrefabPath = "Knight";
        public const string ChestPrefabPath = "Chest";

    }

    public enum CharacterType { Dragon }
}
