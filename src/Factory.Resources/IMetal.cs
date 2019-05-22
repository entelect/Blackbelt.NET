namespace Factory.Resources
{
    public interface IMetal { }
    
    public interface IPureMetal : IMetal { }

    public interface IAlloy<TBase, TSolute> : IMetal
        where TBase : IMetal
        where TSolute : IMetal
    { }
    

    public interface IIron : IPureMetal { }

    public interface ICopper : IPureMetal { }
    
    public interface IAluminium : IPureMetal { }
    
    public interface IZinc : IPureMetal { }
    
    
    public interface IBronze : IAlloy<ICopper, IZinc> { }
    
    public interface ILightBronze : IAlloy<IBronze, IAluminium> { }
}