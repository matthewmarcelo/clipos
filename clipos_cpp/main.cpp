#include <iostream>
#include <vector>
#include <windows.h>
#include <unistd.h>
#include <ctype.h>
#include <conio.h>
#include <stdlib.h>
using namespace std;

// INITIALIZATION
vector<string> productName;
vector<int> productQty;
vector<double> productPrice;
vector<vector<int>> queueIDs;
vector<vector<int>> queueQty;
vector<string> queueStatus;
vector<double> queueTotal;
vector<int> orderIDs;
vector<int> orderQty;

// ENVIRONMENT VARIABLES
string businessName = "Test Cafe";
string currency = "PHP ";

void Message(string message) {
    cout << ("(i) " + message) << endl;
    sleep(1.5);
}

void InventoryDisplay()
{
    cout << "ID\tQTY\tPRICE\tNAME" << endl;
    for (int i = 0; i < productName.size(); i++)
    {
        cout << i << "\t" << productQty[i] << "\t" << productPrice[i] << "\t" << productName[i] << endl;
    }
}

// QUEUE

void Queue()
{
    bool queueLatch = true;
    while (queueLatch)
    {
        system("cls");
        cout << "Queue" << endl;
        cout << "-----------------" << endl;
        for (int i = queueIDs.size()-1; i >= 0; i--)
        {
            cout << "ORDER #" << (i+1) << "[" << queueStatus[i] << "]" << endl;
            cout << "QTY\tNAME" << endl;
            for (int j = 0; j < queueIDs[i].size(); j++)
            {
                cout << queueQty[i][j] <<"\t" << productName[queueIDs[i][j]] << endl;
            }
            cout << "TOTAL: " << currency << queueTotal[i] << "\n" << endl;
        }
        cout << "[D] Complete Order [C] Close --> ";
        try
        {
            char inventoryChoice;
            cin >> inventoryChoice;
            inventoryChoice = toupper(inventoryChoice);
            switch (inventoryChoice)
            {
                //case 'D': CompleteOrder(); break;
                case 'C': queueLatch = false; break;
                default: cout << "Invalid Input" << endl; break;
            }
        }
        catch(...)
        {
            cout << "Invalid Input" << endl;
            continue;
        }
    }
}

// INVENTORY

void AddProduct()
{
    cout << "Add Product" << endl;
    cout << "Product Name: ";
    string addProductName;
    cin.ignore();
    getline(cin, addProductName);
    cout << "Product Quantity: ";
    int addProductQty;
    cin >> addProductQty;
    cout << "Product Price: ";
    float addProductPrice;
    cin >> addProductPrice;
    productName.push_back(addProductName);
    productQty.push_back(addProductQty);
    productPrice.push_back(addProductPrice);
    Message("Product added successfully.");
}

void DeleteProduct()
{
    cout << "Delete Product" << endl;
    cout << "Select product ID: ";
    int selectedProduct;
    cin >> selectedProduct;
    cout << "Are you sure you want to delete this product? [Y/N]: ";
    char confirmChoice;
    cin >> confirmChoice;
    confirmChoice = toupper(confirmChoice);
    if (confirmChoice == 'Y')
    {
        auto deleteProductName = productName.begin() + selectedProduct;
        productName.erase(deleteProductName);
        auto deleteproductQty = productQty.begin() + selectedProduct;
        productQty.erase(deleteproductQty);
        auto deleteproductPrice = productPrice.begin() + selectedProduct;
        productPrice.erase(deleteproductPrice);
        Message("Product deleted successfully.");
    }
}

void EditProductName(int productID)
{
    cout << "New Product Name: ";
    string editProductName;
    cin.ignore();
    getline(cin, editProductName);
    productName.at(productID) = editProductName;
    Message("Edited product name successfully.");
}

void EditProductQty(int productID)
{
    cout << "New Product Quantity: ";
    int editProductQty;
    cin >> editProductQty;
    productQty.at(productID) = editProductQty;
    Message("Edited product quantity successfully.");
}

void EditProductPrice(int productID)
{
    cout << "New Product Price: ";
    float editProductPrice;
    cin >> editProductPrice;
    productPrice.at(productID) = editProductPrice;
    Message("Edited product price successfully.");
}

void EditProduct()
{
    bool editLatch = true;
    cout << "Edit Product" << endl;
    cout << "Select product ID: ";
    int selectedProduct;
    cin >> selectedProduct;
    while (editLatch)
    {
        system("cls");
        cout << "Name:\t\t" << productName[selectedProduct] << endl;
        cout << "Quantity:\t" << productQty[selectedProduct] << endl;
        cout << "Price:\t" << productPrice[selectedProduct] << endl;
        cout << "[E] Edit Name | [Q] Edit Quantity | [D] Edit Price | [C] Close --> ";
        try
        {
            char inventoryChoice;
            cin >> inventoryChoice;
            inventoryChoice = toupper(inventoryChoice);
            switch (inventoryChoice)
            {
                case 'E': EditProductName(selectedProduct); break;
                case 'Q': EditProductQty(selectedProduct); break;
                case 'D': EditProductPrice(selectedProduct); break;
                case 'C': editLatch = false; break;
                default: cout << "Invalid Input" << endl; break;
            }
        }
        catch(...)
        {
            cout << "Invalid Input" << endl;
        }
    }
}

void Inventory()
{
    bool inventoryLatch = true;
    while (inventoryLatch)
    {
        system("cls");
        cout << "Inventory" << endl;
        cout << "-----------------" << endl;
        InventoryDisplay();
        cout << productName.size() << " products(s) in inventory."  << endl;
        cout << "[A] Add Product | [E] Edit Product | [D] Delete Product | [C] Close --> ";
        try
        {
            char inventoryChoice;
            cin >> inventoryChoice;
            inventoryChoice = toupper(inventoryChoice);
            switch (inventoryChoice)
            {
                case 'A': AddProduct(); break;
                case 'E': EditProduct(); break;
                case 'D': DeleteProduct(); break;
                case 'C': inventoryLatch = false; break;
                default: cout << "Invalid Input" << endl; break;
            }
        }
        catch(...)
        {
            cout << "Invalid Input" << endl; break;
            continue;
        }
    }
}

void Start()
{
    bool startLatch = true;
    while (startLatch)
    {
        vector<int> orderIDs;
        vector<int> orderQty;
        system("cls");
        cout << "CLIPOS v1.0 C++" << endl;
        cout << "Command Line Interface Point of Sale\n" << endl;
        cout << businessName << endl;
        cout << "[A] New Order | [Q] Queue | [E] Inventory | [R] Reports | [C] Close --> ";
        try
        {
            char inventoryChoice;
            cin >> inventoryChoice;
            inventoryChoice = toupper(inventoryChoice);
            switch (inventoryChoice)
            {
                //case 'A': NewOrder(); break;
                case 'Q': Queue(); break;
                case 'E': Inventory(); break;
                //case 'R': Reports(); break;
                case 'C': startLatch = false; break;
                default: cout << "Invalid Input" << endl; break;
            }
        }
        catch (...)
        {
            cout << "Invalid Input" << endl;
            continue;
        }
    }
}



int main() {

    // ENVIRONMENT VARIABLES
    productName = {"Latte","Cappuccino","Cold Brew"};
    productQty = {5, 6, 7};
    productPrice = {100, 120, 140};

    Start();

    cout << "Goodbye!" << endl;
    sleep(1.5);
    system("cls");


    /*vector<string> cars = {"Volvo", "BMW", "Ford", "Mazda"};
    auto pos = cars.begin() + 2;
    cars.erase(pos);
    for (string car : cars) {
        cout << car << "\n";
    }
    return 0;*/
}