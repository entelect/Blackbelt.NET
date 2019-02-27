namespace Factory.Resources
{
    public interface IMineral
    {
        float Concentration { get; }
    }
    
    public interface ITraceMineral<T>
        where T : IMineral
    { }

    public interface IIron : IMineral { }

    public interface ICopper : IMineral { }
    
    public interface IAluminium : IMineral { }
    
    public interface IZinc : IMineral { }
}
