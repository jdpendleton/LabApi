public class Query
{
    public Task<List<Sample>> GetSamples([Service] Freezer freezer) =>
         freezer.GetSamplesAsync();
}