using System;
using System.Linq;

using WebApi.DBOperations;


namespace WebApi.Application.GenreOperations.Command
{
    public class DeleteGenreCommand
    {
        private readonly BookStoreDbContext _context;

        public int GenreId { get; set; }
        public DeleteGenreCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x => x.Id == GenreId);
            if (genre is null)
                throw new InvalidOperationException("Kitap Türü Bulunamadı!");

            _context.Genres.Remove(genre);
            _context.SaveChanges();

        }

    }
}