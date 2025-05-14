using Grpc.Core;
using SmartHome.Protos.HeatPump;

namespace SmartHome.LocalServer.Services
{
    public class HeatPumpService : HeatPump.HeatPumpBase
    {
        public override Task<GetParameterResponse> GetParameter(GetParameterRequest request, ServerCallContext context)
        {
            return Task.FromResult(new GetParameterResponse()
            {
                Result = Result.Ok,
                Value = "12345.6567",
                ValueType = "float"

            });
        }
    }
}
