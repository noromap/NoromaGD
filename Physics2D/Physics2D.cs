using Godot;
using Godot.Collections;
using System.Linq;

namespace NoromaGD
{
    public static class Physics2D
    {
        private static CircleShape2D _circleShape = new CircleShape2D();
        private static RectangleShape2D _rectangleShape = new RectangleShape2D();

        /// <summary>
        /// IntersectRayのラッパー関数
        /// </summary>
        /// <param name="space"></param>
        /// <param name="origin">起点</param>
        /// <param name="direction">方向</param>
        /// <param name="distance">距離</param>
        /// <param name="collisionMask">コリジョン判定をするマスク</param>
        /// <param name="collideWithAreas">trueの場合、Area2Dとも衝突判定を行う</param>
        /// <param name="includeInside">trueの場合、起点に重なっているShape2Dとも衝突判定を行う。この場合、normalはVector2(0,0)を返す。</param>
        /// <param name="collideWithBodies">trueの場合、PhysicsBody2Dとも衝突判定を行う。</param>
        /// <param name="exclude"></param>
        public static RaycastHit2D Raycast(
            this PhysicsDirectSpaceState2D space,
            Vector2 origin,
            Vector2 direction,
            float distance,
            uint collisionMask = 0xffffffff,
            bool collideWithAreas = false,
            bool includeInside = false,
            bool collideWithBodies = true,
            params Rid[] exclude
            )
        {
            var query = PhysicsRayQueryParameters2D.Create(origin, origin + direction * distance, collisionMask, new Array<Rid>(exclude));
            query.CollideWithAreas = collideWithAreas;
            query.HitFromInside = includeInside;
            query.CollideWithBodies = collideWithBodies;
            Dictionary result = space.IntersectRay(query);
            if (result.Count == 0) return new RaycastHit2D();
            return result.ToRaycastHit2D();
        }

        public static RaycastHit2D[] ShapeCast(
            this PhysicsDirectSpaceState2D space,
            Shape2D shape,
            Transform2D transform,
            Vector2 direction,
            float distance,
            uint collisionMask = 0xffffffff,
            bool collideWithAreas = false,
            bool includeInside = false,
            bool collideWithBodies = true,
            int maxResultCount = 32,
            params Rid[] exclude
            )
        {
            var query = new PhysicsShapeQueryParameters2D()
            {
                Transform = transform,
                CollideWithAreas = collideWithAreas,
                CollideWithBodies = collideWithBodies,
                CollisionMask = collisionMask,
                Exclude = new Array<Rid>(exclude),
                Shape = shape
            };
            Array<Dictionary> result = space.IntersectShape(query, maxResultCount);
            if (result == null || result.Count == 0) return null;
            return result.Select(d => d.ToRaycastHit2D()).ToArray();
        }

        public static RaycastHit2D[] CircleCast(
            this PhysicsDirectSpaceState2D space,
            Vector2 origin,
            Vector2 direction,
            float distance,
            float radius,
            uint collisionMask = 0xffffffff,
            bool collideWithAreas = false,
            bool includeInside = false,
            bool collideWithBodies = true,
            int maxResultCount = 32,
            params Rid[] exclude
            )
        {
            _circleShape.Radius = radius;
            Transform2D transform = new Transform2D(0, origin);
            return ShapeCast(space, _circleShape, transform, direction, distance, collisionMask, collideWithAreas, includeInside, collideWithBodies, maxResultCount, exclude);
        }

        public static RaycastHit2D[] BoxCast(
            this PhysicsDirectSpaceState2D space,
            Vector2 origin,
            Vector2 direction,
            float distance,
            Vector2 size,
            float angleRad = 0,
            uint collisionMask = 0xffffffff,
            bool collideWithAreas = false,
            bool includeInside = false,
            bool collideWithBodies = true,
            int maxResultCount = 32,
            params Rid[] exclude
            )
        {
            _rectangleShape.Size = size;
            Transform2D transform = new Transform2D(0, origin);
            return ShapeCast(space, _rectangleShape, transform, direction, distance, collisionMask, collideWithAreas, includeInside, collideWithBodies, maxResultCount, exclude);
        }

        /// <summary>
        /// IntersectRayのラッパー関数
        /// </summary>
        /// <param name="origin">起点</param>
        /// <param name="direction">方向</param>
        /// <param name="distance">距離</param>
        /// <param name="collisionMask">コリジョン判定をするマスク</param>
        /// <param name="collideWithAreas">trueの場合、Area2Dとも衝突判定を行う</param>
        /// <param name="includeInside">trueの場合、起点に重なっているShape2Dとも衝突判定を行う。この場合、normalはVector2(0,0)を返す。</param>
        /// <param name="collideWithBodies">trueの場合、PhysicsBody2Dとも衝突判定を行う。</param>
        /// <param name="exclude"></param>
        public static RaycastHit2D Raycast(
            this Node2D node,
            Vector2 origin,
            Vector2 direction,
            float distance,
            uint collisionMask = 0xffffffff,
            bool collideWithAreas = false,
            bool includeInside = false,
            bool collideWithBodies = true,
            params Rid[] exclude
            )
        {
            return node.GetWorld2D().DirectSpaceState.Raycast(origin, direction, distance, collisionMask, collideWithAreas, includeInside, collideWithBodies, exclude);
        }

        public static RaycastHit2D ToRaycastHit2D(this Dictionary result)
        {
            RaycastHit2D hit = new RaycastHit2D();
            if (result.Count == 0) return hit;
            Variant outValue;
            result.TryGetValue("collider", out outValue);
            hit.Collider = outValue.As<GodotObject>();
            result.TryGetValue("collider_id", out outValue);
            hit.ColliderId = outValue.As<int>();
            result.TryGetValue("normal", out outValue);
            hit.Normal = outValue.As<Vector2>();
            result.TryGetValue("position", out outValue);
            hit.Position = outValue.As<Vector2>();
            result.TryGetValue("rid", out outValue);
            hit.Rid = outValue.AsRid();
            result.TryGetValue("shape", out outValue);
            hit.Shape = outValue.As<int>();
            return hit;
        }
    }
}