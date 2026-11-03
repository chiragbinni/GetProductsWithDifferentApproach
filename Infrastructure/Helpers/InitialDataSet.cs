using Infrastructure.Models;

namespace Infrastructure.Helpers;

public static class InitialDataSet
{
    public static IEnumerable<Category> GetCategories() =>
    [
        new Category { CategoryId = Guid.Parse("11111111-1111-1111-1111-111111111111"), IsActive = true, Name = "Graphics Cards" },
        new Category { CategoryId = Guid.Parse("22222222-2222-2222-2222-222222222222"), IsActive = true, Name = "Processors" },
        new Category { CategoryId = Guid.Parse("33333333-3333-3333-3333-333333333333"), IsActive = true, Name = "Motherboards" },
        new Category { CategoryId = Guid.Parse("44444444-4444-4444-4444-444444444444"), IsActive = true, Name = "Memory" },
        new Category { CategoryId = Guid.Parse("55555555-5555-5555-5555-555555555555"), IsActive = true, Name = "Storage" },
    ];

    public static IEnumerable<Product> Get() =>
    [
        new Product { Id = Guid.NewGuid(), Name = "NVIDIA GeForce RTX 4070", Sku = "GPU-4070-NV", IsActive = true, CategoryId = Guid.Parse("11111111-1111-1111-1111-111111111111") },
        new Product { Id = Guid.NewGuid(), Name = "NVIDIA GeForce RTX 4080", Sku = "GPU-4080-NV", IsActive = true, CategoryId = Guid.Parse("11111111-1111-1111-1111-111111111111") },
        new Product { Id = Guid.NewGuid(), Name = "NVIDIA GeForce RTX 4090", Sku = "GPU-4090-NV", IsActive = false, CategoryId = Guid.Parse("11111111-1111-1111-1111-111111111111") },
        new Product { Id = Guid.NewGuid(), Name = "AMD Radeon RX 7800 XT", Sku = "GPU-7800-AMD", IsActive = true, CategoryId = Guid.Parse("11111111-1111-1111-1111-111111111111") },
        new Product { Id = Guid.NewGuid(), Name = "AMD Radeon RX 7900 XT", Sku = "GPU-7900-AMD", IsActive = true, CategoryId = Guid.Parse("11111111-1111-1111-1111-111111111111") },

        new Product { Id = Guid.NewGuid(), Name = "Intel Core i9-13900K", Sku = "CPU-I9-13900K", IsActive = true, CategoryId = Guid.Parse("22222222-2222-2222-2222-222222222222") },
        new Product { Id = Guid.NewGuid(), Name = "Intel Core i7-13700K", Sku = "CPU-I7-13700K", IsActive = true, CategoryId = Guid.Parse("22222222-2222-2222-2222-222222222222") },
        new Product { Id = Guid.NewGuid(), Name = "AMD Ryzen 9 7950X", Sku = "CPU-R9-7950X", IsActive = true, CategoryId = Guid.Parse("22222222-2222-2222-2222-222222222222") },
        new Product { Id = Guid.NewGuid(), Name = "AMD Ryzen 7 7800X3D", Sku = "CPU-R7-7800X3D", IsActive = false, CategoryId = Guid.Parse("22222222-2222-2222-2222-222222222222") },

        new Product { Id = Guid.NewGuid(), Name = "ASUS ROG Strix Z790-E", Sku = "MB-Z790E-ASUS", IsActive = true, CategoryId = Guid.Parse("33333333-3333-3333-3333-333333333333") },
        new Product { Id = Guid.NewGuid(), Name = "MSI B650 Tomahawk", Sku = "MB-B650-MSI", IsActive = true, CategoryId = Guid.Parse("33333333-3333-3333-3333-333333333333") },
        new Product { Id = Guid.NewGuid(), Name = "Gigabyte X670 Aorus Elite", Sku = "MB-X670-GIGA", IsActive = true, CategoryId = Guid.Parse("33333333-3333-3333-3333-333333333333") },

        new Product { Id = Guid.NewGuid(), Name = "Corsair Vengeance DDR5 32GB", Sku = "RAM-32G-DDR5-COR", IsActive = true, CategoryId = Guid.Parse("44444444-4444-4444-4444-444444444444") },
        new Product { Id = Guid.NewGuid(), Name = "G.Skill Trident Z5 RGB 64GB", Sku = "RAM-64G-Z5-GSK", IsActive = false, CategoryId = Guid.Parse("44444444-4444-4444-4444-444444444444") },

        new Product { Id = Guid.NewGuid(), Name = "Samsung 990 Pro 2TB SSD", Sku = "SSD-2TB-990PRO", IsActive = true, CategoryId = Guid.Parse("55555555-5555-5555-5555-555555555555") },
        new Product { Id = Guid.NewGuid(), Name = "WD Black SN850X 1TB SSD", Sku = "SSD-1TB-SN850X", IsActive = true, CategoryId = Guid.Parse("55555555-5555-5555-5555-555555555555") },

        new Product { Id = Guid.NewGuid(), Name = "Logitech MX Master 3 Mouse", Sku = "PER-MX3-LOGI", IsActive = true, CategoryId = null },
        new Product { Id = Guid.NewGuid(), Name = "Razer Huntsman Keyboard", Sku = "PER-HUNT-RAZER", IsActive = false, CategoryId = null },
        new Product { Id = Guid.NewGuid(), Name = "Dell UltraSharp 27 Monitor", Sku = "MON-27-UDEL", IsActive = true, CategoryId = null }
    ];
}