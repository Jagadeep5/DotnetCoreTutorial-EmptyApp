using System.Collections.Generic;
using System.Threading.Tasks;
using core_tool_empty.Data;

namespace core_tool_empty.DALEntity
{
    public interface ICrud
    {
        int AddBook(Books books);

        IEnumerable<Books> GetBooks();

        Task DeleteBook(int id);
    }
}