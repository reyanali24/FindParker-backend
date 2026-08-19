using ClassLibraryDAL.Interfaces;
using ClassLibraryDAL.Interfaces.ClassLibraryDAL.Interfaces;
using ClassLibraryDAL.Services;
using ClassLibraryModels;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter());
    });
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddScoped<IUsersInterface, UsersService>();
builder.Services.AddScoped<IUserProfilesInterface, UserProfilesService>();
builder.Services.AddScoped<IVehiclesInterface, VehiclesService>();
builder.Services.AddScoped<IQrCodesInterface, QrCodesService>();
builder.Services.AddScoped<IEmergencyContactsInterface,EmergencyContactsService>();
builder.Services.AddScoped<IPrivacySettingsInterface,PrivacySettingsService>();
builder.Services.AddScoped<IMaskedCallsInterface,MaskedCallsService>();
builder.Services.AddScoped<IQrScansInterface,QrScansService>();
builder.Services.AddScoped<IAlertsInterface,AlertsService>();
builder.Services.AddScoped<ILoginHistoryInterface,LoginHistoryService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("FlutterPolicy",
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();               // <-- serves swagger.json
   app.UseSwaggerUI();
    
}

//app.UseHttpsRedirection();
app.UseCors("FlutterPolicy");
app.UseAuthorization();


app.MapControllers();

app.Run();
