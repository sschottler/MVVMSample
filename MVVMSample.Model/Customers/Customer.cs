namespace MVVMSample.Model.Customers
{
    public class Customer
    {
        //required fields:
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //optional fields:
        public int Age { get; set; }
        public string City { get; set; }

        public Customer(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}