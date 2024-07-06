using System.ComponentModel.DataAnnotations;
using API.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

//Registrar o serviço de banco de dados na aplicação
builder.Services.AddDbContext<AppDataContext>();

builder.Services.AddCors(
    options =>
    {
        options.AddPolicy("AcessoTotal",
            builder => builder.
                AllowAnyOrigin().
                AllowAnyHeader().
                AllowAnyMethod());
    }
);

var app = builder.Build();

List<Produto> produtos = new List<Produto>();

// Endpoints = Funcionalidades - JSON
// POST: http://localhost:5076/api/produto/cadastrar
app.MapPost("/api/produto/cadastrar", ([FromBody] Produto produto,
    [FromServices] AppDataContext context) =>
{
    //Validando os atributos do objeto produto
    List<ValidationResult> erros = new List<ValidationResult>();
    if (!Validator.TryValidateObject(
            produto, new ValidationContext(produto), erros, true))
    {
        return Results.BadRequest(erros);
    }

    //Adicionando o produto dentro da tabela no banco de dados
    //O produto não pode ter o mesmo nome de algum produto já cadastrado
    Produto? produtoBuscado = context.Produtos.FirstOrDefault(x =>
        x.Nome == produto.Nome);

    if (produtoBuscado is null)
    {
        produto.Nome = produto.Nome.ToUpper();
        context.Produtos.Add(produto);
        context.SaveChanges();
        return Results.Created($"/api/produto/buscar/{produto.Id}", produto);
    }
    return Results.BadRequest("Já existe um produto com o mesmo nome");

});

// GET: http://localhost:5076/api/produto/listar
app.MapGet("/api/produto/listar", ([FromServices] AppDataContext context) =>
{
    if (context.Produtos.Any())
    {
        return Results.Ok(context.Produtos.ToList());
    }
    return Results.NotFound("Não existem produtos na tabela");
});

// GET: http://localhost:5076/api/produto/buscar/{iddoproduto}
app.MapGet("/api/produto/buscar/{id}", ([FromRoute] string id,
    [FromServices] AppDataContext context) =>
{
    //Endpoint com várias linhas de código
    Produto? produto = context.Produtos.FirstOrDefault(x => x.Id == id);

    if (produto is null)
    {
        return Results.NotFound("Produto não encontrado!");
    }
    return Results.Ok(produto);
});

// DELETE: http://localhost:5076/api/produto/deletar/{iddoproduto}
app.MapDelete("/api/produto/deletar/{id}", ([FromRoute] string id,
    [FromServices] AppDataContext context) =>
{
    Produto? produto = context.Produtos.Find(id);

    if (produto is null)
    {
        return Results.NotFound("Produto não encontrado!");
    }
    context.Produtos.Remove(produto);
    context.SaveChanges();
    return Results.Ok(context.Produtos.ToList());
});


// PUT: http://localhost:5076/api/produto/alterar/{iddoproduto}
app.MapPut("/api/produto/alterar/{id}", ([FromRoute] string id,
    [FromBody] Produto produtoAlterado,
    [FromServices] AppDataContext context) =>
{
    //Endpoint com várias linhas de código    
    Produto? produto = context.Produtos.Find(id);

    if (produto is null)
    {
        return Results.NotFound("Produto não encontrado!");
    }

    produto.Nome = produtoAlterado.Nome;
    produto.Descricao = produtoAlterado.Descricao;
    produto.Preco = produtoAlterado.Preco;

    context.Produtos.Update(produto);
    context.SaveChanges();

    return Results.Ok("Produto alterado com sucesso!");
});

app.UseCors("AcessoTotal");
app.Run();

//CONFIGURAR O BANCO NA APLICAÇÃO
//1 - Quais as bibliotecas serão instaladas no projeto
//2 - O que é necessário adicionar/alterar no projeto
//para configurar a aplicação com o banco

//dotnet add package Microsoft.EntityFrameworkCore.Sqlite 
//--version 8.0.3
//dotnet add package Microsoft.EntityFrameworkCore.Design 
//--version 8.0.3 

//EXERÍCIOS PARA O EF
//1 - Cadastrar o objeto de produto no banco
//2 - Listar os registros da tabela