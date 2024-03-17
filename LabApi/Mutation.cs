public class Mutation
{
    public async Task<SamplePayload> AddSample(SampleInput input, [Service] Freezer freezer)
    {
        var duplicate = await freezer.GetSampleByNameAsync(input.name);
        if (duplicate != null)
        {
            throw new GraphQLException("sample already exists with given name");
        }
        var sample = new Sample()
        {
            Name = input.name,
            Owner = input.owner,
            CheckedOut = input.checkedOut ?? false,
            Position = input.position,
            Archived = input.archived ?? false
        };
        await freezer.AddSampleAsync(sample);
        return new SamplePayload(sample);
    }

    public async Task<CheckoutSamplePayload> CheckoutSample(CheckoutSampleInput input, [Service] Freezer freezer)
    {
        var sample = await freezer.GetSampleByNameAsync(input.name)
            ?? throw new GraphQLException("sample not found");
        if (sample.CheckedOut)
        {
            throw new GraphQLException("sample already checked out");
        }
        if (sample.Archived)
        {
            throw new GraphQLException("sample is archived");
        }
        sample.CheckedOut = true;
        await freezer.UpdateSampleAsync(sample);
        return new CheckoutSamplePayload(sample);
    }

    public async Task<CheckinSamplePayload> CheckinSample(CheckinSampleInput input, [Service] Freezer freezer)
    {
        var sample = await freezer.GetSampleByNameAsync(input.name)
            ?? throw new GraphQLException("sample not found");
        if (!sample.CheckedOut)
        {
            throw new GraphQLException("sample already checked in");
        }
        if (sample.Archived)
        {
            throw new GraphQLException("sample is archived");
        }
        sample.CheckedOut = false;
        await freezer.UpdateSampleAsync(sample);
        return new CheckinSamplePayload(sample);
    }

    public async Task<MoveSamplePayload> MoveSample(MoveSampleInput input, [Service] Freezer freezer)
    {
        var sample = await freezer.GetSampleByNameAsync(input.name)
            ?? throw new GraphQLException("sample not found");
        if (sample.CheckedOut)
        {
            throw new GraphQLException("sample is checked out");
        }
        if (sample.Archived)
        {
            throw new GraphQLException("sample is archived");
        }
        if (sample.Position == input.position)
        {
            throw new GraphQLException("sample already in position");
        }
        sample.Position = input.position;
        await freezer.UpdateSampleAsync(sample);
        return new MoveSamplePayload(sample);
    }

    public async Task<ArchiveSamplePayload> ArchiveSample(ArchiveSampleInput input, [Service] Freezer freezer)
    {
        var sample = await freezer.GetSampleByNameAsync(input.name)
            ?? throw new GraphQLException("sample not found");
        if (sample.CheckedOut)
        {
            throw new GraphQLException("sample is checked out");
        }
        if (sample.Archived)
        {
            throw new GraphQLException("sample already archived");
        }
        sample.Archived = true;
        await freezer.UpdateSampleAsync(sample);
        return new ArchiveSamplePayload(sample);
    }
}

public record SamplePayload(Sample? sample, string? error = null);
public record SampleInput(string name, Guid owner, bool? checkedOut, int position, bool? archived);
public record CheckoutSamplePayload(Sample? sample, string? error = null);
public record CheckoutSampleInput(string name);
public record CheckinSamplePayload(Sample? sample, string? error = null);
public record CheckinSampleInput(string name);
public record MoveSamplePayload(Sample? sample, string? error = null);
public record MoveSampleInput(string name, int position);
public record ArchiveSamplePayload(Sample? sample, string? error = null);
public record ArchiveSampleInput(string name);
