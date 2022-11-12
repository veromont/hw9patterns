using System;
using System.Collections.Generic;

namespace Decorator.Examples
{
    class MainApp
    {
        static void Main()
        {
            // Create ConcreteComponent and two Decorators
            ChristmasBulb bluebulb = new ChristmasBulb(Colour.Purple);
            ChristmasBulb redbulb = new ChristmasBulb(Colour.Red);
            ChristmasBulb greenbulb = new ChristmasBulb(Colour.Green);
            ChristmasBulb whitebulb = new ChristmasBulb(Colour.White);
            ChristmasGarland merrygarland = new ChristmasGarland(Colour.Yellow);
            Fir Tree = new Fir();
            ChristmasFir ChristmasTree = new ChristmasFir(Tree);

            // Link decorators
            Console.WriteLine("No components: ");
            ChristmasTree.ShowOff();
            
            Console.WriteLine();
            Console.WriteLine("Some components: ");
            ChristmasTree.SetBulb(greenbulb);
            ChristmasTree.SetBulb(bluebulb);
            ChristmasTree.ShowOff();

            Console.WriteLine();
            Console.WriteLine("All components: ");
            ChristmasTree.SetBulb(redbulb);
            ChristmasTree.SetBulb(whitebulb);
            ChristmasTree.WrapGarland(merrygarland);
            ChristmasTree.InstallStar();
            ChristmasTree.ShowOff();

            // Wait for user
            Console.Read();
        }
    }
    enum Colour
    {
        Red,
        Green,
        Blue,
        White,
        Yellow,
        Purple
    }
    // "Component"
    abstract class TreeToy
    {
        public Colour MyColour { get; protected set; }
        public abstract void ShowOff();
    }

    // "ConcreteComponent"
    class ChristmasBulb : TreeToy
    {
        public override void ShowOff()
        {
            Console.WriteLine("Me is Chritstmas bulb, my color is {0}", MyColour);
        }
        public ChristmasBulb(Colour colour) { MyColour = colour; }
    }
    // Concrete component 2
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
    // "Decorator"
    class Fir
    {
        public void ShowOff()
        {
            Console.WriteLine("Me is Fir, and I am wonderful!");
        }
    }
    abstract class Decorator
    {
        public Fir Tree;
        protected List<ChristmasBulb> Bulbs;
        public ChristmasGarland Garland;
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
        public void WrapGarland(ChristmasGarland garland)
        {
            Garland = garland;
        }
        public virtual bool ShowOff()
        {
            if (Bulbs == null)
            {
                Console.WriteLine("No christmas tree can be showed off without bulbs, first install any");
                return false;
            }
            Tree.ShowOff();
            foreach (ChristmasBulb b in Bulbs)
            {
                b.ShowOff();
            }
            if (Garland != null)
            {
                if (!Garland.lightened) { Garland.SwitchLight(); }
                Garland.ShowOff();
            }
            return true;
        }
    }

    // "ConcreteDecoratorA"
    class ChristmasFir : Decorator
    {
        protected bool StarInstalled;
        public ChristmasFir(Fir tree) : base(tree)
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
}