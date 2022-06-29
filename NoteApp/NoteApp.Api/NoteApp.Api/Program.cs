using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NoteApp.Api.Data;
using NoteApp.Api.Models;

var builder = WebApplication.CreateBuilder(args);

//添加数据库链接字符串
var connectionString = builder.Configuration.GetConnectionString("Notes") ?? "Data Source=Notes.db";

builder.Services.AddEndpointsApiExplorer();

//使用SQLite数据库
builder.Services.AddSqlite<NoteAppApiContext>(connectionString);

builder.Services.AddControllers();

//标题、属性、描述 设置
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "NoteApp Api接口",
        Description = "用于承载持久性数据操作",
        Version = "version 0.0.0.1"
    });
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PizzaStore API V1");
});

app.MapGet("/", () => "Hello World!");
//从列表中的项列表中进行读取
app.MapGet("/notes", async (NoteAppApiContext db) => await db.Users.ToListAsync());
app.MapGet("/notes/{id}", async (NoteAppApiContext db, int id) => await db.Users.FindAsync(id));
app.MapPost("/notes", async (NoteAppApiContext db, User user) =>
{
    await db.Users.AddAsync(user);
    await db.SaveChangesAsync();
    return Results.Created($"/notes/{user.ID}", user);
});
app.MapPut("/notes/{id}", async (NoteAppApiContext db, User updateuser, int id) =>
{
    var user = await db.Users.FindAsync(id);
    if (user is null) return Results.NotFound();
    user.UserName = updateuser.UserName;
    user.TelphoneNumber = updateuser.TelphoneNumber;
    user.UpdateDate = DateTime.Now;
    await db.SaveChangesAsync();
    return Results.NoContent();
});
//删除披萨
app.MapDelete("/notes/{id}", async (NoteAppApiContext db, int id) =>
{
    var user = await db.Users.FindAsync(id);
    if (user is null) return Results.NotFound();
    db.Users.Remove(user);
    await db.SaveChangesAsync();
    return Results.Ok();
});



app.Run();