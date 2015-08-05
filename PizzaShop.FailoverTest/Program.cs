using System;
using System.Collections.Generic;
using System.Fabric;
using System.Fabric.Testability;
using System.Fabric.Testability.Scenario;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PizzaShop.FailoverTest
{
    class Program
    {
        public static int Main(string[] args)
        {
            string clusterConnection = "localhost:19000";
            Uri serviceName = new Uri("fabric:/PizzaStore/PizzaStore.OrderService");

            Console.WriteLine("Starting Chaos Test Scenario...");
            try
            {
                RunFailoverTestScenarioAsync(clusterConnection, serviceName).Wait();
            }
            catch (AggregateException ae)
            {
                Console.WriteLine("Chaos Test Scenario did not complete: ");
                foreach (Exception ex in ae.InnerExceptions)
                {
                    if (ex is FabricException)
                    {
                        Console.WriteLine("HResult: {0} Message: {1}", ex.HResult, ex.Message);
                    }
                }
                return -1;
            }

            Console.WriteLine("Chaos Test Scenario completed.");
            return 0;
        }

        static async Task RunFailoverTestScenarioAsync(string clusterConnection, Uri serviceName)
        {
            TimeSpan maxServiceStabilizationTimeout = TimeSpan.FromSeconds(180);
            PartitionSelector randomPartitionSelector = PartitionSelector.RandomOf(serviceName);

            // Create FabricClient with connection & security information here.
            FabricClient fabricClient = new FabricClient(clusterConnection);

            // The Chaos Test Scenario should run at least 60 minutes or up until it fails.
            TimeSpan timeToRun = TimeSpan.FromMinutes(60);
            FailoverTestScenarioParameters scenarioParameters = new FailoverTestScenarioParameters(
              randomPartitionSelector,
              timeToRun,
              maxServiceStabilizationTimeout);

            // Other related parameters:
            // Pause between two iterations for a random duration bound by this value.
            // scenarioParameters.WaitTimeBetweenIterations = TimeSpan.FromSeconds(30);
            // Pause between concurrent actions for a random duration bound by this value.
            // scenarioParameters.WaitTimeBetweenFaults = TimeSpan.FromSeconds(10);

            // Create the scenario class and execute it asynchronously.
            FailoverTestScenario chaosScenario = new FailoverTestScenario(fabricClient, scenarioParameters);

            try
            {
                await chaosScenario.ExecuteAsync(CancellationToken.None);
            }
            catch (AggregateException ae)
            {
                throw ae.InnerException;
            }
        }

    }
}
