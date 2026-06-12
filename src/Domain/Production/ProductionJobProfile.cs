namespace Domain.Production;

public sealed class ProductionJobProfile
{
    public Guid FilmId { get; set; }

    public FilmPriority Priority { get; set; } = FilmPriority.Normal;

    public DateTime? Deadline { get; set; }

    public decimal BudgetLimit { get; set; }

    public decimal Spent { get; set; }
}
