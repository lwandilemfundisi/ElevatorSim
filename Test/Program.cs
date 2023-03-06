using ElevatorSim.Persistence;
using Microsoft.Extensions.DependencyInjection;
using ElevatorSim.Persistence.Extensions;
using ElevatorSim.Domain.Extensions;
using ElevatorSim.Domain.DomainModel.ElevatorControlModel.ValueObjects;
using ElevatorSim.Domain.DomainModel.ElevatorControlModel;
using ElevatorSim.Domain.DomainModel.ElevatorModel;
using Microservice.Framework.Common;
using Microservice.Framework.Domain.Aggregates;
using Microservice.Framework.Domain.Queries;
using Microservice.Framework.Domain.Commands;
using ElevatorSim.Domain.DomainModel.ElevatorControlModel.Entities;
using ElevatorSim.Domain.DomainModel.ElevatorControlModel.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
    .AddJsonFile($"appsettings.json")
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
    .Build();

var _serviceProvider = new ServiceCollection()
.AddLogging(b => {
    b.AddConsole();
    b.SetMinimumLevel(LogLevel.Information);
})
.AddSingleton(configuration)
.ConfigureElevatorSimDomain()
.ConfigureElevatorSimPersistence<ElevatorSimContext, ElevatorSimContextProvider>()
.ServiceCollection
.BuildServiceProvider();

var _aggregateStore = _serviceProvider.GetRequiredService<IAggregateStore>();
var _queryProcessor = _serviceProvider.GetService<IQueryProcessor>();
var _commandBus = _serviceProvider.GetService<ICommandBus>();

var testElevatorId = ElevatorId.New;
var testElevatorControlId = ElevatorControlId.New;
var initControl = new InitializeControl(new List<ManagedElevator> 
{
    new ManagedElevator 
    { 
        Id = ManagedElevatorId.New,
        ElevatorId = testElevatorId.Value
    } 
});

await InitializeElevatorAggregateAsync(testElevatorId, 6, 10);
await InitializeElevatorControlAggregateAsync(testElevatorControlId, initControl);

var result = await _commandBus
    .PublishAsync(new RequestElevatorCommand(
        testElevatorControlId,
        new RequestElevetor(1, 2, 3)), CancellationToken.None);


// Keep the console window open
Console.WriteLine("Press any key to continue...");
Console.ReadLine();


Task InitializeElevatorAggregateAsync(
            ElevatorId id,
            uint floor,
            uint weightLimit,
            uint currentWeight = 0)
{
    return UpdateAsync<Elevator, ElevatorId>(id, a
        => a.InitializeElevator(floor, weightLimit, currentWeight));
}

Task InitializeElevatorControlAggregateAsync(ElevatorControlId id, InitializeControl initializeControl)
{
    return UpdateAsync<ElevatorControl, ElevatorControlId>(id, a => a.InitializeElevatorControl(initializeControl));
}

async Task UpdateAsync<TAggregate, TIdentity>(TIdentity id, Action<TAggregate> action)
    where TAggregate : class, IAggregateRoot<TIdentity>
    where TIdentity : IIdentity
{
    await _aggregateStore.UpdateAsync<TAggregate, TIdentity>(
        id,
        SourceId.New,
        (a, c) =>
        {
            action(a);
            return Task.FromResult(0);
        },
        CancellationToken.None);
}
