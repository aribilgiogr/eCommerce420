namespace Tools.Responses
{
    public class Reply
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public IEnumerable<string> Errors { get; set; } = [];

        public static Reply Success(string? message = null) => new() { IsSuccess = true, Message = message };
        public static Reply Fail(string error) => new() { IsSuccess = false, Errors = [error] };
        public static Reply Fail(IEnumerable<string> errors) => new() { IsSuccess = false, Errors = errors };
    }

    public class Reply<T> : Reply where T : class
    {
        public T? Data { get; set; }

        public static Reply<T> Success(T? data, string? message) => new() { IsSuccess = true, Data = data, Message = message };
    }
}
