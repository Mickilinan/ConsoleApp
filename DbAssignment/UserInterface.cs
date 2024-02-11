using DbAssignment.Logos;
using DbAssignment.Services;


namespace DbAssignment;

public class UserInterface(ProductService productService, UserService userService, OrderService orderService, OrderItemService orderItemService, AllLogos allLogos)
{

    private readonly ProductService _productService = productService;
    private readonly UserService _userService = userService;
    private readonly OrderService _orderService = orderService;  
    private readonly OrderItemService _orderItemService = orderItemService;
    private readonly AllLogos _allLogos = allLogos;

    public int DisplayMenu(string[] options, Action displayLogo)
    {
        int currentSelection = 0;

        ConsoleKey key;

        do
        {
            Console.Clear();

            displayLogo();

            for (int i = 0; i < options.Length; i++)
            {
                if (i == currentSelection)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                Console.WriteLine(options[i]);

                Console.ResetColor();
            }

            key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (currentSelection > 0)
                        currentSelection--;
                    break;
                case ConsoleKey.DownArrow:
                    if (currentSelection < options.Length - 1)
                        currentSelection++;
                    break;
                case ConsoleKey.Enter:
                    return currentSelection;
            }
        } while (key != ConsoleKey.Escape);

        return -1;
    }


    public void ManageProductsMenu()
    {
        bool exit = false;

        while (!exit)
        {
            Console.Clear();
            _allLogos.ProductsLogo(); 
            Console.WriteLine("===Manage Products Menu===");
            string[] options = ["Create Product", "View Products", "Update Product", "Delete Product", "Go back"];
            int choice = DisplayMenu(options, _allLogos.ProductsLogo);

            switch (choice)
            {
                case 0:
                    CreateProduct_UI();
                    break;
                case 1:
                    GetProducts_UI();
                    break;
                case 2:
                    UpdateProduct_UI();
                    break;
                case 3:
                    DeleteProduct_UI();
                    break;
                case 4:
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    public void ManageUsersMenu()
    {
        bool exit = false;

        while (!exit)
        {
            Console.Clear();
            _allLogos.UsersLogo();
            string[] options = ["Create User", "View Users", "Update user", "Delete user", "Go back"];
            int choice = DisplayMenu(options, _allLogos.UsersLogo);

            switch (choice)
            {
                case 0:
                    CreateUser_UI();
                    break;
                case 1:
                    GetUsers_UI();
                    break;
                case 2:
                    UpdateUser_UI();
                    break;
                case 3:
                    DeleteUser_UI();
                    break;
                case 4:
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    public void ManageOrdersMenu()
    {
        bool exit = false;

        while (!exit)
        {
            Console.Clear();
            _allLogos.OrdersLogo(); // Display the logo here
            Console.WriteLine("===Manage Orders Menu===");
            string[] options = ["Create order", "View orders", "Update order", "Delete order", "Go back"];
            int choice = DisplayMenu(options, _allLogos.OrdersLogo);

            switch (choice)
            {
                case 0:
                    CreateOrder_UI();
                    break;
                case 1:
                    GetOrders_UI();
                    break;
                case 2:
                    UpdateOrder_UI();
                    break;
                case 3:
                    DeleteOrder_UI();
                    break;
                case 4:
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }


    //Product UI
    public void CreateProduct_UI()
    {
        Console.Clear();
        Console.WriteLine("Create Product");

        Console.Write("Product name: ");  
        var productName = Console.ReadLine();   

        Console.Write("Product price: ");
        var price = decimal.Parse(Console.ReadLine()!);

        Console.Write("Product description: ");
        var description = Console.ReadLine();

        Console.Write("Product category: ");
        var category = (Console.ReadLine()!);

        var result = _productService.CreateProduct(productName, price, description, category);
        if (result !=null)
        {
            Console.Clear();
            Console.WriteLine("Product created");
            Console.ReadKey();
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Product not created");
            Console.ReadKey();
        }
    }  
    
    public void GetProducts_UI()
    {

        Console.Clear();
        var products = _productService.GetAllProducts();    
        foreach (var product in products)
        {
            Console.WriteLine($"Name: {product.ProductName} Price: {product.Price}SEK Description: {product.Description} Category: {product.Category.CategoryName}");
        }

        Console.ReadKey();
    }

    public void UpdateProduct_UI()
    {
        Console.Clear();
        Console.WriteLine("Update Product");

        Console.Write("Product Id: ");
        var id = int.Parse(Console.ReadLine()!);

        Console.Write("Product name: ");
        var productName = Console.ReadLine();

        Console.Write("Product price: ");
        var price = decimal.Parse(Console.ReadLine()!);

        Console.Write("Product description: ");
        var description = Console.ReadLine();

        Console.Write("Product category: ");
        var category = (Console.ReadLine()!);

        var productEntity = _productService.GetProductById(id);
        productEntity.ProductName = productName;
        productEntity.Price = price;
        productEntity.Description = description;
        productEntity.Category.CategoryName = category;

        var product = _productService.UpdateProduct(productEntity);
        if (product != null)
        {
            Console.Clear();
            Console.WriteLine("Product updated");
            Console.ReadKey();
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Product not updated");
            Console.ReadKey();
        }
    }

    public void DeleteProduct_UI()
    {
        Console.Clear();
        Console.WriteLine("Delete Product");

        Console.Write("Product Id: ");
        var id = int.Parse(Console.ReadLine()!);

        _productService.DeleteProduct(id);
        Console.Clear();
        Console.WriteLine("Product deleted");
        Console.ReadKey();
    }

    //User UI

    public void CreateUser_UI()
    {
        Console.Clear();
        Console.WriteLine("Create User");

        Console.Write("Firstname: ");
        var firstName = Console.ReadLine();

        Console.Write("Lastname: ");
        var lastName = Console.ReadLine();

        Console.Write("Email: ");
        var email = Console.ReadLine();
        

        var result = _userService.CreateUser(firstName, lastName, email );
        if (result != null)
        {
            Console.Clear();
            Console.WriteLine("User created");
            Console.ReadKey();
        }
        else
        {
            Console.Clear();
            Console.WriteLine("User not created");
            Console.ReadKey();
        }
    }

    public void GetUsers_UI()
    {

        Console.Clear();
        var users = _userService.GetAllUsers();
        foreach (var user in users)
        {
            Console.WriteLine($"Firstname: {user.FirstName} Lastname: {user.LastName} Email: {user.Email}");
        }

        Console.ReadKey();
    }

    public void UpdateUser_UI()
    {
        Console.Clear();
        Console.WriteLine("Update User");

        Console.Write("User Id: ");
        var id = int.Parse(Console.ReadLine()!);

        Console.Write("Firstname: ");
        var firstName = Console.ReadLine();

        Console.Write("Lastname: ");
        var lastName = Console.ReadLine();

        Console.Write("Email: ");
        var email = Console.ReadLine();

        var userEntity = _userService.GetUserById(id);
        userEntity.FirstName = firstName;
        userEntity.LastName = lastName;
        userEntity.Email = email;
        

        var user = _userService.UpdateUser(userEntity);
        if (user != null)
        {
            Console.Clear();
            Console.WriteLine("User updated");
            Console.ReadKey();
        }
        else
        {
            Console.Clear();
            Console.WriteLine("User not updated");
            Console.ReadKey();
        }
    }

    public void DeleteUser_UI()
    {
        Console.Clear();
        Console.WriteLine("Delete user");

        Console.Write("User Id: ");
        var id = int.Parse(Console.ReadLine()!);

        _userService.DeleteUser(id);
        Console.Clear();
        Console.WriteLine("User deleted");
        Console.ReadKey();
    }


    //Order UI

    public void CreateOrder_UI()
    {
        
        Console.Write("Enter the status of the order: ");
        string status = Console.ReadLine();

        Console.Write("Enter the user's first name: ");
        string firstName = Console.ReadLine();

        Console.Write("Enter the user's last name: ");
        string lastName = Console.ReadLine();

        Console.Write("Enter the user's email: ");
        string email = Console.ReadLine();

        var order = _orderService.CreateOrder(status, DateTime.Now, DateTime.Now, firstName, lastName, email);
    

            Console.Write("How many items would you like to add to the order? ");
        int itemCount = int.Parse(Console.ReadLine());

        for (int i = 0; i < itemCount; i++)
        {
            Console.Write("Enter the product name for item " + (i + 1) + ": ");
            string productName = Console.ReadLine();

            

            Console.Write("Enter the quantity for item " + (i + 1) + ": ");
            int quantity = int.Parse(Console.ReadLine());

           
        }
    }

    public void GetOrders_UI()
    {

        Console.Clear();
        var orders = _orderService.GetAllOrders();
        foreach (var order in orders)
        {
            var user = _userService.GetUserById(order.UserId);
            Console.WriteLine($"Order ID: {order.Id}, User: {user.FirstName} {user.LastName}, Status: {order.Status}");

            var orderItems = _orderItemService.GetOrderItemsByOrderId(order.Id);
            foreach (var orderItem in orderItems)
            {
                var product = _productService.GetProductById(orderItem.ProductId);
                Console.WriteLine($"\tProduct: {product.ProductName}, Quantity: {orderItem.Quantity}");
            }
        }

        Console.ReadKey();
    }

    public void UpdateOrder_UI()
    {
        

        Console.Write("How many items would you like to update in the order? ");
        int itemCount = int.Parse(Console.ReadLine());

        for (int i = 0; i < itemCount; i++)
        {
            Console.Write("Enter the order item ID for item " + (i + 1) + ": ");
            int orderItemId = int.Parse(Console.ReadLine());

            Console.Write("Enter the new product name for item " + (i + 1) + ": ");
            string newProductName = Console.ReadLine();

            

            Console.Write("Enter the new quantity for item " + (i + 1) + ": ");
            int newQuantity = int.Parse(Console.ReadLine());

            
        }
    }

    public void DeleteOrder_UI()
    {
        Console.Clear();
        Console.WriteLine("Delete order");

        Console.Write("Order Id: ");
        var id = int.Parse(Console.ReadLine()!);

        _orderService.DeleteOrder(id);
        Console.Clear();
        Console.WriteLine("Order deleted");
        Console.ReadKey();
    }
}


