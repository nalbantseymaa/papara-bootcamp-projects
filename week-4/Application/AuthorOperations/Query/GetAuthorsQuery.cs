using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Query
{
    public class GetAuthorsQuery
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorsQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //yazarlar kitaplarıyla birlikte listelenecek
        public List<AuthorsViewModel> Handle()
        {
            var authors = _context.Authors
                 .Include(x => x.Books)
                 .ThenInclude(b => b.Genre)
                 .ToList();


            List<AuthorsViewModel> returnObj = _mapper.Map<List<AuthorsViewModel>>(authors);
            return returnObj;

        }

        public class AuthorsViewModel
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public DateTime BirthDate { get; set; }
            public List<BookViewModel> Books { get; set; }

        }
    }
}