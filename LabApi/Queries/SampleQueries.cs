using LabApi.Business.DTOs;
using LabApi.Business.Interfaces;

[ExtendObjectType("Query")]
public class SampleQueries
{
    public Task<IEnumerable<SampleDTO>> GetSamples([Service] ISampleService _sampleService) =>
         _sampleService.GetSamplesAsync();
}