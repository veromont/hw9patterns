using System;
using System.Collections.Generic;

namespace Decorator.Examples
{
    enum Colour
    {
        Red,
        Green,
        Blue,
        White,
        Yellow,
        Purple
    }


    class MainApp
    {
        static void Main()
        {
            // Create ConcreteToys and two Decorators
            ChristmasBulb bluebulb = new ChristmasBulb(Colour.Purple);
            ChristmasBulb redbulb = new ChristmasBulb(Colour.Red);
            ChristmasBulb greenbulb = new ChristmasBulb(Colour.Green);
            ChristmasBulb whitebulb = new ChristmasBulb(Colour.White);
            ChristmasGarland merrygarland = new ChristmasGarland(Colour.Yellow);
            Fir Tree = new Fir();
            GarlandFir GarlandTree = new GarlandFir(Tree);
            StarFir StarTree = new StarFir(Tree);

            // Link decorators
            Console.WriteLine("No components: ");
            GarlandTree.ShowOff();

            Console.WriteLine();
            Console.WriteLine("Garland tree components: ");
            GarlandTree.SetBulb(greenbulb);
            GarlandTree.SetBulb(bluebulb);
            GarlandTree.WrapGarland(merrygarland);
            GarlandTree.ShowOff();

            Console.WriteLine();
            Console.WriteLine("Star tree components: ");
            StarTree.SetBulb(redbulb);
            StarTree.SetBulb(whitebulb);
            StarTree.InstallStar();
            StarTree.ShowOff();

            // Wait for user
            Console.Read();
        }
    }
    // "Toys"
    abstract class TreeToy
    {
        public Colour MyColour { get; protected set; }
        public abstract void ShowOff();
    }

    // "ConcreteToys"
    class ChristmasBulb : TreeToy
    {
        public override void ShowOff()
        {
            Console.WriteLine("Me is Chritstmas bulb, my color is {0}", MyColour);
        }
        public ChristmasBulb(Colour colour) { MyColour = colour; }
    }
    // Concrete Toy 2
    class ChristmasGarland : TreeToy
    {
        public bool lightened;
        public override void ShowOff()
        {
            Console.Write("Me is Chritstmas garland, my color is {0}, I am ", MyColour);
            if (lightened)
            {
                Console.WriteLine("lightened up");
            }
            else
            {
                Console.WriteLine("not lightened up");
            }
        }
        public void SwitchLight()
        {
            if (lightened) { lightened = false; }
            else { lightened = true; }
        }
        public ChristmasGarland(Colour colour) { MyColour = colour; lightened = false; }
    }
    // "Component"
    class Fir
    {
        public virtual bool ShowOff()
        {
            Console.WriteLine("Me is Fir, and I am wonderful!");
            return true;
        }
    }

    //abstract decorator
    abstract class Decorator : Fir
    {
        protected Fir Tree;
        protected List<ChristmasBulb> Bulbs;

        public Decorator(Fir Sosna)
        {
            Tree = Sosna;
        }
        public void SetBulb(ChristmasBulb component)
        {
            if (Bulbs == null)
            {
                Bulbs = new List<ChristmasBulb>();
            }
            if (component != null)
            {
                Bulbs.Add(component);
            }
        }
        public override bool ShowOff()
        {
            if (Bulbs == null)
            {
                Console.WriteLine("No christmas tree can be showed off without bulbs, first install any");
                return false;
            }
            base.ShowOff();
            foreach (ChristmasBulb b in Bulbs)
            {
                b.ShowOff();
            }
            return true;
        }
    }

    // "ConcreteDecoratorA"
    class StarFir : Decorator
    {
        protected bool StarInstalled;
        public StarFir(Fir tree) : base(tree)
        {
        }
        public override bool ShowOff()
        {
            Console.WriteLine("Our tree components: ");
            if (!base.ShowOff())
            {
                return false;
            }
            if (StarInstalled)
            {
                Console.WriteLine("Me is the Star, I am on the top of the tree");
            }
            else
            {
                Console.WriteLine("No star installed, the tree is incomplete");
            }
            return true;
        }
        public void InstallStar() { StarInstalled = true; }
    }

    //Concrete decorator B
    class GarlandFir : Decorator
    {
        public ChristmasGarland? Garland { get; private set; }
        public GarlandFir(Fir tree) : base(tree)
        {
        }
        public override bool ShowOff()
        {
            Console.WriteLine("Our tree components: ");
            if (!base.ShowOff())
            {
                return false;
            }
            if (Garland == null)
            {
                Console.WriteLine("No garland wrapped");
                return false;
            }
            else
            {
                Garland.ShowOff();
            }
            return true;
        }
        public void WrapGarland(ChristmasGarland garland)
        {
            Garland = garland;
        }
    }
}
