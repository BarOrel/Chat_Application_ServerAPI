using Chat_Application_ServerAPI.Data.Models;
using Chat_Application_ServerAPI.Data.Repository.ChatRoomRepo;
using Chat_Application_ServerAPI.Data.Service;
using Chat_Application_ServerAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ToDoListPractice.Data.Services.JWT;
using ToDoListPractice.Data.Services;
using Chat_Application_ServerAPI.Data.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ChatDbContext>(options =>
    options.UseSqlServer("Data Source=DESKTOP-MGPQKAT;Initial Catalog=FinalChat;Integrated Security=True;Pooling=False;trustServerCertificate=true"));

builder.Services.AddControllers().AddJsonOptions(x => {
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
}) ;

builder.Services.AddScoped<IService, Service>();
builder.Services.AddScoped<IChatRoomRepository, ChatRoomRepository>();
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IJWTTokenService, JWTTokenService>();

builder.Services.AddIdentity<ApplicationUser,IdentityRole>(options => {
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
    .AddEntityFrameworkStores<ChatDbContext>();


builder.Services.AddAuthentication(cnf =>
{
    cnf.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    cnf.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options => {
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:Key"])),
        ValidIssuer = builder.Configuration["Token:Issuer"],
        ValidateIssuer = true,
        ValidateAudience = false,
    };
});

builder.Services.AddCors(options => options.AddPolicy(name: "clientsOrigins",
    policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();

    }));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();
app.UseWebSockets();
app.UseCors("clientsOrigins");
app.MapControllers();

app.Run();
