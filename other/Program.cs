using System;
using System.IO;
using System.Linq;

class ImageDeleter
{
    class Program
    {

        static void Main()
        {
            ProductFactory productFactory = new ProductFactory();
            WarpFactory warpFactory = new WarpFactory();

            Func<Product> func1 = new Func<Product>(productFactory.MakePizza);
            Func<Product> func2 = new Func<Product>(productFactory.MakeToyCar);

            Logger logger = new Logger();
            Action<Product> log = new Action<Product>(logger.Log);

            Box box1 = warpFactory.WrapProduct(func1, log);
            Box box2 = warpFactory.WrapProduct(func2, log);

            Console.WriteLine(box1.Product.Name);
            Console.WriteLine(box2.Product.Name);
        }
    }

    class Logger
    {
        public void Log(Product product)
        {
            Console.WriteLine(product.Price);
        }
    }

    class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }
    }

    class Box
    {
        public Product Product { get; set; }
    }

    class WarpFactory
    {
        public Box WrapProduct(Func<Product> getProdcut, Action<Product> logCallback)
        {
            Box box = new Box();
            Product product = getProdcut?.Invoke();
            if (product.Price > 50)
                logCallback(product);

            box.Product = product;
            return box;
        }
    }

    class ProductFactory
    {
        public Product MakePizza()
        {
            Product product = new Product();
            product.Name = "Pizza";
            product.Price = 12;
            return product;
        }

        public Product MakeToyCar()
        {
            Product product = new Product();
            product.Name = "toy car";
            product.Price = 1000;
            return product;
        }
    }

}
