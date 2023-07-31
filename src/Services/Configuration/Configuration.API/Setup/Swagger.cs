using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace Configuration.API.Setup
{
    public static class Swagger
    {
        public static void RegisterSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1", new OpenApiInfo { Title = "Configuration", Version = "v1", Description = "# Formatos\n\r Todos os recursos são acessíveis em formato JSON. O tipo de formato\r\n obrigatório pode ser passado no cabeçalho da solicitação em Content-Type, por padrão o formato application/json\r\n será usado em application/type.\r\n# Autenticação \r\n Todos os recursos da API precisam ser autenticados por um token de portador JWT (JWT bearer).\r\n O token do portador (JWT bearer) precisa ser passado no cabeçalho da solicitação ao fazer qualquer\r\n solicitação que pode exigir autenticação. O token de acesso JWT é válido apenas por um período de tempo finito. Usar um JWT expirado fará com que as operações falhem.  \r\n # Erros\n\r A API usa códigos de status HTTP para indicar o sucesso ou falha de uma chamada de API. Em geral, os códigos de status no intervalo 2xx significam sucesso, o intervalo 4xx significa que houve um erro nas informações fornecidas e aqueles no intervalo 5xx indicam erros do lado do servidor." });
            });

            services.AddSwaggerGen(setup =>
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
        }
    }
}
