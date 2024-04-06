using LabTec.GraphQL.Services;
using LabTec.GraphQL.DTOs;

[ExtendObjectType("Query")]
public class SampleQueries
{
    public async Task<IEnumerable<SampleDTO>> GetSamplesAsync([Service] SampleService _sampleService)
    {
        var result = await _sampleService.GetAllAsync();
        if (!result.Success)
        {
            throw new GraphQLException(new ErrorBuilder()
                .SetMessage($"Error fetching samples: {result.Message}")
                .SetCode("SAMPLES_GET_FAILED")
                .Build());
        }

        var sampleDtos = result.Value.Select(s => new SampleDTO(s));

        return sampleDtos;
    }
}