namespace ps40165_Main.Shared.ModelResult;

public class Result<T>
{
    public bool IsSuccess { get; set; }

    public Stack<string> Messages { get; } = new Stack<string>();

    public Stack<string> Errors { get; } = new Stack<string>();

    public T? Data { get; }

    private Result(bool isSuccess, string message)
    {
        IsSuccess = isSuccess;

        if (isSuccess)
        {
            if (!string.IsNullOrEmpty(message))
                Messages.Push(message);
        }
        else
        {
            if (!string.IsNullOrEmpty(message))
                Errors.Push(message);
        }
    }

    private Result(bool isSuccess, T data, string message) : this(isSuccess, message)
    {
        Data = data;
    }

    public static Result<T> Ok(string message = "") => new Result<T>(true, message);

    public static Result<T> Ok(T data, string message = "") => new Result<T>(true, data, message);

    public static Result<T> Fail(string error) => new Result<T>(false, error);
}

public static class ResultMapping
{
    public static Result<TOut> Bind<TIn, TOut>(this Result<TIn> result, Func<Result<TIn>, Result<TOut>> bind)
    {
        var r = bind(result);

        if (!result.IsSuccess)
        {
            r.IsSuccess = false;

            foreach (var error in result.Errors)
            {
                r.Errors.Push(error);
            }
        }

        return r;
    }
}
