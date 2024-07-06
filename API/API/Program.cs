using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

//  COMO CRIAR PROJETO NO GIT 

// //  1 - CRIAR REPOSITORIO NO GIT "ProvaSub"
// //  2 - CLONAR NA AREA DE TRABALHO USANDO O POWERSHEL
// //  3 - CLONA O PROJETO BASE QUE O PROFESSOR VAI DISPONIBILIZAR NA ARERA DE TRABALHO TBM
// //  4 - PASSA O PROJETO BASE PRA DENTRO DA SUA PASTA DO COMIT 
// //  5 - git add . depois git commit -m "projeto" depois git push 
// //  6 - CRIAR O FRONT DENTRO DA PASTA DO SEu PROJETO CHAMADO "ProvaSub" COM O POWERSHELL
// //  7 - npx create-react-app front --template typescript  para criar o front
// //  8 - manda para o repositorio de novo com os pasos do numero 5
// 9 - Deixe esse projeto aqui de lado só para as Colas


// COMANDOS PARA FAZER FUNCIONAR O BANCO

// 1 - dotnet ef migrations add Initial dentro da pasta do projeto
// 2 - dotnet ef database update  atualiza a base
// 3 - apague o banco ja existente e as migracoes
// 4 - entra no AppDataContext dentro de  models e aqui  optionsBuilder.UseSqlite("Data Source=app.db"); mude o app.db para seu nome ex("Antonio.db")
// 5 - va nos tests http e mude a porta dos testes para 5000


// COISAS IMPORTANTES DO PROGRAM.CS

// 1 - Prestar atençao nas implementacoes dos if e else que com certeza tera
// 2 - na parte das tarefas concluidas e nao concluidas, seria para lista-las
// 3 - copiar as url das coisas que voce implementou e colocar para fazer os testes
// 4 - para atualizar uma tarefa, precisa antes lista la e pegar o id dela para colocar na url da alteração
// 5 - troca a porta dos testes para 5000

//COISAS IMPORTANTES DO FRONT

// 1 - Começar pela listagem
// 2 - instalar as bibliotecas: npm install react-router-dom
// 3 - instalar as tipagens: npm install @types/react-router-dom
// 4 - instalar o axios ou o fetch: npm install axios @types/axios
// 5 - depois de instalar tudo, da um npm start para ver se esta correto







var builder = WebApplication.CreateBuilder(args);
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


app.MapGet("/", () => "Prova A1");

//ENDPOINTS DE CATEGORIA
//GET: http://localhost:5000/categoria/listar
app.MapGet("/categoria/listar", ([FromServices] AppDataContext ctx) =>
{
    if (ctx.Categorias.Any())
    {
        return Results.Ok(ctx.Categorias.ToList());
    }
    return Results.NotFound("Nenhuma categoria encontrada");
});

//POST: http://localhost:5000/categoria/cadastrar
app.MapPost("/categoria/cadastrar", ([FromServices] AppDataContext ctx, [FromBody] Categoria categoria) =>
{
    ctx.Categorias.Add(categoria);
    ctx.SaveChanges();
    return Results.Created("", categoria);
});

//ENDPOINTS DE TAREFA
//GET: http://localhost:5000/tarefas/listar
app.MapGet("/tarefas/listar", ([FromServices] AppDataContext ctx) =>
{
    if (ctx.Tarefas.Any())
    {
        return Results.Ok(ctx.Tarefas.ToList());
    }
    return Results.NotFound("Nenhuma tarefa encontrada");
});

//POST: http://localhost:5000/tarefas/cadastrar
app.MapPost("/tarefas/cadastrar", ([FromServices] AppDataContext ctx, [FromBody] Tarefa tarefa) =>
{
    Categoria? categoria = ctx.Categorias.Find(tarefa.CategoriaId);
    if (categoria == null)
    {
        return Results.NotFound("Categoria não encontrada");
    }
    tarefa.Categoria = categoria;
    ctx.Tarefas.Add(tarefa);
    ctx.SaveChanges();
    return Results.Created("", tarefa);
});



//PUT: http://localhost:5000/tarefas/alterar/{id}
app.MapPut("/tarefas/alterar/{id}", ([FromServices] AppDataContext ctx, [FromRoute] string id) =>
{
    Tarefa? tarefa = ctx.Tarefas.Find(id);
    if (tarefa == null)
    {
        return Results.NotFound("Tarefa não encontrada");
    }
    
    if(tarefa.Status == "Não iniciada"){
        tarefa.Status = "Em andamento";
    }
    else if(tarefa.Status == "Em andamento")
    {
        tarefa.Status = "Concluída";
    }

    ctx.Tarefas.Update(tarefa);
    ctx.SaveChanges();
    return Results.Ok("Tarefa alterada com sucesso");
});

//GET: http://localhost:5000/tarefas/naoconcluidas
app.MapGet("/tarefas/naoconcluidas", ([FromServices] AppDataContext ctx) =>
{
    return Results.Ok(ctx.Tarefas.ToList().Where(s => s.Status == "Não iniciada" || s.Status == "Em andamento"));
    
});

//GET: http://localhost:5000/tarefas/concluidas
app.MapGet("/tarefas/concluidas", ([FromServices] AppDataContext ctx) =>
{
    return Results.Ok(ctx.Tarefas.ToList().Where(s => s.Status == "Concluída"));
});


app.UseCors("AcessoTotal");
app.Run();
