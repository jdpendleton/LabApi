using LabApi.Business.DTOs;
using LabApi.Business.Interfaces;

[ExtendObjectType("Mutation")]
public class SampleMutations
{
    public async Task<CreateSampleResponse> CreateSample(CreateSampleRequest request, [Service] ISampleService _sampleService)
    {
        try
        {
            var sample = await _sampleService.CreateSampleAsync(request.name, request.owner, request.position);
            return new CreateSampleResponse(sample);
        }
        catch (Exception ex)
        {
            throw new GraphQLException(new ErrorBuilder()
                .SetMessage($"Error creating sample: {ex.Message}")
                .SetCode("SAMPLE_CREATION_FAILED")
                .Build());
        }
    }

    public async Task<CheckoutSampleResponse> CheckoutSample(CheckoutSampleRequest request, [Service] ISampleService _sampleService)
    {
        try
        {
            var sample = await _sampleService.CheckoutSampleAsync(request.name);
            return new CheckoutSampleResponse(sample);
        }
        catch (Exception ex)
        {
            throw new GraphQLException(new ErrorBuilder()
                .SetMessage($"Error checking out sample: {ex.Message}")
                .SetCode("SAMPLE_CHECKOUT_FAILED")
                .Build());
        }
    }

    public async Task<CheckinSampleResponse> CheckinSample(CheckinSampleRequest request, [Service] ISampleService _sampleService)
    {
        try
        {
            var sample = await _sampleService.CheckinSampleAsync(request.name);
            return new CheckinSampleResponse(sample);
        }
        catch (Exception ex)
        {
            throw new GraphQLException(new ErrorBuilder()
                .SetMessage($"Error checking in sample: {ex.Message}")
                .SetCode("SAMPLE_CHECKIN_FAILED")
                .Build());
        }
    }

    public async Task<MoveSampleResponse> MoveSample(MoveSampleRequest request, [Service] ISampleService _sampleService)
    {
        try
        {
            var sample = await _sampleService.MoveSampleAsync(request.name, request.position);
            return new MoveSampleResponse(sample);
        }
        catch (Exception ex)
        {
            throw new GraphQLException(new ErrorBuilder()
                .SetMessage($"Error moving sample: {ex.Message}")
                .SetCode("SAMPLE_MOVE_FAILED")
                .Build());
        }
    }

    public async Task<ArchiveSampleResponse> ArchiveSample(ArchiveSampleRequest request, [Service] ISampleService _sampleService)
    {
        try
        {
            var sample = await _sampleService.ArchiveSampleAsync(request.name);
            return new ArchiveSampleResponse(sample);
        }
        catch (Exception ex)
        {
            throw new GraphQLException(new ErrorBuilder()
                .SetMessage($"Error archiving sample: {ex.Message}")
                .SetCode("SAMPLE_ARCHIVE_FAILED")
                .Build());
        }
    }
}

public record CreateSampleRequest(string name, Guid owner, bool? checkedOut, int position, bool? archived);
public record CreateSampleResponse(SampleDTO? sample, string? error = null);
public record CheckoutSampleRequest(string name);
public record CheckoutSampleResponse(SampleDTO? sample, string? error = null);
public record CheckinSampleRequest(string name);
public record CheckinSampleResponse(SampleDTO? sample, string? error = null);
public record MoveSampleRequest(string name, int position);
public record MoveSampleResponse(SampleDTO? sample, string? error = null);
public record ArchiveSampleRequest(string name);
public record ArchiveSampleResponse(SampleDTO? sample, string? error = null);
