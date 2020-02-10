using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Todo.Domain.Entities;
using Todo.Domain.Queries;

namespace Todo.Domain.Tests.HandlerTests
{
    [TestClass]
    public class TodoQueriesTests
    {
        private List<TodoItem> _items;

        public TodoQueriesTests()
        {
            _items = new List<TodoItem>();
            _items.Add(new TodoItem("Tarefa 1", "usuarioA", DateTime.Now));
            _items.Add(new TodoItem("Tarefa 2", "usuarioB", DateTime.Now));
            _items.Add(new TodoItem("Tarefa 3", "guilherme", DateTime.Now));
            _items.Add(new TodoItem("Tarefa 4", "usuarioA", DateTime.Now));
            _items.Add(new TodoItem("Tarefa 5", "guilherme", DateTime.Now));
        }

        [TestMethod]
        public void Dado_a_consulta_deve_retornar_tarefas_apenas_do_usuario_guilherme()
        {
            var result = _items.AsQueryable().Where(TodoQueries.GetAll("guilherme"));
            Assert.AreEqual(2, result.Count());
        }
    }
}