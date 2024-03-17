var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddSingleton<Freezer>()
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>();
var app = builder.Build();

app.MapGraphQL();

app.Run();
