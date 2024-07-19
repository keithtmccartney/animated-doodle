using animated_doodle.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Services.AddCors(builder.Services);

Services.AddSql(builder.Services, builder.Configuration);

Services.AddServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors("Any");

app.UseAuthorization();

app.MapControllers();

app.Run();
