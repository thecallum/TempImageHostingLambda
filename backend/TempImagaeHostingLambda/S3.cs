using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TempImageHostingLambda
{
    public class S3
    {
        private readonly AmazonS3Client _client;
        private const string _bucketName = "test-image-upload-test";
        private const string _baseUrl = "https://test-image-upload-test.s3.eu-west-1.amazonaws.com/";
        private readonly RegionEndpoint _bucketRegion = RegionEndpoint.EUWest1;

        private readonly ParameterStore _parameterStore;

        public S3()
        {
            _parameterStore = new ParameterStore(_bucketRegion);


            var access_key = _parameterStore.GetValueAsync("/TempImageHosting/dev/aws_access_key").Result;
            var secret = _parameterStore.GetValueAsync("/TempImageHosting/dev/aws_secret_access_key").Result;

            _client = new AmazonS3Client(access_key, secret, _bucketRegion);
        }

        public string GenerateUploadUrl(string fileName)
        {
            string key = GenerateObjectKey(fileName);

            var uploadedUrl = _client.GetPreSignedURL(new GetPreSignedUrlRequest
            {
                BucketName = _bucketName,
                Key = key,
                Expires = DateTime.Now.AddSeconds(60),
                Verb = HttpVerb.PUT,
                //ContentType = "image/jpeg",

            });

            return uploadedUrl;
        }

        private static string GenerateObjectKey(string fileName)
        {
            string randomId = Guid.NewGuid().ToString();
            string date = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");

            return $"{date}-{randomId}.{fileName}";
        }
    }

}