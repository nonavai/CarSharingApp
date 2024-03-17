namespace CarSharingApp.DealService.BusinessLogic.Queries;

public class GetCollectionBaseQuery
{
    public int CurrentPage { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}