using System;

namespace TripleTriad.Core.SOs
{
    [Serializable]
    public class StringReference : SOReference<string>
    {
        public static implicit operator string(StringReference reference)
        {
            return reference.Value;
        }
    }
}
