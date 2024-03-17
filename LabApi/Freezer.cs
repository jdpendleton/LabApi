public class Freezer
{
    List<Sample> Samples = new List<Sample>();

    public Task<List<Sample>> GetSamplesAsync()
    {
        return Task.FromResult(Samples);
    }

    public Task<Sample?> GetSampleByNameAsync(string name)
    {
        var sample = Samples.Where(x => x.Name == name).FirstOrDefault();
        return Task.FromResult(sample);
    }

    public Task<Sample> AddSampleAsync(Sample sample)
    {
        var duplucate = Samples.Any(x => x.Name == sample.Name);
        if (duplucate)
        {
            throw new Exception("sample already exists with given name");
        }
        Samples.Add(sample);
        return Task.FromResult(sample);
    }

    public async Task<Sample> UpdateSampleAsync(Sample sample)
    {
        var update = await GetSampleByNameAsync(sample.Name)
            ?? throw new Exception("sample not found");
        update.Name = sample.Name;
        update.Owner = sample.Owner;
        update.Position = sample.Position;
        update.CheckedOut = sample.CheckedOut;
        update.Archived = sample.Archived;
        return await Task.FromResult(update);
    }

    public async Task DeleteSampleAsync(Sample sample)
    {
        var delete = await GetSampleByNameAsync(sample.Name)
            ?? throw new Exception("sample not found");
        Samples.Remove(delete);
        return;
    }
}