using PersonalManager.Application.Common.Mappings;
using PersonalManager.Domain.Entities;

namespace PersonalManager.Application.TodoLists.Queries.ExportTodos
{
    public class TodoItemRecord : IMapFrom<TodoItem>
    {
        public string Title { get; set; }

        public bool Done { get; set; }
    }
}
