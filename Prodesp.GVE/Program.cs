using Prodesp.Infra.EF;

var builder = WebApplication.CreateBuilder(args);

var sharedFolder = Path.Combine(builder.Environment.ContentRootPath, "..", "Shared");
builder.Configuration.AddJsonFile(Path.Combine(sharedFolder, "SharedSettings.json"), optional: true);
builder.Configuration.AddJsonFile("SharedSettings.json", optional: true);

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDeveloperExceptionPage(); // por enquanto, tem que tirar depois isso aí


app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();
