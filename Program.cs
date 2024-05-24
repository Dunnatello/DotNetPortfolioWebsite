var builder = WebApplication.CreateBuilder( args );

// Add services to the container.
builder.Services.AddRazorPages( );

var app = builder.Build( );

// Configure the HTTP request pipeline.
if ( !app.Environment.IsDevelopment( ) ) {
    app.UseExceptionHandler( "/Error" );
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts( );
}

app.UseHttpsRedirection( );
app.UseStaticFiles( );

/*app.Use( async ( context, next ) => {
	string EnteredPath = context.Request.HttpContext.Request.Path.ToString( ).Remove( 0, 1 );
	await next( );

	if ( context.Response.StatusCode == 404 ) {
		context.Request.Path = "/Errors/404";
		await next( );
	}
} );*/
app.UseStatusCodePagesWithRedirects( "/Errors/Generic" );

app.UseRouting( );
app.UseRouting( );

app.UseAuthorization( );

app.MapRazorPages( );

app.Run( );

