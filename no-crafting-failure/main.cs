using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Reflection;

namespace AttackAndMove
{
    [BepInPlugin(ModGUID, ModName, ModVersion)]
    public class Mod : BaseUnityPlugin
    {
        public const string ModGUID = "com.xaymar.nocraftingfailure";
        public const string ModName = "No Crafting Failure";
        public const string ModVersion = "1.0.0";

        public static Mod instance = null;

        public Harmony harmony = null;
        public bool wasAttack = false;

        public Mod()
        {
            Mod.instance = this;
            harmony = new Harmony(ModGUID);
            harmony.PatchAll();
        }

        public ManualLogSource GetLogger()
        {
            return Logger;
        }
    }

    [HarmonyPatch(typeof(CookingManagerRework), "IsCookSuccess", MethodType.Normal)]
    [HarmonyPatch(new Type[] { typeof(float) })]
    class CookingManagerRework_IsCookSuccess_Patch
    {
        static void Postfix(CookingManagerRework __instance, ref bool __result)
        {
            __result = true;
        }
    }


    [HarmonyPatch(typeof(CookingManagerRework), "CalculateSuccessRate", MethodType.Normal)]
    [HarmonyPatch(new Type[] { typeof(int), typeof(int) })]
    class CookingManagerRework_CalculateSuccessRate_Patch
    {
        static void Postfix(CookingManagerRework __instance, ref float __result)
        {
            __result = 1.0f;
        }
    }
}