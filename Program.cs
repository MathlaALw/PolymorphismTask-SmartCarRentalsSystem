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


} // End of namespace
