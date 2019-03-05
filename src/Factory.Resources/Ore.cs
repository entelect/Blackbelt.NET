namespace Factory.Resources
{
    public abstract class Ore
    {
        public double Weight { get; }

        internal Ore(double weight)
        {
            this.Weight = weight;
        }
    }

    public class Hematite : Ore, IIron
    {
        public Hematite(double weight, double concentration) : base(weight)
        {
            this.Concentration = concentration;
        }

        public double Concentration { get; }
    }

    public class Magnetite : Ore, IIron
    {
        public Magnetite(double weight, double concentration) : base(weight)
        {
            this.Concentration = concentration;
        }

        public double Concentration { get; }
    }

    public class Chalcopyrite : Ore, ICopper
    {
        public Chalcopyrite(double weight, double concentration) : base(weight)
        {
            this.Concentration = concentration;
        }

        public double Concentration { get; }
    }

    public class Chalcocite : Ore, ICopper
    {
        public Chalcocite(double weight, double concentration) : base(weight)
        {
            this.Concentration = concentration;
        }

        public double Concentration { get; }
    }

    public class Bauxite : Ore, IAluminium
    {
        public Bauxite(double weight, double concentration) : base(weight)
        {
            this.Concentration = concentration;
        }

        public double Concentration { get; }
    }

    public class Corundum : Ore, IAluminium
    {
        public Corundum(double weight, double concentration) : base(weight)
        {
            this.Concentration = concentration;
        }

        public double Concentration { get; }
    }

    public class Zincite : Ore, IZinc
    {
        public Zincite(double weight, double concentration) : base(weight)
        {
            this.Concentration = concentration;
        }

        public double Concentration { get; }
    }

    public class Calamine : Ore, IZinc
    {
        public Calamine(double weight) : base(weight) { }

        public double Concentration { get; }
    }
}
