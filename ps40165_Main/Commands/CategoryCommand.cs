﻿namespace ps40165_Main.Commands;

public class AddCategoryCommand
{
    public required string Name { get; set; }

    public string Description { get; set; } = string.Empty;
}

public class EditCategoryCommand
{
    public int CategoryId { get; set; }

    public required string Name { get; set; }

    public string Description { get; set; } = string.Empty;
}
