namespace Shop
{
    using System.Collections.Generic;
    using NServiceBus;

    /// <summary>
    /// A string object bag of commandContext objects.
    /// </summary>
    public class CommandContext : ReadOnlyCommandContext
    {
        /// <summary>
        /// Initialized a new instance of <see cref="CommandContext" />.
        /// </summary>
        public CommandContext(ISendOnlyBus bus)
        {
            parent = null;
            Bus = bus;
        }

        public ISendOnlyBus Bus { get; }

        /// <summary>
        /// Retrieves the specified type from the commandContext.
        /// </summary>
        /// <typeparam name="T">The type to retrieve.</typeparam>
        /// <returns>The type instance.</returns>
        public T Get<T>()
        {
            return Get<T>(typeof(T).FullName);
        }

        /// <summary>
        /// Tries to retrieves the specified type from the commandContext.
        /// </summary>
        /// <typeparam name="T">The type to retrieve.</typeparam>
        /// <param name="result">The type instance.</param>
        /// <returns><code>true</code> if found, otherwise <code>false</code>.</returns>
        public bool TryGet<T>(out T result)
        {
            return TryGet(typeof(T).FullName, out result);
        }

        /// <summary>
        /// Tries to retrieves the specified type from the commandContext.
        /// </summary>
        /// <typeparam name="T">The type to retrieve.</typeparam>
        /// <param name="key">The key of the value being looked up.</param>
        /// <param name="result">The type instance.</param>
        /// <returns><code>true</code> if found, otherwise <code>false</code>.</returns>
        public bool TryGet<T>(string key, out T result)
        {
            object value;
            if (stash.TryGetValue(key, out value))
            {
                result = (T) value;
                return true;
            }

            if (parent != null)
            {
                return parent.TryGet(key, out result);
            }

            if (typeof(T).IsValueType)
            {
                result = default(T);
                return false;
            }

            result = default(T);
            return false;
        }

        public string GetParameters()
        {
            return currentCommandLine;
        }

        public void SetParameters(string commandLine)
        {
            currentCommandLine = commandLine;
        }

        /// <summary>
        /// Gets the requested extension, a new one will be created if needed.
        /// </summary>
        public T GetOrCreate<T>() where T : class, new()
        {
            T value;

            if (TryGet(out value))
            {
                return value;
            }

            var newInstance = new T();

            Set(newInstance);

            return newInstance;
        }


        /// <summary>
        /// Stores the type instance in the commandContext.
        /// </summary>
        /// <typeparam name="T">The type to store.</typeparam>
        /// <param name="t">The instance type to store.</param>
        public void Set<T>(T t)
        {
            Set(typeof(T).FullName, t);
        }


        /// <summary>
        /// Removes the instance type from the commandContext.
        /// </summary>
        /// <typeparam name="T">The type to remove.</typeparam>
        public void Remove<T>()
        {
            Remove(typeof(T).FullName);
        }


        /// <summary>
        /// Merges the passed commandContext into this one.
        /// </summary>
        /// <param name="commandContext">The source commandContext.</param>
        internal void Merge(CommandContext commandContext)
        {
            foreach (var kvp in commandContext.stash)
            {
                stash[kvp.Key] = kvp.Value;
            }
        }

        /// <summary>
        /// Stores the passed instance in the commandContext.
        /// </summary>
        public void Set<T>(string key, T t)
        {
            stash[key] = t;
        }

        /// <summary>
        /// Walk the tree of commandContext until one is found of the type <typeparamref name="T" />.
        /// </summary>
        internal bool TryGetRootContext<T>(out T result) where T : CommandContext
        {
            var cast = this as T;
            if (cast != null)
            {
                result = cast;
                return true;
            }

            if (parent == null)
            {
                result = null;
                return false;
            }

            return parent.TryGetRootContext(out result);
        }

        T Get<T>(string key)
        {
            T result;

            if (!TryGet(key, out result))
            {
                throw new KeyNotFoundException("No item found in behavior commandContext with key: " + key);
            }

            return result;
        }

        public void Remove(string key)
        {
            stash.Remove(key);
        }

        string currentCommandLine;

        CommandContext parent;

        Dictionary<string, object> stash = new Dictionary<string, object>();
    }
}