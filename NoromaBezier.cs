using Godot;

namespace NoromaGD
{
    /// <summary>
    /// ベジェ曲線、サイン曲線
    /// </summary>
    public static class NoromaCurves
    {
        public static Vector2 BezierCarve(float t, Vector2 start, Vector2 middleControllPoint, Vector2 end)
        {
            var tp = 1 - t;
            float x = t * t * end.X + 2 * t * tp * middleControllPoint.X + tp * tp * start.X;
            float y = t * t * end.Y + 2 * t * tp * middleControllPoint.Y + tp * tp * start.Y;
            return new Vector2(x, y);
        }

        public static Vector2 CalcControllPointOfBezie(Vector2 start, Vector2 end, Vector2 throughPoint)
        {
            return throughPoint * 2 - (start + end) / 2;
        }

        public static Vector2 BezieThroughMiddlePoint(float t, Vector2 start, Vector2 end, Vector2 throughPoint)
        {
            var cp = CalcControllPointOfBezie(start, end, throughPoint);
            return BezierCarve(t, start, cp, end);
        }

        /// <summary>
        /// サインカーブのyを返す。
        /// </summary>
        /// <param name="t"> t=1でπ（180°）と一緒。</param>
        /// <param name="distortion">カーブのゆがみ</param>
        /// <param name="height">カーブの高さ</param>
        /// <returns></returns>
        public static float SinCarve(float t, float distortion, float height = 1)
        {
            return (1.0f - Mathf.Pow((1.0f - Mathf.Sin(Mathf.Pi * t)), distortion)) * height;
        }

        public static void JumpCarve(ref bool isJump, ref float velocityY, ref float currentTime, float jumpTime, float height, double deltaTime, float distortion = 1)
        {
            if (!isJump) return;

            float t = currentTime / jumpTime; //経過時間 ( 0.0 -> 1.0 )

            float currentY = SinCarve(t, distortion, height);
            currentTime += (float)deltaTime;
            t = currentTime / jumpTime;
            float nextY = SinCarve(t, distortion, height);
            velocityY = (nextY - currentY) / (float)deltaTime;
            if (currentTime >= jumpTime) isJump = false;
        }
    }
}