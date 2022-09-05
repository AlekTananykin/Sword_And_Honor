using Asserts.Code;
using System;
using UnityEngine;

namespace Assets.Code.ECS.Components
{
    [Serializable]
    public struct CharacterData
    {
        public Vector3 Position;
        public CharacterType Type;
    }
}
