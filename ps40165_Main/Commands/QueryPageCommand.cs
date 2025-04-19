namespace ps40165_Main.Commands;

public class QueryPageCommand
{
    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public string? SearchTerm { get; set; }
}
