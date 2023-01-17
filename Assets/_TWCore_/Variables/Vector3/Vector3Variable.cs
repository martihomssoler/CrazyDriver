using System;
using UnityEngine;

namespace TWCore.Variables
{
    [CreateAssetMenu]
    public class Vector3Variable : BaseVariable<Vector3>
    {
        #region ADD OPERATOR
        public static Vector3 operator +(Vector3Variable variableA, Vector3Variable variableB) => variableA.Value + variableB.Value;
        public static Vector3 operator +(Vector3Variable variable, Vector3 value) => variable.Value + value;
        public static Vector3 operator +(Vector3 value, Vector3Variable variable) => variable.Value + value;
        #endregion

        #region SUBTRACT OPERATOR
        public static Vector3 operator -(Vector3Variable variableA, Vector3Variable variableB) => variableA.Value - variableB.Value;
        public static Vector3 operator -(Vector3Variable variable, Vector3 value) => variable.Value - value;
        public static Vector3 operator -(Vector3 value, Vector3Variable variable) => variable.Value - value;
        #endregion

        #region PRODUCT OPERATOR
        public static Vector3 operator *(Vector3Variable variable, float value) => variable.Value * value;
        public static Vector3 operator *(float value, Vector3Variable variable) => variable.Value * value;
        #endregion
    }

}