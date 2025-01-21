using Otodom;
using Otodom.Models;
using Otodom.Repositories;
using Otodom.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<OtodomContext>();

builder.Services.AddScoped<IAgencjaRepository, AgencjaRepository>();
builder.Services.AddScoped<IAgencjaService, AgencjaService>();
builder.Services.AddScoped<INieruchomoscRepository, NieruchomoscRepository>();
builder.Services.AddScoped<INieruchomoscService, NieruchomoscService>();
builder.Services.AddScoped<IOgloszenieRepository, OgloszenieRepository>();
builder.Services.AddScoped<IOgloszenieService, OgloszenieService>();
builder.Services.AddScoped<IKlientRepository, KlientRepository>();
builder.Services.AddScoped<IKlientService, KlientService>();
builder.Services.AddScoped<IZdjecieRepository, ZdjecieRepository>();
builder.Services.AddScoped<IZdjecieService, ZdjecieService>();

builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        builder => builder.WithOrigins("https://localhost:7085")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

var app = builder.Build();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowLocalhost");

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.Run();