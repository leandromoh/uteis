using Curryfy;
using System;
using System.Linq;
using System.Threading;

namespace teste
{
    public static class CacheProgram
    {
        public static void Main()
        {
            Func<int, int, int, int> add = (a, b, c) => a + b + c;
            var curried = add.Curry();            // curried is Func<int, Func<int, Func<int, int>>>
            var add5 = curried(5);                // add5 is Func<int, Func<int, int>>
            var uncurried = add5.UnCurry();       // uncurried is Func<int, int, int>

            var x = uncurried(7, 3) + curried(5)(7)(3);    // 15 + 15


            bool result = f_abc_1('b');

            Console.WriteLine();

            Func<int, int> Pow = x => (int)Math.Pow(x, 2);
            var asd = Pow.Cache(2 * 1000, (c, a) => new { c, a });

            Console.WriteLine(asd.c(5));
            Thread.Sleep(1 * 1000);
            Console.WriteLine(asd.c(5));
            Thread.Sleep(1 * 1000);

            Console.WriteLine(asd.c(5));
            Thread.Sleep(1 * 1000);
            Console.WriteLine(asd.c(5));
            Thread.Sleep(1 * 1000);

            Console.WriteLine(asd.c(5));


            Console.Read();
        }

        public static TSelector Cache<T1, TResult, TSelector>(this Func<T1, TResult> function, int? cacheExpirationInSeconds, Func<Func<T1, TResult>, Func<T1, bool>, TSelector> selector)
        {
            var arguments = new { arg1 = default(T1) };
            var result = Tuple.Create(default(DateTime?), default(TResult));
            var dic = Enumerable.Repeat(result, 0).ToDictionary(x => arguments);

            return selector((arg1) =>
            {
                var tuple = Tuple.Create(default(DateTime?), default(TResult));
                var key = new { arg1 };

                if (!dic.TryGetValue(key, out tuple) || (tuple.Item1.HasValue && tuple.Item1.Value < DateTime.Now))
                {
                    var value = function(arg1);
                    var expiration = cacheExpirationInSeconds.HasValue
                                     ? DateTime.Now.AddMilliseconds(cacheExpirationInSeconds.Value)
                                     : (DateTime?)null;

                    dic[key] = tuple = Tuple.Create(expiration, value);
                }

                return tuple.Item2;
            }, (arg1) =>
            {
                var key = new { arg1 };

                return dic.Remove(key);
            });
        }

        public static TSelector Cache<T1, T2, TResult, TSelector>(this Func<T1, T2, TResult> function, int? cacheExpirationInSeconds, Func<Func<T1, T2, TResult>, Func<T1, T2, bool>, TSelector> selector)
        {
            var arguments = new { arg1 = default(T1), arg2 = default(T2) };
            var result = Tuple.Create(default(DateTime?), default(TResult));
            var dic = Enumerable.Repeat(result, 0).ToDictionary(x => arguments);

            return selector((arg1, arg2) =>
            {
                var tuple = Tuple.Create(default(DateTime?), default(TResult));
                var key = new { arg1, arg2 };

                if (!dic.TryGetValue(key, out tuple) || (tuple.Item1.HasValue && tuple.Item1.Value < DateTime.Now))
                {
                    var value = function(arg1, arg2);
                    var expiration = cacheExpirationInSeconds.HasValue
                                     ? DateTime.Now.AddMilliseconds(cacheExpirationInSeconds.Value)
                                     : (DateTime?)null;

                    dic[key] = tuple = Tuple.Create(expiration, value);
                }

                return tuple.Item2;
            }, (arg1, arg2) =>
            {
                var key = new { arg1, arg2 };

                return dic.Remove(key);
            });
        }

        public static TSelector Cache<T1, T2, T3, TResult, TSelector>(this Func<T1, T2, T3, TResult> function, int? cacheExpirationInSeconds, Func<Func<T1, T2, T3, TResult>, Func<T1, T2, T3, bool>, TSelector> selector)
        {
            var arguments = new { arg1 = default(T1), arg2 = default(T2), arg3 = default(T3) };
            var result = Tuple.Create(default(DateTime?), default(TResult));
            var dic = Enumerable.Repeat(result, 0).ToDictionary(x => arguments);

            return selector((arg1, arg2, arg3) =>
            {
                var tuple = Tuple.Create(default(DateTime?), default(TResult));
                var key = new { arg1, arg2, arg3 };

                if (!dic.TryGetValue(key, out tuple) || (tuple.Item1.HasValue && tuple.Item1.Value < DateTime.Now))
                {
                    var value = function(arg1, arg2, arg3);
                    var expiration = cacheExpirationInSeconds.HasValue
                                     ? DateTime.Now.AddMilliseconds(cacheExpirationInSeconds.Value)
                                     : (DateTime?)null;

                    dic[key] = tuple = Tuple.Create(expiration, value);
                }

                return tuple.Item2;
            }, (arg1, arg2, arg3) =>
            {
                var key = new { arg1, arg2, arg3 };

                return dic.Remove(key);
            });
        }

        public static TSelector Cache<T1, T2, T3, T4, TResult, TSelector>(this Func<T1, T2, T3, T4, TResult> function, int? cacheExpirationInSeconds, Func<Func<T1, T2, T3, T4, TResult>, Func<T1, T2, T3, T4, bool>, TSelector> selector)
        {
            var arguments = new { arg1 = default(T1), arg2 = default(T2), arg3 = default(T3), arg4 = default(T4) };
            var result = Tuple.Create(default(DateTime?), default(TResult));
            var dic = Enumerable.Repeat(result, 0).ToDictionary(x => arguments);

            return selector((arg1, arg2, arg3, arg4) =>
            {
                var tuple = Tuple.Create(default(DateTime?), default(TResult));
                var key = new { arg1, arg2, arg3, arg4 };

                if (!dic.TryGetValue(key, out tuple) || (tuple.Item1.HasValue && tuple.Item1.Value < DateTime.Now))
                {
                    var value = function(arg1, arg2, arg3, arg4);
                    var expiration = cacheExpirationInSeconds.HasValue
                                     ? DateTime.Now.AddMilliseconds(cacheExpirationInSeconds.Value)
                                     : (DateTime?)null;

                    dic[key] = tuple = Tuple.Create(expiration, value);
                }

                return tuple.Item2;
            }, (arg1, arg2, arg3, arg4) =>
            {
                var key = new { arg1, arg2, arg3, arg4 };

                return dic.Remove(key);
            });
        }

        public static TSelector Cache<T1, T2, T3, T4, T5, TResult, TSelector>(this Func<T1, T2, T3, T4, T5, TResult> function, int? cacheExpirationInSeconds, Func<Func<T1, T2, T3, T4, T5, TResult>, Func<T1, T2, T3, T4, T5, bool>, TSelector> selector)
        {
            var arguments = new { arg1 = default(T1), arg2 = default(T2), arg3 = default(T3), arg4 = default(T4), arg5 = default(T5) };
            var result = Tuple.Create(default(DateTime?), default(TResult));
            var dic = Enumerable.Repeat(result, 0).ToDictionary(x => arguments);

            return selector((arg1, arg2, arg3, arg4, arg5) =>
            {
                var tuple = Tuple.Create(default(DateTime?), default(TResult));
                var key = new { arg1, arg2, arg3, arg4, arg5 };

                if (!dic.TryGetValue(key, out tuple) || (tuple.Item1.HasValue && tuple.Item1.Value < DateTime.Now))
                {
                    var value = function(arg1, arg2, arg3, arg4, arg5);
                    var expiration = cacheExpirationInSeconds.HasValue
                                     ? DateTime.Now.AddMilliseconds(cacheExpirationInSeconds.Value)
                                     : (DateTime?)null;

                    dic[key] = tuple = Tuple.Create(expiration, value);
                }

                return tuple.Item2;
            }, (arg1, arg2, arg3, arg4, arg5) =>
            {
                var key = new { arg1, arg2, arg3, arg4, arg5 };

                return dic.Remove(key);
            });
        }

        public static TSelector Cache<T1, T2, T3, T4, T5, T6, TResult, TSelector>(this Func<T1, T2, T3, T4, T5, T6, TResult> function, int? cacheExpirationInSeconds, Func<Func<T1, T2, T3, T4, T5, T6, TResult>, Func<T1, T2, T3, T4, T5, T6, bool>, TSelector> selector)
        {
            var arguments = new { arg1 = default(T1), arg2 = default(T2), arg3 = default(T3), arg4 = default(T4), arg5 = default(T5), arg6 = default(T6) };
            var result = Tuple.Create(default(DateTime?), default(TResult));
            var dic = Enumerable.Repeat(result, 0).ToDictionary(x => arguments);

            return selector((arg1, arg2, arg3, arg4, arg5, arg6) =>
            {
                var tuple = Tuple.Create(default(DateTime?), default(TResult));
                var key = new { arg1, arg2, arg3, arg4, arg5, arg6 };

                if (!dic.TryGetValue(key, out tuple) || (tuple.Item1.HasValue && tuple.Item1.Value < DateTime.Now))
                {
                    var value = function(arg1, arg2, arg3, arg4, arg5, arg6);
                    var expiration = cacheExpirationInSeconds.HasValue
                                     ? DateTime.Now.AddMilliseconds(cacheExpirationInSeconds.Value)
                                     : (DateTime?)null;

                    dic[key] = tuple = Tuple.Create(expiration, value);
                }

                return tuple.Item2;
            }, (arg1, arg2, arg3, arg4, arg5, arg6) =>
            {
                var key = new { arg1, arg2, arg3, arg4, arg5, arg6 };

                return dic.Remove(key);
            });
        }

        public static TSelector Cache<T1, T2, T3, T4, T5, T6, T7, TResult, TSelector>(this Func<T1, T2, T3, T4, T5, T6, T7, TResult> function, int? cacheExpirationInSeconds, Func<Func<T1, T2, T3, T4, T5, T6, T7, TResult>, Func<T1, T2, T3, T4, T5, T6, T7, bool>, TSelector> selector)
        {
            var arguments = new { arg1 = default(T1), arg2 = default(T2), arg3 = default(T3), arg4 = default(T4), arg5 = default(T5), arg6 = default(T6), arg7 = default(T7) };
            var result = Tuple.Create(default(DateTime?), default(TResult));
            var dic = Enumerable.Repeat(result, 0).ToDictionary(x => arguments);

            return selector((arg1, arg2, arg3, arg4, arg5, arg6, arg7) =>
            {
                var tuple = Tuple.Create(default(DateTime?), default(TResult));
                var key = new { arg1, arg2, arg3, arg4, arg5, arg6, arg7 };

                if (!dic.TryGetValue(key, out tuple) || (tuple.Item1.HasValue && tuple.Item1.Value < DateTime.Now))
                {
                    var value = function(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
                    var expiration = cacheExpirationInSeconds.HasValue
                                     ? DateTime.Now.AddMilliseconds(cacheExpirationInSeconds.Value)
                                     : (DateTime?)null;

                    dic[key] = tuple = Tuple.Create(expiration, value);
                }

                return tuple.Item2;
            }, (arg1, arg2, arg3, arg4, arg5, arg6, arg7) =>
            {
                var key = new { arg1, arg2, arg3, arg4, arg5, arg6, arg7 };

                return dic.Remove(key);
            });
        }

        public static TSelector Cache<T1, T2, T3, T4, T5, T6, T7, T8, TResult, TSelector>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> function, int? cacheExpirationInSeconds, Func<Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>, Func<T1, T2, T3, T4, T5, T6, T7, T8, bool>, TSelector> selector)
        {
            var arguments = new { arg1 = default(T1), arg2 = default(T2), arg3 = default(T3), arg4 = default(T4), arg5 = default(T5), arg6 = default(T6), arg7 = default(T7), arg8 = default(T8) };
            var result = Tuple.Create(default(DateTime?), default(TResult));
            var dic = Enumerable.Repeat(result, 0).ToDictionary(x => arguments);

            return selector((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) =>
            {
                var tuple = Tuple.Create(default(DateTime?), default(TResult));
                var key = new { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8 };

                if (!dic.TryGetValue(key, out tuple) || (tuple.Item1.HasValue && tuple.Item1.Value < DateTime.Now))
                {
                    var value = function(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
                    var expiration = cacheExpirationInSeconds.HasValue
                                     ? DateTime.Now.AddMilliseconds(cacheExpirationInSeconds.Value)
                                     : (DateTime?)null;

                    dic[key] = tuple = Tuple.Create(expiration, value);
                }

                return tuple.Item2;
            }, (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) =>
            {
                var key = new { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8 };

                return dic.Remove(key);
            });
        }

        public static TSelector Cache<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult, TSelector>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> function, int? cacheExpirationInSeconds, Func<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, bool>, TSelector> selector)
        {
            var arguments = new { arg1 = default(T1), arg2 = default(T2), arg3 = default(T3), arg4 = default(T4), arg5 = default(T5), arg6 = default(T6), arg7 = default(T7), arg8 = default(T8), arg9 = default(T9) };
            var result = Tuple.Create(default(DateTime?), default(TResult));
            var dic = Enumerable.Repeat(result, 0).ToDictionary(x => arguments);

            return selector((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) =>
            {
                var tuple = Tuple.Create(default(DateTime?), default(TResult));
                var key = new { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9 };

                if (!dic.TryGetValue(key, out tuple) || (tuple.Item1.HasValue && tuple.Item1.Value < DateTime.Now))
                {
                    var value = function(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
                    var expiration = cacheExpirationInSeconds.HasValue
                                     ? DateTime.Now.AddMilliseconds(cacheExpirationInSeconds.Value)
                                     : (DateTime?)null;

                    dic[key] = tuple = Tuple.Create(expiration, value);
                }

                return tuple.Item2;
            }, (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) =>
            {
                var key = new { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9 };

                return dic.Remove(key);
            });
        }

        public static TSelector Cache<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult, TSelector>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> function, int? cacheExpirationInSeconds, Func<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, bool>, TSelector> selector)
        {
            var arguments = new { arg1 = default(T1), arg2 = default(T2), arg3 = default(T3), arg4 = default(T4), arg5 = default(T5), arg6 = default(T6), arg7 = default(T7), arg8 = default(T8), arg9 = default(T9), arg10 = default(T10) };
            var result = Tuple.Create(default(DateTime?), default(TResult));
            var dic = Enumerable.Repeat(result, 0).ToDictionary(x => arguments);

            return selector((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) =>
            {
                var tuple = Tuple.Create(default(DateTime?), default(TResult));
                var key = new { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10 };

                if (!dic.TryGetValue(key, out tuple) || (tuple.Item1.HasValue && tuple.Item1.Value < DateTime.Now))
                {
                    var value = function(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
                    var expiration = cacheExpirationInSeconds.HasValue
                                     ? DateTime.Now.AddMilliseconds(cacheExpirationInSeconds.Value)
                                     : (DateTime?)null;

                    dic[key] = tuple = Tuple.Create(expiration, value);
                }

                return tuple.Item2;
            }, (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) =>
            {
                var key = new { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10 };

                return dic.Remove(key);
            });
        }

        public static TSelector Cache<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult, TSelector>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> function, int? cacheExpirationInSeconds, Func<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, bool>, TSelector> selector)
        {
            var arguments = new { arg1 = default(T1), arg2 = default(T2), arg3 = default(T3), arg4 = default(T4), arg5 = default(T5), arg6 = default(T6), arg7 = default(T7), arg8 = default(T8), arg9 = default(T9), arg10 = default(T10), arg11 = default(T11) };
            var result = Tuple.Create(default(DateTime?), default(TResult));
            var dic = Enumerable.Repeat(result, 0).ToDictionary(x => arguments);

            return selector((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) =>
            {
                var tuple = Tuple.Create(default(DateTime?), default(TResult));
                var key = new { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11 };

                if (!dic.TryGetValue(key, out tuple) || (tuple.Item1.HasValue && tuple.Item1.Value < DateTime.Now))
                {
                    var value = function(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
                    var expiration = cacheExpirationInSeconds.HasValue
                                     ? DateTime.Now.AddMilliseconds(cacheExpirationInSeconds.Value)
                                     : (DateTime?)null;

                    dic[key] = tuple = Tuple.Create(expiration, value);
                }

                return tuple.Item2;
            }, (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) =>
            {
                var key = new { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11 };

                return dic.Remove(key);
            });
        }

        public static TSelector Cache<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult, TSelector>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> function, int? cacheExpirationInSeconds, Func<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, bool>, TSelector> selector)
        {
            var arguments = new { arg1 = default(T1), arg2 = default(T2), arg3 = default(T3), arg4 = default(T4), arg5 = default(T5), arg6 = default(T6), arg7 = default(T7), arg8 = default(T8), arg9 = default(T9), arg10 = default(T10), arg11 = default(T11), arg12 = default(T12) };
            var result = Tuple.Create(default(DateTime?), default(TResult));
            var dic = Enumerable.Repeat(result, 0).ToDictionary(x => arguments);

            return selector((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) =>
            {
                var tuple = Tuple.Create(default(DateTime?), default(TResult));
                var key = new { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12 };

                if (!dic.TryGetValue(key, out tuple) || (tuple.Item1.HasValue && tuple.Item1.Value < DateTime.Now))
                {
                    var value = function(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
                    var expiration = cacheExpirationInSeconds.HasValue
                                     ? DateTime.Now.AddMilliseconds(cacheExpirationInSeconds.Value)
                                     : (DateTime?)null;

                    dic[key] = tuple = Tuple.Create(expiration, value);
                }

                return tuple.Item2;
            }, (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) =>
            {
                var key = new { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12 };

                return dic.Remove(key);
            });
        }

        public static TSelector Cache<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult, TSelector>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> function, int? cacheExpirationInSeconds, Func<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, bool>, TSelector> selector)
        {
            var arguments = new { arg1 = default(T1), arg2 = default(T2), arg3 = default(T3), arg4 = default(T4), arg5 = default(T5), arg6 = default(T6), arg7 = default(T7), arg8 = default(T8), arg9 = default(T9), arg10 = default(T10), arg11 = default(T11), arg12 = default(T12), arg13 = default(T13) };
            var result = Tuple.Create(default(DateTime?), default(TResult));
            var dic = Enumerable.Repeat(result, 0).ToDictionary(x => arguments);

            return selector((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) =>
            {
                var tuple = Tuple.Create(default(DateTime?), default(TResult));
                var key = new { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13 };

                if (!dic.TryGetValue(key, out tuple) || (tuple.Item1.HasValue && tuple.Item1.Value < DateTime.Now))
                {
                    var value = function(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
                    var expiration = cacheExpirationInSeconds.HasValue
                                     ? DateTime.Now.AddMilliseconds(cacheExpirationInSeconds.Value)
                                     : (DateTime?)null;

                    dic[key] = tuple = Tuple.Create(expiration, value);
                }

                return tuple.Item2;
            }, (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) =>
            {
                var key = new { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13 };

                return dic.Remove(key);
            });
        }

        public static TSelector Cache<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult, TSelector>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> function, int? cacheExpirationInSeconds, Func<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, bool>, TSelector> selector)
        {
            var arguments = new { arg1 = default(T1), arg2 = default(T2), arg3 = default(T3), arg4 = default(T4), arg5 = default(T5), arg6 = default(T6), arg7 = default(T7), arg8 = default(T8), arg9 = default(T9), arg10 = default(T10), arg11 = default(T11), arg12 = default(T12), arg13 = default(T13), arg14 = default(T14) };
            var result = Tuple.Create(default(DateTime?), default(TResult));
            var dic = Enumerable.Repeat(result, 0).ToDictionary(x => arguments);

            return selector((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) =>
            {
                var tuple = Tuple.Create(default(DateTime?), default(TResult));
                var key = new { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14 };

                if (!dic.TryGetValue(key, out tuple) || (tuple.Item1.HasValue && tuple.Item1.Value < DateTime.Now))
                {
                    var value = function(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
                    var expiration = cacheExpirationInSeconds.HasValue
                                     ? DateTime.Now.AddMilliseconds(cacheExpirationInSeconds.Value)
                                     : (DateTime?)null;

                    dic[key] = tuple = Tuple.Create(expiration, value);
                }

                return tuple.Item2;
            }, (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) =>
            {
                var key = new { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14 };

                return dic.Remove(key);
            });
        }

        public static TSelector Cache<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult, TSelector>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> function, int? cacheExpirationInSeconds, Func<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, bool>, TSelector> selector)
        {
            var arguments = new { arg1 = default(T1), arg2 = default(T2), arg3 = default(T3), arg4 = default(T4), arg5 = default(T5), arg6 = default(T6), arg7 = default(T7), arg8 = default(T8), arg9 = default(T9), arg10 = default(T10), arg11 = default(T11), arg12 = default(T12), arg13 = default(T13), arg14 = default(T14), arg15 = default(T15) };
            var result = Tuple.Create(default(DateTime?), default(TResult));
            var dic = Enumerable.Repeat(result, 0).ToDictionary(x => arguments);

            return selector((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) =>
            {
                var tuple = Tuple.Create(default(DateTime?), default(TResult));
                var key = new { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15 };

                if (!dic.TryGetValue(key, out tuple) || (tuple.Item1.HasValue && tuple.Item1.Value < DateTime.Now))
                {
                    var value = function(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
                    var expiration = cacheExpirationInSeconds.HasValue
                                     ? DateTime.Now.AddMilliseconds(cacheExpirationInSeconds.Value)
                                     : (DateTime?)null;

                    dic[key] = tuple = Tuple.Create(expiration, value);
                }

                return tuple.Item2;
            }, (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) =>
            {
                var key = new { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15 };

                return dic.Remove(key);
            });
        }

        public static TSelector Cache<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult, TSelector>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> function, int? cacheExpirationInSeconds, Func<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, bool>, TSelector> selector)
        {
            var arguments = new { arg1 = default(T1), arg2 = default(T2), arg3 = default(T3), arg4 = default(T4), arg5 = default(T5), arg6 = default(T6), arg7 = default(T7), arg8 = default(T8), arg9 = default(T9), arg10 = default(T10), arg11 = default(T11), arg12 = default(T12), arg13 = default(T13), arg14 = default(T14), arg15 = default(T15), arg16 = default(T16) };
            var result = Tuple.Create(default(DateTime?), default(TResult));
            var dic = Enumerable.Repeat(result, 0).ToDictionary(x => arguments);

            return selector((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) =>
            {
                var tuple = Tuple.Create(default(DateTime?), default(TResult));
                var key = new { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16 };

                if (!dic.TryGetValue(key, out tuple) || (tuple.Item1.HasValue && tuple.Item1.Value < DateTime.Now))
                {
                    var value = function(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
                    var expiration = cacheExpirationInSeconds.HasValue
                                     ? DateTime.Now.AddMilliseconds(cacheExpirationInSeconds.Value)
                                     : (DateTime?)null;

                    dic[key] = tuple = Tuple.Create(expiration, value);
                }

                return tuple.Item2;
            }, (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) =>
            {
                var key = new { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16 };

                return dic.Remove(key);
            });
        }



        private static string GetCache(int arity)
        {
            var n = Enumerable.Range(1, arity).ToArray();

            return string.Format(
@"
            public static TSelector Cache<{0}, TResult, TSelector>(this Func<{0}, TResult> function, int? cacheExpirationInSeconds, Func<Func<{0}, TResult>, Func<{0}, bool>, TSelector> selector)
            {{
                var arguments = new {{ {1} }};
                var result = Tuple.Create(default(DateTime?), default(TResult));
                var dic = Enumerable.Repeat(result, 0).ToDictionary(x => arguments);

                return selector(({2}) =>
                {{
                    var tuple = Tuple.Create(default(DateTime?), default(TResult));
                    var key = new {{ {2} }};

                    if (!dic.TryGetValue(key, out tuple) || (tuple.Item1.HasValue && tuple.Item1.Value < DateTime.Now))
                    {{
                        var value = function({2});
                        var expiration = cacheExpirationInSeconds.HasValue
                                         ? DateTime.Now.AddMilliseconds(cacheExpirationInSeconds.Value)
                                         : (DateTime?)null;

                        dic[key] = tuple = Tuple.Create(expiration, value);
                    }}

                    return tuple.Item2;
                }}, ({2}) =>
                {{
                    var key = new {{ {2} }};

                    return dic.Remove(key);
                }});
            }}", string.Join(", ", n.Select(x => "T" + x).ToArray()),
                 string.Join(", ", n.Select(x => string.Format("arg{0} = default(T{0})", x)).ToArray()),
                 string.Join(", ", n.Select(x => string.Format("arg{0}", x)).ToArray()));
        }
    }
}
