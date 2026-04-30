using CodeBibliotec.Context;
using CodeBibliotec.Interfaces;
using CodeBibliotec.Repositories;
using CodeBibliotec.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);







//pegando connection string:
//builder.configurador, pegue as conexoes
var connectionString = builder.Configuration.GetConnectionString ("DefaultConnection");
//BibliotecContext: camada de contexto criada pelo scaffold
//oq significa? agora ele sabe qual camada está linkada com o banco
//conectando atraves da string de conexão criada no appsettings.json
builder.Services.AddDbContext<BibliotecContext>(options => options.UseSqlServer(connectionString));


//Registra dependencias(injeção de dependencias)
//onde AddScoped define que uma nova instancia do serviço será criada para cada requisição HTTP;
//*tambem é feita a associação entre as interfaces e suas respectivas implementações

//oq cada metodo vai fazer está em LivroRepository
//interface(ILivroRepository) depois camada de serviço/implementação(LivroRepository)
builder.Services.AddScoped<ILivroRepository, LivroRepository> ();
builder.Services.AddScoped<ILivroService, LivroService>();


//CategoriaRepository e CategoriaService
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
// Add services to the container.




//adiconando serialização para evitar erros de ciclo
//serialização de objetos para json, ou seja, para o formato de resposta da API
builder.Services.AddControllers()
   .AddJsonOptions(options => {
       options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
       //IgnoreCycles: ignorar ciclos, pois esta entrando em um looping de ir para livro, categoria..
   });



// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//começando code, configurando Swagger:
// Add JWT authentication to Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "Input your Bearer token in this format - Bearer {your token here} to access this API"
    });

    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("bearer", document)] = []
    });


});

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
