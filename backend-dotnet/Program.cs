using Microsoft.EntityFrameworkCore;
using backend_dotnet; 
using backend_dotnet.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. Database Connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// 2. CORS Policy - Dashboard connection ke liye (Crucial Fix)
builder.Services.AddCors(options => {
    options.AddDefaultPolicy(policy => {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// CORS Middleware enable karna zaroori hai
app.UseCors();

// --- API ENDPOINTS (Minimal API) ---

// A. Status Check
app.MapGet("/api/status", () => new { status = "Backend is running!" });

// B. GET ALL TASKS (Dashboard ke liye)
app.MapGet("/api/tasks", async (AppDbContext db) => {
    try 
    {
        var tasks = await db.MigrationTasks.ToListAsync();
        return Results.Ok(tasks); 
    }
    catch (Exception ex)
    {
        return Results.Problem("Error fetching tasks: " + ex.Message);
    }
});

// C. ADD NEW TASK (Dashboard POST action)
app.MapPost("/migration/add", async (AppDbContext db, string fileName) => {
    if (string.IsNullOrWhiteSpace(fileName)) 
        return Results.BadRequest(new { message = "FileName is required!" });

    try 
    {
        var task = new MigrationTask { FileName = fileName, Status = "Pending" };
        db.MigrationTasks.Add(task);
        await db.SaveChangesAsync();
        return Results.Ok(new { message = "Task saved to DB!", taskId = task.Id });
    }
    catch (Exception ex)
    {
        return Results.Problem("Database error: " + ex.Message);
    }
});

// D. COMPLETE TASK (Worker status update action)
app.MapPost("/migration/complete/{id}", async (AppDbContext db, int id) => {
    var task = await db.MigrationTasks.FindAsync(id);
    if (task == null) return Results.NotFound();

    task.Status = "Completed";
    await db.SaveChangesAsync();
    return Results.Ok(new { message = "Task marked as completed" });
});

// E. GET NEXT PENDING TASK (Python Worker ke liye loop connection)
app.MapGet("/migration/next-task", async (AppDbContext db) => {
    try
    {
        var nextTask = await db.MigrationTasks
            .FirstOrDefaultAsync(t => t.Status == "Pending");

        if (nextTask == null) 
            return Results.NotFound(new { message = "No pending tasks found" });

        return Results.Ok(nextTask);
    }
    catch (Exception ex)
    {
        return Results.Problem("Worker query error: " + ex.Message);
    }
});

// 3. DB Auto-Creation
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.EnsureCreated();
    }
    catch (Exception ex)
    {
        Console.WriteLine("DB Creation Error: " + ex.Message);
    }
}

app.Run();