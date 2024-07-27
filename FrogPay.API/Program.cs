using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using FrogPay.Domain.Entities;
using FrogPay.Repository.Context;
using FrogPay.Repository.Interfaces;
using FrogPay.Repository.Repositories;
using FrogPay.Services.Interfaces;
using FrogPay.Services.Services;
using FrogPay.Services.Validator;
using FrogPay.Services.ViewModels.ContaBancaria;
using FrogPay.Services.ViewModels.Endereco;
using FrogPay.Services.ViewModels.Loja;
using FrogPay.Services.ViewModels.Pessoa;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Inserindo Autenticação
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = builder.Configuration["Jwt:Issuer"],
//        ValidAudience = builder.Configuration["Jwt:Audience"],
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
//    };
//});

builder.Services.AddControllers();

#region Jwt
var secretKey = builder.Configuration["Jwt:key"];

builder.Services.AddAuthentication(x =>
{

    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
#endregion

#region AutoMapper
var automapperConfig = new MapperConfiguration(cfg =>
{

    cfg.CreateMap<CreatePessoaViewModel, Pessoa>().ReverseMap();
    cfg.CreateMap<UpdatePessoaViewModel, Pessoa>().ReverseMap();

    cfg.CreateMap<CreateDadosBancariosViewModel, DadosBancarios>().ReverseMap();
    cfg.CreateMap<UpdateDadosBancariosViewModel, DadosBancarios>().ReverseMap();

    cfg.CreateMap<CreateEnderecoViewModel, Endereco>().ReverseMap();
    cfg.CreateMap<UpdateEnderecoViewModel, Endereco>().ReverseMap();

    cfg.CreateMap<CreateLojaViewModel, Loja>().ReverseMap();
    cfg.CreateMap<UpdateLojaViewModel, Loja>().ReverseMap();

});

builder.Services.AddSingleton(automapperConfig.CreateMapper());
#endregion



// DbContext
builder.Services.AddDbContext<ManagerContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ManagerAPISqlServer")));

#region InjecaoDependencia

// Repositories
//builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

builder.Services.AddScoped<IPessoaRepository, PessoaRepository>();
builder.Services.AddScoped<IDadosBancariosRepository, DadosBancariosRepository>();
builder.Services.AddScoped<IEnderecoRepository, EnderecoRepository>();
builder.Services.AddScoped<ILojaRepository, LojaRepository>();

// Services
builder.Services.AddScoped<IPessoaService, PessoaService>();
builder.Services.AddScoped<ILojaService, LojaService>();
builder.Services.AddScoped<IEnderecoService, EnderecoService>();
builder.Services.AddScoped<IDadosBancariosService, DadosBancariosService>();

#endregion

// FluentValidation
builder.Services.AddTransient<IValidator<Pessoa>, PessoaValidator>();
builder.Services.AddFluentValidationAutoValidation();

// Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "FrogPay API",
        Description = "API FrogPay Teste Anderson Barbosa"
    });

    // Definindo o esquema de segurança
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header usando o esquema Bearer. \r\n\r\n" +
                      "Digite 'Bearer' [espaço] e então seu token na entrada de texto abaixo.\r\n\r\n" +
                      "Exemplo: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "FrogPay API v1");
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();