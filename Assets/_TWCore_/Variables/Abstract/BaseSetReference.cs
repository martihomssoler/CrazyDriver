using System;
using System.Collections.Generic;

namespace TWCore.Variables
{
    [Serializable]
    public class BaseSetReference<T>
    {
        public bool UseConstant = true;
        public List<T> ConstantValue;
        public BaseSetVariable<T> Variable;

        public BaseSetReference()
        { }

        public BaseSetReference(List<T> value)
        {
            UseConstant = true;
            ConstantValue = value;
        }

        public List<T> Values => UseConstant ? ConstantValue : Variable.Values;

        public static implicit operator List<T>(BaseSetReference<T> reference) => reference.Values;

        public override string ToString() => UseConstant ? ConstantValue.ToString() : Variable.Values.ToString();
    }
}