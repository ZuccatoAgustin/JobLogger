using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger.Fwk
{
    //solo a modeo de ejemplo , usar FWK DI 

    public static class Locator
    {
        private readonly static Dictionary<Type, Func<object>>
            services = new Dictionary<Type, Func<object>>();

        public static void Register<T>(Func<T> resolver)
        {
            Locator.services[typeof(T)] = () => resolver();
        }

        public static T Resolve<T>()
        {
            if (Locator.services.Any(e => e.Key == typeof(T)))
            {
                return (T)Locator.services[typeof(T)]();
            }
            return default(T);
        }

        public static void Reset()
        {
            Locator.services.Clear();
        }
    }
}
