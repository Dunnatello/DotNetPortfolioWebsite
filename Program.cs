using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder( args );

// Add services to the container.
builder.Services.AddRazorPages( );

// Source: https://learn.microsoft.com/en-us/aspnet/core/performance/caching/response?view=aspnetcore-8.0
builder.Services.AddResponseCaching( );

builder.Services.AddControllers( options => {
    options.CacheProfiles.Add( "Default60",
        new CacheProfile( ) {
            Duration = 60
        } );
} );

// Source: https://learn.microsoft.com/en-us/aspnet/core/performance/rate-limit?view=aspnetcore-8.0
builder.Services.AddRateLimiter( _ => _
    .AddFixedWindowLimiter( policyName: "fixed", options => {
        options.PermitLimit = 4;
        options.Window = TimeSpan.FromSeconds( 12 );
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 2;
    } ) );

var app = builder.Build( );

// Configure the HTTP request pipeline.
if ( !app.Environment.IsDevelopment( ) ) {
    app.UseExceptionHandler( "/Error" );
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts( );
}

// Additional Features to Reduce Bandwidth Impact
app.UseResponseCaching( );
app.UseRateLimiter( );

app.UseHttpsRedirection( );
app.UseStaticFiles( );

app.UseStatusCodePagesWithRedirects( "/Errors/Generic" );

app.UseRouting( );

app.UseAuthorization( );

app.MapRazorPages( );

app.Run( );