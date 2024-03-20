using LabApi.Business.Interfaces;
using LabApi.Business.Services;
using LabApi.Data.Interfaces;
using LabApi.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddSingleton<ISampleRepository, SampleRepository>()
    .AddSingleton<ISampleService, SampleService>()
    .AddGraphQLServer()
    .AddQueryType(d => d.Name("Query"))
        .AddType<SampleQueries>()
    .AddMutationType(d => d.Name("Mutation"))
        .AddType<SampleMutations>()
    .AddErrorFilter<SampleNotFoundExceptionFilter>()
    .AddErrorFilter<DuplicateSampleExceptionFilter>()
    .AddErrorFilter<SampleCheckedOutExceptionFilter>()
    .AddErrorFilter<SampleArchivedExceptionFilter>()
    .AddErrorFilter<RedundantSampleRequestExceptionFilter>();
var app = builder.Build();

app.MapGraphQL();

app.Run();
