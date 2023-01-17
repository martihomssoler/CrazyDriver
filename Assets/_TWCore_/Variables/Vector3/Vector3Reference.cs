using System;
using UnityEngine;

namespace TWCore.Variables
{
    [Serializable]
    public class Vector3Reference : BaseReference<Vector3>
    {
        #region ADD OPERATOR
        public static Vector3 operator +(Vector3Reference variableA, Vector3Reference variableB) => variableA.Value + variableB.Value;
        public static Vector3 operator +(Vector3Reference variable, Vector3 value) => variable.Value + value;
        public static Vector3 operator +(Vector3 value, Vector3Reference variable) => variable.Value + value;
        #endregion

        #region SUBTRACT OPERATOR
        public static Vector3 operator -(Vector3Reference variableA, Vector3Reference variableB) => variableA.Value - variableB.Value;
        public static Vector3 operator -(Vector3Reference variable, Vector3 value) => variable.Value - value;
        public static Vector3 operator -(Vector3 value, Vector3Reference variable) => variable.Value - value;
        #endregion

        #region PRODUCT OPERATOR
        public static Vector3 operator *(Vector3Reference variable, float value) => variable.Value * value;
        public static Vector3 operator *(float value, Vector3Reference variable) => variable.Value * value;
        #endregion
    }
}