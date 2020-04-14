using System.Collections.Generic;
using Amazon.SQS.Model;
using AWSXRay.SQS.Consumer.Extensions.Metadata;

namespace AWSXRay.SQS.Consumer.Extensions
{
    public static class ReceiveMessageRequestExtensions
    {
        public static ReceiveMessageRequest WithTraceHeader(this ReceiveMessageRequest request)
        {
            if (request.AttributeNames == null)
            {
                request.AttributeNames = new List<string>();
            }

            if (!request.AttributeNames.Contains(Constants.TraceHeader))
            {
                request.AttributeNames.Add(Constants.TraceHeader);
            }

            return request;
        }
    }
}