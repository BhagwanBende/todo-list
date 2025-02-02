using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TODO.Models
{
    public class ToDoDbContext : DbContext
    {

        public ToDoDbContext() : base("ToDoListDB") { }

        public DbSet<Task> Tasks { get; set; }

    }
}