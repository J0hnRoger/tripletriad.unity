using System;

namespace TripleTriad.Core.SOs
{
    [Serializable]
    public class FloatReference : SOReference<float>
    {
        public static implicit operator float(FloatReference reference)
        {
            return reference.Value;
        }
    }
}
