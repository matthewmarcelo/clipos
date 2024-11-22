import time
from os import system, name
def clear():
    # for windows
    if name == 'nt':
        _ = system('cls')
    # for mac and linux(here, os.name is 'posix')
    else:
        _ = system('clear')

# INITIALIZATION
productName = []
productQty = []
productPrice = []
queueIDs = []
queueQty = []
queueStatus = []
queueTotal = []
orderIDs = []
orderQty = []

# ENVIRONMENT VARIABLES
businessName = "Test Cafe"
currency = "PHP "
productName = ["Latte","Cappuccino","Cold Brew"]
productQty = [5, 6, 7]
productPrice = [100.00, 120.00, 140.00]
#orderIDs = [0, 1]
#orderQty = [3, 4]
#queueIDs.append(orderIDs)
#queueQty.append(orderQty)
#orderIDs = [2]
#orderQty = [5]
#queueIDs.append(orderIDs)
#queueQty.append(orderQty)
#queueStatus = ["Completed", "Pending"]
#queueTotal = [780, 700]

def Message(message):
    print("(i) " + message)
    time.sleep(1.5)

def InventoryDisplay():
    print("ID\tQTY\tPRICE\tNAME")
    for i in range(len(productName)):
        print(f"{i}\t{productQty[i]}\t{productPrice[i]}\t{productName[i]}")

def Start():
    startLatch = True
    while (startLatch):
        clear()
        print("CLIPOS v1.0 Python")
        print("Command Line Interface Point of Sale\n")
        print(businessName)

        try:
            inventoryChoice = input("[A] New Order | [Q] Queue | [E] Inventory | [R] Reports | [C] Close --> ")
            inventoryChoice = inventoryChoice.upper()
            match inventoryChoice.upper():
                case 'A': NewOrder()
                case 'Q': Queue()
                case 'E': Inventory()
                case 'R': Reports()
                case 'C': startLatch = False
                case _: print("Invalid Input")
        except Exception as e:
            print(e)
            time.sleep(10)

# NEW ORDER / TEST COMPLETE ORDER
def NewOrder():
    newLatch = True
    while (newLatch):
        clear()
        print("New Order")
        print("-----------------")
        print("PRODUCTS")
        InventoryDisplay()
        print("\nCART");
        print("ID\tQTY\tPRICE\tNAME")
        for i in range(len(orderIDs)):
            print(f"{i}\t{orderQty[i]}\t{productPrice[orderIDs[i]]}\t{productName[orderIDs[i]]}")
        try:
            inventoryChoice = input("[A] Add Item | [E] Edit Item Quantity | [D] Delete Item | [S] Submit Order | [C] Close --> ")
            inventoryChoice = inventoryChoice.upper()
            match inventoryChoice.upper()[0]:
                case 'S': SubmitOrder()
                case 'A': AddToCart()
                case 'E': EditCartQuantity()
                case 'D': DeleteFromCart()
                case 'C': newLatch = False
                case _: print("Invalid Input")
        except:
            print("Invalid Input")

def AddToCart():
    selectedProduct = int(input("Select product ID: "))
    quantityInput = int(input("Insert product quantity: "))
    orderIDs.append(selectedProduct)
    orderQty.append(quantityInput)
    Message("Added to cart successfully.")

def DeleteFromCart():
    selectedID = int(input("Select cart item ID: "))
    orderIDs.pop(selectedID)
    orderQty.pop(selectedID)
    Message("Removed from cart successfully.")

def EditCartQuantity():
    selectedID = int(input("Select cart item ID: "))
    quantityInput = int(input("Insert new product quantity: "))
    orderQty[selectedID] = quantityInput
    Message("Edited quantity successfully.")

def SubmitOrder():
    queueIDs.append(orderIDs)
    queueQty.append(orderQty)
    queueStatus.append("Pending")
    totalPrice = 0.0
    for i in range(len(orderIDs)):
        productQty[orderIDs[i]] -= orderQty[i]
        totalPrice += (productPrice[orderIDs[i]] * orderQty[i])
    queueTotal.append(totalPrice)
    orderIDs.clear()
    orderQty.clear()
    Message("Submitted order successfully.")

# QUEUE

def Queue():
    queueLatch = True
    while (queueLatch):
        clear()
        print("Queue")
        print("-----------------")
        for i in reversed(range(len(queueIDs))):
        #for (int i = len(queueIDs)-1; i >= 0; i--)
            print(f"ORDER #{i+1} [{queueStatus[i]}]")
            print("QTY\tNAME")
            for j in range(len(queueIDs[i])):
                print(f"{queueQty[i][j]}\t{productName[queueIDs[i][j]]}")
            print(f"TOTAL: {currency}{queueTotal[i]}\n")
        try:
            inventoryChoice = input("[D] Complete Order | [C] Close --> ")
            inventoryChoice = inventoryChoice.upper()
            match inventoryChoice.upper()[0]:
                case 'D': CompleteOrder()
                case 'C': queueLatch = False
                case _: print("Invalid Input")
        except:
            print("Invalid Input")

def CompleteOrder():
    selectedOrder = int(input("Select order ID: "))-1
    queueStatus[selectedOrder] = "Completed"
    Message("Order set to Completed.")

# INVENTORY

def Inventory():
    inventoryLatch = True
    while (inventoryLatch):
        clear()
        print("Inventory")
        print("-----------------")
        InventoryDisplay()
        print(f"{len(productName)} products(s) in inventory.")
        try:
            inventoryChoice = input("[A] Add Product | [E] Edit Product | [D] Delete Product | [C] Close --> ")
            inventoryChoice = inventoryChoice.upper()
            match inventoryChoice.upper():
                case 'A': AddProduct()
                case 'E': EditProduct()
                case 'D': DeleteProduct()
                case 'C': inventoryLatch = False
                case _: print("Invalid Input")
        except:
            print("Invalid Input")


def EditProduct():
    editLatch = True
    print("Edit Product")
    selectedProduct = int(input("Select product ID: "))
    while (editLatch):
        clear()
        print(f"Name:\t\t{productName[selectedProduct]}")
        print(f"Quantity:\t{productQty[selectedProduct]}")
        print(f"Price:\t\t{productPrice[selectedProduct]}")

        try:
            inventoryChoice = input("[E] Edit Name | [Q] Edit Quantity | [D] Edit Price | [C] Close --> ")
            inventoryChoice = inventoryChoice.upper()
            match inventoryChoice.upper():
                case 'E': EditProductName(selectedProduct)
                case 'Q': EditProductQty(selectedProduct)
                case 'D': EditProductPrice(selectedProduct)
                case 'C': editLatch = False
                case _: print("Invalid Input")
        except:
            print("Invalid Input")

def EditProductName(productID):
    editProductName = input("New Product Name: ")
    productName[productID] = editProductName
    Message("Edited product name successfully.")

def EditProductQty(productID):
    editProductQty = int(input("New Product Quantity: "))
    productQty[productID] = editProductQty
    Message("Edited product quantity successfully.")

def EditProductPrice(productID):
    editProductPrice = float(input("New Product Price: "))
    productPrice[productID] = editProductPrice
    Message("Edited product price successfully.")

def AddProduct():
    print("Add Product")
    addProductName = input("Product Name: ")
    addProductQty = int(input("Product Quantity: "))
    addProductPrice = float(input("Product Price: "))
    productName.append(addProductName)
    productQty.append(addProductQty)
    productPrice.append(addProductPrice)
    Message("Product added successfully.")

def DeleteProduct():
    print("Delete Product")
    selectedProduct = int(input("Select product ID: "))
    confirmChoice = input("Are you sure you want to delete this product? [Y/N]: ")
    confirmChoice = confirmChoice.upper()
    if (confirmChoice == 'Y'):
        productName.pop(selectedProduct)
        productQty.pop(selectedProduct)
        productPrice.pop(selectedProduct)
        Message("Product deleted successfully.")

def Reports():
    reportsLatch = True
    while (reportsLatch):
        clear()
        print("Revenue")
        print("---------------")
        print(f"Total transactions:\t{len(queueIDs)}")
        totalRevenue = 0.0
        for i in range(len(queueTotal)):
            totalRevenue += queueTotal[i]
        print(f"Total revenue:\t{currency}{totalRevenue}\n")
        try:
            inventoryChoice = input("[C] Close --> ")
            inventoryChoice = inventoryChoice.upper()
            match inventoryChoice.upper()[0]:
                case 'C': reportsLatch = False
                case _: print("Invalid Input")
        except:
            print("Invalid Input")

Start()

print("Goodbye!")
time.sleep(1.5)
clear()