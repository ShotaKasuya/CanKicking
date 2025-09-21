using System;
using UnityEngine;

namespace Module.EditorExtension.Runtime
{
    [Serializable]
    public struct TagSelector
    {
        [SerializeField] private string tag;

        public TagSelector(string t)
        {
            tag = t;
        }

        public static implicit operator string(TagSelector t) => t.tag;
        public static implicit operator TagSelector(string s) => new TagSelector(s);

        public override string ToString() => tag ?? string.Empty;
    }
}