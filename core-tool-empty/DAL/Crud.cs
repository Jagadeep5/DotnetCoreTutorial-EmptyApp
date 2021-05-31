using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core_tool_empty.Data;
using core_tool_empty.DALEntity;

namespace core_tool_empty.DAL
{
    public class Crud : ICrud
    {
        public readonly AppDBContext _appDB = null;

        public Crud(AppDBContext appDBContext)
        {
            _appDB = appDBContext;
        }
        public int AddBook(Books books)
        {
            this._appDB.tblBooks.Add(books);
            this._appDB.SaveChanges();
            return books.Id;
        }

        public async Task DeleteBook(int id)
        {
            Books books = await this._appDB.tblBooks.FindAsync(id);
            var delete =  this._appDB.tblBooks.Remove(books);
            this._appDB.SaveChanges();
        }

        public IEnumerable<Books> GetBooks()
        {
            return this._appDB.tblBooks;
        }

        
    }
}
