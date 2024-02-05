﻿using DbAssignment;
using DbAssignment.Contexts;
using DbAssignment.Logos;
using DbAssignment.Repositories;
using DbAssignment.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
{
    services.AddDbContext<DataContext>(x => x.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\WIN23\Datalagring\ConsoleApp\DbAssignment\Data\DataBase.mdf;Integrated Security=True;Connect Timeout=30"));

    services.AddScoped<CategoryRepository>();
    services.AddScoped<OrderItemRepository>();
    services.AddScoped<OrderRepository>();
    services.AddScoped<ProductRepository>();
    services.AddScoped<UserRepository>();


    services.AddScoped<CategoryService>();
    services.AddScoped<OrderItemService>();
    services.AddScoped<OrderService>();
    services.AddScoped<ProductService>();
    services.AddScoped<UserService>();

    services.AddSingleton<UserInterface>();
    services.AddSingleton<AllLogos>();




}).Build();

var userInterface = builder.Services.GetRequiredService<UserInterface>();
var allLogos = builder.Services.GetRequiredService<AllLogos>();




bool exit = false;



while (!exit)
{
    Console.Clear();
    allLogos.MainMenuLogo();
    Console.WriteLine("===Main Menu===");
    string[] options = { "Manage products", "Manage users", "Manage orders", "Exit" };
    int choice = userInterface.DisplayMenu(options, allLogos.ProductsLogo);

    switch (choice)
    {
        case 0:
            userInterface.ManageProductsMenu();
            break;
        case 1:
            userInterface.ManageUsersMenu();
            break;
        case 2:
            userInterface.ManageOrdersMenu();
            break;
        case 3:
            exit = true;
            break;
        default:
            Console.WriteLine("Invalid choice. Please try again.");
            break;
    }
}


