namespace Replate.Application.Common.Models;

public class Result<T>
{
    public bool IsSuccess { get; set; }
    public T? Data { get; set; }
    public string? ErrorMessage { get; set; }
    public List<string> ValidationErrors  { get; set; }
    
    public static Result<T> Success(T data)
    {
        return new Result<T>
        {
            IsSuccess = true, 
            Data = data
           
        };
    }

    public static Result<T> Failure(string errorMessage)
    {
        return new Result<T>
        {
            IsSuccess = false,
            ErrorMessage = errorMessage,
        };
    }
    
    public static Result<T> ValidationFailure(List<string> validationErrors)
    {
        return new Result<T>
        {
            IsSuccess = false,
            ValidationErrors = validationErrors,
        };
    }
}