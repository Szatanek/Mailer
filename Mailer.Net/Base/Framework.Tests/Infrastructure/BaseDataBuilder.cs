using System;
using System.Collections.Generic;

namespace Framework.Tests.Infrastructure
{
    public abstract class BaseDataBuilder
    {
        private readonly Queue<Tuple<string, object>> Actions = new Queue<Tuple<string, object>>();

        public void Build()
        {
            while (Actions.Count > 0)
            {
                var action = Actions.Dequeue();
                Execute(action.Item1, action.Item2);
            }
        }

        protected abstract void Execute(string sql, object parameters);

        protected void Enqueue(string sql, object parameters = null)
        {
            Actions.Enqueue(new Tuple<string, object>(sql, parameters));
        }
    }
}