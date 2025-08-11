namespace Application.Respondbodies;

public class Response<T> where T : class
{
    public bool Success { get; set; }
    
    public string Message { get; set; }
    
    public T? Data { get; set; }
    
    
    
    
    public static Response<T> GetNotAcceptedRequest(string Message)
    {
        return new Response<T>()
        {
            Message = Message,
            Success = false,
            Data = null
        };
    }

    public static Response<T> GetAcceptedRequest(string Message, T Data)
    {
        return new Response<T>()
        {
            Message = Message,
            Success = true,
            Data = Data
        };
    }
}

