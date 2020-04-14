using Amazon.SQS.Model;
using Amazon.XRay.Recorder.Core.Internal.Entities;
using AWSXRay.SQS.Consumer.Extensions.Metadata;

namespace AWSXRay.SQS.Consumer.Extensions
{
    public static class MessageExtensions
    {
        public static TraceHeader GetTraceHeader(this Message message)
        {
            return 
                message.Attributes.TryGetValue(Constants.TraceHeader, out var value) 
                    ? TraceHeader.FromString(value) 
                    : null;
        }
    }
}