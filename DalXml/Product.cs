using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal;

internal class Product : IProduct
{

    XElement productRoot;
    string productPath = @"..\xml\products.xml";
    /// <summary>
    /// build the product dal
    /// </summary>
    public Product()
    {
        if (!File.Exists(productPath))//not exist file 
        {
            CreateFiles();//create this file
        }
        else
            LoadData();//load data from xml
    }

    /// <summary>
    /// create a file with products tag
    /// </summary>
    private void CreateFiles()
    {
        productRoot = new XElement("products");
        productRoot.Save(productPath);
    }
    /// <summary>
    /// load data from product.xml
    /// </summary>
    private void LoadData()
    {
        try
        {
            productRoot = XElement.Load(productPath);
        }
        catch
        {
            Console.WriteLine("File");
        }
    }


    /// <summary>
    /// add product
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public int Add(DO.Product p)
    {
        LoadData();
        if (productExist(p.ID))
            throw new Duplication(p.ID, "product");

        XElement id = new XElement("id", p.ID);
        XElement name = new XElement("name", p.Name);
        XElement price = new XElement("price", p.Price);
        XElement category = new XElement("category", p.Category_);
        XElement instock = new XElement("inStock", p.InStock);

        productRoot.Add(new XElement("product", id, name, price, category, instock));
        productRoot.Save(productPath);
        return p.ID;
    }
    /// <summary>
    ///  the function checks that there is no product duplication
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="DO.MissingInputValue"></exception>
    private bool productExist(int id)
    {
        LoadData();
        List<DO.Product> products=new();
            products = (from p in productRoot.Elements()
                        select new DO.Product()
                        {
                            ID = Convert.ToInt32(p.Element("id")!.Value),
                            Name = p.Element("name")!.Value,
                            Price = Convert.ToInt32(p.Element("price")!.Value),
                            Category_ = (Category)Enum.Parse(typeof(Category), p.Element("category")!.Value ?? throw new DO.MissingInputValue("category")),
                            InStock = Convert.ToInt32(p.Element("inStock")!.Value)
                        }).ToList();
            if (products.Count(p => p.ID == id) > 0)
                return true;
            return false;
   
        }
    /// <summary>
    /// get product
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public DO.Product Get(int id)
    {
        LoadData();
        DO.Product product;
        try
        {
            product = (from p in productRoot.Elements()
                       where Convert.ToInt32(p.Element("id")!.Value) == id
                       select new DO.Product()
                       {
                           ID = Convert.ToInt32(p.Element("id")!.Value),
                           Name = p.Element("name")!.Value,
                           Price = Convert.ToInt32(p.Element("price")!.Value),
                           Category_ = (Category)Enum.Parse(typeof(Category), p.Element("category")!.Value ?? throw new DO.MissingInputValue("category")),
                           InStock = Convert.ToInt32(p.Element("inStock")!.Value)
                       }).FirstOrDefault();

        }
        catch
        {
            throw new NotExist(id, "product");
        }
        return product;
    }



    /// <summary>
    /// delete product
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public void Delete(int id)
    {
        LoadData();
        XElement productElement;
        try
        {
            productElement = (from p in productRoot.Elements()
                              where Convert.ToInt32(p.Element("id")!.Value) == id
                              select p).FirstOrDefault()!;
            productElement.Remove();
            productRoot.Save(productPath);
        }
        catch
        {
            throw new NotExist(id, "product");
        }
    }

    /// <summary>
    /// update product
    /// </summary>
    /// <param name="p"></param>
    /// <exception cref="Exception"></exception>
    public void Update(DO.Product pro)
    {
        LoadData();
        XElement productElement = (from p in productRoot.Elements()
                                   where Convert.ToInt32(p.Element("id")!.Value) == pro.ID
                                   select p).FirstOrDefault()!;
        productElement.Element("id")!.Value = (pro.ID).ToString();
        productElement.Element("name")!.Value = pro.Name!;
        productElement.Element("price")!.Value = (pro.Price).ToString();
        productElement.Element("category")!.Value = (pro.Category_).ToString()!;
        productElement.Element("inStock")!.Value = (pro.InStock).ToString();

        productRoot.Save(productPath);




    }

    /// <summary>
    /// get all products
    /// </summary>
    /// <returns></returns>
    public IEnumerable<DO.Product?> GetAll(Func<DO.Product?, bool>? Condition = null)
    {
        LoadData();
        List<DO.Product> products;
        try
        {
            products = (from p in productRoot.Elements()
                        select new DO.Product()
                        {
                            ID = Convert.ToInt32(p.Element("id")!.Value),
                            Name = p.Element("name")!.Value,
                            Price = Convert.ToInt32(p.Element("price")!.Value),
                            Category_ = (Category)Enum.Parse(typeof(Category), p.Element("category")!.Value ?? throw new DO.MissingInputValue("category")),
                            InStock = Convert.ToInt32(p.Element("inStock")!.Value)
                        }).ToList();

            if (Condition != null)
            {
                    products.Where(p => Condition(p));
            }

            return products.Count() == 0 ? null : products.Select(p=>(DO.Product?)p);
        }
        catch
        {
            products = null;
            return null;
        }

    }
    /// <summary>
    /// get product by condition
    /// </summary>
    /// <param name="Condition"></param>
    /// <returns></returns>
    /// <exception cref="DO.MissingInputValue"></exception>
    public DO.Product GetByCon(Func<DO.Product?, bool> Condition)
    {
        LoadData();
        IEnumerable<DO.Product> products;

        products = (from p in productRoot.Elements()
                    select new DO.Product()
                    {
                        ID = Convert.ToInt32(p.Element("id")!.Value),
                        Name = p.Element("name")!.Value,
                        Price = Convert.ToInt32(p.Element("price")!.Value),
                        Category_ = (Category)Enum.Parse(typeof(Category), p.Element("category")!.Value ?? throw new DO.MissingInputValue("category")),
                        InStock = Convert.ToInt32(p.Element("inStock")!.Value)
                    }).ToList();
        return products.FirstOrDefault(x => Condition(x));


    }

}

