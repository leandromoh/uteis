using System.Collections.Generic;
using System;

namespace EffectiveLINQ
{
    static partial class LinqExtensions
    {
        //https://jsfiddle.net/0zoozh03/

        public static IEnumerable<TResult> Zip<T1, T2, T3, TResult>(this IEnumerable<T1> i1, IEnumerable<T2> i2, IEnumerable<T3> i3, Func<T1, T2, T3, TResult> func)
        {
            using (var e1 = i1.GetEnumerator())
            using (var e2 = i2.GetEnumerator())
            using (var e3 = i3.GetEnumerator())
            {
                while (e1.MoveNext() && e2.MoveNext() && e3.MoveNext())
                {
                    yield return func(e1.Current, e2.Current, e3.Current);
                }
            }
        }

        public static IEnumerable<TResult> Zip<T1, T2, T3, T4, TResult>(this IEnumerable<T1> i1, IEnumerable<T2> i2, IEnumerable<T3> i3, IEnumerable<T4> i4, Func<T1, T2, T3, T4, TResult> func)
        {
            using (var e1 = i1.GetEnumerator())
            using (var e2 = i2.GetEnumerator())
            using (var e3 = i3.GetEnumerator())
            using (var e4 = i4.GetEnumerator())
            {
                while (e1.MoveNext() && e2.MoveNext() && e3.MoveNext() && e4.MoveNext())
                {
                    yield return func(e1.Current, e2.Current, e3.Current, e4.Current);
                }
            }
        }

        public static IEnumerable<TResult> Zip<T1, T2, T3, T4, T5, TResult>(this IEnumerable<T1> i1, IEnumerable<T2> i2, IEnumerable<T3> i3, IEnumerable<T4> i4, IEnumerable<T5> i5, Func<T1, T2, T3, T4, T5, TResult> func)
        {
            using (var e1 = i1.GetEnumerator())
            using (var e2 = i2.GetEnumerator())
            using (var e3 = i3.GetEnumerator())
            using (var e4 = i4.GetEnumerator())
            using (var e5 = i5.GetEnumerator())
            {
                while (e1.MoveNext() && e2.MoveNext() && e3.MoveNext() && e4.MoveNext() && e5.MoveNext())
                {
                    yield return func(e1.Current, e2.Current, e3.Current, e4.Current, e5.Current);
                }
            }
        }

        public static IEnumerable<TResult> Zip<T1, T2, T3, T4, T5, T6, TResult>(this IEnumerable<T1> i1, IEnumerable<T2> i2, IEnumerable<T3> i3, IEnumerable<T4> i4, IEnumerable<T5> i5, IEnumerable<T6> i6, Func<T1, T2, T3, T4, T5, T6, TResult> func)
        {
            using (var e1 = i1.GetEnumerator())
            using (var e2 = i2.GetEnumerator())
            using (var e3 = i3.GetEnumerator())
            using (var e4 = i4.GetEnumerator())
            using (var e5 = i5.GetEnumerator())
            using (var e6 = i6.GetEnumerator())
            {
                while (e1.MoveNext() && e2.MoveNext() && e3.MoveNext() && e4.MoveNext() && e5.MoveNext() && e6.MoveNext())
                {
                    yield return func(e1.Current, e2.Current, e3.Current, e4.Current, e5.Current, e6.Current);
                }
            }
        }

        public static IEnumerable<TResult> Zip<T1, T2, T3, T4, T5, T6, T7, TResult>(this IEnumerable<T1> i1, IEnumerable<T2> i2, IEnumerable<T3> i3, IEnumerable<T4> i4, IEnumerable<T5> i5, IEnumerable<T6> i6, IEnumerable<T7> i7, Func<T1, T2, T3, T4, T5, T6, T7, TResult> func)
        {
            using (var e1 = i1.GetEnumerator())
            using (var e2 = i2.GetEnumerator())
            using (var e3 = i3.GetEnumerator())
            using (var e4 = i4.GetEnumerator())
            using (var e5 = i5.GetEnumerator())
            using (var e6 = i6.GetEnumerator())
            using (var e7 = i7.GetEnumerator())
            {
                while (e1.MoveNext() && e2.MoveNext() && e3.MoveNext() && e4.MoveNext() && e5.MoveNext() && e6.MoveNext() && e7.MoveNext())
                {
                    yield return func(e1.Current, e2.Current, e3.Current, e4.Current, e5.Current, e6.Current, e7.Current);
                }
            }
        }

        public static IEnumerable<TResult> Zip<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(this IEnumerable<T1> i1, IEnumerable<T2> i2, IEnumerable<T3> i3, IEnumerable<T4> i4, IEnumerable<T5> i5, IEnumerable<T6> i6, IEnumerable<T7> i7, IEnumerable<T8> i8, Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func)
        {
            using (var e1 = i1.GetEnumerator())
            using (var e2 = i2.GetEnumerator())
            using (var e3 = i3.GetEnumerator())
            using (var e4 = i4.GetEnumerator())
            using (var e5 = i5.GetEnumerator())
            using (var e6 = i6.GetEnumerator())
            using (var e7 = i7.GetEnumerator())
            using (var e8 = i8.GetEnumerator())
            {
                while (e1.MoveNext() && e2.MoveNext() && e3.MoveNext() && e4.MoveNext() && e5.MoveNext() && e6.MoveNext() && e7.MoveNext() && e8.MoveNext())
                {
                    yield return func(e1.Current, e2.Current, e3.Current, e4.Current, e5.Current, e6.Current, e7.Current, e8.Current);
                }
            }
        }

        public static IEnumerable<TResult> Zip<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(this IEnumerable<T1> i1, IEnumerable<T2> i2, IEnumerable<T3> i3, IEnumerable<T4> i4, IEnumerable<T5> i5, IEnumerable<T6> i6, IEnumerable<T7> i7, IEnumerable<T8> i8, IEnumerable<T9> i9, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> func)
        {
            using (var e1 = i1.GetEnumerator())
            using (var e2 = i2.GetEnumerator())
            using (var e3 = i3.GetEnumerator())
            using (var e4 = i4.GetEnumerator())
            using (var e5 = i5.GetEnumerator())
            using (var e6 = i6.GetEnumerator())
            using (var e7 = i7.GetEnumerator())
            using (var e8 = i8.GetEnumerator())
            using (var e9 = i9.GetEnumerator())
            {
                while (e1.MoveNext() && e2.MoveNext() && e3.MoveNext() && e4.MoveNext() && e5.MoveNext() && e6.MoveNext() && e7.MoveNext() && e8.MoveNext() && e9.MoveNext())
                {
                    yield return func(e1.Current, e2.Current, e3.Current, e4.Current, e5.Current, e6.Current, e7.Current, e8.Current, e9.Current);
                }
            }
        }

        public static IEnumerable<TResult> Zip<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(this IEnumerable<T1> i1, IEnumerable<T2> i2, IEnumerable<T3> i3, IEnumerable<T4> i4, IEnumerable<T5> i5, IEnumerable<T6> i6, IEnumerable<T7> i7, IEnumerable<T8> i8, IEnumerable<T9> i9, IEnumerable<T10> i10, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> func)
        {
            using (var e1 = i1.GetEnumerator())
            using (var e2 = i2.GetEnumerator())
            using (var e3 = i3.GetEnumerator())
            using (var e4 = i4.GetEnumerator())
            using (var e5 = i5.GetEnumerator())
            using (var e6 = i6.GetEnumerator())
            using (var e7 = i7.GetEnumerator())
            using (var e8 = i8.GetEnumerator())
            using (var e9 = i9.GetEnumerator())
            using (var e10 = i10.GetEnumerator())
            {
                while (e1.MoveNext() && e2.MoveNext() && e3.MoveNext() && e4.MoveNext() && e5.MoveNext() && e6.MoveNext() && e7.MoveNext() && e8.MoveNext() && e9.MoveNext() && e10.MoveNext())
                {
                    yield return func(e1.Current, e2.Current, e3.Current, e4.Current, e5.Current, e6.Current, e7.Current, e8.Current, e9.Current, e10.Current);
                }
            }
        }

        public static IEnumerable<TResult> Zip<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(this IEnumerable<T1> i1, IEnumerable<T2> i2, IEnumerable<T3> i3, IEnumerable<T4> i4, IEnumerable<T5> i5, IEnumerable<T6> i6, IEnumerable<T7> i7, IEnumerable<T8> i8, IEnumerable<T9> i9, IEnumerable<T10> i10, IEnumerable<T11> i11, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> func)
        {
            using (var e1 = i1.GetEnumerator())
            using (var e2 = i2.GetEnumerator())
            using (var e3 = i3.GetEnumerator())
            using (var e4 = i4.GetEnumerator())
            using (var e5 = i5.GetEnumerator())
            using (var e6 = i6.GetEnumerator())
            using (var e7 = i7.GetEnumerator())
            using (var e8 = i8.GetEnumerator())
            using (var e9 = i9.GetEnumerator())
            using (var e10 = i10.GetEnumerator())
            using (var e11 = i11.GetEnumerator())
            {
                while (e1.MoveNext() && e2.MoveNext() && e3.MoveNext() && e4.MoveNext() && e5.MoveNext() && e6.MoveNext() && e7.MoveNext() && e8.MoveNext() && e9.MoveNext() && e10.MoveNext() && e11.MoveNext())
                {
                    yield return func(e1.Current, e2.Current, e3.Current, e4.Current, e5.Current, e6.Current, e7.Current, e8.Current, e9.Current, e10.Current, e11.Current);
                }
            }
        }

        public static IEnumerable<TResult> Zip<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(this IEnumerable<T1> i1, IEnumerable<T2> i2, IEnumerable<T3> i3, IEnumerable<T4> i4, IEnumerable<T5> i5, IEnumerable<T6> i6, IEnumerable<T7> i7, IEnumerable<T8> i8, IEnumerable<T9> i9, IEnumerable<T10> i10, IEnumerable<T11> i11, IEnumerable<T12> i12, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> func)
        {
            using (var e1 = i1.GetEnumerator())
            using (var e2 = i2.GetEnumerator())
            using (var e3 = i3.GetEnumerator())
            using (var e4 = i4.GetEnumerator())
            using (var e5 = i5.GetEnumerator())
            using (var e6 = i6.GetEnumerator())
            using (var e7 = i7.GetEnumerator())
            using (var e8 = i8.GetEnumerator())
            using (var e9 = i9.GetEnumerator())
            using (var e10 = i10.GetEnumerator())
            using (var e11 = i11.GetEnumerator())
            using (var e12 = i12.GetEnumerator())
            {
                while (e1.MoveNext() && e2.MoveNext() && e3.MoveNext() && e4.MoveNext() && e5.MoveNext() && e6.MoveNext() && e7.MoveNext() && e8.MoveNext() && e9.MoveNext() && e10.MoveNext() && e11.MoveNext() && e12.MoveNext())
                {
                    yield return func(e1.Current, e2.Current, e3.Current, e4.Current, e5.Current, e6.Current, e7.Current, e8.Current, e9.Current, e10.Current, e11.Current, e12.Current);
                }
            }
        }

        public static IEnumerable<TResult> Zip<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(this IEnumerable<T1> i1, IEnumerable<T2> i2, IEnumerable<T3> i3, IEnumerable<T4> i4, IEnumerable<T5> i5, IEnumerable<T6> i6, IEnumerable<T7> i7, IEnumerable<T8> i8, IEnumerable<T9> i9, IEnumerable<T10> i10, IEnumerable<T11> i11, IEnumerable<T12> i12, IEnumerable<T13> i13, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> func)
        {
            using (var e1 = i1.GetEnumerator())
            using (var e2 = i2.GetEnumerator())
            using (var e3 = i3.GetEnumerator())
            using (var e4 = i4.GetEnumerator())
            using (var e5 = i5.GetEnumerator())
            using (var e6 = i6.GetEnumerator())
            using (var e7 = i7.GetEnumerator())
            using (var e8 = i8.GetEnumerator())
            using (var e9 = i9.GetEnumerator())
            using (var e10 = i10.GetEnumerator())
            using (var e11 = i11.GetEnumerator())
            using (var e12 = i12.GetEnumerator())
            using (var e13 = i13.GetEnumerator())
            {
                while (e1.MoveNext() && e2.MoveNext() && e3.MoveNext() && e4.MoveNext() && e5.MoveNext() && e6.MoveNext() && e7.MoveNext() && e8.MoveNext() && e9.MoveNext() && e10.MoveNext() && e11.MoveNext() && e12.MoveNext() && e13.MoveNext())
                {
                    yield return func(e1.Current, e2.Current, e3.Current, e4.Current, e5.Current, e6.Current, e7.Current, e8.Current, e9.Current, e10.Current, e11.Current, e12.Current, e13.Current);
                }
            }
        }

        public static IEnumerable<TResult> Zip<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(this IEnumerable<T1> i1, IEnumerable<T2> i2, IEnumerable<T3> i3, IEnumerable<T4> i4, IEnumerable<T5> i5, IEnumerable<T6> i6, IEnumerable<T7> i7, IEnumerable<T8> i8, IEnumerable<T9> i9, IEnumerable<T10> i10, IEnumerable<T11> i11, IEnumerable<T12> i12, IEnumerable<T13> i13, IEnumerable<T14> i14, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> func)
        {
            using (var e1 = i1.GetEnumerator())
            using (var e2 = i2.GetEnumerator())
            using (var e3 = i3.GetEnumerator())
            using (var e4 = i4.GetEnumerator())
            using (var e5 = i5.GetEnumerator())
            using (var e6 = i6.GetEnumerator())
            using (var e7 = i7.GetEnumerator())
            using (var e8 = i8.GetEnumerator())
            using (var e9 = i9.GetEnumerator())
            using (var e10 = i10.GetEnumerator())
            using (var e11 = i11.GetEnumerator())
            using (var e12 = i12.GetEnumerator())
            using (var e13 = i13.GetEnumerator())
            using (var e14 = i14.GetEnumerator())
            {
                while (e1.MoveNext() && e2.MoveNext() && e3.MoveNext() && e4.MoveNext() && e5.MoveNext() && e6.MoveNext() && e7.MoveNext() && e8.MoveNext() && e9.MoveNext() && e10.MoveNext() && e11.MoveNext() && e12.MoveNext() && e13.MoveNext() && e14.MoveNext())
                {
                    yield return func(e1.Current, e2.Current, e3.Current, e4.Current, e5.Current, e6.Current, e7.Current, e8.Current, e9.Current, e10.Current, e11.Current, e12.Current, e13.Current, e14.Current);
                }
            }
        }

        public static IEnumerable<TResult> Zip<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(this IEnumerable<T1> i1, IEnumerable<T2> i2, IEnumerable<T3> i3, IEnumerable<T4> i4, IEnumerable<T5> i5, IEnumerable<T6> i6, IEnumerable<T7> i7, IEnumerable<T8> i8, IEnumerable<T9> i9, IEnumerable<T10> i10, IEnumerable<T11> i11, IEnumerable<T12> i12, IEnumerable<T13> i13, IEnumerable<T14> i14, IEnumerable<T15> i15, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> func)
        {
            using (var e1 = i1.GetEnumerator())
            using (var e2 = i2.GetEnumerator())
            using (var e3 = i3.GetEnumerator())
            using (var e4 = i4.GetEnumerator())
            using (var e5 = i5.GetEnumerator())
            using (var e6 = i6.GetEnumerator())
            using (var e7 = i7.GetEnumerator())
            using (var e8 = i8.GetEnumerator())
            using (var e9 = i9.GetEnumerator())
            using (var e10 = i10.GetEnumerator())
            using (var e11 = i11.GetEnumerator())
            using (var e12 = i12.GetEnumerator())
            using (var e13 = i13.GetEnumerator())
            using (var e14 = i14.GetEnumerator())
            using (var e15 = i15.GetEnumerator())
            {
                while (e1.MoveNext() && e2.MoveNext() && e3.MoveNext() && e4.MoveNext() && e5.MoveNext() && e6.MoveNext() && e7.MoveNext() && e8.MoveNext() && e9.MoveNext() && e10.MoveNext() && e11.MoveNext() && e12.MoveNext() && e13.MoveNext() && e14.MoveNext() && e15.MoveNext())
                {
                    yield return func(e1.Current, e2.Current, e3.Current, e4.Current, e5.Current, e6.Current, e7.Current, e8.Current, e9.Current, e10.Current, e11.Current, e12.Current, e13.Current, e14.Current, e15.Current);
                }
            }
        }

        public static IEnumerable<TResult> Zip<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(this IEnumerable<T1> i1, IEnumerable<T2> i2, IEnumerable<T3> i3, IEnumerable<T4> i4, IEnumerable<T5> i5, IEnumerable<T6> i6, IEnumerable<T7> i7, IEnumerable<T8> i8, IEnumerable<T9> i9, IEnumerable<T10> i10, IEnumerable<T11> i11, IEnumerable<T12> i12, IEnumerable<T13> i13, IEnumerable<T14> i14, IEnumerable<T15> i15, IEnumerable<T16> i16, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> func)
        {
            using (var e1 = i1.GetEnumerator())
            using (var e2 = i2.GetEnumerator())
            using (var e3 = i3.GetEnumerator())
            using (var e4 = i4.GetEnumerator())
            using (var e5 = i5.GetEnumerator())
            using (var e6 = i6.GetEnumerator())
            using (var e7 = i7.GetEnumerator())
            using (var e8 = i8.GetEnumerator())
            using (var e9 = i9.GetEnumerator())
            using (var e10 = i10.GetEnumerator())
            using (var e11 = i11.GetEnumerator())
            using (var e12 = i12.GetEnumerator())
            using (var e13 = i13.GetEnumerator())
            using (var e14 = i14.GetEnumerator())
            using (var e15 = i15.GetEnumerator())
            using (var e16 = i16.GetEnumerator())
            {
                while (e1.MoveNext() && e2.MoveNext() && e3.MoveNext() && e4.MoveNext() && e5.MoveNext() && e6.MoveNext() && e7.MoveNext() && e8.MoveNext() && e9.MoveNext() && e10.MoveNext() && e11.MoveNext() && e12.MoveNext() && e13.MoveNext() && e14.MoveNext() && e15.MoveNext() && e16.MoveNext())
                {
                    yield return func(e1.Current, e2.Current, e3.Current, e4.Current, e5.Current, e6.Current, e7.Current, e8.Current, e9.Current, e10.Current, e11.Current, e12.Current, e13.Current, e14.Current, e15.Current, e16.Current);
                }
            }
        }
    }
}
