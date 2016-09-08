using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UseFollowerAbility
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            GeneralAoeMobCount.Value = UseFollowerAbility.S.GeneralMobCount;
            GeneralUseOnlyOnBosses.Checked = UseFollowerAbility.S.UseOnlyOnBosses;
            GeneralUseOnlyOnElites.Checked = UseFollowerAbility.S.UseOnlyOnElites;
            UseDkMeatHookAtLowHp.Checked = UseFollowerAbility.S.DkMeatHookLowHp;
            DkMeatHookHpValue.Value = UseFollowerAbility.S.DkMeatHookHpValue;
            UsePriestLightOfTheNaaruLowHp.Checked = UseFollowerAbility.S.PriestLightOfTheNaaruLowHp;
            PriestLightOfTheNaaruHpValue.Value = UseFollowerAbility.S.PriestLightOfTheNaaruHpValue;
            RogueSealOfRavenholdtHpValue.Value = UseFollowerAbility.S.RogueSealOfRavenholdtMinimumHpValue;
            ShamanBoundlessQuintessenceHpValue.Value = UseFollowerAbility.S.ShamanBoundlessQuintessenceHpValue;
        }

        private void GeneralAoeMobCount_ValueChanged(object sender, EventArgs e)
        {
            UseFollowerAbility.S.GeneralMobCount = (int)GeneralAoeMobCount.Value;
        }

        private void GeneralUseOnlyOnBosses_CheckedChanged(object sender, EventArgs e)
        {
            UseFollowerAbility.S.UseOnlyOnBosses = GeneralUseOnlyOnBosses.Checked;
        }

        private void GeneralUseOnlyOnElites_CheckedChanged(object sender, EventArgs e)
        {
            UseFollowerAbility.S.UseOnlyOnElites = GeneralUseOnlyOnElites.Checked;
        }

        private void UseDkMeatHookAtLowHp_CheckedChanged(object sender, EventArgs e)
        {
            UseFollowerAbility.S.DkMeatHookLowHp = UseDkMeatHookAtLowHp.Checked;
        }

        private void DkMeatHookHpValue_ValueChanged(object sender, EventArgs e)
        {
            UseFollowerAbility.S.DkMeatHookHpValue = (int)DkMeatHookHpValue.Value;
        }

        private void UsePriestLightOfTheNaaruLowHp_CheckedChanged(object sender, EventArgs e)
        {
            UseFollowerAbility.S.PriestLightOfTheNaaruLowHp = UsePriestLightOfTheNaaruLowHp.Checked;
        }

        private void PriestLightOfTheNaaruHpValue_ValueChanged(object sender, EventArgs e)
        {
            UseFollowerAbility.S.PriestLightOfTheNaaruHpValue = (int)PriestLightOfTheNaaruHpValue.Value;
        }

        private void RogueSealOfRavenholdtHpValue_ValueChanged(object sender, EventArgs e)
        {
            UseFollowerAbility.S.RogueSealOfRavenholdtMinimumHpValue = (int)RogueSealOfRavenholdtHpValue.Value;
        }

        private void ShamanBoundlessQuintessenceHpValue_ValueChanged(object sender, EventArgs e)
        {
            UseFollowerAbility.S.ShamanBoundlessQuintessenceHpValue = (int)ShamanBoundlessQuintessenceHpValue.Value;
        }
    }
}
