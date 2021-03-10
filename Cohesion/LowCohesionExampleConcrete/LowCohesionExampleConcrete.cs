using System.Collections.Generic;
using System.IO;

class StoreApplication
{
    private string _fileLocation;
    private string _lineSeparator;
    private List<Product> _availableProducts;
    
    //Constructor omitted for brevity.
    public void RefreshInventory()
    {
        File.Create(_fileLocation);
    }

    public void LoadInventory()
    {
        string[] lines = File.ReadAllLines(_fileLocation);
        foreach (string line in lines)
        {
            string[] productElements = line.Split(_lineSeparator);
            _availableProducts.Add(new Product(productElements));
        }
    }

    public Product GetProduct(string name)
    {
        foreach (var product in _availableProducts)
        {
            if (product.Name.Equals(name)) return product;
        }
        return null;
    }

    public List<Product> GetProductsCheaperThan(double price)
    {
        List<Product> retVal = new List<Product>();
        foreach (var product in _availableProducts)
        {
            if (product.Price < price) retVal.Add(product);
        }

        return retVal;
    }
}

internal class Product
{
    public string Name { get; }
    public double Price { get; }
    internal Product(string[] productElements)
    {

    }
}
//Active: While keeping all fields and methods, refactor the code to have one or more highly-cohesive classes.

//Reflective: Look at the code and think how it would change and expand as new functionality is added, including:
//1) Adding new products to the inventory and buying (removing) existing ones.
//2) Persisting each one product to a standalone file, where the inventory becomes a folder instead.
//3) Introducing a product category, enabling the user to retrieve cheap products of a particular category or to view all products of a category.
//4) Retrieving the product inventory from a remote application using HTTP.
//Which regions of the class change with the new requirements?