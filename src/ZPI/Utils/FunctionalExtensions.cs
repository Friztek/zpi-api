public static class FunctionalExtensions
{
    public static T Apply<T>(this T target, Action<T> action)
    {
        action?.Invoke(target);
        return target;
    }
}