using Microsoft.EntityFrameworkCore;
using VeggieStoreAPI.Data;
using VeggieStoreAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// Configure MySQL Database
builder.Services.AddDbContext<VeggieStoreContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 32)) // Adjust based on MySQL version
    ));

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");

// Default route
app.MapGet("/", () => Results.Ok(new { message = "Veggie Store API is running!" }));

// ✅ CRUD APIs for Vegetables
app.MapGet("/vegetables", async (VeggieStoreContext db) =>
    await db.Vegetables.ToListAsync());

app.MapGet("/vegetables/{id}", async (VeggieStoreContext db, int id) =>
{
    var veg = await db.Vegetables.FindAsync(id);
    return veg is not null ? Results.Ok(veg) : Results.NotFound(new { error = "Vegetable not found." });
});

app.MapPost("/vegetables", async (VeggieStoreContext db, Vegetable veg) =>
{
    if (string.IsNullOrWhiteSpace(veg.Name) || veg.Price <= 0)
        return Results.BadRequest(new { error = "Invalid vegetable data." });

    db.Vegetables.Add(veg);
    await db.SaveChangesAsync();
    return Results.Created($"/vegetables/{veg.Id}", veg);
});

app.MapPut("/vegetables/{id}", async (VeggieStoreContext db, int id, Vegetable updatedVeg) =>
{
    var veg = await db.Vegetables.FindAsync(id);
    if (veg is null) return Results.NotFound(new { error = "Vegetable not found." });

    veg.Name = updatedVeg.Name;
    veg.Price = updatedVeg.Price;
    veg.Stock = updatedVeg.Stock;
    veg.Category = updatedVeg.Category;
    veg.ImageUrl = updatedVeg.ImageUrl;

    await db.SaveChangesAsync();
    return Results.Ok(veg);
});

app.MapDelete("/vegetables/{id}", async (VeggieStoreContext db, int id) =>
{
    var veg = await db.Vegetables.FindAsync(id);
    if (veg is null) return Results.NotFound(new { error = "Vegetable not found." });

    db.Vegetables.Remove(veg);
    await db.SaveChangesAsync();
    return Results.Ok(new { message = $"Deleted Vegetable ID {id}" });
});

// ✅ CRUD APIs for Users
app.MapGet("/users", async (VeggieStoreContext db) =>
    await db.Users.ToListAsync());

app.MapGet("/users/{id}", async (VeggieStoreContext db, int id) =>
{
    var user = await db.Users.FindAsync(id);
    return user is not null ? Results.Ok(user) : Results.NotFound(new { error = "User not found." });
});

app.MapPost("/users", async (VeggieStoreContext db, User user) =>
{
    if (string.IsNullOrWhiteSpace(user.Name) || string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.Password))
        return Results.BadRequest(new { error = "Invalid user data." });

    db.Users.Add(user);
    await db.SaveChangesAsync();
    return Results.Created($"/users/{user.Id}", user);
});

// ✅ CRUD APIs for Orders
app.MapGet("/orders", async (VeggieStoreContext db) =>
    await db.Orders.Include(o => o.OrderItems).ToListAsync());

app.MapGet("/orders/{id}", async (VeggieStoreContext db, int id) =>
{
    var order = await db.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id == id);
    return order is not null ? Results.Ok(order) : Results.NotFound(new { error = "Order not found." });
});

app.MapPost("/orders", async (VeggieStoreContext db, Order order) =>
{
    if (order.UserId == 0 || order.OrderItems == null || !order.OrderItems.Any())
        return Results.BadRequest(new { error = "Invalid order data." });

    db.Orders.Add(order);
    await db.SaveChangesAsync();
    return Results.Created($"/orders/{order.Id}", order);
});

// ✅ CRUD APIs for Cart
app.MapGet("/cart/{userId}", async (VeggieStoreContext db, int userId) =>
    await db.Carts.Where(c => c.UserId == userId).Include(c => c.Vegetable).ToListAsync());

app.MapPost("/cart", async (VeggieStoreContext db, Cart cartItem) =>
{
    if (cartItem.UserId == 0 || cartItem.VegetableId == 0 || cartItem.Quantity <= 0)
        return Results.BadRequest(new { error = "Invalid cart data." });

    db.Carts.Add(cartItem);
    await db.SaveChangesAsync();
    return Results.Created($"/cart/{cartItem.Id}", cartItem);
});

app.MapDelete("/cart/{id}", async (VeggieStoreContext db, int id) =>
{
    var cartItem = await db.Carts.FindAsync(id);
    if (cartItem is null) return Results.NotFound(new { error = "Cart item not found." });

    db.Carts.Remove(cartItem);
    await db.SaveChangesAsync();
    return Results.Ok(new { message = $"Removed item {id} from cart." });
});

app.Run();
