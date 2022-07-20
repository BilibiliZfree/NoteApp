using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NoteApp.Api.Data;
using NoteApp.Api.Models;
using NoteApp.Api.Services;
using NoteApp.Api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// 添加Service
builder.Services.AddTransient<UsersService>();
builder.Services.AddTransient<BlogsService>();
// 定义数据库地址
builder.Services.AddSqlite<NoteAppContext>("Data Source=Databases/NoteDatabase.db");
//builder.Services.AddSqlite<NoteAppContext>("Data Source=Databases/NoteDatabaseTest.db");


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
