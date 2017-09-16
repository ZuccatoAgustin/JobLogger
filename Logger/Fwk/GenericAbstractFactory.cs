using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger.Fwk
{
    /// <summary>
    /// It represents an Abstract Factory that always returns the same instance identified by a generic value.
    /// </summary>
    /// <typeparam name="T">Generic type that is used to identify a generic type to be build.</typeparam>
    /// <typeparam name="W">Generic type that builds the Factory.</typeparam>
    public abstract class GenericAbstractFactory<T, W>
        where T : IComparable
        where W : class
    {

        /// <summary>
        /// It contains intancias.
        /// </summary>
        private Dictionary<T, W> items = new Dictionary<T, W>();

        /// <summary>
        /// An instance of concrete class to be manufactured is added.
        /// </summary>
        /// <typeparam name="V"></typeparam>
        /// <param name="key"></param>
        protected void Add<U>(T key) where U : W, new()
        {
            this.items.Add(key, new U());
        }

        /// <summary>
        /// Returns an instance of the generic type.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual W Create(T key)
        {
            if (this.items.ContainsKey(key))
            {
                return this.items[key];
            }

            throw new ArgumentException();
        }
    }
}
