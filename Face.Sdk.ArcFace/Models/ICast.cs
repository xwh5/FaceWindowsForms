namespace Face.Sdk.ArcFace.Models
{
    public interface ICast<out T>
    {
        T Cast();
    }
}