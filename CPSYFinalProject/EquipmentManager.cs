using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSYFinalProject
{
    public class EquipmentManager
    {
        private const string EquipmentFilePath = "equipment.txt";
        private List<RentalEquipment> equipmentList;

        public EquipmentManager()
        {
            equipmentList = new List<RentalEquipment>();
            LoadEquipmentData();
        }

        private void LoadEquipmentData()
        {
            try
            {
                string[] lines = File.ReadAllLines(EquipmentFilePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    RentalEquipment equipment = new RentalEquipment
                    {
                        equipmentId = int.Parse(parts[0]),
                        categoryId = int.Parse(parts[1]),
                        name = parts[2],
                        description = parts[3],
                        dailyRate = decimal.Parse(parts[4])
                    };
                    equipmentList.Add(equipment);
                }
            }
            catch (FileNotFoundException)
            {
                // Handle file not found exception
                Console.WriteLine("Equipment file not found.");
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine($"An error occurred while loading equipment data: {ex.Message}");
            }
        }

        public void AddEquipment()
        {
            Console.WriteLine("\nEnter equipment details:");
            Console.Write("Equipment ID: ");
            int equipmentId;
            if (!int.TryParse(Console.ReadLine(), out equipmentId))
            {
                Console.WriteLine("Invalid input. Please enter a valid equipment ID.");
                return;
            }

            // Check if equipment with the same ID already exists
            if (GetEquipmentById(equipmentId) != null)
            {
                Console.WriteLine("Error: Equipment with the same ID already exists.");
                return;
            }

            Console.Write("Category ID: ");
            int categoryId;
            if (!int.TryParse(Console.ReadLine(), out categoryId))
            {
                Console.WriteLine("Invalid input. Please enter a valid category ID.");
                return;
            }

            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Description: ");
            string description = Console.ReadLine();

            Console.Write("Daily Rate: ");
            decimal dailyRate;
            if (!decimal.TryParse(Console.ReadLine(), out dailyRate))
            {
                Console.WriteLine("Invalid input. Please enter a valid daily rate.");
                return;
            }

            RentalEquipment newEquipment = new RentalEquipment
            {
                equipmentId = equipmentId,
                categoryId = categoryId,
                name = name,
                description = description,
                dailyRate = dailyRate
            };

            equipmentList.Add(newEquipment);
            SaveEquipmentData();
            Console.WriteLine("Equipment added successfully.");
            DisplayAllEquipment();
        }


        public void DeleteEquipment()
        {
            Console.Write("\nEnter equipment ID to delete: ");
            int equipmentId;
            if (!int.TryParse(Console.ReadLine(), out equipmentId))
            {
                Console.WriteLine("Invalid input. Please enter a valid equipment ID.");
                return;
            }

            RentalEquipment equipment = equipmentList.Find(e => e.equipmentId == equipmentId);
            if (equipment != null)
            {
                equipmentList.Remove(equipment);
                SaveEquipmentData();
                Console.WriteLine("Equipment deleted successfully.");
                DisplayAllEquipment();
            }
            else
            {
                Console.WriteLine("Equipment not found.");
            }
        }


        private void SaveEquipmentData()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(EquipmentFilePath))
                {
                    foreach (RentalEquipment equipment in equipmentList)
                    {
                        writer.WriteLine($"{equipment.equipmentId},{equipment.categoryId},{equipment.name},{equipment.description},{equipment.dailyRate}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while saving equipment data: {ex.Message}");
            }
        }

        public void DisplayAllEquipment()
        {
            Console.WriteLine("ID   Name                 Category ID   Description                                      Daily Rate");
            foreach (RentalEquipment equipment in equipmentList)
            {
                Console.WriteLine($"{equipment.equipmentId,-4} {equipment.name,-20} {equipment.categoryId,-12} {equipment.description,-50} {equipment.dailyRate,10:F2}");
            }
        }




        public RentalEquipment GetEquipmentById(int equipmentId)
        {
            return equipmentList.FirstOrDefault(e => e.equipmentId == equipmentId);
        }

    }
}
