using System;
using System.Collections;
namespace FolderExample
{
    class MainApp
    {
        static void Main()
        {
            // Create a tree structure
            Component root = new Folder("Main Folder");
            string text = "Text here, also tree and ok\nFile manager I dare say";
            root.Add(new File("File A",text));
            root.Add(new File("File B",text));
            Component comp = new Folder("Folder X");
            comp.Add(new File("File XA",text));
            comp.Add(new File("File XB",text));
            root.Add(comp);
            root.Add(new File("File C",text));
            // Add and remove a leaf
            File leaf = new File("File D",text);
            root.Add(leaf);
            root.Remove(leaf);
            // Recursively display tree
            root.Display(1);
            // Wait for user
            Console.Read();
        }
    }
    // "Component"
    abstract class Component
    {
        protected string name;
        // Constructor
        public Component(string name)
        {
            this.name = name;
        }

        public abstract void Add(Component c);
        public abstract void Remove(Component c);
        public abstract void Display(int depth);
    }
    // "Folder"
    class Folder : Component
    {
        private ArrayList children = new ArrayList();
        // Constructor
        public Folder(string name)
            : base(name)
        {
        }

        public override void Add(Component component)
        {
            children.Add(component);
        }

        public override void Remove(Component component)
        {
            children.Remove(component);
        }

        public override void Display(int depth)
        {
            Console.WriteLine(new String('-', depth) + name);

            // Recursively display child nodes
            foreach (Component component in children)
            {
                component.Display(depth + 2);
            }
        }
    }

    // "File"
    class File : Component
    {
        string Text;
        // Constructor
        public File(string name,string text)
            : base(name)
        {
            Text = text;
        }

        public override void Add(Component c)
        {
            Console.WriteLine("Wrong");
        }

        public override void Remove(Component c)
        {
            Console.WriteLine("Cannot remove from a leaf");
        }

        public override void Display(int depth)
        {
            Console.WriteLine(new String('-', depth) + name);
            Console.Write(new String('-', depth + 1));
            foreach (char ch in Text)
            {
                if (ch == '\n')
                {
                    Console.Write(ch + new String('-', depth + 1));
                }
                else
                {
                    Console.Write(ch);
                }
            }
            Console.WriteLine();
        }

    }
}