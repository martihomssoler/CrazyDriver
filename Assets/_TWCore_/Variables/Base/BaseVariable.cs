using System;
using UnityEngine;

namespace TWCore.Variables
{
    [Serializable]
    public class BaseVariable<T> : ScriptableObject
    {
        public T Value;

        public BaseVariable()
        { }
        
        public BaseVariable(T value)
        {
            Value = value;
        }

        public void SetValue(T value)
        {
            Value = value;
        }

        public void SetValue(BaseVariable<T> value)
        {
            Value = value.Value;
        }

        public static implicit operator T(BaseVariable<T> variable) => variable.Value;
    }
}