using System.Text;
using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//String DB {appsettings.json}
string mysqlConnectio = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseMySql(mysqlConnectio,ServerVersion.AutoDetect(mysqlConnectio)));
//String DB END

//JWT Authorization
builder.Services.AddSingleton<ITokenService>(
    new TokenService()
);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer( options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    }
);
//JWT Authorization

var app = builder.Build();

#region  categorias

app.MapGet("/categorias", async(AppDbContext db) =>
    await db.Categorias.ToListAsync()
);

app.MapGet("/categorias/{id:int}", async (int id, AppDbContext db) =>
{
    var categoria = await db.Categorias.FindAsync(id);

    if (categoria != null)
    {
        return Results.Ok(categoria);
    }
    else
    {
        return Results.NotFound();
    }
});

app.MapPost("/categorias", async(Categoria categoria, AppDbContext db) =>
    {
        db.Categorias.Add(categoria);
        await db.SaveChangesAsync();

        return Results.Created($"/categorias/{categoria.CategoriaID}", categoria);
    }
);

app.MapPut("/categorias/{id:int}", async(int id, Categoria categoria, AppDbContext db) =>
    {
        if (categoria.CategoriaID != id)
        {
            return Results.BadRequest();
        }

        var categoriaDB = await db.Categorias.FindAsync(id);

        if(categoriaDB is null) return Results.NotFound();

        categoriaDB.Nome = categoria.Nome;
        categoriaDB.Descricao = categoria.Descricao;

        await db.SaveChangesAsync();
        return Results.Ok(categoriaDB);
    }
);

app.MapDelete("/categorias/{id:int}", async (int id, AppDbContext db) =>
{
    var categoria = await db.Categorias.FindAsync(id);

    if (categoria != null)
    {
        db.Categorias.Remove(categoria);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
    else
    {
        return Results.NotFound();
    }
});

#endregion

#region Produtos

app.MapGet("/produtos", async (AppDbContext db) =>
    await db.Produtos.ToListAsync()
);

app.MapGet("/produtos/{id:int}", async (int id, AppDbContext db) =>
{
    var produto = await db.Produtos.FindAsync(id);

    if (produto != null)
    {
        return Results.Ok(produto);
    }
    else
    {
        return Results.NotFound();
    }
});

app.MapPost("/produtos", async (Produto produto, AppDbContext db) =>
{
    db.Produtos.Add(produto);
    await db.SaveChangesAsync();

    return Results.Created($"/produtos/{produto.ProdutoID}", produto);
});

app.MapPut("/produtos/{id:int}", async (int id, Produto produto, AppDbContext db) =>
{
    if (produto.ProdutoID != id)
    {
        return Results.BadRequest();
    }

    var produtoDB = await db.Produtos.FindAsync(id);

    if (produtoDB is null) return Results.NotFound();

    produtoDB.Nome = produto.Nome;
    produtoDB.Descricao = produto.Descricao;
    produtoDB.Preco = produto.Preco;
    produtoDB.Imagem = produto.Imagem;
    produtoDB.DataCompra = produto.DataCompra;
    produtoDB.Estoque = produto.Estoque;

    await db.SaveChangesAsync();
    return Results.Ok(produtoDB);
});

app.MapDelete("/produtos/{id:int}", async (int id, AppDbContext db) =>
{
    var produto = await db.Produtos.FindAsync(id);

    if (produto != null)
    {
        db.Produtos.Remove(produto);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
    else
    {
        return Results.NotFound();
    }
});

#endregion

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();