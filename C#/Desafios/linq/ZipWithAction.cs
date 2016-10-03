using System.Collections.Generic;
using System;

namespace EffectiveLINQ
{
    static partial class LinqExtensions
    {
        //https://jsfiddle.net/v9v4073m/

        public static void ForEach<T1>(this IEnumerable<T1> i1, Action<T1> action)
        {
            using (var e1 = i1.GetEnumerator())
            {
                while (e1.MoveNext())
                {
                    action(e1.Current);
                }
            }
        }

        public static void ZipWithAction<T1, T2>(this IEnumerable<T1> i1, IEnumerable<T2> i2, Action<T1, T2> action)
        {
            using (var e1 = i1.GetEnumerator())
            using (var e2 = i2.GetEnumerator())
            {
                while (e1.MoveNext() && e2.MoveNext())
                {
                    action(e1.Current, e2.Current);
                }
            }
        }

        public static void ZipWithAction<T1, T2, T3>(this IEnumerable<T1> i1, IEnumerable<T2> i2, IEnumerable<T3> i3, Action<T1, T2, T3> action)
        {
            using (var e1 = i1.GetEnumerator())
            using (var e2 = i2.GetEnumerator())
            using (var e3 = i3.GetEnumerator())
            {
                while (e1.MoveNext() && e2.MoveNext() && e3.MoveNext())
                {
                    action(e1.Current, e2.Current, e3.Current);
                }
            }
        }

        public static void ZipWithAction<T1, T2, T3, T4>(this IEnumerable<T1> i1, IEnumerable<T2> i2, IEnumerable<T3> i3, IEnumerable<T4> i4, Action<T1, T2, T3, T4> action)
        {
            using (var e1 = i1.GetEnumerator())
            using (var e2 = i2.GetEnumerator())
            using (var e3 = i3.GetEnumerator())
            using (var e4 = i4.GetEnumerator())
            {
                while (e1.MoveNext() && e2.MoveNext() && e3.MoveNext() && e4.MoveNext())
                {
                    action(e1.Current, e2.Current, e3.Current, e4.Current);
                }
            }
        }

        public static void ZipWithAction<T1, T2, T3, T4, T5>(this IEnumerable<T1> i1, IEnumerable<T2> i2, IEnumerable<T3> i3, IEnumerable<T4> i4, IEnumerable<T5> i5, Action<T1, T2, T3, T4, T5> action)
        {
            using (var e1 = i1.GetEnumerator())
            using (var e2 = i2.GetEnumerator())
            using (var e3 = i3.GetEnumerator())
            using (var e4 = i4.GetEnumerator())
            using (var e5 = i5.GetEnumerator())
            {
                while (e1.MoveNext() && e2.MoveNext() && e3.MoveNext() && e4.MoveNext() && e5.MoveNext())
                {
                    action(e1.Current, e2.Current, e3.Current, e4.Current, e5.Current);
                }
            }
        }

        public static void ZipWithAction<T1, T2, T3, T4, T5, T6>(this IEnumerable<T1> i1, IEnumerable<T2> i2, IEnumerable<T3> i3, IEnumerable<T4> i4, IEnumerable<T5> i5, IEnumerable<T6> i6, Action<T1, T2, T3, T4, T5, T6> action)
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
                    action(e1.Current, e2.Current, e3.Current, e4.Current, e5.Current, e6.Current);
                }
            }
        }

        public static void ZipWithAction<T1, T2, T3, T4, T5, T6, T7>(this IEnumerable<T1> i1, IEnumerable<T2> i2, IEnumerable<T3> i3, IEnumerable<T4> i4, IEnumerable<T5> i5, IEnumerable<T6> i6, IEnumerable<T7> i7, Action<T1, T2, T3, T4, T5, T6, T7> action)
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
                    action(e1.Current, e2.Current, e3.Current, e4.Current, e5.Current, e6.Current, e7.Current);
                }
            }
        }

        public static void ZipWithAction<T1, T2, T3, T4, T5, T6, T7, T8>(this IEnumerable<T1> i1, IEnumerable<T2> i2, IEnumerable<T3> i3, IEnumerable<T4> i4, IEnumerable<T5> i5, IEnumerable<T6> i6, IEnumerable<T7> i7, IEnumerable<T8> i8, Action<T1, T2, T3, T4, T5, T6, T7, T8> action)
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
                    action(e1.Current, e2.Current, e3.Current, e4.Current, e5.Current, e6.Current, e7.Current, e8.Current);
                }
            }
        }

        public static void ZipWithAction<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this IEnumerable<T1> i1, IEnumerable<T2> i2, IEnumerable<T3> i3, IEnumerable<T4> i4, IEnumerable<T5> i5, IEnumerable<T6> i6, IEnumerable<T7> i7, IEnumerable<T8> i8, IEnumerable<T9> i9, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action)
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
                    action(e1.Current, e2.Current, e3.Current, e4.Current, e5.Current, e6.Current, e7.Current, e8.Current, e9.Current);
                }
            }
        }

        public static void ZipWithAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this IEnumerable<T1> i1, IEnumerable<T2> i2, IEnumerable<T3> i3, IEnumerable<T4> i4, IEnumerable<T5> i5, IEnumerable<T6> i6, IEnumerable<T7> i7, IEnumerable<T8> i8, IEnumerable<T9> i9, IEnumerable<T10> i10, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action)
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
                    action(e1.Current, e2.Current, e3.Current, e4.Current, e5.Current, e6.Current, e7.Current, e8.Current, e9.Current, e10.Current);
                }
            }
        }

        public static void ZipWithAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this IEnumerable<T1> i1, IEnumerable<T2> i2, IEnumerable<T3> i3, IEnumerable<T4> i4, IEnumerable<T5> i5, IEnumerable<T6> i6, IEnumerable<T7> i7, IEnumerable<T8> i8, IEnumerable<T9> i9, IEnumerable<T10> i10, IEnumerable<T11> i11, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action)
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
                    action(e1.Current, e2.Current, e3.Current, e4.Current, e5.Current, e6.Current, e7.Current, e8.Current, e9.Current, e10.Current, e11.Current);
                }
            }
        }

        public static void ZipWithAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this IEnumerable<T1> i1, IEnumerable<T2> i2, IEnumerable<T3> i3, IEnumerable<T4> i4, IEnumerable<T5> i5, IEnumerable<T6> i6, IEnumerable<T7> i7, IEnumerable<T8> i8, IEnumerable<T9> i9, IEnumerable<T10> i10, IEnumerable<T11> i11, IEnumerable<T12> i12, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action)
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
                    action(e1.Current, e2.Current, e3.Current, e4.Current, e5.Current, e6.Current, e7.Current, e8.Current, e9.Current, e10.Current, e11.Current, e12.Current);
                }
            }
        }

        public static void ZipWithAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this IEnumerable<T1> i1, IEnumerable<T2> i2, IEnumerable<T3> i3, IEnumerable<T4> i4, IEnumerable<T5> i5, IEnumerable<T6> i6, IEnumerable<T7> i7, IEnumerable<T8> i8, IEnumerable<T9> i9, IEnumerable<T10> i10, IEnumerable<T11> i11, IEnumerable<T12> i12, IEnumerable<T13> i13, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action)
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
                    action(e1.Current, e2.Current, e3.Current, e4.Current, e5.Current, e6.Current, e7.Current, e8.Current, e9.Current, e10.Current, e11.Current, e12.Current, e13.Current);
                }
            }
        }

        public static void ZipWithAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this IEnumerable<T1> i1, IEnumerable<T2> i2, IEnumerable<T3> i3, IEnumerable<T4> i4, IEnumerable<T5> i5, IEnumerable<T6> i6, IEnumerable<T7> i7, IEnumerable<T8> i8, IEnumerable<T9> i9, IEnumerable<T10> i10, IEnumerable<T11> i11, IEnumerable<T12> i12, IEnumerable<T13> i13, IEnumerable<T14> i14, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action)
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
                    action(e1.Current, e2.Current, e3.Current, e4.Current, e5.Current, e6.Current, e7.Current, e8.Current, e9.Current, e10.Current, e11.Current, e12.Current, e13.Current, e14.Current);
                }
            }
        }

        public static void ZipWithAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this IEnumerable<T1> i1, IEnumerable<T2> i2, IEnumerable<T3> i3, IEnumerable<T4> i4, IEnumerable<T5> i5, IEnumerable<T6> i6, IEnumerable<T7> i7, IEnumerable<T8> i8, IEnumerable<T9> i9, IEnumerable<T10> i10, IEnumerable<T11> i11, IEnumerable<T12> i12, IEnumerable<T13> i13, IEnumerable<T14> i14, IEnumerable<T15> i15, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action)
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
                    action(e1.Current, e2.Current, e3.Current, e4.Current, e5.Current, e6.Current, e7.Current, e8.Current, e9.Current, e10.Current, e11.Current, e12.Current, e13.Current, e14.Current, e15.Current);
                }
            }
        }

        public static void ZipWithAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this IEnumerable<T1> i1, IEnumerable<T2> i2, IEnumerable<T3> i3, IEnumerable<T4> i4, IEnumerable<T5> i5, IEnumerable<T6> i6, IEnumerable<T7> i7, IEnumerable<T8> i8, IEnumerable<T9> i9, IEnumerable<T10> i10, IEnumerable<T11> i11, IEnumerable<T12> i12, IEnumerable<T13> i13, IEnumerable<T14> i14, IEnumerable<T15> i15, IEnumerable<T16> i16, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> action)
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
                    action(e1.Current, e2.Current, e3.Current, e4.Current, e5.Current, e6.Current, e7.Current, e8.Current, e9.Current, e10.Current, e11.Current, e12.Current, e13.Current, e14.Current, e15.Current, e16.Current);
                }
            }
        }
    }
}
