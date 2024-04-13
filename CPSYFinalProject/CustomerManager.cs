using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSYFinalProject
{
    public class CustomerManager
    {
        private const string CustomerFilePath = "customer.txt";
        public List<Customer> customerList;

        public CustomerManager()
        {
            customerList = new List<Customer>();
            LoadCustomerData();
        }

        private void LoadCustomerData()
        {
            try
            {
                string[] lines = File.ReadAllLines(CustomerFilePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    Customer customer = new Customer
                    {
                        customerId = int.Parse(parts[0]),
                        lastName = parts[1],
                        firstName = parts[2],
                        contactPhone = parts[3],
                        Email = parts[4]
                    };
                    customerList.Add(customer);
                }
            }
            catch (FileNotFoundException)
            {
                // Handle file not found exception
                Console.WriteLine("Customer file not found.");
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine($"An error occurred while loading customer data: {ex.Message}");
            }
        }

        public void AddCustomer()
        {
            Customer newCustomer = new Customer();

            Console.WriteLine("\nEnter customer details:");
            Console.Write("CustomerId: ");
            int customerId = int.Parse(Console.ReadLine());

            if (customerList.Any(c => c.customerId == customerId))
            {
                Console.WriteLine("Error: Customer with the same ID already exists.");
                return;
            }

            newCustomer.customerId = customerId;

            Console.Write("Last Name: ");
            newCustomer.lastName = Console.ReadLine();
            Console.Write("First Name: ");
            newCustomer.firstName = Console.ReadLine();
            Console.Write("Contact Phone: ");
            newCustomer.contactPhone = Console.ReadLine();
            Console.Write("Email: ");
            newCustomer.Email = Console.ReadLine();

            // Add the new customer to the list
            customerList.Add(newCustomer);

            // Save the updated customer data
            SaveCustomerData();

            Console.WriteLine("Customer added successfully.");
        }

        private void SaveCustomerData()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(CustomerFilePath))
                {
                    foreach (Customer customer in customerList)
                    {
                        writer.WriteLine($"{customer.customerId},{customer.lastName},{customer.firstName},{customer.contactPhone},{customer.Email}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while saving customer data: {ex.Message}");
            }
        }
            public void DisplayAllCustomers()
            {
                Console.WriteLine("All Customers:");
                foreach (Customer customer in customerList)
                {
                    Console.WriteLine($"Customer ID: {customer.customerId}");
                    Console.WriteLine($"Last Name: {customer.lastName}");
                    Console.WriteLine($"First Name: {customer.firstName}");
                    Console.WriteLine($"Contact Phone: {customer.contactPhone}");
                    Console.WriteLine($"Email: {customer.Email}");
                    Console.WriteLine();
                }
            }
        }
    }

