using LabApi.Business.Exceptions;

class SampleNotFoundExceptionFilter : IErrorFilter
{
    public IError OnError(IError error)
    {
        if (error.Exception is SampleNotFoundException ex)
            return error.WithMessage($"Sample with name {ex.Name} not found")
                .WithCode("SAMPLE_NOT_FOUND");

        return error;
    }
}

class DuplicateSampleExceptionFilter : IErrorFilter
{
    public IError OnError(IError error)
    {
        if (error.Exception is DuplicateSampleException ex)
            return error.WithMessage($"A sample with name {ex.Name} already exists.")
                .WithCode("DUPLICATE_SAMPLE");

        return error;
    }
}

class SampleCheckedOutExceptionFilter : IErrorFilter
{
    public IError OnError(IError error)
    {
        if (error.Exception is SampleNotFoundException ex)
            return error.WithMessage($"Sample with name {ex.Name} is checked out. Mutations cannot be performed.")
                .WithCode("SAMPLE_CHECKED_OUT");

        return error;
    }
}

class SampleArchivedExceptionFilter : IErrorFilter
{
    public IError OnError(IError error)
    {
        if (error.Exception is SampleArchivedException ex)
            return error.WithMessage($"Sample with name {ex.Name} is archived. Mutations cannot be performed.")
                .WithCode("SAMPLE_ARCHIVED");

        return error;
    }
}

class RedundantSampleRequestExceptionFilter : IErrorFilter
{
    public IError OnError(IError error)
    {
        if (error.Exception is RedundantSampleRequestException ex)
        {
            if (ex.CheckedOut != null)
                return error.WithMessage($"Sample with name {ex.Name} is already checked out.")
                    .WithCode("REDUNDANT_CHECKOUT");

            if (ex.Position != null)
                return error.WithMessage($"Sample with name {ex.Name} is already at position {ex.Position}.")
                    .WithCode("REDUNDANT_MOVE");

            if (ex.Archived != null)
                return error.WithMessage($"Sample with name {ex.Name} is already archived.")
                    .WithCode("REDUNDANT_ARCHIVE");
        }

        return error;
    }
}