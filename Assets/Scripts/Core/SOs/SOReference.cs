using TripleTriad.Assets.Scripts.Core.SOs;

public abstract class SOReference<T> : SOReference
{
    public bool UseConstant = true;
    public T ConstantValue;
    
    public SOVariable<T> Variable;

    public SOReference()
    { }

    public SOReference(T value)
    {
        UseConstant = true;
        ConstantValue = value;
    }

    public T Value => UseConstant ? ConstantValue : Variable.Value;
}

public class SOReference
{
    
} 
