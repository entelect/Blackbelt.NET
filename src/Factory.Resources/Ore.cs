namespace Factory.Resources
{
    public abstract class Ore<TMetal>
        where TMetal: IPureMetal
    {
        public double Weight { get; }

        public double Concentration { get; }

        internal Ore(double weight, double concentration)
        {
            this.Weight = weight;
            this.Concentration = concentration;
        }
    }

    public class Hematite : Ore<Iron>
    {
        internal Hematite(double weight, double concentration) : base(weight, concentration) { }
    }

    public class Magnetite : Ore<Iron>
    {
        internal Magnetite(double weight, double concentration) : base(weight, concentration) { }
    }

    public class Chalcopyrite : Ore<Copper>
    {
        internal Chalcopyrite(double weight, double concentration) : base(weight, concentration) { }
    }

    public class Chalcocite : Ore<Copper>
    {
        internal Chalcocite(double weight, double concentration) : base(weight, concentration) { }
    }

    public class Bauxite : Ore<Aluminium>
    {
        internal Bauxite(double weight, double concentration) : base(weight, concentration) { }
    }

    public class Corundum : Ore<Aluminium>
    {
        internal Corundum(double weight, double concentration) : base(weight, concentration) { }
    }

    public class Zincite : Ore<Zinc>
    {
        internal Zincite(double weight, double concentration) : base(weight, concentration) { }
    }

    public class Calamine : Ore<Zinc>
    {
        internal Calamine(double weight, double concentration) : base(weight, concentration) { }
    }
}
