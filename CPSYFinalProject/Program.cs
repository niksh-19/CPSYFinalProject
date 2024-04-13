namespace CPSYFinalProject
{
    public class Program
    {
        static void Main(string[] args)
        {
            EquipmentManager equipmentManager = new EquipmentManager();
            CustomerManager customerManager = new CustomerManager();
            RentalManagement rentalManagement = new RentalManagement();

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Add new equipment");
                Console.WriteLine("2. Delete equipment");
                Console.WriteLine("3. Display all equipment");
                Console.WriteLine("4. Add new customer");
                Console.WriteLine("5. Display all customers");
                Console.WriteLine("6. Process rental");
                Console.WriteLine("7. Display Rentals");
                Console.WriteLine("8. Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        equipmentManager.AddEquipment();
                        break;
                    case "2":
                        equipmentManager.DeleteEquipment();
                        break;
                    case "3":
                        equipmentManager.DisplayAllEquipment();
                        break;
                    case "4":
                        customerManager.AddCustomer();
                        break;
                    case "5":
                        customerManager.DisplayAllCustomers();
                        break;
                    case "6":
                        rentalManagement.ProcessRental(customerManager,equipmentManager);
                        break;
                    case "7":
                        rentalManagement.DisplayAllRentals();
                        break;
                    case "8":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a number from 1 to 7.");
                        break;
                }
            }
        }
    }
}

