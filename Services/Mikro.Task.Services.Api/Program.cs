using Mikro.Task.Services.Db;
using Microsoft.EntityFrameworkCore;
using MikroTask.Services.Api.HostedServices;
using Microsoft.Extensions.DependencyInjection;
using Mikro.Task.Services.Application.Services;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;
using Mikro.Task.Services.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MikroTask.Services.Api.Helpers;
using Mikro.Task.Services.Application.Services.Interfaces;
using Microsoft.Extensions.Options;
using Mikro.Task.Services.Application.Models;
using MassTransit;
using MikroTask.Services.Application.Consumers;
using Mikro.Task.Services.Application.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<SendEmailCommandConsumer>();
    // Default Port : 5672
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMQUrl"], "/", host =>
        {
            host.Username("guest");
            host.Password("guest");
        });

        cfg.ReceiveEndpoint("sendemailqueue", e =>
        {
            e.ConfigureConsumer<SendEmailCommandConsumer>(context);
        });
    });
});


builder.Services.AddHttpClient();

builder.Services.AddDbContext<MovieDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), configure =>
    {
        configure.MigrationsAssembly("Mikro.Task.Services.Db");
    });
});

builder.Services.AddDbContext<IdDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"), configure =>
    {
        configure.MigrationsAssembly("Mikro.Task.Services.Db");
    });
});

builder.Services.AddSingleton<IHostedService, TheMovieDbService>();

builder.Services.AddScoped<IMovieService,MovieService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddCors();
builder.Services.AddIdentity<UserModel, IdentityRole>(s =>
{
    s.Password.RequiredLength = 8;
    s.Password.RequireLowercase = false;
    s.Password.RequireUppercase = false;
    s.Password.RequireDigit = false;
    s.Password.RequireNonAlphanumeric = false;
    s.User.RequireUniqueEmail = true;
})
.AddRoleManager<RoleManager<IdentityRole>>()
.AddEntityFrameworkStores<IdDbContext>()
.AddDefaultTokenProviders();
builder.Services.AddHttpContextAccessor();

// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
// Adding Jwt Bearer
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});

builder.Services.Configure<RedisSettings>(builder.Configuration.GetSection("RedisSettings"));
builder.Services.AddSingleton<RedisService>(sp =>
{
    var redisSettings = sp.GetRequiredService<IOptions<RedisSettings>>().Value;
    var redis = new RedisService(redisSettings.Host, redisSettings.Port);
    redis.Connect();
    return redis;
});



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.Run();
