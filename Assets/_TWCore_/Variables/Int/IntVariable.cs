using UnityEngine;

namespace TWCore.Variables
{
    [CreateAssetMenu]
    public class IntVariable : BaseVariable<int>
    {
        public void Add(int x) => Value += x;    
        public void Subtract(int x) => Value -= x;    
    }
}