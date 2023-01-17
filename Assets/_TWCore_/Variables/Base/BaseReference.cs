using System;

namespace TWCore.Variables
{
    [Serializable]
    public class BaseReference<T>
    {
        public bool UseConstant = true;
        public T ConstantValue;
        public BaseVariable<T> Variable;

        public BaseReference()
        { }

        public BaseReference(BaseReference<T> baseRef)
        {
            UseConstant = baseRef.UseConstant;
            ConstantValue = baseRef.ConstantValue;
            Variable = baseRef.Variable;
        }

        public BaseReference(T value)
        {
            UseConstant = true;
            ConstantValue = value;
        }

        public T Value => UseConstant ? ConstantValue : Variable.Value;

        public void SetValue(T value)
        {
            if (UseConstant) ConstantValue = value;
            else Variable.SetValue(value);
        }

        public void WithVariable(BaseVariable<T> value)
        {
            UseConstant = false;
            Variable = value;
        }

        public static implicit operator T(BaseReference<T> reference) => reference.Value;

        public override string ToString() => UseConstant ? ConstantValue.ToString() : Variable.Value.ToString();
    }
}