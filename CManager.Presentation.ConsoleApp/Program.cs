// Program.cs

using CManager.Business.Services;
using CManager.Infrastructure.Repositories;
using CManager.Presentation.ConsoleApp.Controllers;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection()
    .AddScoped<ICustomerService, CustomerService>()
    .AddScoped<ICustomerRepository, CustomerRepository>()
    .AddScoped<MenuController>()
    .BuildServiceProvider();

var controller = services.GetRequiredService<MenuController>();
controller.MainMenu();