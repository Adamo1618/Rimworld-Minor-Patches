using System;
using System.Reflection;
using Harmony;
using RimWorld;
using Verse;

namespace VariableBleedRate
{
    [StaticConstructorOnStartup]
    internal static class Main
    {
        static Main()
        {
            Log.Message("VBRF :: Patching TotalBleedRate");
            HarmonyInstance harmonyInstance = HarmonyInstance.Create("com.rate.bleed");
            harmonyInstance.PatchAll(Assembly.GetExecutingAssembly());
        }
    }

    [HarmonyPatch(typeof(HediffSet), "CalculateBleedRate")]
    internal static class bleedRatePatch
    {
        public static void Postfix(ref float __result, HediffSet __instance)
        {
            __result *= StatExtension.GetStatValue(__instance.pawn, StatDef.Named("BleedRate"), true);
        }
    }
}