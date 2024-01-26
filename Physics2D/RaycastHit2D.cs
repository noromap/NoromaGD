using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoromaGD
{
    public struct RaycastHit2D
    {
        public CollisionObject2D Collider;
        public ulong ColliderId;
        public Vector2 Normal;
        public Vector2 Position;
        public Rid Rid;
        public CollisionShape2D Shape;

        public static implicit operator bool(RaycastHit2D hit)
        {
            return hit.Collider != null;
        }
    }
}