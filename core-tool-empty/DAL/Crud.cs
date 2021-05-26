using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core_tool_empty.Data;

namespace core_tool_empty.DAL
{
    public class Crud
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
    }
}
