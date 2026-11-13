namespace ZooSimulator.API.Models
{
    public record FeedResponse(DateTime FedAt, IReadOnlyList<SpeciesFeed> Feeds, ZooStatusResponse Status);
}
