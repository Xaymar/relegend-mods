using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace TutorialSkipper
{
    [BepInPlugin(ModGUID, ModName, ModVersion)]
    public class Mod : BaseUnityPlugin
    {
        public const string ModGUID = "com.xaymar.tutorialskipper";
        public const string ModName = "Tutorial Skipper";
        public const string ModVersion = "1.0.0";

        public static Mod instance = null;

        public Harmony harmony = null;

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
}