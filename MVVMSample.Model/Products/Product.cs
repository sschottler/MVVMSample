namespace MVVMSample.Model.Products
{
    public abstract class Product
    {
        public string Name { get; set; }
        public string Description { get; set; }

        protected Product(string name)
        {
            Name = name;
        }
    }
}