using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using SimpleCrudApi.HostedServices;
using SimpleFrontEnd.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

var accessKey = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY");
var secretAccessKey = Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY");
var credentials = new BasicAWSCredentials(accessKey, secretAccessKey);

var awsConfig = new AmazonDynamoDBConfig
{
    RegionEndpoint = RegionEndpoint.EUWest2,    
};

builder.Services.AddHealthChecks().AddCheck<DynamoDBHealthCheck>("DynamoDBHealthCheck");

var dynamoDbClient = new AmazonDynamoDBClient(credentials, awsConfig);

builder.Services.AddSingleton<IAmazonDynamoDB>(dynamoDbClient);
builder.Services.AddSingleton<IDynamoDBContext, DynamoDBContext>();
builder.Services.AddHostedService<DynamoDBDataPopulator>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health");

app.Run();
