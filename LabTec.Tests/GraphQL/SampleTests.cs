using HotChocolate.Execution;
using LabTec.GraphQL.Services;
using LabTec.Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Snapshooter.Xunit;

namespace LabTec.Tests.GraphQL
{
    public class SampleTests
    {
        [Fact]
        public async Task SchemaChangeTest()
        {
            #region Arrange



            #endregion

            #region Act

            var schema = await new ServiceCollection()
                .AddGraphQLServer()
                .AddQueryType(d => d.Name("Query"))
                    .AddType<SampleQueries>()
                .AddMutationType(d => d.Name("Mutation"))
                    .AddType<SampleMutations>()
                .BuildSchemaAsync();

            #endregion

            #region Assert

            schema.ToString().MatchSnapshot();

            #endregion
        }

        [Fact]
        public async Task CreateSample_Snapshot()
        {
            #region Arrange

            var query = $@"
                mutation {{
                    createSample (
                        request: {{
                            name: ""a4dac2d0-eda7-4a47-aabc-809b201364aa""
                            owner: ""a4dac2d0-eda7-4a47-aabc-809b201364aa""
                            position: 1 }}
                        ) {{
                        sample {{
                            name
                        }}
                    }}
                }}";

            #endregion

            #region Act

            var result = await new ServiceCollection()
                .AddInfrastructureServices()
                .AddSingleton<SampleService>()
                .AddGraphQLServer()
                .AddQueryType(d => d.Name("Query"))
                    .AddType<SampleQueries>()
                .AddMutationType(d => d.Name("Mutation"))
                    .AddType<SampleMutations>()
                .ExecuteRequestAsync(query);

            #endregion

            #region Assert

            result.ToJson().MatchSnapshot();

            #endregion
        }
    }
}