using Flunt.Notifications;
using Todo.Domain.Commands;
using Todo.Domain.Commands.Contracts;
using Todo.Domain.Entities;
using Todo.Domain.Handlers.Contracts;
using Todo.Domain.Repositories;

namespace Todo.Domain.Handlers
{
    public class TodoHandler : 
        Notifiable,
        IHandler<CreateTodoCommand>,
        IHandler<UpdateTodoCommand>,
        IHandler<MarkTodoAsDoneCommand>,
        IHandler<MarkTodoAsUndoneCommand>
    {
        private readonly ITodoRepository _repository;

        public TodoHandler(ITodoRepository repository)
        {
            _repository = repository;
        }

        public ICommandResult Handler(CreateTodoCommand command)
        {
            // Fail Fast Validation
            command.Validate();
            if(command.Invalid)
                return new GenericCommandResult(false, "Ops, parece que sua tarefa está errada!", command.Notifications);

            // Gera um TodoItem
            var todo = new TodoItem(command.Title, command.User, command.Date);

            // Salva no Banco
            _repository.Create(todo);

            //Retorna o resultado
            return new GenericCommandResult(true, "Tarefa salva", todo);
        }

        public ICommandResult Handler(UpdateTodoCommand command)
        {
            // Fail Fast Validation
            command.Validate();
            if(command.Invalid)
                return new GenericCommandResult(false, "Ops, parece que sua tarefa está errada!", command.Notifications);

            // Recupera o TodoItem
            var todo = _repository.GetById(command.Id, command.User);

            // Altera o título
            todo.UpdateTitle(command.Title);

            // Salva no Banco
            _repository.Update(todo);

            //Retorna o resultado
            return new GenericCommandResult(true, "Tarefa salva", todo);
        }

        public ICommandResult Handler(MarkTodoAsDoneCommand command)
        {
            // Fail Fast Validation
            command.Validate();
            if(command.Invalid)
                return new GenericCommandResult(false, "Ops, parece que sua tarefa está errada!", command.Notifications);

            // Recupera o TodoItem
            var todo = _repository.GetById(command.Id, command.User);

            // Altera estado
            todo.MarkAsDone();

            // Salva no Banco
            _repository.Update(todo);

            //Retorna o resultado
            return new GenericCommandResult(true, "Tarefa salva", todo);
        }

        public ICommandResult Handler(MarkTodoAsUndoneCommand command)
        {
            // Fail Fast Validation
            command.Validate();
            if(command.Invalid)
                return new GenericCommandResult(false, "Ops, parece que sua tarefa está errada!", command.Notifications);

            // Recupera o TodoItem
            var todo = _repository.GetById(command.Id, command.User);

            // Altera estado
            todo.MarkAsUnDone();

            // Salva no Banco
            _repository.Update(todo);

            //Retorna o resultado
            return new GenericCommandResult(true, "Tarefa salva", todo);
        }
    }
}