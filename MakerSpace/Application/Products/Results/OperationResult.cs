namespace MakerSpace.Application.Products.Results;

public record OperationResult<T> {
   public bool IsSuccess { get; init; }
   public T? Data { get; init; }
   public string? Error { get; init; }
   
   public static OperationResult<T> Success(T data) => new() { IsSuccess = true, Data = data };
   public static OperationResult<T> Failure(string error) => new() { IsSuccess = false, Error = error };
}