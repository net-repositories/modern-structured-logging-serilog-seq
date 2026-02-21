var builder = DistributedApplication.CreateBuilder(args);

//builder.AddProject<Projects.Demo>("demo");

//var raven = builder.AddContainer("ravendb", "ravendb/ravendb:4.2.101-ubuntu.18.04-x64")
//    .WithEnvironment("RAVEN_EULA_ACCEPTED", "true")
//    .WithEnvironment("RAVEN_Setup_Mode", "None")
//    .WithEnvironment("RAVEN_Security_UnsecuredAccessAllowed", "PrivateNetwork")
//    .WithEnvironment("RAVEN_ServerUrl", "http://0.0.0.0:8080")
//    .WithEndpoint(port: 8080, targetPort: 8080, name: "http");


var ravenServer = builder.AddRavenDB("ravenServer").WithImageTag("4.2.101-ubuntu.18.04-x64");

var ravendb = ravenServer.AddDatabase("LogsDB", "logs", true);

builder.AddProject<Projects.Demo>("demo").WaitFor(ravendb);

builder.Build().Run();
