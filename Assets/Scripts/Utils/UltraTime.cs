using UnityEngine;

namespace AdvantTest.Utils
{
    public static class UltraTime
    {
        public static float DeltaTimeMultiplier = 1f;
        public static float deltaTime => Time.deltaTime * DeltaTimeMultiplier;
    }
}