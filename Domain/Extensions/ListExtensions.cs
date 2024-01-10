namespace Domain.Extensions;

public static class ListExtensions {
    public static List<TEntity> Shuffle<TEntity>(this List<TEntity> list) {
        var rng = new Random();
        var n = list.Count;
        while (n > 1) {
            n--;
            var k = rng.Next(n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }
        return list;
    }
}