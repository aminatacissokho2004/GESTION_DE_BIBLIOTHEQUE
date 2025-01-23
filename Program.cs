using Gestion_de_Bibliothéque;
using Gestion_de_Bibliothéque.DTOs;
using AutoMapper;
using Gestion_de_Bibliothéque.datas;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Configuration du context de la base de donnee
var ConnexionString = builder.Configuration.GetConnectionString("Bibliotheque");
builder.Services.AddSqlite<BookDbContext>(ConnexionString);

builder.Services.AddAutoMapper(typeof(Program)); 
builder.Services.AddControllersWithViews();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapGet("/", () => "Hello World!"); 

#region CREATION DE LISTE DE LIVRES ET LES RECUPERER
var books = new List<BookDTOs>
{
    new BookDTOs
    {
        Id = 1,
        Title = "sous l'orage", 
        Author = "Mariama Ba", 
        PubDate = new DateOnly(2000,10,29)
    },
    new BookDTOs 
    { 
        Id = 2, 
        Title = "Une vie de boy", 
        Author = "Ferdinant Oyono", 
        PubDate = new DateOnly(2004,1,7)
    }
};
// EndPoint pour recuperer la liste de livres
app.MapGet("/books", () => books);
#endregion

#region CREATION DE ENDPONIT POUR RECUPERER A PARTIR DE L'ID
//app.MapGet("/books/{id}", (int id) => books.FirstOrDefault(book=>book.Id == id));
app.MapGet("/books/{id}", async(int id, BookDbContext dbContext)=>
{
    var book = await dbContext.Books.ToListAsync();
    return Results.Ok(book);
    // var book = books.FirstOrDefault(book => book.Id == id);
    // if(book is null)
    // {
    //     return Results.NotFound(new {Message = $"Le book avec l'ID {id} n'existe pas !"});
    // }
    // return Results.Ok(book);
});
#endregion
                            
#region ENDPOINTE POUR AJOUTER 
// Creation d'un Endponit qui permet d'ajouter des livres
app.MapPost("/books", (BookDTOs newBookDTOS, IMapper mapper)=>
{
    //int newId = books.Any() ? books.Max(book=> book.Id) +1 : 1;
    var newBook = mapper.Map<Book>(newBookDTOS);


    // var book = new CreateBookDTOs
    // {
    //     Title = newBookDTOS.Title,
    //     Author = newBookDTOS.Author,
    //     PubDate = newBookDTOS.PubDate,
    // };
    //var bookReponse = mapper.Map<CreateBookDTOs>(newBookDTOS);
    books.Add(newBookDTOS);
    return Results.Created($"/book/{newBook.Id}", newBook);
});
#endregion

#region ENDPOINTE POUR MODIFIER

app.MapPut("/books/{id}",(int id, UpdateBookDTOs newBook, IMapper mapper) =>
{
    var bookToUpdate = books.FirstOrDefault(b => b.Id == id);
    if(bookToUpdate is null)
    {
        return Results.NotFound(new {Message = $"le livre de id {id} n'existe pas !"});
    }
    bookToUpdate.Title = newBook.Title ?? bookToUpdate.Title;
    bookToUpdate.Author = newBook.Author ?? bookToUpdate.Author;
    bookToUpdate.PubDate = newBook.PubDate != default ? newBook.PubDate : bookToUpdate.PubDate;
    return Results.NoContent();
});
#endregion

#region ENDPOINTs POUR SUPPRIMER

app.MapDelete("/books/{id}",(int id) =>
{
    var book = books.FirstOrDefault(book => book.Id == id);
    if(book is null)
    {
        return Results.NotFound(new {Message = $"Le book avec l'ID {id} n'existe pas !"});
    }
    books.Remove(book);
    return Results.Ok(book);
});
#endregion

app.Run();











