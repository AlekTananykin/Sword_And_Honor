using Asserts.Code;
using System;
using UnityEngine;

namespace Assets.Code.ECS.Components
{
    [Serializable]
    public struct Bud
    {
        public Vector3 Position;
        public string Path;
        
        public Vector3 LeftUpperPoint;
        public Vector3 LowBottomPoint;
    }
}
