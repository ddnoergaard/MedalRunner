using MedalRunner.Repositories;
using MedalRunner.Repositories.Interfaces;
using MedalRunner.Services;
using MedalRunner.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/App", "PlayerOnly");
    options.Conventions.AuthorizeFolder("/Admin", "AdminOnly");
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("PlayerOnly", policy => policy.RequireRole("Player"));
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
});

//Cookie auth -> Hvor brugere bliver sendt til hvis de ikke har adgang.
/*
    Hvis en bruger ikke er logget ind, og prøver at tilgå en auth page, så bliver de redirectet til /Login
    Hvis en bruger er logget ind, og prøver at tilgå Admin siderne, så bliver de redirected to /Error
 */
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.AccessDeniedPath = "/Error";
    });

//Gør så vi kan access client http context.
builder.Services.AddHttpContextAccessor();

//DI - REPO
builder.Services.AddScoped<IBossRepository, BossRepository>();
builder.Services.AddScoped<ICharacterRepository, CharacterRepository>();
builder.Services.AddScoped<IClassRepository, ClassRepository>();
builder.Services.AddScoped<IConsumableRepository, ConsumableRepository>();
builder.Services.AddScoped<IDungeonRepository, DungeonRepository>();
builder.Services.AddScoped<IGemRepository, GemRepository>();
builder.Services.AddScoped<IGlyphRepository, GlyphRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IScoreboardRepository, ScoreboardRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

//DI - SERVICE
builder.Services.AddScoped<IBossService, BossService>();
builder.Services.AddScoped<ICharacterService, CharacterService>();
builder.Services.AddScoped<IClassService, ClassService>();
builder.Services.AddScoped<IConsumableService, ConsumableService>();
builder.Services.AddScoped<IDungeonService, DungeonService>();
builder.Services.AddScoped<IGemService, GemService>();
builder.Services.AddScoped<IGlyphService, GlyphService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IScoreboardService, ScoreboardService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<CookieService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
