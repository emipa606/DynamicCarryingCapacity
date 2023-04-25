using System;
using System.Text;
using HarmonyLib;
using RimWorld;
using Verse;

namespace Dynamic_Carrying_Capacity;

[HarmonyPatch(typeof(MassUtility), nameof(MassUtility.Capacity))]
public static class MassUtility_Capacity_Patch
{
    public static bool Prefix(Pawn p, ref float __result, ref StringBuilder explanation)
    {
        if (!MassUtility.CanEverCarryAnything(p))
        {
            __result = 0f;
            return false;
        }

        var num = (float)Math.Round(
            5.0 * (Math.Sqrt(p.GetStatValue(StatDefOf.CarryingCapacity)) * (1.0 + Math.Log10(p.BodySize))), 2);
        if (explanation != null)
        {
            if (explanation.Length > 0)
            {
                explanation.AppendLine();
            }

            explanation.Append($"  - {p.LabelShortCap}: {num.ToStringMassOffset()}");
        }

        __result = num;
        return false;
    }
}