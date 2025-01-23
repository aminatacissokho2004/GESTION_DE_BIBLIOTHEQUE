// les DTOs peuvent interagir avec les bases de donnees
namespace Gestion_de_Bibliothéque.DTOs
{
    public record BookDTOs
    {
        public int Id { get; set; }
        public string? Title { get; set; }  //Pour en lever les warning
        public string? Author { get; set; }    //on fait un point d'iterrogation apres le type
        public DateOnly PubDate { get; set; }
    }
}