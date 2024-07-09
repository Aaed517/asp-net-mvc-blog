using System.Collections.Generic;
using Entities.Models;  
public class PaginatedPosts
{
    public List<Post>? Posts { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPosts { get; set; }

    public int TotalPages => (int)Math.Ceiling((double)TotalPosts / PageSize);

    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;
}