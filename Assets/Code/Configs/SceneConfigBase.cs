using Assets.Code.ECS.Components;
using System;
using System.Text;
using UnityEngine;

namespace Assets.Code.Configs
{
    public class SceneConfigBase<TargetMarkers>: ScriptableObject
        where TargetMarkers: MonoBehaviour
    {
        public Bud this[int index]
        {
            get
            {
                return _targets[index];
            }
        }

        public int Count => _targets == default ? 0 : _targets.Length;

        public object Current => throw new NotImplementedException();

        [SerializeField]
        private Bud[] _targets = default;

        protected void AddTargets(TargetMarkers[] targetMarkers)
        {
            _targets = new Bud[targetMarkers.Length];
            for (int i = 0; i < targetMarkers.Length; ++i)
            {
                _targets[i].Position =
                    targetMarkers[i].gameObject.transform.position;
                _targets[i].Path =
                    CreatePath(targetMarkers[i].gameObject.transform.name);
            }
        }

        private string CreatePath(string name)
        {
            int subPos = name.IndexOf('_');
            StringBuilder pathBuilder = new StringBuilder();

            if (subPos < 0)
            {
                pathBuilder.Append(name);
            }
            else
            {
                pathBuilder.Append(name.Substring(0, subPos));
            }
            pathBuilder.Append("s/");
            pathBuilder.Append(name);
            return pathBuilder.ToString();
        }
    }
}
