using Infrastructure;
using Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<InMemDbContext>(options => options.UseInMemoryDatabase("Default"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () => (object?)null)
    .WithName("Default Endpoint");

# region InMemoryDB Setup
using (IServiceScope scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<InMemDbContext>();

    if (!context.Categories.Any())
    {
        context.Categories.AddRange(InitialDataSet.GetCategories());
        context.SaveChanges();
    }

    if (!context.Products.Any())
    {
        context.Products.AddRange(InitialDataSet.Get());
        context.SaveChanges();
    }
}
# endregion

#region get all products. filter all products by skus
app.MapGet("/api/products", async (InMemDbContext db, string? search) =>
{
    var query = db.Products.AsQueryable();
    if (!string.IsNullOrWhiteSpace(search))
    {
        query = query.Where(p => p.Name.Contains(search) || p.Sku.Contains(search));
    }
    var products = await query.ToListAsync();
    return Results.Ok(products);
})
.WithName("GetProducts");

#endregion

#region get all categories. filter all products by skus
app.MapGet("/api/categories", async (InMemDbContext db, string? search) =>
{
    var query = db.Categories.AsQueryable();
    if (!string.IsNullOrWhiteSpace(search))
    {
        query = query.Where(c => c.Name.Contains(search));
    }
    var categories = await query.ToListAsync();
    return Results.Ok(categories);
})
.WithName("GetCategories");

#endregion

app.Run();
