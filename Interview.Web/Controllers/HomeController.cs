using Infrastructure;
using Infrastructure.Models;
using Interview.Web.Helpers;
using Interview.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Interview.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult Products(string? search, string? sort, string? direction, Guid? categoryId)
    {
        using var context = new InMemDbContext(new DbContextOptionsBuilder<InMemDbContext>()
            .UseInMemoryDatabase("Default").Options);



        var categories = context.Categories.Where(c => c.IsActive).ToList();
        ViewBag.Categories = categories;
        ViewBag.SelectedCategory = categoryId;

        var products = context.Products.Where(p => p.IsActive);

        if (categoryId.HasValue)
            products = products.Where(p => p.CategoryId == categoryId);

        if (!string.IsNullOrWhiteSpace(search))
            products = products.Where(p => p.Name.Contains(search) || p.Sku.Contains(search));

        bool desc = direction == "desc";
        products = sort switch
        {
            "name" => desc ? products.OrderByDescending(p => p.Name) : products.OrderBy(p => p.Name),
            "sku" => desc ? products.OrderByDescending(p => p.Sku) : products.OrderBy(p => p.Sku),
            _ => products.OrderBy(p => p.Name)
        };

        var model = products
            .ToList()
            .Select(p => {
                var cat = categories.FirstOrDefault(c => c.CategoryId == p.CategoryId);
                return new ProductWithCategoryViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Sku = p.Sku,
                    CategoryName = cat != null ? cat.Name : "Uncategorized"
                };
            })
            .ToList();

        ViewBag.Search = search;
        ViewBag.Sort = sort;
        ViewBag.Direction = direction;
        return View(model);
    }

    public async Task<IActionResult> ProductsViaAPI(SearchFilters filters)
    {
        var productUrl = "https://localhost:7072/api/products";
        var categoryUrl = "https://localhost:7072/api/categories";

        var products = new List<Product>();
        var categories = new List<Category>();
        using (var client = new HttpClient())
        {
            #pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            products.AddRange(await client.GetFromJsonAsAsyncEnumerable<Product>(requestUri: productUrl)
                .Where(p => p.IsActive)
                .ToListAsync());
            #pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            #pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            categories.AddRange(await client.GetFromJsonAsAsyncEnumerable<Category>(requestUri: categoryUrl)
                .Where(c => c.IsActive) 
                .ToListAsync());
            #pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        }

        ViewBag.Categories = categories;
        ViewBag.SelectedCategory = filters.CategoryId;


        if (filters.CategoryId.HasValue)
            products = (products.Where(p => p.CategoryId == filters.CategoryId).ToList());

        if (!string.IsNullOrWhiteSpace(filters.Search))
            products = (products.Where(p => p.Name.Contains(filters.Search) || p.Sku.Contains(filters.Search)).ToList());

        bool desc = filters.Direction == "desc";
        products = filters.Sort switch
        {
            "name" => desc ? (products.OrderByDescending(p => p.Name).ToList()) : (products.OrderBy(p => p.Name).ToList()),
            "sku" => desc ? (products.OrderByDescending(p => p.Sku).ToList()) : (products.OrderBy(p => p.Sku).ToList()),
            _ => products.OrderBy(p => p.Name).ToList()
        };

        var model = products
            .Select(p => {
                var cat = categories.FirstOrDefault(c => c.CategoryId == p.CategoryId);
                return new ProductWithCategoryViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Sku = p.Sku,
                    CategoryName = cat != null ? cat.Name : "Uncategorized"
                };
            })
            .ToList();

        ViewBag.Search = filters.Search;
        ViewBag.Sort = filters.Sort;
        ViewBag.Direction = filters.Direction;
        return View(model);
    }
}