namespace MVVMSample.Model.Products
{
    public class Car : Product
    {
        //required stuff:
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }

        //optional stuff:
        public bool HasGPS { get; set; }
        public bool HasSunroof { get; set; }

        public Car(string name, string make, string model, int year, string color) : base(name)
        {
            Make = make;
            Model = model;
            Year = year;
            Color = color;
        }
    }
}