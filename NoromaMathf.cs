using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoromaGD
{
    public static class NoromaMathf
    {
        public static Vector2 Closest(Vector2 target, params Vector2[] points)
        {
            return points.MinBy(p => (p - target).LengthSquared());
        }

        public static Vector2 ClampCurrentToTarget(Vector2 current, Vector2 target, float min_matchToTarget)
        {
            if ((target - current).LengthSquared() <= min_matchToTarget.Pow(2)) return target;
            return current;
        }

        public static Vector2 LerpToTargetSpeed(Vector2 current, Vector2 target, float speed, double deltaTime)
        {
            if (current == target) return target;
            if ((current - target).LengthSquared() <= (speed * deltaTime).Pow(2)) return target;
            Vector2 direction = (target - current).Normalized();
            return current + direction * speed * (float)deltaTime;
        }

        public static Vector2 RotateVector(Vector2 vector, float angle, bool angleIsDegree = true)
        {
            if (angleIsDegree) angle = Mathf.DegToRad(angle);
            return RotateVector(vector, angle);
        }

        public static Vector2 RotateVector(Vector2 vector, float radian)
        {
            float newX = vector.X * Mathf.Cos(radian) - vector.Y * Mathf.Sin(radian);
            float newY = vector.X * Mathf.Sin(radian) + vector.Y * Mathf.Cos(radian);
            return new Vector2(newX, newY);
        }

        public static float SmoothDampAngle(float current, float target, ref float currentVelocity, float smoothTime, double deltaTime, float maxSpeed = Mathf.Inf)
        {
            target = current + DeltaAngle(current, target);
            return SmoothDamp(current, target, ref currentVelocity, smoothTime, deltaTime, maxSpeed);
        }

        // Loops the value t, so that it is never larger than length and never smaller than 0.
        public static float Repeat(float t, float length)
        {
            return Mathf.Clamp(t - Mathf.Floor(t / length) * length, 0.0f, length);
        }

        // PingPongs the value t, so that it is never larger than length and never smaller than 0.
        public static float PingPong(float t, float length)
        {
            t = Repeat(t, length * 2F);
            return length - Mathf.Abs(t - length);
        }

        // Calculates the ::ref::Lerp parameter between of two values.
        public static float InverseLerp(float a, float b, float value)
        {
            if (a != b)
                return Clamp01((value - a) / (b - a));
            else
                return 0.0f;
        }

        // Calculates the shortest difference between two given angles.
        public static float DeltaAngle(float current, float target)
        {
            float delta = Repeat((target - current), 360.0F);
            if (delta > 180.0F)
                delta -= 360.0F;
            return delta;
        }

        public static Vector2 SmoothDampVec2(Vector2 current, Vector2 target, ref Vector2 currentVelocity, float smoothTime, double deltaTime, float maxSpeed = Mathf.Inf)
        {
            if (deltaTime == 0) return target;
            var currentVelocityX = currentVelocity.X;
            var currentVelocityY = currentVelocity.Y;
            var vector = new Vector2();
            vector.X = SmoothDamp(current.X, target.X, ref currentVelocityX, smoothTime, deltaTime, maxSpeed);
            vector.Y = SmoothDamp(current.Y, target.Y, ref currentVelocityY, smoothTime, deltaTime, maxSpeed);
            currentVelocity = new Vector2(currentVelocityX, currentVelocityY);
            return vector;
        }

        public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime, double deltaTime, float maxSpeed = Mathf.Inf)
        {
            if (deltaTime == 0) return target;
            // Based on Game Programming Gems 4 Chapter 1.10
            smoothTime = Mathf.Max(0.0001F, smoothTime);
            float omega = 2F / smoothTime;

            float x = omega * (float)deltaTime;
            float exp = 1F / (1F + x + 0.48F * x * x + 0.235F * x * x * x);
            float change = current - target;
            float originalTo = target;

            // Clamp maximum speed
            float maxChange = maxSpeed * smoothTime;
            change = Mathf.Clamp(change, -maxChange, maxChange);
            target = current - change;

            float temp = (currentVelocity + omega * change) * (float)deltaTime;
            currentVelocity = (currentVelocity - omega * temp) * exp;
            float output = target + (change + temp) * exp;

            // Prevent overshooting
            if (originalTo - current > 0.0F == output > originalTo)
            {
                output = originalTo;
                currentVelocity = (output - originalTo) / (float)deltaTime;
            }

            return output;
        }

        // Clamps value between 0 and 1 and returns value
        public static float Clamp01(float value)
        {
            if (value < 0F)
                return 0F;
            else if (value > 1F)
                return 1F;
            else
                return value;
        }
    }
}