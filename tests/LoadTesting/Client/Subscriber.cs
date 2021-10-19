using LoadTesting.Extensions;
using LoadTesting.Server;
using NBomber.Contracts;
using Serilog;
using System;
using System.Threading.Tasks;

namespace LoadTesting.Client
{
    public class Subscriber : TokenClientBase
    {
        public Subscriber(HttpClientPool pool, string secretKey) : base(pool, secretKey, "Subscriber")
        {
        }

        public OrderResponse Order { get; private set; }

        protected override async Task<Response> OnExecuteAsync(System.Threading.CancellationToken cancellationToken)
        {
            try
            {
                var order = await ClientPool.UseClientAsync(client => client.SubscribeOrderAsync(SecretKey, Token, Order, cancellationToken));
                if (order != Order)
                {
                    Order = order;
                    Log.Logger.Information($"Subscriber:{SecretKey}:OrderChanged:{Order.Text}");
                }
                return Response.Ok();
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                return Response.Fail($"Subscribe order failed. {e.Message}");
            }
        }
    }
}
