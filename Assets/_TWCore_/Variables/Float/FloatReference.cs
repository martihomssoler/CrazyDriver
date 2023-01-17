using System;

namespace TWCore.Variables
{
    [Serializable]
    public class FloatReference : BaseReference<float>
    {
        #region ADD OPERATOR
        public static float operator +(FloatReference variableA, FloatReference variableB) => variableA.Value + variableB.Value;
        public static float operator +(FloatReference variable, float value) => variable.Value + value;
        public static float operator +(float value, FloatReference variable) => variable.Value + value;
        #endregion

        #region SUBTRACT OPERATOR
        public static float operator -(FloatReference variableA, FloatReference variableB) => variableA.Value - variableB.Value;
        public static float operator -(FloatReference variable, float value) => variable.Value - value;
        public static float operator -(float value, FloatReference variable) => variable.Value - value;
        #endregion

        #region PRODUCT OPERATOR
        public static float operator *(FloatReference variable, float value) => variable.Value * value;
        public static float operator *(float value, FloatReference variable) => variable.Value * value;
        public static float operator *(FloatReference variableA, FloatReference variableB) => variableA.Value * variableB.Value;
        #endregion
    }
}