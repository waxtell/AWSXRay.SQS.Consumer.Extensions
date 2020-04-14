using Amazon.SQS.Model;
using Amazon.XRay.Recorder.Core;
using Amazon.XRay.Recorder.Core.Internal.Entities;
using Amazon.XRay.Recorder.Core.Sampling;

namespace AWSXRay.SQS.Consumer.Extensions
{
    public static class AWSXRayRecorderExtensions
    {
        public static void ContinueFrom(this AWSXRayRecorder recorder, string serviceName, Message message)
        {
            var traceHeader = message?.GetTraceHeader() ?? new TraceHeader();

            string ruleName = null;
            
            if (traceHeader.Sampled == SampleDecision.Unknown || traceHeader.Sampled == SampleDecision.Requested)
            {
                var samplingInput = new SamplingInput(serviceName);
                var sampleResponse = AWSXRayRecorder.Instance.SamplingStrategy.ShouldTrace(samplingInput);
                traceHeader.Sampled = sampleResponse.SampleDecision;
                ruleName = sampleResponse.RuleName;
            }

            var samplingResponse = new SamplingResponse(ruleName, traceHeader.Sampled); 

            recorder
                .BeginSegment
                (
                    serviceName,
                    traceHeader.RootTraceId,
                    traceHeader.ParentId,
                    samplingResponse
                );
        }
    }
}