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

    public class Hematite : Ore<IIron>
    {
        public Hematite(double weight, double concentration) : base(weight, concentration) { }
    }

    public class Magnetite : Ore<IIron>
    {
        public Magnetite(double weight, double concentration) : base(weight, concentration) { }
    }

    public class Chalcopyrite : Ore<ICopper>
    {
        public Chalcopyrite(double weight, double concentration) : base(weight, concentration) { }
    }

    public class Chalcocite : Ore<ICopper>
    {
        public Chalcocite(double weight, double concentration) : base(weight, concentration) { }
    }

    public class Bauxite : Ore<IAluminium>
    {
        public Bauxite(double weight, double concentration) : base(weight, concentration) { }
    }

    public class Corundum : Ore<IAluminium>
    {
        public Corundum(double weight, double concentration) : base(weight, concentration) { }
    }

    public class Zincite : Ore<IZinc>
    {
        public Zincite(double weight, double concentration) : base(weight, concentration) { }
    }

    public class Calamine : Ore<IZinc>
    {
        public Calamine(double weight, double concentration) : base(weight, concentration) { }
    }
}
