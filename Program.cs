using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW4
{
    class Program
    {
        static void Main(string[] args)
        {
            //Выведите платёжные ссылки для трёх разных систем платежа: 
            //pay.system1.ru/order?amount=12000RUB&hash={MD5 хеш ID заказа}
            //order.system2.ru/pay?hash={MD5 хеш ID заказа + сумма заказа}
            //system3.com/pay?amount=12000&curency=RUB&hash={SHA-1 хеш сумма заказа + ID заказа + секретный ключ от системы}

            Order order = new Order(4, 89);
            order.GetPayingLink(order);
            order.GetPayingLink(new PaymentSystemAmountOrder(8, 42));
            order.GetPayingLink(new SecretPaymentSystemAmountOrder(11, 2388));

            Console.ReadLine();
        }
    }

    public interface IPaymentSystem
    {
        void GetPayingLink(Order order);
    }

    public class Order : IPaymentSystem
    {
        public readonly string Url = "pay.system1.ru/order?amount=12000RUB&hash=";
        public readonly int Id;
        public readonly int Amount;

        public Order(int id, int amount) => (Id, Amount) = (id, amount);

        public void GetPayingLink(Order order)
        {
            order.GetDataPersone();
        }

        public virtual void GetDataPersone()
        {
            Console.WriteLine(Url + "{" + Id + "}");
        }
    }

    public class PaymentSystemAmountOrder : Order
    {
        public readonly string Url = "order.system2.ru/pay?hash=";

        public PaymentSystemAmountOrder(int id, int amount) : base(id, amount) { }

        public override void GetDataPersone()
        {
            Console.WriteLine(Url + "{" + Id + ", " + Amount + "}");
        }
    }

    public class SecretPaymentSystemAmountOrder : Order
    {
        public readonly string Url = "system3.com/pay?amount=12000&curency=RUB&hash=";
        private string _key = "Secret Key";

        public SecretPaymentSystemAmountOrder(int id, int amount) : base(id, amount) { }

        public override void GetDataPersone()
        {
            Console.WriteLine(Url + "{" + Id + ", " + Amount + ", " + _key + "}");
        }
    }
}
