using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(setup =>
{
    setup.SwaggerDoc("v1", new OpenApiInfo { Title = "Configuration", Version = "v1", Description = "# Formatos\n\r Todos os recursos s�o acess�veis em formato JSON. O tipo de formato\r\n obrigat�rio pode ser passado no cabe�alho da solicita��o em Content-Type, por padr�o o formato application/json\r\n ser� usado em application/type.\r\n# Autentica��o \r\n Todos os recursos da API precisam ser autenticados por um token de portador JWT (JWT bearer).\r\n O token do portador (JWT bearer) precisa ser passado no cabe�alho da solicita��o ao fazer qualquer\r\n solicita��o que pode exigir autentica��o. O token de acesso JWT � v�lido apenas por um per�odo de tempo finito. Usar um JWT expirado far� com que as opera��es falhem.  \r\n # Erros\n\r A API usa c�digos de status HTTP para indicar o sucesso ou falha de uma chamada de API. Em geral, os c�digos de status no intervalo 2xx significam sucesso, o intervalo 4xx significa que houve um erro nas informa��es fornecidas e aqueles no intervalo 5xx indicam erros do lado do servidor." });
});

builder.Services.AddSwaggerGen(setup =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Coloque APENAS seu token JWT Bearer na caixa de texto abaixo!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
});

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.Authority = "https://localhost:5001";

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
