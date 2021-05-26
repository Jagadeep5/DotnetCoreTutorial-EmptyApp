using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace core_tool_empty.Data
{
    public class Books
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        [Required]
        public int Pages { get; set; }
    }
}
