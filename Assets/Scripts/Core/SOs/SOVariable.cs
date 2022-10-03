using System;
using UnityEngine;

namespace TripleTriad.Assets.Scripts.Core.SOs
{
    public abstract class SOVariable<T> : ScriptableObject
    {
        #if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
        #endif
        
        public T Value;

        [NonSerialized] public T RuntimeValue;
        
        public void SetValue(T value)
        {
            Value = value;
        }

        public void SetValue(SOVariable<T> value)
        {
            Value = value.Value;
        }
    }
}