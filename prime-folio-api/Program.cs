using DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy",
        policy =>
        {
            policy.WithOrigins(
                "http://localhost:4200",
                "https://dev.dnnuymfxr7fs5.amplifyapp.com/");
            policy.AllowAnyMethod();
            policy.AllowAnyHeader();
        });
});
builder.Services.AddControllers();
builder.Services.RegisterServices(builder.Environment, builder.Configuration);

// Add AWS Lambda support. When application is run in Lambda Kestrel is swapped out as the web server with Amazon.Lambda.AspNetCoreServer. This
// package will act as the webserver translating request and responses between the Lambda event source and ASP.NET Core.
builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
Console.WriteLine($"Environment id: {builder.Configuration.GetValue<string>("env_id")}");
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAllPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
