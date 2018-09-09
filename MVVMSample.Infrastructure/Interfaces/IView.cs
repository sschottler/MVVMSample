namespace MVVMSample.Infrastructure.Interfaces
{
    /// <summary>
    /// Allows us to mock views when testing without having to open actual windows:
    /// </summary>
    public interface IView
    {
        object DataContext { get; set; }
    }
}