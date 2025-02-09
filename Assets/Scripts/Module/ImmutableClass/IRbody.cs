using UnityEngine;

namespace Module.ImmutableClass
{
    public class ConstRbody
    {
        public ConstRbody(Rigidbody2D rigidbody2D)
        {
            Instance = rigidbody2D;
        }
        
        private Rigidbody2D Instance { get; }
        public Vector2 Velocity => Instance.linearVelocity;
        public float AnglerVelocity => Instance.angularVelocity;
    }
}