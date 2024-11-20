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
productPrice = [100, 120, 140]

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
        orderIDs = []
        orderQty = []
        clear()
        print("CLIPOS v1.0 Python")
        print("Command Line Interface Point of Sale\n")
        print(businessName)

        try:
            inventoryChoice = input("[A] New Order | [Q] Queue | [E] Inventory | [R] Reports | [C] Close --> ")
            inventoryChoice = inventoryChoice.upper()
            match inventoryChoice.upper()[0]:
                #case 'A': NewOrder()
                #case 'Q': Queue()
                case 'E': Inventory()
                #case 'R': Reports()
                case 'C': startLatch = False
                case _: print("Invalid Input")
        except:
            print("Invalid Input")
            
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
            if (inventoryChoice == "C"):
                inventoryLatch = False
            else:
                print("Invalid Input")
            match inventoryChoice.upper():
                #case 'A': AddProduct()
                #case 'E': EditProduct()
                #case 'D': DeleteProduct()
                case 'C': inventoryLatch = False
                case _: print("Invalid Input")
        except:
            print("Invalid Input")

Start()

print("Goodbye!")
time.sleep(1.5)
clear()