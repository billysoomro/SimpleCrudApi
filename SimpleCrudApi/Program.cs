using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using SimpleCrudApi.HostedServices;
using SimpleCrudApi.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

var awsConfig = new AmazonDynamoDBConfig
{
    RegionEndpoint = RegionEndpoint.EUWest2,    
};

builder.Services.AddHealthChecks().AddCheck<DynamoDBHealthCheck>("DynamoDBHealthCheck");

var dynamoDbClient = new AmazonDynamoDBClient(awsConfig);

builder.Services.AddSingleton<IAmazonDynamoDB>(dynamoDbClient);
builder.Services.AddSingleton<IDynamoDBContext, DynamoDBContext>();
builder.Services.AddHostedService<DynamoDBDataPopulator>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);

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
