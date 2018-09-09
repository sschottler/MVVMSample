namespace MVVMSample.Model.Products
{
    public class Boat : Product
    {
        //required stuff:
        public BoatEngineType EngineType { get; set; }
        public int Length { get; set; }
     
        //optional stuff:
        public bool HasJoystick { get; set; }
        public bool HasDTS { get; set; }
        public int TopSpeed { get; set; }

        public Boat(string name, BoatEngineType engineType, int length) : base(name)
        {
            EngineType = engineType;
            Length = length;
        }
    }
}