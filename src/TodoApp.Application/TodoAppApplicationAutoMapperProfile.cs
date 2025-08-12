using AutoMapper;

namespace TodoApp
{
    public class TodoAppApplicationAutoMapperProfile : Profile
    {
        public TodoAppApplicationAutoMapperProfile()
        {
            CreateMap<TodoItem, TodoItemDto>()
                .ForAllMembers(opts => opts.PreCondition((src, dest, srcValue) => srcValue != null));

            CreateMap<TodoItemDto, TodoItem>()
                .ForAllMembers(opts => opts.PreCondition((src, dest, srcValue) => srcValue != null));
        }
    }
}
