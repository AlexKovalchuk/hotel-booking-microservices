namespace Search.Application.DTOs.Search;

public class SearchHotelsRequest
{
    public string? City { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public int? MinRating { get; set; }
}