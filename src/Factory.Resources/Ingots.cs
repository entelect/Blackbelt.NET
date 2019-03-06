using System.Threading;

namespace Factory.Resources
{
    public abstract class Ingot
    {
        private double weight;
        private bool isCooled;

        protected Ingot()
        {
            this.weight = 0;
            this.isCooled = false;
        }

        public double Weight => Weight;

        public bool IsCooled => this.isCooled;

        public void AddWeight(double weight)
        {
            this.weight += weight;
        }

        public void Cool()
        {
            Thread.Sleep(20);
            this.isCooled = true;
        }
    }

    public interface IIngot<TMineral>
        where TMineral : IMineral { }

    public interface IAlloy<TBase, TSolute>
        where TBase : Ingot
        where TSolute : Ingot { }

    public class IronIngot : Ingot, IIngot<IIron>
    {
    }

    public class CopperIngot : Ingot, IIngot<ICopper> { }

    public class AluminiumIngot : Ingot, IIngot<IAluminium> { }

    public class ZincIngot : Ingot, IIngot<IZinc> { }

    public class BronzeIngot : Ingot, IAlloy<CopperIngot, ZincIngot> { }
    
    public class LightBronzeIngot : Ingot, IAlloy<BronzeIngot, AluminiumIngot> { }
}
