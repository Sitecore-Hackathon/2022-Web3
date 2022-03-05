using Core;
using Web3.Operator.Api;
using Web3.Operator.Engines.Core;
using Web3.Operator.Engines.DockerEngine;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.AddJsonFile("appsettings.json");
builder.Configuration.AddEnvironmentVariables();
var cfg1 = builder.Configuration.GetRequiredSection("Operator").Get<OperatorConfiguration>();
builder.Services.AddTransient(_ => cfg1);

builder.Services.AddDockerEngineOperator();
var cfg2 = builder.Configuration.GetRequiredSection("DockerEngine").Get<DockerEngineConfiguration>();
builder.Services.AddTransient(_ => cfg2);

// TODO: Dynamically switch engines
builder.Services.AddTransient(typeof(IOperatorEngine), typeof(Web3.Operator.Engines.DockerEngine.DockerEngineOperator));

var app = builder.Build();
app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI();

app.UseOperatorRoutes();

app.Run();
