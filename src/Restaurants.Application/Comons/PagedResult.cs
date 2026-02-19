namespace Restaurants.Application.Comons;

public class PagedResult <T>
{
    public IEnumerable<T> Items { get; set; }
    public int TotalCount { get; set; }

    public int TotalPages { get; set; }

    public int ResultFrom { get; set; }
    public int ResultTo { get; set; }

    public PagedResult(IEnumerable<T> values , int recordscount , int pagesize , int pagenum)
    {
        Items = values;

        TotalCount = recordscount;

        TotalPages = (int) Math.Ceiling((recordscount /(double) pagesize));

        ResultFrom = (pagenum - 1) * pagesize + 1;

        ResultTo = ResultFrom + pagesize -1;

    }

}
