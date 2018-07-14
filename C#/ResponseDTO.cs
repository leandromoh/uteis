using System;

namespace DTO
{
    public enum Result
    {
        Success = 1,
        Error = 2,
        Exception = 3,
    }

    public struct ResponseDTO<T>
    {
        public Result Result { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public bool Success => this.Result == Result.Success;

        public ResponseDTO<R> Cast<R>(R data = default(R))
        {
            return new ResponseDTO<R>
            {
                Result = Result,
                Message = Message,
                Data = data,
            };
        }

        public static implicit operator ResponseDTO<T>(Exception ex)
        {
            var x = ResponseDTOHelper.Create<T>(Result.Exception);
            x.Message = ex.Message;
            return x;
        }
    }

    public static class ResponseDTOHelper
    {
        public static ResponseDTO<R> Functor<T, R>(this ResponseDTO<T> dto, Func<T, R> selector)
        {
            if (dto.Success)
            {
                try
                {
                    return selector(dto.Data).ToSucess();
                }
                catch (Exception ex)
                {
                    return ex;
                }
            }
            else
            {
                return dto.Cast<R>();
            }
        }

        public static ResponseDTO<R> Applicative<T, R>(this ResponseDTO<T> dto, ResponseDTO<Func<T, R>> selector)
        {
            return selector.Success ? dto.Functor(selector.Data) : selector.Cast<R>();
        }

        public static ResponseDTO<R> Monad<T, R>(this ResponseDTO<T> dto, Func<T, ResponseDTO<R>> selector)
        {
            return dto.Success ? selector(dto.Data) : dto.Cast<R>();
        }

        public static ResponseDTO<T> ToResposeDTO<T>(this Func<T> func)
        {
            return Sucess<T>().Functor(_ => func());
        }

        public static ResponseDTO<T> Create<T>(Result res, T obj = default(T))
        {
            return new ResponseDTO<T> { Data = obj, Result = res };
        }

        public static ResponseDTO<T> Sucess<T>(T obj = default(T))
        {
            return Create(Result.Success, obj);
        }

        public static ResponseDTO<T> Fail<T>(T obj = default(T))
        {
            return Create(Result.Error, obj);
        }

        public static ResponseDTO<T> ToSucess<T>(this T obj)
        {
            return Sucess(obj);
        }

        public static ResponseDTO<T> ToFail<T>(this T obj)
        {
            return Fail(obj);
        }
    }
}
