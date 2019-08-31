using System;
using HarmonyLib;

namespace TutorialSkipper
{
    [HarmonyPatch(typeof(TutorialManager), "StartDisplayTutorial", new Type[] { typeof(string) })]
    class StartDisplayTutorialPatch
    {
        static bool Prefix(string _title)
        {
            Mod.instance.GetLogger().LogInfo(string.Format("Skipped Tutorial '{0}'.", _title));
            TutorialManager.instance.SearchTutorial(_title);
            TutorialManager.instance.unlockTutorials.Add(new UnlockInfo(_title, true));
            TutorialManager.instance.EndTutorial();
            return false;
        }
    }
}