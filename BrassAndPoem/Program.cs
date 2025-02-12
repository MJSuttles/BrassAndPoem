﻿using System;
using System.Runtime.InteropServices;

List<ProductType> productTypes = new List<ProductType>
{
    new ProductType() { Title = "Book", Id = 1 },
    new ProductType() { Title = "Musical Instrument", Id = 2 }
};

List<Product> products = new List<Product>
{
    new Product() { Name = "The Sun and Her Flowers", Price = 14.99m, ProductType = productTypes[0],ProductTypeId = 100 },
    new Product() { Name = "Golden Melody Acoustic Guitar", Price = 249.99m, ProductType = productTypes[0], ProductTypeId = 200 },
    new Product() { Name = "Leaves of Grass", Price = 10.75m, ProductType = productTypes[0], ProductTypeId = 101 },
    new Product() { Name = "SilverTone Alto Saxophone", Price = 599.50m, ProductType = productTypes[0], ProductTypeId = 201 },
    new Product() { Name = "Devotions: The Selected Poems of Mary Oliver", ProductType = productTypes[0], Price = 18.50m, ProductTypeId = 102 },
    new Product() { Name = "HarmonyKeys Digital Piano", Price = 429.00m, ProductType = productTypes[0], ProductTypeId = 202 }
};


void DisplayMenu(string greeting)
{
    string choice = null;
    while (choice != "5")
    {
        Console.WriteLine(greeting);
        Console.WriteLine(@"Choose an option:
                            1. Display all products
                            2. Delete a product
                            3. Add a new product
                            4. Update product properties
                            5. Exit");
        choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                DisplayAllProducts(products, productTypes);
                break;

            case "2":
                DeleteProduct(products, productTypes);
                break;

            case "3":
                AddProduct(products, productTypes);
                break;

            case "4":
                UpdateProduct(products, productTypes);
                break;

            case "5":
                Console.WriteLine("Goodbye!");
                break;
        }
    }
}

string greeting = @"Welcome to BrassAndPoem -
Your one place online to find your eclectic taste of music instruments and books of poetry!";

DisplayMenu(greeting);

void DisplayAllProducts(List<Product> products, List<ProductType> productTypes)
{
    foreach (Product product in products)
    {
        ProductType productType = productTypes.FirstOrDefault(pt => pt.Id == product.ProductTypeId);
        if (productType != null)
        {
            Console.WriteLine($"Name: {product.Name}, Price: {product.Price}, Type: {productType.Title}");
        }
        else
        {
            Console.WriteLine($"Name: {product.Name}, Price: {product.Price}, Type: Unknown");
        }
    }
}

void DeleteProduct(List<Product> products, List<ProductType> productTypes)
{
    Product deletedProduct = null;

    while (deletedProduct == null)
    {
        Console.WriteLine("Available products: ");
        for (int i = 0; i < products.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {products[i].Name}");
        }

        Console.WriteLine("Please enter a product number: ");

        try
        {
            int response = int.Parse(Console.ReadLine().Trim());

            if (response < 1 || response > products.Count)
            {
                Console.WriteLine("Please enter a valid product number.");
                continue;
            }

            deletedProduct = products[response - 1];
            products.RemoveAt(response - 1);

            Console.WriteLine($"You deleted {deletedProduct.Name}. Congratulations!");
        }
        catch (FormatException)
        {
            Console.WriteLine("Please type only integers.");
        }
        catch (Exception)
        {
            Console.WriteLine("An error occurred.  Please try again");
        }
    }
}

void AddProduct(List<Product> products, List<ProductType> productTypes)
{
    Product newProduct = new Product();

    Console.WriteLine("Enter the name of the new product: ");
    newProduct.Name = Console.ReadLine();

    Console.WriteLine("Enter the price of the new product: ");
    newProduct.Price = decimal.Parse(Console.ReadLine());

    Console.WriteLine("Select the product type: ");
    foreach (ProductType type in productTypes)
    {
        Console.WriteLine($"{type.Id}. {type.Title}");
    }

    int typeId = 0;
    while (!productTypes.Any(t => t.Id == typeId))
    {
        Console.WriteLine("Enter the product type ID: ");
        typeId = int.Parse(Console.ReadLine());
    }

    newProduct.ProductTypeId = typeId;

    int productId = products.Count > 0 ? products.Max(p => p.ProductTypeId) + 1 : 1;
    newProduct.ProductTypeId = productId;

    products.Add(newProduct);

    Console.WriteLine("Product added! Congratulations!");
}

void UpdateProduct(List<Product> products, List<ProductType> productTypes)
{
    Console.WriteLine("Available products to update: ");
    for (int i = 0; i < products.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {products[i].Name} - {products[i].Price}");
    }

    Console.WriteLine("Enter the number of the product you want to update: ");

    if (int.TryParse(Console.ReadLine(), out int productNumber) && productNumber > 0 && productNumber <= products.Count)
    {
        Product selectedProduct = products[productNumber - 1];

        Console.WriteLine($"Updating product: {selectedProduct.Name}");

        Console.WriteLine("Enter the new name of the product: ");
        string newName = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newName))
        {
            selectedProduct.Name = newName;
        }

        Console.WriteLine("Enter the new price of the product: ");
        string priceInput = Console.ReadLine();
        if (decimal.TryParse(priceInput, out decimal newPrice))
        {
            selectedProduct.Price = newPrice;
        }

        Console.WriteLine("Select the product type: ");
        foreach (ProductType type in productTypes)
        {
            Console.WriteLine($"{type.Id}. {type.Title}");
        }

        string typeIdInput = Console.ReadLine();
        if (int.TryParse(typeIdInput, out int newTypeId) && productTypes.Any(t => t.Id == newTypeId))
        {
            selectedProduct.ProductTypeId = newTypeId;
            selectedProduct.ProductType = productTypes.First(t => t.Id == newTypeId);
        }

        Console.WriteLine("Product updated successfully!");
    }
    else
    {
        Console.WriteLine("Product not found.");
    }
}

// don't move or change this!
public partial class Program { }
