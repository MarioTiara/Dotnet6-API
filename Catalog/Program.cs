using Catalog.Config;
using Catalog.Repositories;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);
BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
BsonSerializer.RegisterSerializer( new DateTimeOffsetSerializer(BsonType.String));
// Add services to the container.
var provider=builder.Services.BuildServiceProvider();
var configuration=provider.GetRequiredService<IConfiguration>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IItemRepository, MongoDbRepository>();

builder.Services.Configure<MongoDbConfig>(
    builder.Configuration.GetSection("MongoDbConfig")
);

var mongoDbConfig=configuration.GetSection(nameof(MongoDbConfig)).Get<MongoDbConfig>();
builder.Services.AddHealthChecks()
    .AddMongoDb(
        mongoDbConfig.ConnectionString,
        name:"mongodb",
        timeout:TimeSpan.FromMilliseconds(3),
        tags:new []{"ready"}
    );

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

app.MapHealthChecks("/health/ready", new HealthCheckOptions{
    Predicate= (check)=>check.Tags.Contains("ready")
});
app.MapHealthChecks("/health/live", new HealthCheckOptions{
    Predicate= (_)=>false
});
app.Run();


