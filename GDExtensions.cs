using Godot;
using System.Collections.Generic;

namespace NoromaGD
{
    public static class GDExtensions
    {
        #region CollisionShape2D

        public static Rect2 ToGlobalRect(this CollisionShape2D collisionShape)
        {
            var rect = collisionShape.Shape.GetRect();
            rect = new Rect2(rect.Position + collisionShape.GlobalPosition, rect.Size);
            return rect;
        }

        #endregion CollisionShape2D

        #region ShapeCast2D

        public static GodotObject GetColliderSafe(this ShapeCast2D shape, int index)
        {
            if (shape.IsColliding() == false) return null;
            return shape.GetCollider(index);
        }

        #endregion ShapeCast2D

        #region Rect2

        /// <summary>
        /// 左上　※Y軸上方向はマイナス　Y軸下方向はプラス
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static Vector2 TopLeft(this Rect2 rect)
        {
            return rect.Position;
        }

        /// <summary>
        /// 右上　※Y軸上方向はマイナス　Y軸下方向はプラス
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static Vector2 TopRight(this Rect2 rect)
        {
            return new Vector2(rect.End.X, rect.Position.Y);
        }

        public static Vector2 BottomLeft(this Rect2 rect)
        {
            return new Vector2(rect.Position.X, rect.End.Y);
        }

        public static Vector2 BottomRight(this Rect2 rect)
        {
            return new Vector2(rect.End.X, rect.End.Y);
        }

        public static Vector2 Top(this Rect2 rect)
        {
            return new Vector2(rect.GetCenter().X, rect.Position.Y);
        }

        public static Vector2 Right(this Rect2 rect)
        {
            return new Vector2(rect.End.X, rect.GetCenter().Y);
        }

        public static Vector2 Left(this Rect2 rect)
        {
            return new Vector2(rect.Position.X, rect.GetCenter().Y);
        }

        public static Vector2 Bottom(this Rect2 rect)
        {
            return new Vector2(rect.GetCenter().X, rect.End.Y);
        }

        #endregion Rect2

        #region Node

        public static T GetChildByType<T>(this Node node, bool recursive = true)
            where T : class
        {
            int childCount = node.GetChildCount();

            for (int i = 0; i < childCount; i++)
            {
                Node child = node.GetChild(i);
                if (child is T childT)
                    return childT;

                if (recursive && child.GetChildCount() > 0)
                {
                    T recursiveResult = child.GetChildByType<T>(true);
                    if (recursiveResult != null)
                        return recursiveResult;
                }
            }

            return null;
        }

        public static T GetParentByType<T>(this Node node) where T : class
        {
            Node parent = node.GetParent();
            if (parent != null)
            {
                if (parent is T parentT)
                {
                    return parentT;
                }
                else
                {
                    return parent.GetParentByType<T>();
                }
            }

            return null;
        }

        public static List<T> GetChildrenByType<T>(this Node node) where T : class
        {
            List<T> children = new List<T>();
            node.GetChildrenByType(children);
            return children;
        }

        private static void GetChildrenByType<T>(this Node node, List<T> children) where T : class
        {
            if (children == null) children = new List<T>();
            int childCount = node.GetChildCount();
            for (int i = 0; i < childCount; i++)
            {
                Node child = node.GetChild(i);
                if (child is T childT)
                    children.Add(childT);

                if (child.GetChildCount() > 0)
                {
                    child.GetChildrenByType(children);
                }
            }
        }

        #endregion Node

        #region Vector2

        public static Vector2 Rotate(this Vector2 vector, float angle, bool angleIsDegree = true)
        {
            return NoromaMathf.RotateVector(vector, angle, angleIsDegree);
        }

        #endregion Vector2
    }
}