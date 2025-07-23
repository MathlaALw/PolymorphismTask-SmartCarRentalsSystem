namespace PolymorphismTask_SmartCarRentalsSystem
{
    internal class Program
    {

        static List<Vehicle> vehicles = new List<Vehicle>();
        static string vehicleFilePath = "vehicles.txt";

        static void Main(string[] args)
        {
            
            while (true)
            {
                Console.Clear();
                Console.WriteLine("SmartCar Rentals System");
                Console.WriteLine("1. Add a Vehicle");
                Console.WriteLine("2. View Available Vehicles");
                Console.WriteLine("3. Rent a Vehicle");
                Console.WriteLine("4. Exit");
                Console.Write("Choose an option: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        AddVehicle();
                        Pause();
                        break;
                    case "2":
                        ViewVehicles();
                        Pause();
                        break;
                    case "3":
                        RentVehicle();
                        Pause();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                       // Pause();
                        break;
                }
            }
        } // End of Main method

        // Method to add a new vehicle
        static void AddVehicle()
        {
            Console.WriteLine("\nChoose vehicle type to add:");
            Console.WriteLine("1. Car");
            Console.WriteLine("2. Truck");
            Console.WriteLine("3. Motorbike");
            Console.Write("Enter choice: ");
            string type = Console.ReadLine();

            Console.Write("Brand: ");
            string brand = Console.ReadLine();

            Console.Write("Model: ");
            string model = Console.ReadLine();

            Console.Write("Year: ");
            if (!int.TryParse(Console.ReadLine(), out int year))
            {
                Console.WriteLine("Invalid year.");
                return;
            }

            Console.Write("License Plate: ");
            string plate = Console.ReadLine();

            if (type == "1")
            {
                Console.Write("Is Luxury? (yes/no): ");
                bool isLuxury = Console.ReadLine().ToLower() == "yes";
                var car = new Car(brand, model, year, plate, isLuxury);
                vehicles.Add(car);
                SaveVehicleToFile(car);
            }
            else if (type == "2")
            {
                Console.Write("Max Load (kg): ");
                if (!double.TryParse(Console.ReadLine(), out double maxLoad))
                {
                    Console.WriteLine("Invalid load.");
                    return;
                }
                var truck = new Truck(brand, model, year, plate, maxLoad);
                vehicles.Add(truck);
                SaveVehicleToFile(truck);

            }
            else if (type == "3")
            {
                Console.Write("Requires Helmet? (yes/no): ");
                bool helmet = Console.ReadLine().ToLower() == "yes";
                var bike = new Motorbike(brand, model, year, plate, helmet);
                vehicles.Add(bike);
                SaveVehicleToFile(bike);
            }
            else
            {
                Console.WriteLine("Invalid vehicle type.");
            }


        }


        // Method to view all available vehicles
        static void ViewVehicles()
        {
            Console.WriteLine("\nAvailable Vehicles:");
            for (int i = 0; i < vehicles.Count; i++)
            {
                Vehicle v = vehicles[i];
                Console.Write($"{i + 1}. {v.Brand} {v.Model} {v.Year} | ");
                if (v is Car car)
                    Console.WriteLine($"Car | Luxury: {(car.IsLuxury ? "Yes" : "No")}");
                else if (v is Truck truck)
                    Console.WriteLine($"Truck | Max Load: {truck.MaxLoadKG}kg");
                else if (v is Motorbike bike)
                    Console.WriteLine($"Motorbike | Helmet Required: {(bike.RequiresHelmet ? "Yes" : "No")}");
            }
        }

        // Method to rent a vehicle
        static void RentVehicle()
        {


            ViewVehicles();

            Console.Write("\nEnter vehicle number to rent: ");
            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > vehicles.Count)
            {
                Console.WriteLine("Invalid selection.");
                return;
            }

            Vehicle selected = vehicles[choice - 1];
            Console.Write("Enter rental days: ");
            if (!int.TryParse(Console.ReadLine(), out int days) || days <= 0)
            {
                Console.WriteLine("Invalid number of days.");
                return;
            }

            double totalCost = 0;

            if (selected is Car car)
            {
                Console.Write("Need driver? (yes/no): ");
                string driverInput = Console.ReadLine().ToLower();
                bool withDriver = driverInput == "yes";
                totalCost = car.CalculateRentalCost(days, withDriver);
            }
            else if (selected is Truck truck)
            {
                Console.Write("Enter cargo weight (kg): ");
                if (!double.TryParse(Console.ReadLine(), out double weight))
                {
                    Console.WriteLine("Invalid weight.");
                    return;
                }
                totalCost = truck.CalculateRentalCost(days, weight);
            }
            else if (selected is Motorbike bike)
            {
                totalCost = bike.CalculateRentalCost(days);
            }

            Console.WriteLine($"\nTotal Rental Cost: ${totalCost:F2}");
        }


     

        static void Pause()
        {
            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }



        // Method to save vehicles to file
        static void SaveVehicleToFile(Vehicle vehicle)
        {
            using (StreamWriter writer = File.AppendText(vehicleFilePath))
            {
                if (vehicle is Car car)
                    writer.WriteLine($"Car|{car.Brand}|{car.Model}|{car.Year}|{car.LicensePlate}|{car.IsLuxury}");
                else if (vehicle is Truck truck)
                    writer.WriteLine($"Truck|{truck.Brand}|{truck.Model}|{truck.Year}|{truck.LicensePlate}|{truck.MaxLoadKG}");
                else if (vehicle is Motorbike bike)
                    writer.WriteLine($"Motorbike|{bike.Brand}|{bike.Model}|{bike.Year}|{bike.LicensePlate}|{bike.RequiresHelmet}");
            }
        }



        // Method to Load vehicles from file 
        static void LoadVehicles()
        {
            if (File.Exists(vehicleFilePath))
            {
                using (StreamReader reader = new StreamReader(vehicleFilePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split('|');
                        if (parts[0] == "Car")
                        {
                            vehicles.Add(new Car(parts[1], parts[2], int.Parse(parts[3]), parts[4], bool.Parse(parts[5])));
                        }
                        else if (parts[0] == "Truck")
                        {
                            vehicles.Add(new Truck(parts[1], parts[2], int.Parse(parts[3]), parts[4], double.Parse(parts[5])));
                        }
                        else if (parts[0] == "Motorbike")
                        {
                            vehicles.Add(new Motorbike(parts[1], parts[2], int.Parse(parts[3]), parts[4], bool.Parse(parts[5])));
                        }
                    }
                }
            }
        }



    }// End of Program class

    public class Vehicle
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string LicensePlate { get; set; }

        public Vehicle(string brand, string model, int year, string licensePlate)
        {
            Brand = brand;
            Model = model;
            Year = year;
            LicensePlate = licensePlate;
        }

        public virtual double CalculateRentalCost(int days)
        {
            return 50 * days;
        }

        public virtual double CalculateRentalCost(int days, bool withDriver)
        {
            double cost = CalculateRentalCost(days);
            if (withDriver)
                cost += 60;
            return cost;
        }
    }

    public class Car : Vehicle
    {
        public bool IsLuxury { get; set; }

        public Car(string brand, string model, int year, string licensePlate, bool isLuxury)
            : base(brand, model, year, licensePlate)
        {
            IsLuxury = isLuxury;
        }

        public override double CalculateRentalCost(int days)
        {
            
            double rate = 0;
            if (IsLuxury)
                rate = 80;
            else
                rate = 60;

            double cost = rate * days;
            if (days > 7)
                cost *= 0.9;
            return cost;
        }

        public override double CalculateRentalCost(int days, bool withDriver)
        {
            double cost = CalculateRentalCost(days);
            if (withDriver)
                cost += 60;
            return cost;
        }
    }

    public class Truck : Vehicle
    {
        // Property 
        public double MaxLoadKG { get; set; }
        // Constructor
        public Truck(string brand, string model, int year, string licensePlate, double maxLoadKG)
            : base(brand, model, year, licensePlate)
        {
            MaxLoadKG = maxLoadKG;
        }
        // Method to calculate rental cost
        public override double CalculateRentalCost(int days)
        {
            double cost = 100 * days;
            if (days > 7)
                cost *= 0.9;
            return cost;
        }

        // Overloaded method to calculate rental cost with cargo weight
        public double CalculateRentalCost(int days, double cargoWeight)
        {
            if (cargoWeight > MaxLoadKG)
            {
                Console.WriteLine("Error: cargo exceeds max load.");
                return 0;
            }

            double cost = CalculateRentalCost(days);
            cost += 50;
            return cost;
        }

    }
    public class Motorbike : Vehicle
    {
        // Property 
        public bool RequiresHelmet { get; set; }
        // Constructor
        public Motorbike(string brand, string model, int year, string licensePlate, bool requiresHelmet)
            : base(brand, model, year, licensePlate)
        {
            RequiresHelmet = requiresHelmet;
        }
        // Method to calculate rental cost
        public override double CalculateRentalCost(int days)
        {
            double cost = 40 * days;
            if (days > 7)
                cost *= 0.9;
            return cost;
        }

    }





} // End of namespace
