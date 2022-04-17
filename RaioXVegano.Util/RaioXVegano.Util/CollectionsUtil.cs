using System.Collections.Generic;

namespace RaioXVegano.Util
{
    public static class CollectionsUtil
    {
        public static IList<int> AddIfDoesntExists(this IList<int> @this, int value) 
        {
            if (!@this.Contains(value))
            {
                @this.Add(value);
            }

            return @this;
        }
    }
}
