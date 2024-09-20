using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using RimWorld;
using Verse;

namespace Dynamic_Carrying_Capacity;

[HarmonyPatch(typeof(MassUtility), nameof(MassUtility.Capacity))]
public static class MassUtility_Capacity_Patch
{
    public static MethodInfo GetPawnBodySizeInfo = AccessTools.Method(typeof(Pawn), "get_BodySize");
    public static MethodInfo CalculateDynamicCapacityInfo = AccessTools.Method(typeof(MassUtility_Capacity_Patch), nameof(CalculateDynamicCapacity));

    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        int instructionRemovalCounter = 0;
        foreach (CodeInstruction instruction in instructions)
        {
            if (instructionRemovalCounter > 0)
            {
                instructionRemovalCounter--;
                continue;
            }
            if (instruction.opcode == OpCodes.Callvirt && instruction.operand == GetPawnBodySizeInfo)
            {
                instruction.operand = CalculateDynamicCapacityInfo;
                instructionRemovalCounter = 2;
            }
            yield return instruction;
        }
    }

    public static float CalculateDynamicCapacity(Pawn p)
    {
        return (float)Math.Round(
            5.0 
            * (Math.Sqrt(p.GetStatValue(StatDefOf.CarryingCapacity)) 
            * (1.0 + Math.Log10(p.BodySize))), 2);
    }
}