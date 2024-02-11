using DbAssignment;
using DbAssignment.Contexts;
using DbAssignment.Logos;
using DbAssignment.Repositories;
using DbAssignment.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
{
    services.AddDbContext<DataContext>(x => x.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\WIN23\Datalagring\ConsoleApp\DbAssignment\Data\DataBase.mdf;Integrated Security=True;Connect Timeout=30"));
    services.AddDbContext < DataContext2>(x => x.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\WIN23\Datalagring\ConsoleApp\DbAssignment\Data\ProductCatalogDb.mdf;Integrated Security=True;Connect Timeout=30"));

    services.AddScoped<CategoryRepository>();
    services.AddScoped<OrderItemRepository>();
    services.AddScoped<OrderRepository>();
    services.AddScoped<ProductRepository>();
    services.AddScoped<UserRepository>();
    services.AddScoped<ManufacturerRepository>();
    services.AddScoped<ProductAttributeRepository>();
    services.AddScoped<ProductImageRepository>();
    services.AddScoped<ReviewRepository>();
    services.AddScoped<SupplierRepository>();


    services.AddScoped<CategoryService>();
    services.AddScoped<OrderItemService>();
    services.AddScoped<OrderService>();
    services.AddScoped<ProductService>();
    services.AddScoped<UserService>();
    services.AddScoped<ManufacturerService>();
    services.AddScoped<ProductAttributeService>();
    services.AddScoped<ProductImageService>();
    services.AddScoped<ReviewService>();
    services.AddScoped<SupplierService>();

    services.AddSingleton<UserInterface>();
    services.AddSingleton<AllLogos>();




})
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddConsole();
        logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Error);
    })
    .Build();

var userInterface = builder.Services.GetRequiredService<UserInterface>();
var allLogos = builder.Services.GetRequiredService<AllLogos>();




bool exit = false;



while (!exit)
{
    Console.Clear();
    allLogos.MainMenuLogo();
    Console.WriteLine("===Main Menu===");
    string[] options = ["Manage products", "Manage users", "Manage orders", "Exit"];
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


