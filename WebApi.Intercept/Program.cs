using WebApi.Intercept;

var builder = WebApplication.CreateBuilder(args);

var sharedFolder = Path.Combine(builder.Environment.ContentRootPath, "..", "Shared");
builder.Configuration.AddJsonFile(Path.Combine(sharedFolder, "SharedSettings.json"), optional: true);
builder.Configuration.AddJsonFile("SharedSettings.json", optional: true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();


app.Run();

