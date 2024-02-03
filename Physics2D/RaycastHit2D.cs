using Godot;

namespace NoromaGD
{
    public struct RaycastHit2D
    {
        public GodotObject Collider;
        public int ColliderId;
        public Vector2 Normal;
        public Vector2 Position;
        public Rid Rid;
        public int Shape;

        public CollisionShape2D CollisionShape
        {
            get
            {
                if (Collider == null) return null;
                var obj = Collider as CollisionObject2D;
                if (obj == null) return null;
                var owner = obj.ShapeOwnerGetOwner((uint)ColliderId);
                if (owner == null) return null;
                return owner as CollisionShape2D;
            }
        }

        public static implicit operator bool(RaycastHit2D hit)
        {
            return hit.Collider != null;
        }
    }
}