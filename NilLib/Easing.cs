using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace NilLib
{
public enum EaseType
    {
        Linear,
        Sine,
        Quad,
        Expo,
        Back
    }

    public enum EaseDirection
    {
        In,
        Out,
        InOut
    }

    public static class Easing
    {
        /// <summary>
        /// Evaluates an easing curve at time t [0.0 to 1.0].
        /// </summary>
        public static float Evaluate(float t, EaseType type, EaseDirection direction, float overshoot = 1.70158f)
        {
            t = Mathf.Clamp(t, 0f, 1f);

            return direction switch
            {
                EaseDirection.In => EaseIn(t, type, overshoot),
                EaseDirection.Out => EaseOut(t, type, overshoot),
                EaseDirection.InOut => EaseInOut(t, type, overshoot),
                _ => t
            };
        }

        // --- Interpolation Helpers ---

        public static float Lerp(float start, float end, float t, EaseType type, EaseDirection direction)
        {
            return start + (end - start) * Evaluate(t, type, direction);
        }

        public static Vector2 Lerp(Vector2 start, Vector2 end, float t, EaseType type, EaseDirection direction)
        {
            float easedT = Evaluate(t, type, direction);
            return Vector2.Lerp(start, end, easedT);
        }

        public static Vector3 Lerp(Vector3 start, Vector3 end, float t, EaseType type, EaseDirection direction)
        {
            float easedT = Evaluate(t, type, direction);
            return Vector3.Lerp(start, end, easedT);
        }

        // --- Core Curve Implementations ---

        private static float EaseIn(float t, EaseType type, float s) => type switch
        {
            EaseType.Sine => 1f - Mathf.Cos((t * Mathf.PI) / 2f),
            EaseType.Quad => t * t,
            EaseType.Expo => t == 0f ? 0f : Mathf.Pow(2f, 10f * t - 10f),
            EaseType.Back => (s + 1f) * t * t * t - s * t * t,
     
            _ => t
        };

        private static float EaseOut(float t, EaseType type, float s) => type switch
        {
            EaseType.Sine => Mathf.Sin((t * Mathf.PI) / 2f),
            EaseType.Quad => 1f - (1f - t) * (1f - t),
            EaseType.Expo => t == 1f ? 1f : 1f - Mathf.Pow(2f, -10f * t),
            EaseType.Back => 1f + (s + 1f) * Mathf.Pow(t - 1f, 3f) + s * Mathf.Pow(t - 1f, 2f),
            _ => t
        };

        private static float EaseInOut(float t, EaseType type, float s)
        {
            return type switch
            {
                EaseType.Sine => -(Mathf.Cos(Mathf.PI * t) - 1f) / 2f,
                EaseType.Quad => t < 0.5f
                    ? 2f * t * t
                    : 1f - Mathf.Pow(-2f * t + 2f, 2f) / 2f,
                EaseType.Expo => t switch
                {
                    0f => 0f,
                    1f => 1f,
                    _ => t < 0.5f
                        ? Mathf.Pow(2f, 20f * t - 10f) / 2f
                        : (2f - Mathf.Pow(2f, -20f * t + 10f)) / 2f
                },
                EaseType.Back => EaseInOutBack(t, s),
                _ => t
            };
        }

        private static float EaseInOutBack(float t, float s)
        {
            float c2 = s * 1.525f; // Scales overshoot for smooth transition mid-curve
            return t < 0.5f
                ? (Mathf.Pow(2f * t, 2f) * ((c2 + 1f) * 2f * t - c2)) / 2f
                : (Mathf.Pow(2f * t - 2f, 2f) * ((c2 + 1f) * (t * 2f - 2f) + c2) + 2f) / 2f;
        }
    }
}
