using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSYFinalProject
{

    public class RentalManagement
    {
        CustomerManager customerManager = new CustomerManager();
        EquipmentManager equipmentManager = new EquipmentManager();
        private const string RentalFilePath = "rental.txt";
        private List<Rental> rentalList;

        public RentalManagement()
        {
            rentalList = new List<Rental>();
            LoadRentalData();
        }

        private void LoadRentalData()
        {
            try
            {
                string[] lines = File.ReadAllLines(RentalFilePath);
                foreach (string line in lines) 
                {
                    string[] parts = line.Split(',');
                    Rental rental = new Rental
                    {
                        rentalId = int.Parse(parts[0]),
                        date = DateTime.Parse(parts[1]),
                        customerId = int.Parse(parts[2]),
                        equipmentId = int.Parse(parts[3]),
                        rentalDate = DateTime.Parse(parts[4]),
                        returnDate = DateTime.Parse(parts[5]),
                        cost = decimal.Parse(parts[6])
                    };
                    rentalList.Add(rental);
                }
            }
            catch (FileNotFoundException)
            {
                // Handle file not found exception
                Console.WriteLine("Rental file not found.");
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine($"An error occurred while loading rental data: {ex.Message}");
            }
        }
        public void ProcessRental(CustomerManager customerManager, EquipmentManager equipmentManager)
        {
            // Get input from the user
            Console.WriteLine("\nEnter rental details:");
            Console.Write("Rental ID: ");
            int rentalId = int.Parse(Console.ReadLine());

            // Check if rental ID already exists
            if (rentalList.Any(r => r.rentalId == rentalId))
            {
                Console.WriteLine("Error: Rental ID already exists.");
                return;
            }

            Console.Write("Customer ID: ");
            int customerId = int.Parse(Console.ReadLine());

            // Check if the customer exists
            Customer customer = customerManager.customerList.FirstOrDefault(c => c.customerId == customerId);
            if (customer == null)
            {
                Console.WriteLine("Error: Customer not found.");
                return;
            }

            Console.Write("Equipment ID: ");
            int equipmentId = int.Parse(Console.ReadLine());

            // Check if the equipment exists
            RentalEquipment equipment = equipmentManager.GetEquipmentById(equipmentId);
            if (equipment == null)
            {
                Console.WriteLine("Error: Equipment not found.");
                return;
            }

            Console.Write("Rental Date (yyyy-MM-dd): ");
            DateTime rentalDate;
            if (!DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out rentalDate))
            {
                Console.WriteLine("Invalid date format. Please enter a valid date in yyyy-MM-dd format.");
                return;
            }

            Console.Write("Return Date (yyyy-MM-dd): ");
            DateTime returnDate;
            if (!DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out returnDate))
            {
                Console.WriteLine("Invalid date format. Please enter a valid date in yyyy-MM-dd format.");
                return;
            }


            // Calculate rental cost (e.g., based on daily rate and rental duration)
            decimal cost = CalculateRentalCost(rentalDate, returnDate, equipment.dailyRate);

            // Create a new rental object
            Rental newRental = new Rental
            {
                rentalId = rentalId,
                customerId = customerId,
                equipmentId = equipmentId,
                rentalDate = rentalDate,
                returnDate = returnDate,
                cost = cost
            };

            // Add the rental to the list and save the data
            rentalList.Add(newRental);
            SaveRentalData();

            Console.WriteLine("Rental processed successfully.");
        }

        public void DeleteRentalProcess(int rentalId)
        {
            Rental rental = rentalList.FirstOrDefault(r => r.rentalId == rentalId);
            if (rental != null)
            {
                rentalList.Remove(rental);
                SaveRentalData();
                Console.WriteLine("Rental process deleted successfully.");
            }
            else
            {
                Console.WriteLine("Rental process not found.");
            }
        }
        

        private decimal CalculateRentalCost(DateTime rentalDate, DateTime returnDate, decimal dailyRate)
        {
            // Implement the logic to calculate the rental cost based on the rental duration and daily rate
            TimeSpan rentalDuration = returnDate - rentalDate;
            int rentalDays = (int)Math.Ceiling(rentalDuration.TotalDays);
            return rentalDays * dailyRate;
        }
        public void DisplayAllRentals()
        {
            Console.WriteLine("All Rentals:");
            foreach (Rental rental in rentalList)
            {
                Console.WriteLine($"Rental ID: {rental.rentalId}");
                Console.WriteLine($"Customer ID: {rental.customerId}");
                Console.WriteLine($"Equipment ID: {rental.equipmentId}");
                Console.WriteLine($"Rental Date: {rental.rentalDate.ToShortDateString()}"); // Display only the date part
                Console.WriteLine($"Return Date: {rental.returnDate.ToShortDateString()}"); // Display only the date part
                Console.WriteLine($"Cost: {rental.cost}");
                Console.WriteLine();
            }
        }


        public void SaveRentalData()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(RentalFilePath))
                {
                    foreach (Rental rental in rentalList)
                    {
                        writer.WriteLine($"{rental.rentalId},{rental.date},{rental.customerId},{rental.equipmentId},{rental.rentalDate},{rental.returnDate},{rental.cost}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while saving rental data: {ex.Message}");
            }
        }
    }
}
