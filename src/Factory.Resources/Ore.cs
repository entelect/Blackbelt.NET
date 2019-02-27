namespace Factory.Resources
{
    public abstract class Ore
    {
        public float Weight { get; }

        internal Ore(float weight)
        {
            this.Weight = weight;
        }
    }

    public class Hematite : Ore, IIron
    {
        public Hematite(float weight, float concentration) : base(weight)
        {
            this.Concentration = concentration;
        }

        public float Concentration { get; }
    }

    public class Magnetite : Ore, IIron
    {
        public Magnetite(float weight, float concentration) : base(weight)
        {
            this.Concentration = concentration;
        }

        public float Concentration { get; }
    }

    public class Chalcopyrite : Ore, ICopper
    {
        public Chalcopyrite(float weight, float concentration) : base(weight)
        {
            this.Concentration = concentration;
        }

        public float Concentration { get; }
    }

    public class Chalcocite : Ore, ICopper
    {
        public Chalcocite(float weight, float concentration) : base(weight)
        {
            this.Concentration = concentration;
        }

        public float Concentration { get; }
    }

    public class Bauxite : Ore, IAluminium
    {
        public Bauxite(float weight, float concentration) : base(weight)
        {
            this.Concentration = concentration;
        }

        public float Concentration { get; }
    }

    public class Corundum : Ore, IAluminium
    {
        public Corundum(float weight, float concentration) : base(weight)
        {
            this.Concentration = concentration;
        }

        public float Concentration { get; }
    }

    public class Zincite : Ore, IZinc
    {
        public Zincite(float weight, float concentration) : base(weight)
        {
            this.Concentration = concentration;
        }

        public float Concentration { get; }
    }

    public class Calamine : Ore, IZinc
    {
        public Calamine(float weight) : base(weight) { }

        public float Concentration { get; }
    }
}
