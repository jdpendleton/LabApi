using LabApi.Business.Interfaces;
using LabApi.Business.Services;
using LabApi.Data.Interfaces;
using LabApi.Data.Repositories;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services
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
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGraphQL();
        });
    }
}