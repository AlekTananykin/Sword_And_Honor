
using UnityEngine;

namespace Assets.Code.Fabrics
{
    public sealed class ResourceLoader
    {
        public static GameObject Load(string path)
        {
            return Resources.Load<GameObject>(path);
        }
    }
}
