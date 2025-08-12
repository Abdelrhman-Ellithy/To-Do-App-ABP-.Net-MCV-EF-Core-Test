using System;
using Volo.Abp.Domain.Entities;

namespace TodoApp
{
    public class TodoItem : BasicAggregateRoot<Guid>
    {
        private string _text;

        public string Text
        {
            get => _text;
            set
            {
                if (value.Length > 1000)
                {
                    throw new ArgumentException("Text cannot be longer than 1000 characters", nameof(value));
                }
                _text = value;
            }
        }
    }
}