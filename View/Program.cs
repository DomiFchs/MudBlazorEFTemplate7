using System.Globalization;
using Model.Configuration;
using MudBlazor;
using MudBlazor.Services;
using View.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddMudServices();

CultureInfo.CurrentCulture = new CultureInfo("de-DE");
CultureInfo.CurrentUICulture = new CultureInfo("de-DE");

builder.Services.AddMudServices(config => {
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 10000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});


builder.Services.AddDefaultDbContextOfType<CustomDbContext>(
    builder.Configuration.GetConnectionString("DefaultConnection"));

//AddTransients
//Example for Single Key Repository
//builder.Services.AddTransient(IRepository<Type>, Repository<Type>);
//builder.Services.AddSingleKeyRepository<Type>();

//Example for Composite Key Repository with int keys
//builder.Services.AddTransient(IDefaultCompositeKeyRepository<Type>, DefaultCompositeKeyRepository<Type>);
//builder.Services.AddDefaultCompositeKeyRepository<Type>();

//Example for Composite Key Repository
//builder.Services.AddTransient(ICompositeKeyRepository<Type, TKey1, TKey2>, CompositeKeyRepository<Type, TKey1, TKey2>);
//builder.Services.AddCompositeKeyRepository<Type, TKey1, TKey2>();

var app = builder.Build();


if (!app.Environment.IsDevelopment()) {
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();