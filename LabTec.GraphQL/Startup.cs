using LabTec.GraphQL.Services;
using LabTec.Infrastructure.Extensions;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddInfrastructureServices()
            .AddSingleton<SampleService>()
            .AddGraphQLServer()
            .AddQueryType(d => d.Name("Query"))
                .AddType<SampleQueries>()
            .AddMutationType(d => d.Name("Mutation"))
                .AddType<SampleMutations>();
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