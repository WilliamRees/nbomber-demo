using NBomber.Configuration;
using NBomber.Contracts;
using NBomber.CSharp;
using System;
using System.Net.Http;

namespace NBomberDemo.LoadTestingConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44372"),
                Timeout = TimeSpan.FromMilliseconds(1750)
            };

            var step = Step.Create("fetch_users", async context =>
            {
                try
                {
                    var response = await httpClient.GetAsync("api/users");

                    return response.IsSuccessStatusCode
                        ? Response.Ok()
                        : Response.Fail();

                } catch (Exception ex)
                {
                    return Response.Fail();
                }               
            });

            var scenario = ScenarioBuilder.CreateScenario("fetch_users_scenario", step)
                .WithWarmUpDuration(TimeSpan.FromSeconds(60))
                .WithLoadSimulations(
                    Simulation.InjectPerSec(rate: 1, during: TimeSpan.FromMinutes(10))                
            );


            NBomberRunner
                .RegisterScenarios(scenario)
                .WithReportFileName("fetch_users_report")                
                .WithReportFolder("fetch_users_reports")
                .WithReportFormats(ReportFormat.Txt, ReportFormat.Csv, ReportFormat.Html, ReportFormat.Md)
                .Run();
        }
    }
}
