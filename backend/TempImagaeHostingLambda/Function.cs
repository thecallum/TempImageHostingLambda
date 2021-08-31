using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using System;
using System.Collections.Generic;
using System.Net;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace TempImageHostingLambda
{
    public class Function
    {
        private readonly S3 _s3;

        public Function()
        {
            _s3 = new S3();
        }

        public APIGatewayProxyResponse MyFunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            context.Logger.LogLine("Get Request\n");

            if (request.QueryStringParameters != null && !request.QueryStringParameters.ContainsKey("fileName"))
            {
                return new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = "[fileName] parameter is required.",
                    Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
                };
            }

            try
            {
                string fileName = request.QueryStringParameters["fileName"];

                context.Logger.Log($"Generating Upload URL file for {fileName}.");

                string uploadUrl = _s3.GenerateUploadUrl(fileName);

                var response = new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Body = uploadUrl,
                    Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
                };

                return response;

            }
            catch (Exception)
            {
                context.Logger.LogLine("Oops, something went wrong!");

                return new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = "Oops, something went wrong.",
                    Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }

                };
            }
        }
    }
}
