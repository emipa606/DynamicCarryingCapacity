using System.Reflection;
using HarmonyLib;
using Verse;

namespace Dynamic_Carrying_Capacity;

[StaticConstructorOnStartup]
public static class Dynamic_Carrying_Capacity
{
    public static readonly bool CheckVehicles;

    static Dynamic_Carrying_Capacity()
    {
        new Harmony("yemg.rimworld").PatchAll(Assembly.GetExecutingAssembly());

        CheckVehicles = ModLister.GetActiveModWithIdentifier("SmashPhil.VehicleFramework") != null;
    }
}