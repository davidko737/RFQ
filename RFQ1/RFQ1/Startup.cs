using RFQ1.Repositories.Interface;
using RFQ1.Repositories;
using RFQ1.Services.Interface;
using RFQ1.Services;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // Register services
        services.AddScoped<ITradeService, TradeService>(); // Register TradeService if needed
        services.AddScoped<ITradeRepository, TradeRepository>(); // Register TradeRepository
        services.AddScoped<IEquityInstrumentRepository, EquityInstrumentRepository>();
        services.AddScoped<IQuoteService, QuoteService>();
        services.AddScoped<IQuoteRepository, QuoteRepository>();

        // Add framework services
        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
