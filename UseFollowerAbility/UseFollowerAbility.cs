using CommonBehaviors.Actions;
using Styx;
using Styx.Common;
using Styx.CommonBot;
using Styx.CommonBot.Coroutines;
using Styx.Plugins;
using Styx.TreeSharp;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media;

namespace UseFollowerAbility
{
    public class UseFollowerAbility : HBPlugin
    {


        public override void Pulse()
        {
            // check the current player and make sure they have the ability before we even try anything
            if (PlayerHasRequiredAbility())
            {
                // grab all enemies within 50 yards
                FindEnemies(Range: 50);

                // let's do stuff in close range for melee dps
                if (PlayerIsMeleeClass())
                {
                    // we have 3 or more enemies within 8 yards
                    if (activeEnemies(Me.Location, 8f).Count() >= 3)
                    {
                        if (CastMeleeAbilities()) { return; }       
                    }
                    if (activeEnemies(Me.Location, 8f).Count() >= 1)
                    {

                    }
                }
            }
        }

        #region Helper Methods

        #region Cast and Log

        /// <summary>
        /// Specifies the current target when casting spell and sleeps for lag
        /// </summary>
        /// <param name="Spell"></param>
        /// <param name="reqs"></param>
        /// <returns></returns>
        public bool Cast(int Spell, System.Windows.Media.Color newColor, bool reqs = true)
        {

            if (!CurrentTarget.IsValidCombatUnit())
                return false;
            if (!reqs)
            {
                //L.combatLog("Trying to cast: " + WoWSpell.FromId(Spell).Name + (String.IsNullOrEmpty(addLog) ? "" : " - " + addLog));
                return false;
            }

            if (!SpellManager.CanCast(WoWSpell.FromId(Spell), CurrentTarget, false, false, false)) //Should we check for if out currentTarget is moving? *Second false
                return false;
            if (!SpellManager.Cast(Spell, CurrentTarget))
                return false;
            //lastSpellCast = Spell;
            combatLog("^" + WoWSpell.FromId(Spell).Name, newColor);
            CommonCoroutines.SleepForLagDuration();
            return true;
        }
        public static void combatLog(string Message, System.Windows.Media.Color logColor, params object[] args)
        {
            if (Message == lastCombatMSG)
                return;

            if (CurrentTarget != null)
            {
                Logging.Write(logColor, "[UseFollowerAbility]: {0}" + string.Format($" on {CurrentTarget.SafeName} @ {CurrentTarget.HealthPercent.ToString("F2")}% at {CurrentTarget.Distance.ToString("F2")} yds with {Me.CurrentPower}/{Me.MaxPower} {CurrentPowerDescription}"), Message, args);
            }
            else
            {
                Logging.Write(logColor, "[UseFollowerAbility]: {0}" + string.Format($" on [no target] with {Me.CurrentPower}/{Me.MaxPower} {CurrentPowerDescription}"), Message, args);
            }


            lastCombatMSG = Message;
        }
        #endregion

        #region Find Enemies
        public static IEnumerable<WoWUnit> activeEnemies(Vector3 fromLocation, double Range)
        {
            var Hostile = enemyCount;
            return Hostile != null ? Hostile.Where(x => x.Location.DistanceSquared(fromLocation) <= Range * Range) : null;
        }

        private static IEnumerable<WoWUnit> surroundingEnemies() { return ObjectManager.GetObjectsOfTypeFast<WoWUnit>(); }
        private static List<WoWUnit> enemyCount { get; set; }
        public static void FindEnemies(double Range)
        {

            enemyCount.Clear();
            foreach (var u in surroundingEnemies())
            {
                if (u == null || !u.IsValid)//<~ Use isUnitValid(u, Range) instead of the frist five of these first calls?
                    continue;
                if (!u.IsAlive || u.DistanceSqr > Range * Range)
                    continue;
                if (!u.Attackable || !u.CanSelect)
                    continue;
                if (u.IsFriendly)
                    continue;
                if (u.IsNonCombatPet && u.IsCritter)
                    continue;

                // make sure the unit is targeting something we care about.
                if (u.IsTargetingMeOrPet || u.IsTargetingMyPartyMember || u.IsTargetingMyRaidMember)
                {
                    enemyCount.Add(u);
                }

            }
        }
        #endregion

        #region Has Abilities
        bool PlayerHasRequiredAbility()
        {
            if (Me.Class == WoWClass.DeathKnight) { if (SpellManager.HasSpell(MeatHook)) { return true; } return false; }
            if (Me.Class == WoWClass.DemonHunter) { if (SpellManager.HasSpell(MothersCaress)) { return true; } return false; }
            if (Me.Class == WoWClass.Druid) { if (SpellManager.HasSpell(NightmarishVisions)) { return true; } return false; }
            if (Me.Class == WoWClass.Hunter) { if (SpellManager.HasSpell(EmmarelsAssault)) { return true; } return false; }
            if (Me.Class == WoWClass.Mage) { if (SpellManager.HasSpell(KalecgosFury)) { return true; } return false; }
            if (Me.Class == WoWClass.Paladin) { if (SpellManager.HasSpell(BloodVanguard) || SpellManager.HasSpell(JudgementsGaze)) { return true; } return false; }
            if (Me.Class == WoWClass.Priest) { if (SpellManager.HasSpell(LightOfTheNaaru)) { return true; } return false; }
            if (Me.Class == WoWClass.Rogue) { if (SpellManager.HasSpell(SealOfRavenholdt)) { return true; } return false; }
            if (Me.Class == WoWClass.Shaman) { if (SpellManager.HasSpell(BoundlessQuintessence)) { return true; } return false; }
            if (Me.Class == WoWClass.Warlock) { if (SpellManager.HasSpell(EternalBanishment) || SpellManager.HasSpell(SixSoulBag)) { return true; } return false; }
            if (Me.Class == WoWClass.Warrior) { if (SpellManager.HasSpell(WindsOfTheNorth) || SpellManager.HasSpell(TitansWrath)) { return true; } return false; }

            // default false
            return false;
        }

        bool PlayerIsMeleeClass()
        {
            if (Me.Class == WoWClass.DeathKnight
                || Me.Class == WoWClass.DemonHunter
                || Me.Class == WoWClass.Rogue
                || Me.Class == WoWClass.Warrior
                || (Me.Class == WoWClass.Druid && (Me.Specialization == WoWSpec.DruidFeral || Me.Specialization == WoWSpec.DruidGuardian))
                || (Me.Class == WoWClass.Hunter && Me.Specialization == WoWSpec.HunterSurvival)
                || (Me.Class == WoWClass.Paladin && Me.Specialization != WoWSpec.PaladinHoly)
                || (Me.Class == WoWClass.Shaman && Me.Specialization == WoWSpec.ShamanEnhancement)
            )
            { return true; }

            return false;
        }

        bool PlayerIsRangedClass()
        {
            return !PlayerIsMeleeClass();
        }
        bool CastMeleeAbilities()
        {
            int SpellToCast = 0;
            if (Me.Class == WoWClass.DeathKnight)
            {
                SpellToCast = MeatHook;
                return false;
            }
            if (Me.Class == WoWClass.DemonHunter) { if (SpellManager.HasSpell(MothersCaress)) { return true; } return false; }
            if (Me.Class == WoWClass.Rogue) { if (SpellManager.HasSpell(SealOfRavenholdt)) { return true; } return false; }
            if (Me.Class == WoWClass.Warrior) { if (SpellManager.HasSpell(WindsOfTheNorth) || SpellManager.HasSpell(TitansWrath)) { return true; } return false; }

            if (Me.Class == WoWClass.Druid) { if (SpellManager.HasSpell(NightmarishVisions)) { return true; } return false; }
            if (Me.Class == WoWClass.Hunter) { if (SpellManager.HasSpell(EmmarelsAssault)) { return true; } return false; }
            if (Me.Class == WoWClass.Paladin) { if (SpellManager.HasSpell(BloodVanguard) || SpellManager.HasSpell(JudgementsGaze)) { return true; } return false; }
            if (Me.Class == WoWClass.Shaman) { if (SpellManager.HasSpell(BoundlessQuintessence)) { return true; } return false; }

            if (SpellToCast > 0)
            {
                if (Cast(MeatHook, Colors.Orange))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #endregion

        #region Static and overrides requirements
        private static LocalPlayer Me { get { return StyxWoW.Me; } }
        private static WoWUnit CurrentTarget { get { return StyxWoW.Me.CurrentTarget; } }
        public static string lastCombatMSG;
        private static string CurrentPowerDescription
        {
            get
            {
                return Me.PowerType.ToString().AddSpaces();
            }

        }
        public override string Author
        {
            get
            {
                return "SpeshulK926";
            }
        }

        public override string Name
        {
            get
            {
                return "Use Follower Ability";
            }
        }

        public override Version Version
        {
            get
            {
                return new Version(1, 0);
            }
        }
        #region Initialize
        public UseFollowerAbility()
        {
            enemyCount = new List<WoWUnit>();
        }
        #endregion
        #endregion

        public const int
            MeatHook = 221463,
            MothersCaress = 223137,
            NightmarishVisions = 218534,
            EmmarelsAssault = 219699,
            KalecgosFury = 220906,
            BloodVanguard = 222619,
            JudgementsGaze = 221742,
            LightOfTheNaaru = 222112,
            SealOfRavenholdt = 222357,
            BoundlessQuintessence = 222542,
            EternalBanishment = 215976,
            EredarTwins = 216153,
            SixSoulBag = 215850,
            WindsOfTheNorth = 222931,
            TitansWrath = 223132,
            Done = 0;
    }

    static class Extensions
    {
        public static string AddSpaces(this string data)
        {
            Regex r = new Regex(@"(?!^)(?=[A-Z])");
            return r.Replace(data, " ");
        }
        public static bool HasAura(this Styx.WoWInternals.WoWObjects.LocalPlayer me, IEnumerable<int> auraList)
        {
            foreach (int aura in auraList)
            {
                if (me.HasAura(aura)) { return true; }
            }
            return false;
        }
        /// <summary>
        /// Returns a null-safe integer using an int.TryParse on the string.
        /// <para>return 0 if no good otherwise, will return the data</para>
        /// </summary>
        /// <param name="intData"></param>
        /// <returns></returns>
        public static uint SafeGetInt(this string intData)
        {
            try
            {
                uint safeInt = 0;
                if (uint.TryParse(intData, out safeInt))
                {
                    return safeInt;
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        public static bool IsValidCombatUnit(this WoWUnit Unit)
        {
            return Unit != null && Unit.IsValid && Unit.IsAlive && Unit.Attackable;
        }
        //public static bool IsInMeleeRange(this WoWUnit Unit, LocalPlayer me)
        //{
        //    return Unit.Distance < calculateMeleeRange(Unit, me);
        //}
        private static float calculateMeleeRange(WoWUnit Unit, LocalPlayer me)
        {
            return !me.GotTarget ? 0f : Math.Max(5f, me.CombatReach + 1.3333334f + Unit.CombatReach);
        }
    }
}
