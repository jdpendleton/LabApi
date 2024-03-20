using LabApi.Data.Interfaces;
using LabApi.Data.Models;

namespace LabApi.Data.Repositories
{

    public class SampleRepository : ISampleRepository
    {
        List<Sample> Samples = new List<Sample>();

        public Task<IEnumerable<Sample>> GetSamplesAsync()
        {
            return Task.FromResult(Samples.AsEnumerable());
        }

        public Task<Sample?> GetSampleByNameAsync(string name)
        {
            var sample = Samples.Where(x => x.Name == name).FirstOrDefault();
            return Task.FromResult(sample);
        }

        public Task AddSampleAsync(Sample sample)
        {
            var duplucate = Samples.Any(x => x.Name == sample.Name);
            if (duplucate)
            {
                throw new Exception("sample already exists with given name");
            }
            Samples.Add(sample);
            return Task.CompletedTask;
        }

        public async Task UpdateSampleAsync(Sample sample)
        {
            var update = await GetSampleByNameAsync(sample.Name)
                ?? throw new Exception("sample not found");
            update.Name = sample.Name;
            update.Owner = sample.Owner;
            update.Position = sample.Position;
            update.CheckedOut = sample.CheckedOut;
            update.Archived = sample.Archived;
            return;
        }

        public async Task DeleteSampleAsync(Sample sample)
        {
            var delete = await GetSampleByNameAsync(sample.Name)
                ?? throw new Exception("sample not found");
            Samples.Remove(delete);
            return;
        }
    }
}
