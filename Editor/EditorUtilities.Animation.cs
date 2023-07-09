using UnityEngine;

namespace gomoru.su
{
    partial class EditorUtilities
    {
        public static class Animation
        {
            private static AnimationCurve _singleton;
            private static readonly Keyframe[] _buffer1 = new Keyframe[1];
            private static readonly Keyframe[] _buffer2 = new Keyframe[2];

            public static AnimationCurve Constant(float value)
            {
                var curve = _singleton;
                if (curve == null)
                {
                    return _singleton = AnimationCurve.Constant(0, 0, value);
                }
                _buffer1[0] = new Keyframe(0, value);
                curve.keys = _buffer1;
                return curve;
            }

            public static AnimationCurve Linear(float start, float end)
            {
                var curve = _singleton;
                if (curve == null)
                {
                    return _singleton = AnimationCurve.Linear(0, start, 1 / 60f, end);
                }
                float num = (end - start) / (1 / 60f);
                _buffer2[0] = new Keyframe(0, start, 0, num);
                _buffer2[1] = new Keyframe(1 / 60f, end, num, 0);
                curve.keys = _buffer2;
                return curve;
            }

            public static AnimationCurve EaseInOut(float start, float end)
            {
                var curve = _singleton;
                if (curve == null)
                {
                    return _singleton = AnimationCurve.EaseInOut(0, start, 1 / 60f, end);
                }
                _buffer2[0] = new Keyframe(0, start, 0, 0);
                _buffer2[1] = new Keyframe(1 / 60f, end, 0, 0);
                curve.keys = _buffer2;
                return curve;
            }
        }
    }
}