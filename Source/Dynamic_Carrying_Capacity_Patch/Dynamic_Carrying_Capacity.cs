using System.Reflection;
using HarmonyLib;
using Verse;

namespace Dynamic_Carrying_Capacity;

[StaticConstructorOnStartup]
public static class Dynamic_Carrying_Capacity
{
    static Dynamic_Carrying_Capacity()
    {
        new Harmony("yemg.rimworld").PatchAll(Assembly.GetExecutingAssembly());
    }
}