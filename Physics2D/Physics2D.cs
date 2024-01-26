using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Xml.XPath;

namespace NoromaGD
{
    public static class Physics2D
    {
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
            RaycastHit2D hit = new RaycastHit2D();

            if (result.Count == 0) return hit;
            Variant outValue;
            result.TryGetValue("collider", out outValue);
            hit.Collider = outValue.As<CollisionObject2D>();
            result.TryGetValue("collider_id", out outValue);
            hit.ColliderId = outValue.As<uint>();
            result.TryGetValue("normal", out outValue);
            hit.Normal = outValue.As<Vector2>();
            result.TryGetValue("position", out outValue);
            hit.Position = outValue.As<Vector2>();
            result.TryGetValue("rid", out outValue);
            hit.Rid = outValue.AsRid();
            result.TryGetValue("shape", out outValue);
            hit.Shape = outValue.As<CollisionShape2D>();

            return hit;
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
    }
}