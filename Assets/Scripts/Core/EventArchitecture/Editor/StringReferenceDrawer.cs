using TripleTriad.Core.SOs;
using UnityEditor;

namespace TripleTriad.Core.EventArchitecture.Editor
{
    [CustomPropertyDrawer(typeof(StringReference))]
    public class StringReferenceDrawer : SOReferenceDrawer<StringReference>
    { }
}