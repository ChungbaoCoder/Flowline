namespace ps40165_Main.Shared;

public class CentralResponse<T>
{
    public Stack<string>? Messages { get; set; }

    public Stack<string>? Errors { get; set; }

    public T? Data { get; set; }
}
