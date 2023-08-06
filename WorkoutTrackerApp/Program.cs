using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Fluxor;
using WorkoutTrackerApp;
using WorkoutTrackerApp.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient<IWorkoutDataService, WorkoutDataService>(client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

builder.Services.AddScoped<IDispatcher, Dispatcher>();

builder.Services.AddFluxor(options =>
    {
        options.ScanAssemblies(typeof(Program).Assembly);

    #if DEBUG
        options.UseReduxDevTools();
    #endif
    });



await builder.Build().RunAsync();
