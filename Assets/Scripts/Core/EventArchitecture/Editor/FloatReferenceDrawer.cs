using TripleTriad.Core.SOs;
using UnityEditor;

namespace TripleTriad.Core.EventArchitecture.Editor
{
    [CustomPropertyDrawer(typeof(FloatReference))]
    public class FloatReferenceDrawer : SOReferenceDrawer<FloatReference>
    { }
}