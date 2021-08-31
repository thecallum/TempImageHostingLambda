using Amazon;
using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;
using System.Threading.Tasks;

namespace TempImageHostingLambda
{
    class ParameterStore
    {
        private readonly AmazonSimpleSystemsManagementClient _client;

        public ParameterStore(RegionEndpoint region)
        {
            _client = new AmazonSimpleSystemsManagementClient(region);
        }

        public async Task<string> GetValueAsync(string parameter)
        {
            var response = await _client.GetParameterAsync(new GetParameterRequest
            {
                Name = parameter,
                WithDecryption = true
            });

            return response.Parameter.Value;
        }
    }
}
