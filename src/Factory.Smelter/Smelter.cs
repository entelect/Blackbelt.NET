using Factory.Resources;

namespace Factory.Smelter
{
    public class Smelter
    {
        delegate IronIngot Smelt(Hematite oreItem);
    }
}
