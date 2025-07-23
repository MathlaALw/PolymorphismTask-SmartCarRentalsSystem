namespace PolymorphismTask_SmartCarRentalsSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        } // End of Main method
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
