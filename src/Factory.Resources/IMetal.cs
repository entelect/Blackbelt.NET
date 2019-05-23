using System;
using System.Reflection;

namespace Factory.Resources
{
    public interface IMetal { }
    
    public interface IPureMetal : IMetal { }

    public interface IAlloy<TBase, TSolute> : IMetal
        where TBase : IMetal
        where TSolute : IMetal
    { }


    public class Iron : IPureMetal
    {
        internal Iron() { }
    }

    public class Copper : IPureMetal
    {
        internal Copper() { }
    }

    public class Aluminium : IPureMetal
    {
        internal Aluminium() { }
    }

    public class Zinc : IPureMetal
    {
        internal Zinc() { }
    }

    public class Bronze : IAlloy<Copper, Zinc>
    {
        internal Bronze() { }
    }

    public class LightBronze : IAlloy<Bronze, Aluminium>
    {
        internal LightBronze() { }
    }

    public static class ForgeHeatingTime
    {
        private const int iron = 1000;
        private const int copper = 1500;
        private const int aluminium = 2000;
        private const int zinc = 800;
        private static readonly int bronze = Math.Max(copper, zinc);
        private static readonly int lightBronze = Math.Max(bronze, aluminium);

        public static int Get<TMetal>()
            where TMetal : IMetal
        {
            // This reflection is here to serve as helper in looking up the heating times according to type.
            var obj = Activator.CreateInstance(typeof(TMetal),
                BindingFlags.CreateInstance |
                BindingFlags.NonPublic |
                BindingFlags.Instance,
                null, null, null);

            if(obj is Iron)
            {
                return iron;
            }

            if(obj is Copper)
            {
                return copper;
            }

            if(obj is Aluminium)
            {
                return aluminium;
            }

            if(obj is Zinc)
            {
                return zinc;
            }

            if(obj is Bronze)
            {
                return bronze;
            }

            if(obj is LightBronze)
            {
                return lightBronze;
            }
            
            throw new NotImplementedException($"Heating time lookup for type {typeof(TMetal).Name} has not been implemented");
        }
    }
}
