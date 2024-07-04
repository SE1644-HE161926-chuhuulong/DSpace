namespace Domain;

public class Statistic
{
    public int StatisticId { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public int ViewCount { get; set; }
    public int ViewOnDay { get; set; }
    public int ItemId { get; set; }
    public Item Item { get; set; }
}