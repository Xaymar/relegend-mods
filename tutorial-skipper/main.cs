using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace TutorialSkipper
{
    [BepInPlugin(ModGUID, ModName, ModVersion)]
    public class TutorialSkipper : BaseUnityPlugin
    {
        public const string ModGUID = "com.xaymar.tutorialskipper";
        public const string ModName = "Tutorial Skippper";
        public const string ModVersion = "1.0.0";

        public static TutorialSkipper instance = null;

        public Harmony harmony = null;

        public TutorialSkipper()
        {
            TutorialSkipper.instance = this;
            harmony = new Harmony(ModGUID);
            harmony.PatchAll();
        }

        public ManualLogSource GetLogger()
        {
            return Logger;
        }
    }
}