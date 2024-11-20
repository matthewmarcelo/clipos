// INITIALIZATION
var productName = new List<string>();
var productQty = new List<int>();
var productPrice = new List<decimal>();
var queueIDs = new List<List<int>>();
var queueQty = new List<List<int>>();
var queueStatus = new List<string>();
var queueTotal = new List<decimal>();
var orderIDs = new List<int>();
var orderQty = new List<int>();

// ENVIRONMENT VARIABLES
string businessName = "Test Cafe";
string currency = "PHP ";
productName = ["Latte","Cappuccino","Cold Brew"];
productQty = [5, 6, 7];
productPrice = [100, 120, 140];
/*orderIDs = [0, 1];
orderQty = [3, 4];
queueIDs.Add(orderIDs);
queueQty.Add(orderQty);
orderIDs = [2];
orderQty = [5];
queueIDs.Add(orderIDs);
queueQty.Add(orderQty);
queueStatus = ["Completed", "Pending"];
queueTotal = [780, 700];*/
//Console.WriteLine(queueIDs[0][1]);

void Message(string message)
{
    Console.WriteLine("(i) " + message);
    System.Threading.Thread.Sleep(1500);
}

void InventoryDisplay()
{
    Console.WriteLine("ID\tQTY\tPRICE\tNAME");
    for (int i = 0; i < productName.Count; i++)
    {
        Console.WriteLine($"{i}\t{productQty[i]}\t{productPrice[i]}\t{productName[i]}");
    }
}

void Start()
{
    bool startLatch = true;
    while (startLatch)
    {
        orderIDs = new List<int>();
        orderQty = new List<int>();
        Console.Clear();
        Console.WriteLine("CLIPOS v1.0 C#");
        Console.WriteLine("Command Line Interface Point of Sale\n");
        Console.WriteLine(businessName);
        Console.Write("[A] New Order | [Q] Queue | [E] Inventory | [R] Reports | [C] Close --> ");
        try
        {
            char inventoryChoice = char.Parse(Console.ReadLine().ToUpper());
            switch (inventoryChoice)
            {
                case 'A': NewOrder(); break;
                case 'Q': Queue(); break;
                case 'E': Inventory(); break;
                case 'R': Reports(); break;
                case 'C': startLatch = false; break;
                default: Console.WriteLine("Invalid Input"); break;
            }
        }
        catch
        {
            Console.WriteLine("Invalid Input");
            continue;
        }
    }
}

// NEW ORDER

void NewOrder()
{
    bool newLatch = true;
    while (newLatch)
    {
        
        Console.Clear();
        Console.WriteLine("New Order");
        Console.WriteLine("-----------------");
        Console.WriteLine("PRODUCTS");
        InventoryDisplay();
        Console.WriteLine("\nCART");
        Console.WriteLine("ID\tQTY\tPRICE\tNAME");
        for (int i = 0; i < orderIDs.Count; i++)
        {
            Console.WriteLine($"{i}\t{orderQty[i]}\t{productPrice[orderIDs[i]]}\t{productName[orderIDs[i]]}");
        }
        Console.Write("[A] Add Item | [E] Edit Item Quantity | [D] Delete Item | [S] Submit Order | [C] Close --> ");
        try
        {
            char inventoryChoice = char.Parse(Console.ReadLine().ToUpper());
            switch (inventoryChoice)
            {
                case 'S': SubmitOrder(); break;
                case 'A': AddToCart(); break;
                case 'E': EditCartQuantity(); break;
                case 'D': DeleteFromCart(); break;
                case 'C': newLatch = false; break;
                default: Console.WriteLine("Invalid Input"); break;
            }
        }
        catch
        {
            Console.WriteLine("Invalid Input");
            continue;
        }
    }
}

void AddToCart()
{
    Console.Write("Select product ID: ");
    int selectedProduct = int.Parse(Console.ReadLine());
    Console.Write("Insert product quantity: ");
    int quantityInput = int.Parse(Console.ReadLine());
    orderIDs.Add(selectedProduct);
    orderQty.Add(quantityInput);
    Message("Added to cart successfully.");
}

void DeleteFromCart()
{
    Console.Write("Select cart item ID: ");
    int selectedID = int.Parse(Console.ReadLine());
    orderIDs.RemoveAt(selectedID);
    orderQty.RemoveAt(selectedID);
    Message("Removed from cart successfully.");
}

void EditCartQuantity()
{
    Console.Write("Select cart item ID: ");
    int selectedID = int.Parse(Console.ReadLine());
    Console.Write("Insert new product quantity: ");
    int quantityInput = int.Parse(Console.ReadLine());
    orderQty[selectedID] = quantityInput;
    Message("Edited quantity successfully.");
}

void SubmitOrder()
{
    queueIDs.Add(new List<int>(orderIDs));
    queueQty.Add(new List<int>(orderQty));
    queueStatus.Add("Pending");
    decimal totalPrice = 0;
    for (int i = 0; i < orderIDs.Count; i++)
    {
        productQty[orderIDs[i]] -= orderQty[i];
        totalPrice += (productPrice[orderIDs[i]] * orderQty[i]);
    }
    queueTotal.Add(totalPrice);
    orderIDs.Clear();
    orderQty.Clear();
    Message("Submitted order successfully.");
}

// QUEUE

void Queue()
{
    bool queueLatch = true;
    while (queueLatch)
    {
        Console.Clear();
        Console.WriteLine("Queue");
        Console.WriteLine("-----------------");
        for (int i = queueIDs.Count-1; i >= 0; i--)
        {
            Console.WriteLine($"ORDER #{i+1} [{queueStatus[i]}]");
            Console.WriteLine("QTY\tNAME");
            for (int j = 0; j < queueIDs[i].Count; j++)
            {
                Console.WriteLine($"{queueQty[i][j]}\t{productName[queueIDs[i][j]]}");
            }
            Console.WriteLine($"TOTAL: {currency}{queueTotal[i]}\n");
        }
        Console.Write("[D] Complete Order [C] Close --> ");
        try
        {
            char inventoryChoice = char.Parse(Console.ReadLine().ToUpper());
            switch (inventoryChoice)
            {
                case 'D': CompleteOrder(); break;
                case 'C': queueLatch = false; break;
                default: Console.WriteLine("Invalid Input"); break;
            }
        }
        catch
        {
            Console.WriteLine("Invalid Input");
            continue;
        }
    }
}

void CompleteOrder()
{
    Console.Write("Select order ID: ");
    int selectedOrder = int.Parse(Console.ReadLine())-1;
    queueStatus[selectedOrder] = "Completed";
    Message("Order set to Completed.");
}

// INVENTORY

void Inventory()
{
    bool inventoryLatch = true;
    while (inventoryLatch)
    {
        Console.Clear();
        Console.WriteLine("Inventory");
        Console.WriteLine("-----------------");
        InventoryDisplay();
        Console.WriteLine($"{productName.Count} products(s) in inventory.");
        Console.Write("[A] Add Product | [E] Edit Product | [D] Delete Product | [C] Close --> ");
        try
        {
            char inventoryChoice = char.Parse(Console.ReadLine().ToUpper());
            switch (inventoryChoice)
            {
                case 'A': AddProduct(); break;
                case 'E': EditProduct(); break;
                case 'D': DeleteProduct(); break;
                case 'C': inventoryLatch = false; break;
                default: Console.WriteLine("Invalid Input"); break;
            }
        }
        catch
        {
            Console.WriteLine("Invalid Input");
            continue;
        }
    }
}

void EditProduct()
{
    bool editLatch = true;
    Console.WriteLine("Edit Product");
    Console.Write("Select product ID: ");
    int selectedProduct = int.Parse(Console.ReadLine());
    while (editLatch)
    {
        Console.Clear();
        Console.WriteLine($"Name:\t\t{productName[selectedProduct]}");
        Console.WriteLine($"Quantity:\t{productQty[selectedProduct]}");
        Console.WriteLine($"Price:\t\t{productPrice[selectedProduct]}");
        Console.Write("[E] Edit Name | [Q] Edit Quantity | [D] Edit Price | [C] Close --> ");
        try
        {
            char inventoryChoice = char.Parse(Console.ReadLine().ToUpper());
            switch (inventoryChoice)
            {
                case 'E': EditProductName(selectedProduct); break;
                case 'Q': EditProductQty(selectedProduct); break;
                case 'D': EditProductPrice(selectedProduct); break;
                case 'C': editLatch = false; break;
                default: Console.WriteLine("Invalid Input"); break;
            }
        }
        catch
        {
            Console.WriteLine("Invalid Input");
        }
    }
    
}

void EditProductName(int productID)
{
    Console.Write("New Product Name: ");
    string editProductName = Console.ReadLine();
    productName[productID] = editProductName;
    Message("Edited product name successfully.");
}

void EditProductQty(int productID)
{
    Console.Write("New Product Quantity: ");
    int editProductQty = int.Parse(Console.ReadLine());
    productQty[productID] = editProductQty;
    Message("Edited product quantity successfully.");
}

void EditProductPrice(int productID)
{
    Console.Write("New Product Price: ");
    decimal editProductPrice = decimal.Parse(Console.ReadLine());
    productPrice[productID] = editProductPrice;
    Message("Edited product price successfully.");
}

void AddProduct()
{
    Console.WriteLine("Add Product");
    Console.Write("Product Name: ");
    string addProductName = Console.ReadLine();
    Console.Write("Product Quantity: ");
    int addProductQty = int.Parse(Console.ReadLine());
    Console.Write("Product Price: ");
    decimal addProductPrice = decimal.Parse(Console.ReadLine());
    productName.Add(addProductName);
    productQty.Add(addProductQty);
    productPrice.Add(addProductPrice);
    Message("Product added successfully.");
}

void DeleteProduct()
{
    Console.WriteLine("Delete Product");
    Console.Write("Select product ID: ");
    int selectedProduct = int.Parse(Console.ReadLine());
    Console.Write("Are you sure you want to delete this product? [Y/N]: ");
    char confirmChoice = char.Parse(Console.ReadLine().ToUpper());
    if (confirmChoice == 'Y')
    {
        productName.RemoveAt(selectedProduct);
        productQty.RemoveAt(selectedProduct);
        productPrice.RemoveAt(selectedProduct);
        Message("Product deleted successfully.");
    }
}

void Reports()
{
    bool reportsLatch = true;
    while (reportsLatch)
    {
        Console.Clear();
        Console.WriteLine("Revenue");
        Console.WriteLine("---------------");
        Console.WriteLine($"Total transactions:\t{queueIDs.Count}");
        decimal totalRevenue = 0;
        for (int i = 0; i < queueTotal.Count; i++)
        {
            totalRevenue += queueTotal[i];
        }
        Console.WriteLine($"Total revenue:\t{currency}{totalRevenue}\n");
        Console.Write("[C] Close --> ");
        try
        {
            char inventoryChoice = char.Parse(Console.ReadLine().ToUpper());
            switch (inventoryChoice)
            {
                case 'C': reportsLatch = false; break;
                default: Console.WriteLine("Invalid Input"); break;
            }
        }
        catch
        {
            Console.WriteLine("Invalid Input");
            continue;
        }
    }
    
    
}

Start();

Console.WriteLine("Goodbye!");
System.Threading.Thread.Sleep(1500);
Console.Clear();