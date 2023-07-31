using Identity;
using Identity.Context;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(setup =>
{
    setup.SwaggerDoc("v1", new OpenApiInfo { Title = "Identity", Version = "v1", Description = "Aplicação de identidade baseado no padrão OpenID Connect. A documentação encontra-se disponível em /.well-known/openid-configuration, onde você pode encontrar metadados e links para todos os endpoints." });
});

builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDataContext>()
                .AddDefaultTokenProviders();

builder.Services.AddDbContext<IdentityDataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;
    // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
    options.EmitStaticAudienceClaim = true;
}).AddInMemoryIdentityResources(Config.IdentityResources)
  .AddInMemoryApiScopes(Config.ApiScopes)
  .AddInMemoryClients(Config.Clients)
  .AddAspNetIdentity<User>();
//.AddDeveloperSigningCredential() // not recommended for production - you need to store your key material somewhere secure

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("v1/swagger.json", "Identity");
    });
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseIdentityServer();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
