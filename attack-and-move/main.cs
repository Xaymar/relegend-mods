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
        public const string ModGUID = "com.xaymar.attackandmove";
        public const string ModName = "Attack & Move";
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

    [HarmonyPatch(typeof(MovementPlayer), "SetWalkCondition", MethodType.Normal)]
    [HarmonyPatch(new Type[] { typeof(int) })]
    class MovementPlayer_SetWalkCondition_Patch
    {
        static bool Prefix(MovementPlayer __instance, int value)
        {
            if (Mod.instance.wasAttack)
            {
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Mod.instance.GetLogger().LogInfo(string.Format("Called with {0}", value));
                Mod.instance.GetLogger().LogInfo(t.ToString());
                return false;
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(AttackPlayer), "ConditionSet", MethodType.Normal)]
    [HarmonyPatch(new Type[] { typeof(int) })]
    class AttackPlayer_ConditionSet_Patch
    {
        static bool Prefix(AttackPlayer __instance, int value)
        {
            if (Mod.instance.wasAttack)
            {
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Mod.instance.GetLogger().LogInfo(string.Format("Called with {0}", value));
                Mod.instance.GetLogger().LogInfo(t.ToString());
                Mod.instance.wasAttack = false;
                return false;
            }
            return true;
        }
    }



    [HarmonyPatch(typeof(AttackPlayer), "LeftAttack", MethodType.Normal)]
    [HarmonyPatch(new Type[] { typeof(string), typeof(WeaponType) })]
    class AttackPlayer_LeftAttack_Patch
    {
        static void Prefix(string _itemName, WeaponType _type)
        {
            Mod.instance.wasAttack = true;
        }

        static void DisabledPostfix(AttackPlayer __instance, string _itemName, WeaponType _type)
        {
            var herofield = __instance.GetType().GetField("Hero", BindingFlags.NonPublic | BindingFlags.Instance);
            NetworkHandlerPlayer player = (NetworkHandlerPlayer)herofield.GetValue(__instance);
            Item item;
            Weapon weapon;

            if (_itemName == string.Empty)
            {
                item = player.Equipment.CurrWeapon.slotItem;
            }
            else
            {
                item = ItemDatabase.DB[_itemName];
            }
            weapon = (Weapon)item;

            Mod.instance.GetLogger().LogInfo(string.Format("Left Attack with item {0} and type {1}.", item.itemName, weapon.weaponType));
            Mod.instance.GetLogger().LogInfo(string.Format("Have player {0}.", player.PlayerName));

            player.Equipment.SetBattlePosition(weapon.weaponType);
            __instance.isCombatMode = true;

            //player.Equipment.ChangeBattlePosition(true, item.itemName, weapon.weaponType);

            weapon.EquipItem(player);
            player.Anim.SetTrigger("Attack", true);
            player.Anim.SetInteger("WeaponLayer", item.itemAnimVar);
            player.Movement.SetWalkCondition(0);
            player.Attack.SetAttackCondition(1);
            
            
            // Code above allows us to slide up until the moment we actually shoot.
            // But it also interrupts attacking if left click isn't spammed.
            // When put into Postfix, we actually just start casting fireball like a Lvl1 Wizard in D&D.
        }
    }
}