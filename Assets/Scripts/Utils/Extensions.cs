using System;
using System.Runtime.CompilerServices;
using Leopotam.EcsLite;

namespace AdvantTest.Utils
{
    public static class Extensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetFirstEntity(this EcsFilter filter)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            if (filter.GetEntitiesCount() <= 0)
                throw new Exception("No entities in filter!");
#endif
            return filter.GetRawEntities()[0];
        }
    }
}