namespace LabTec.Common.Utilities
{
    public class Result
    {
        public bool Success { get; }
        public string Message { get; }
        public Exception Exception { get; }

        public static Result Fail(string message)
        {
            return new Result(false, message);
        }
        public static Result Fail(Exception exception)
        {
            return new Result(false, exception.Message, exception);
        }
        public static Result Ok()
        {
            return new Result(true, string.Empty);
        }

        protected Result(bool success, string message, Exception exception = null)
        {
            Success = success;
            Message = message;
            Exception = exception;
        }
    }

    public class Result<T> : Result
    {
        public T Value { get; }

        public static new Result<T> Fail(string message)
        {
            return new Result<T>(default, false, message, null);
        }
        public static new Result<T> Fail(Exception exception)
        {
            return new Result<T>(default, false, exception.Message, exception);
        }
        public static Result<T> Ok(T value)
        {
            return new Result<T>(value, true, string.Empty);
        }

        private Result(T value, bool success, string message, Exception exception = null)
            : base(success, message, exception)
        {
            Value = value;
        }
    }
}
