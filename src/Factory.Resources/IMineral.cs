namespace Factory.Resources
{
    public interface IMineral
    {
        double Concentration { get; }
    }

    public interface IIron : IMineral { }

    public interface ICopper : IMineral { }
    
    public interface IAluminium : IMineral { }
    
    public interface IZinc : IMineral { }
}
