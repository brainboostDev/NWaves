using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NWaves.Utils
{
    public static class MathSpan
    {
        public static float Sum(this Span<float> span)
        {
            var sum = 0.0f;
            for (var i = 0; i < span.Length; i++)
            {
                sum += span[i];
            }
            return sum;
        }

        public static float SquaredSum(this Span<float> span)
        {
            var sum = 0.0f;
            for (var i = 0; i < span.Length; i++)
            {
                sum += span[i] * span[i];
            }
            return sum;
        }

        public static float[] Reverse(this Span<float> span)
        {
            var reversed = new float[span.Length];
            for (var i = 0; i < span.Length; i++)
            {
                reversed[i] = span[span.Length - 1 - i];
            }
            return reversed;
        }

        public static float Last(this Span<float> span)
        {
            return span[span.Length - 1];
        }

        public static float Zip(this Span<float> span, Span<float> other, Func<float, float, float> func)
        {
            var result = 0.0f;
            for (var i = 0; i < span.Length; i++)
            {
                result += func(span[i], other[i]);
            }
            return result;
        }

        public static float Max(this Span<float> span)
        {
            var max = float.MinValue;
            for (var i = 0; i < span.Length; i++)
            {
                if (span[i] > max)
                {
                    max = span[i];
                }
            }
            return max;
        }

        public static float Max(this Span<float> span, Func<float, float> func)
        {
            var max = float.MinValue;
            for (var i = 0; i < span.Length; i++)
            {
                var val = func(span[i]);
                if (val > max)
                {
                    max = val;
                }
            }
            return max;
        }


        public static float Min(this Span<float> span)
        {
            var min = float.MaxValue;
            for (var i = 0; i < span.Length; i++)
            {
                if (span[i] < min)
                {
                    min = span[i];
                }
            }
            return min;
        }

        public static IEnumerable<T> Select<T>(this Span<T> span, Func<T, T> func)
        {
            for (var i = 0; i < span.Length; i++)
            {
                yield return func(span[i]);
            }
        }

        public static float[] RepeatArray(this Span<float> span, int times)
        {
            var repeated = new float[span.Length * times];
            for (var i = 0; i < times; i++)
            {
                span.CopyTo(repeated.AsSpan().Slice(i * span.Length));
            }
            return repeated;
        }
    }

}
