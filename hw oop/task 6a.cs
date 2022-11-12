using System;
namespace AdapterExample
{
    // Система яку будемо адаптовувати
    class OldCurrency
    {
        public readonly string currency;
        double sum;
        public OldCurrency(double money)
        {
            sum = money;
            currency = "tugriki";
        }
        public double MoneyInTugriks()
        {
            return sum;
        }
    }
    interface INewCurrency
    {
        public double MoneyInTalants();
    }

    // Ну і власне сама розетка у новій квартирі
    class NewCurrency : INewCurrency
    {
        public string currency { get; private set; }
        double sum;
        public NewCurrency(double money)
        {
            sum = money;
            currency = "talant";
        }
        public double MoneyInTalants()
        {
            return sum;
        }
    }
    class Adapter : INewCurrency
    {
        // Але всередині він старий
        private readonly OldCurrency _adaptee;
        public Adapter(OldCurrency adaptee)
        {
            _adaptee = adaptee;
        }

        // А тут відбувається вся магія: наш адаптер «перекладає»
        // функціональність із нового стандарту на старий
        public double MoneyInTalants()
        {
            // Якщо б була різниця 
            // то тут ми б помістили трансформатор
            return _adaptee.MoneyInTugriks() * 0.5;
        }
    }

    class Client
    {
        // Зарядний пристрій, який розуміє тільки нову систему
        public static void CountMoney(INewCurrency money)
        {
            Console.WriteLine(money.MoneyInTalants());
        }
    }

    public class AdapterDemo
    {
        static void Main()
        {
            var talFromTal = new NewCurrency(12.23);
            Client.CountMoney(talFromTal);
            var talFromTugr = new OldCurrency(56.34);
            var adapter = new Adapter(talFromTugr);
            Client.CountMoney(adapter);
            Console.ReadKey();
        }
    }
}