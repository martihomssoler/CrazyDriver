using System.Collections.Generic;
using UnityEngine;

namespace TWCore.Variables
{
    public class BaseSetVariable<T> : ScriptableObject
    {
        public List<T> Values = new List<T>();

        public void Add(T thing)
        {
            if (!Values.Contains(thing))
                Values.Add(thing);
        }

        public void Remove(T thing)
        {
            if (Values.Contains(thing))
                Values.Remove(thing);
        }

        public void Clear()
        {
            Values.Clear();
        }
    }
}