using LabTec.GraphQL.Services;
using LabTec.GraphQL.DTOs;

[ExtendObjectType("Mutation")]
public class SampleMutations
{
    public async Task<CreateSampleResponse> CreateSample(CreateSampleRequest request, [Service] SampleService _sampleService)
    {
        var result = await _sampleService.CreateAsync(request.name, request.owner, request.position);
        if (!result.Success)
        {
            throw new GraphQLException(new ErrorBuilder()
                .SetMessage($"Error creating sample: {result.Message}")
                .SetCode("SAMPLE_CREATION_FAILED")
                .Build());
        }

        var sampleDto = new SampleDTO(result.Value);

        return new CreateSampleResponse(sampleDto);
    }

    public async Task<CheckoutSampleResponse> CheckoutSample(CheckoutSampleRequest request, [Service] SampleService _sampleService)
    {
        var result = await _sampleService.CheckOutAsync(request.name);
        if (!result.Success)
        {
            throw new GraphQLException(new ErrorBuilder()
                .SetMessage($"Error checking out sample: {result.Message}")
                .SetCode("SAMPLE_CHECKOUT_FAILED")
                .Build());
        }

        var sampleDto = new SampleDTO(result.Value);

        return new CheckoutSampleResponse(sampleDto);
    }

    public async Task<CheckinSampleResponse> CheckinSample(CheckinSampleRequest request, [Service] SampleService _sampleService)
    {
        var result = await _sampleService.CheckInAsync(request.name);
        if (!result.Success)
        {
            throw new GraphQLException(new ErrorBuilder()
                .SetMessage($"Error checking in sample: {result.Message}")
                .SetCode("SAMPLE_CHECKIN_FAILED")
                .Build());
        }

        var sampleDto = new SampleDTO(result.Value);

        return new CheckinSampleResponse(sampleDto);
    }

    public async Task<MoveSampleResponse> MoveSample(MoveSampleRequest request, [Service] SampleService _sampleService)
    {
        var result = await _sampleService.MoveAsync(request.name, request.position);
        if (!result.Success)
        {
            throw new GraphQLException(new ErrorBuilder()
                .SetMessage($"Error moving sample: {result.Message}")
                .SetCode("SAMPLE_MOVE_FAILED")
                .Build());
        }

        var sampleDto = new SampleDTO(result.Value);

        return new MoveSampleResponse(sampleDto);
    }

    public async Task<ArchiveSampleResponse> ArchiveSample(ArchiveSampleRequest request, [Service] SampleService _sampleService)
    {
        var result = await _sampleService.ArchiveAsync(request.name);
        if (!result.Success)
        {
            throw new GraphQLException(new ErrorBuilder()
                .SetMessage($"Error archiving sample: {result.Message}")
                .SetCode("SAMPLE_ARCHIVE_FAILED")
                .Build());
        }

        var sampleDto = new SampleDTO(result.Value);

        return new ArchiveSampleResponse(sampleDto);
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