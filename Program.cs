using BlazorState.UI;
using BlazorState.UI.Pages.Container;
using BlazorState.UI.Pages.ContainersEverywhere;
using BlazorState.UI.Pages.MegaContainer;
using BlazorState.Utils;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// state container
builder.Services.AddSingleton<NameStateContainer>();

// state containers
builder.Services
    .AddSingleton(new StateContainer<StateMessage>(new StateMessage()))
    .AddSingleton(new StateContainer<StateProfile>(new StateProfile()))
    .AddSingleton(new StateContainer<StateLoginEntries>(new StateLoginEntries()))
    .AddSingleton(new StateContainer<StateMessages>(new StateMessages()));

// use cases
builder.Services
    .AddTransient<UseCaseFetchMessage>()
    .AddTransient<UseCaseFetchLoginEntries>()
    .AddTransient<UseCaseFetchMessages>()
    .AddTransient<UseCaseSendMessage>()
    .AddTransient<UseCaseFetchProfile>();
await builder.Build().RunAsync();
