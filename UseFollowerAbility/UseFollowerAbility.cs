using Buddy.Coroutines;
using CommonBehaviors.Actions;
using Styx;
using Styx.Common;
using Styx.CommonBot;
using Styx.CommonBot.Coroutines;
using Styx.Helpers;
using Styx.Plugins;
using Styx.TreeSharp;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;
using System;
using System.Collections.Generic;
using System.IO;
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
        public static FaSettings S = new FaSettings();

        private readonly List<Coroutine> _coroutines = new List<Coroutine>();

        public override void Pulse()
        {
            if (!StyxWoW.IsInGame || !StyxWoW.IsInWorld || Me == null || !Me.IsValid || !Me.IsAlive || Me.Mounted)
                return;
            if (!Me.Combat)
                return;

            // check the current player and make sure they have the ability before we even try anything
            if (PlayerHasRequiredAbility())
            {
                // remove any already-finished routines
                for (int i = _coroutines.Count - 1; i >= 0; i--)
                {
                    if (_coroutines[i].IsFinished)
                        _coroutines.RemoveAt(i);
                    else
                        _coroutines[i].Resume();
                }

                // grab all enemies within 50 yards
                FindEnemies(Range: 50);

                // let's do stuff in close range for melee dps
                if (PlayerIsMeleeClass())
                {
                    // we have 3 or more enemies within 8 yards
                    if (activeEnemies(Me.Location, 8f).Count() >= S.GeneralMobCount)
                    {
                        _coroutines.Add(new Coroutine(() => CastMeleeAbilities(aoe: true)));
                        return;
                    }
                    _coroutines.Add(new Coroutine(() => CastMeleeAbilities()));
                    return;
                }
                else
                {
                    // ranged targets don't need 8 yard radius
                    if (activeEnemies(Me.Location, 40f).Count() >= S.GeneralMobCount)
                    {
                        _coroutines.Add(new Coroutine(() => CastRangedAbilities(aoe: true)));
                        return;
                    }
                    _coroutines.Add(new Coroutine(() => CastRangedAbilities()));
                    return;
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
        public async Task<bool> Cast(int Spell, Color newColor, bool reqs = true)
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
            await CommonCoroutines.SleepForLagDuration();
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
        bool UseBossEliteCheck()
        {
            return (S.UseOnlyOnBosses && CurrentTarget.IsBoss) || (S.UseOnlyOnElites && CurrentTarget.Elite) || (!S.UseOnlyOnBosses && !S.UseOnlyOnElites);
        }
        bool PlayerIsRangedClass()
        {
            return !PlayerIsMeleeClass();
        }
        /// <summary>
        /// These are cast within an 8 yard range since they are melee
        /// </summary>
        /// <param name="aoe"></param>
        /// <returns></returns>
        async Task<bool> CastMeleeAbilities(bool aoe = false)
        {
            if (Me.Class == WoWClass.DeathKnight) { if (await CastMeatHook(aoe)) { return true; } }
            if (Me.Class == WoWClass.DemonHunter) { if (await CastMothersCaress(aoe)) { return true; } }
            if (Me.Class == WoWClass.Rogue) { if (await CastSealOfRavenholdt()) { return true; } }
            if (Me.Class == WoWClass.Warrior) { if (await CastWindsOfTheNorth(aoe)) { return true; } }
            if (Me.Class == WoWClass.Warrior) { if (await CastTitansWrath(aoe)) { return true; } }
            if (Me.Class == WoWClass.Druid) { if (await CastNightmarishVisions()) { return true; } }
            if (Me.Class == WoWClass.Hunter) { if (await CastEmmarelsAssault(aoe)) { return true; } }
            if (Me.Class == WoWClass.Paladin) { if (await CastBloodVanguard(aoe)) { return true; } }
            if (Me.Class == WoWClass.Paladin) { if (await CastJudgementsGaze(aoe)) { return true; } }
            if (Me.Class == WoWClass.Shaman) { if (await CastBoundlessQuintessence()) { return true; } }

            return false;
        }

        async Task<bool> CastRangedAbilities(bool aoe = false)
        {
            if (Me.Class == WoWClass.Druid) { if (await CastNightmarishVisions()) { return true; } }
            if (Me.Class == WoWClass.Hunter) { if (await CastEmmarelsAssault(aoe)) { return true; } }
            if (Me.Class == WoWClass.Paladin) { if (await CastBloodVanguard(aoe)) { return true; } }
            if (Me.Class == WoWClass.Paladin) { if (await CastJudgementsGaze(aoe)) { return true; } }
            if (Me.Class == WoWClass.Shaman) { if (await CastBoundlessQuintessence()) { return true; } }
            if (Me.Class == WoWClass.Mage) { if (await CastKalecgosFury(aoe)) { return true; } }
            if (Me.Class == WoWClass.Priest) { if (await CastLightOfTheNaaru(aoe)) { return true; } }
            if (Me.Class == WoWClass.Warlock) { if (await CastEternalBanishment()) { return true; } }
            if (Me.Class == WoWClass.Warlock) { if (await CastSixSoulBag()) { return true; } }

            return false;
        }
        #region Cast Individual Spells
        async Task<bool> CastMeatHook(bool aoe)
        {

            if (aoe || (S.DkMeatHookLowHp && Me.HealthPercent <= S.DkMeatHookHpValue))
            {
                if (await Cast(MeatHook, Colors.Orange))
                {
                    return true;
                }
            }
            return false;
        }
        async Task<bool> CastMothersCaress(bool aoe)
        {

            if (aoe || UseBossEliteCheck())
            {
                if (await Cast(MothersCaress, Colors.Orange))
                {
                    return true;
                }
            }
            return false;
        }

        async Task<bool> CastNightmarishVisions()
        {

            if (UseBossEliteCheck())
            {
                if (await Cast(NightmarishVisions, Colors.Orange))
                {
                    return true;
                }
            }
            return false;
        }
        async Task<bool> CastEmmarelsAssault(bool aoe)
        {

            if (aoe || UseBossEliteCheck())
            {
                if (await Cast(EmmarelsAssault, Colors.Orange))
                {
                    return true;
                }
            }
            return false;
        }
        async Task<bool> CastKalecgosFury(bool aoe)
        {

            if (aoe || UseBossEliteCheck())
            {
                if (await Cast(KalecgosFury, Colors.Orange))
                {
                    return true;
                }
            }
            return false;
        }

        async Task<bool> CastBloodVanguard(bool aoe)
        {

            if (aoe || UseBossEliteCheck())
            {
                if (await Cast(BloodVanguard, Colors.Orange))
                {
                    return true;
                }
            }
            return false;
        }
        async Task<bool> CastJudgementsGaze(bool aoe)
        {

            if (aoe || UseBossEliteCheck())
            {
                if (await Cast(JudgementsGaze, Colors.Orange))
                {
                    return true;
                }
            }
            return false;
        }
        async Task<bool> CastLightOfTheNaaru(bool aoe)
        {

            if (aoe || S.PriestLightOfTheNaaruLowHp && Me.HealthPercent <= S.PriestLightOfTheNaaruHpValue)
            {
                if (await Cast(LightOfTheNaaru, Colors.Orange))
                {
                    return true;
                }
            }
            return false;
        }
        async Task<bool> CastSealOfRavenholdt()
        {

            if (UseBossEliteCheck() && CurrentTarget.HealthPercent >= S.RogueSealOfRavenholdtMinimumHpValue)
            {
                if (await Cast(SealOfRavenholdt, Colors.Orange))
                {
                    return true;
                }
            }
            return false;
        }
        async Task<bool> CastBoundlessQuintessence()
        {

            if (Me.HealthPercent <= S.ShamanBoundlessQuintessenceHpValue)
            {
                if (await Cast(BoundlessQuintessence, Colors.Orange))
                {
                    return true;
                }
            }
            return false;
        }
        async Task<bool> CastEternalBanishment()
        {

            if (UseBossEliteCheck())
            {
                if (await Cast(EternalBanishment, Colors.Orange))
                {
                    return true;
                }
            }
            return false;
        }
        async Task<bool> CastEredarTwins()
        {

            if (UseBossEliteCheck())
            {
                if (await Cast(EredarTwins, Colors.Orange))
                {
                    return true;
                }
            }
            return false;
        }
        async Task<bool> CastSixSoulBag()
        {

            if (UseBossEliteCheck())
            {
                if (await Cast(SixSoulBag, Colors.Orange))
                {
                    return true;
                }
            }
            return false;
        }
        async Task<bool> CastWindsOfTheNorth(bool aoe)
        {

            if (aoe || UseBossEliteCheck())
            {
                if (await Cast(WindsOfTheNorth, Colors.Orange))
                {
                    return true;
                }
            }
            return false;
        }
        async Task<bool> CastTitansWrath(bool aoe)
        {

            if (aoe || UseBossEliteCheck())
            {
                if (await Cast(TitansWrath, Colors.Orange))
                {
                    return true;
                }
            }
            return false;
        }






        #endregion

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
        private void ClearCoroutines()
        {
            foreach (Coroutine ct in _coroutines)
                ct.Dispose();

            _coroutines.Clear();
        }

        private void BotEvents_OnBotStopped(EventArgs args)
        {
            ClearCoroutines();
        }
        public override void OnEnable()
        {
            BotEvents.OnBotStopped += BotEvents_OnBotStopped;
            enemyCount = new List<WoWUnit>();
            combatLog("Enabled!", Colors.OrangeRed);
        }
        public override void OnDisable()
        {
            ClearCoroutines();
            enemyCount = null;
            combatLog("Disabled!", Colors.OrangeRed);
        }



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

    public class FaSettings : Settings
    {
        public FaSettings()
            : base(Path.Combine(SettingsDirectory, "UseFollowerAbility"))
        { }
        [Setting, DefaultValue(3)]
        public int GeneralMobCount { get; set; }
        [Setting, DefaultValue(false)]
        public bool UseOnlyOnBosses { get; set; }
        [Setting, DefaultValue(false)]
        public bool UseOnlyOnElites { get; set; }
        [Setting, DefaultValue(true)]
        public bool DkMeatHookLowHp { get; set; }
        [Setting, DefaultValue(60)]
        public int DkMeatHookHpValue { get; set; }
        [Setting, DefaultValue(true)]
        public bool PriestLightOfTheNaaruLowHp { get; set; }
        [Setting, DefaultValue(60)]
        public int PriestLightOfTheNaaruHpValue { get; set; }
        [Setting, DefaultValue(90)]
        public int RogueSealOfRavenholdtMinimumHpValue { get; set; }
        [Setting, DefaultValue(60)]
        public int ShamanBoundlessQuintessenceHpValue { get; set; }

    }
}
