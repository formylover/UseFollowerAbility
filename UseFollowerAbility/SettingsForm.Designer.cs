namespace UseFollowerAbility
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.GeneralAoeMobCount = new System.Windows.Forms.NumericUpDown();
            this.GeneralUseOnlyOnBosses = new System.Windows.Forms.CheckBox();
            this.GeneralUseOnlyOnElites = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.UseDkMeatHookAtLowHp = new System.Windows.Forms.CheckBox();
            this.DkMeatHookHpValue = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.PriestLightOfTheNaaruHpValue = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.UsePriestLightOfTheNaaruLowHp = new System.Windows.Forms.CheckBox();
            this.RogueSealOfRavenholdtHpValue = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.ShamanBoundlessQuintessenceHpValue = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GeneralAoeMobCount)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DkMeatHookHpValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PriestLightOfTheNaaruHpValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RogueSealOfRavenholdtHpValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShamanBoundlessQuintessenceHpValue)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.GeneralUseOnlyOnElites);
            this.groupBox1.Controls.Add(this.GeneralUseOnlyOnBosses);
            this.groupBox1.Controls.Add(this.GeneralAoeMobCount);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(255, 109);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "General";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "AoE Mob Count:";
            // 
            // GeneralAoeMobCount
            // 
            this.GeneralAoeMobCount.Location = new System.Drawing.Point(101, 23);
            this.GeneralAoeMobCount.Name = "GeneralAoeMobCount";
            this.GeneralAoeMobCount.Size = new System.Drawing.Size(69, 20);
            this.GeneralAoeMobCount.TabIndex = 1;
            this.GeneralAoeMobCount.ValueChanged += new System.EventHandler(this.GeneralAoeMobCount_ValueChanged);
            // 
            // GeneralUseOnlyOnBosses
            // 
            this.GeneralUseOnlyOnBosses.AutoSize = true;
            this.GeneralUseOnlyOnBosses.Location = new System.Drawing.Point(9, 55);
            this.GeneralUseOnlyOnBosses.Name = "GeneralUseOnlyOnBosses";
            this.GeneralUseOnlyOnBosses.Size = new System.Drawing.Size(123, 17);
            this.GeneralUseOnlyOnBosses.TabIndex = 2;
            this.GeneralUseOnlyOnBosses.Text = "Use Only On Bosses";
            this.GeneralUseOnlyOnBosses.UseVisualStyleBackColor = true;
            this.GeneralUseOnlyOnBosses.CheckedChanged += new System.EventHandler(this.GeneralUseOnlyOnBosses_CheckedChanged);
            // 
            // GeneralUseOnlyOnElites
            // 
            this.GeneralUseOnlyOnElites.AutoSize = true;
            this.GeneralUseOnlyOnElites.Location = new System.Drawing.Point(9, 78);
            this.GeneralUseOnlyOnElites.Name = "GeneralUseOnlyOnElites";
            this.GeneralUseOnlyOnElites.Size = new System.Drawing.Size(114, 17);
            this.GeneralUseOnlyOnElites.TabIndex = 3;
            this.GeneralUseOnlyOnElites.Text = "Use Only On Elites";
            this.GeneralUseOnlyOnElites.UseVisualStyleBackColor = true;
            this.GeneralUseOnlyOnElites.CheckedChanged += new System.EventHandler(this.GeneralUseOnlyOnElites_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label2.Location = new System.Drawing.Point(125, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 31);
            this.label2.TabIndex = 4;
            this.label2.Text = "} ?";
            this.toolTip1.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ShamanBoundlessQuintessenceHpValue);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.RogueSealOfRavenholdtHpValue);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.PriestLightOfTheNaaruHpValue);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.UsePriestLightOfTheNaaruLowHp);
            this.groupBox2.Controls.Add(this.DkMeatHookHpValue);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.UseDkMeatHookAtLowHp);
            this.groupBox2.Location = new System.Drawing.Point(13, 128);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(255, 179);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Class Specific";
            // 
            // UseDkMeatHookAtLowHp
            // 
            this.UseDkMeatHookAtLowHp.AutoSize = true;
            this.UseDkMeatHookAtLowHp.Location = new System.Drawing.Point(9, 33);
            this.UseDkMeatHookAtLowHp.Name = "UseDkMeatHookAtLowHp";
            this.UseDkMeatHookAtLowHp.Size = new System.Drawing.Size(229, 17);
            this.UseDkMeatHookAtLowHp.TabIndex = 0;
            this.UseDkMeatHookAtLowHp.Text = "Use DK > Rottgut > Meat Hook At Low HP";
            this.UseDkMeatHookAtLowHp.UseVisualStyleBackColor = true;
            this.UseDkMeatHookAtLowHp.CheckedChanged += new System.EventHandler(this.UseDkMeatHookAtLowHp_CheckedChanged);
            // 
            // DkMeatHookHpValue
            // 
            this.DkMeatHookHpValue.Location = new System.Drawing.Point(70, 52);
            this.DkMeatHookHpValue.Name = "DkMeatHookHpValue";
            this.DkMeatHookHpValue.Size = new System.Drawing.Size(69, 20);
            this.DkMeatHookHpValue.TabIndex = 3;
            this.DkMeatHookHpValue.ValueChanged += new System.EventHandler(this.DkMeatHookHpValue_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "HP %:";
            // 
            // PriestLightOfTheNaaruHpValue
            // 
            this.PriestLightOfTheNaaruHpValue.Location = new System.Drawing.Point(70, 97);
            this.PriestLightOfTheNaaruHpValue.Name = "PriestLightOfTheNaaruHpValue";
            this.PriestLightOfTheNaaruHpValue.Size = new System.Drawing.Size(69, 20);
            this.PriestLightOfTheNaaruHpValue.TabIndex = 6;
            this.PriestLightOfTheNaaruHpValue.ValueChanged += new System.EventHandler(this.PriestLightOfTheNaaruHpValue_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "HP %:";
            // 
            // UsePriestLightOfTheNaaruLowHp
            // 
            this.UsePriestLightOfTheNaaruLowHp.AutoSize = true;
            this.UsePriestLightOfTheNaaruLowHp.Location = new System.Drawing.Point(9, 78);
            this.UsePriestLightOfTheNaaruLowHp.Name = "UsePriestLightOfTheNaaruLowHp";
            this.UsePriestLightOfTheNaaruLowHp.Size = new System.Drawing.Size(219, 17);
            this.UsePriestLightOfTheNaaruLowHp.TabIndex = 4;
            this.UsePriestLightOfTheNaaruLowHp.Text = "Use Priest > Ishanah > Naaru At Low HP";
            this.UsePriestLightOfTheNaaruLowHp.UseVisualStyleBackColor = true;
            this.UsePriestLightOfTheNaaruLowHp.CheckedChanged += new System.EventHandler(this.UsePriestLightOfTheNaaruLowHp_CheckedChanged);
            // 
            // RogueSealOfRavenholdtHpValue
            // 
            this.RogueSealOfRavenholdtHpValue.Location = new System.Drawing.Point(180, 125);
            this.RogueSealOfRavenholdtHpValue.Name = "RogueSealOfRavenholdtHpValue";
            this.RogueSealOfRavenholdtHpValue.Size = new System.Drawing.Size(69, 20);
            this.RogueSealOfRavenholdtHpValue.TabIndex = 8;
            this.RogueSealOfRavenholdtHpValue.ValueChanged += new System.EventHandler(this.RogueSealOfRavenholdtHpValue_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 128);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(165, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Rogue Seal of Ravenholdt HP %:";
            // 
            // ShamanBoundlessQuintessenceHpValue
            // 
            this.ShamanBoundlessQuintessenceHpValue.Location = new System.Drawing.Point(180, 151);
            this.ShamanBoundlessQuintessenceHpValue.Name = "ShamanBoundlessQuintessenceHpValue";
            this.ShamanBoundlessQuintessenceHpValue.Size = new System.Drawing.Size(69, 20);
            this.ShamanBoundlessQuintessenceHpValue.TabIndex = 10;
            this.ShamanBoundlessQuintessenceHpValue.ValueChanged += new System.EventHandler(this.ShamanBoundlessQuintessenceHpValue_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 153);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(158, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Shaman Boundless Quint HP %:";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 319);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GeneralAoeMobCount)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DkMeatHookHpValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PriestLightOfTheNaaruHpValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RogueSealOfRavenholdtHpValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShamanBoundlessQuintessenceHpValue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown GeneralAoeMobCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox GeneralUseOnlyOnElites;
        private System.Windows.Forms.CheckBox GeneralUseOnlyOnBosses;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown PriestLightOfTheNaaruHpValue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox UsePriestLightOfTheNaaruLowHp;
        private System.Windows.Forms.NumericUpDown DkMeatHookHpValue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox UseDkMeatHookAtLowHp;
        private System.Windows.Forms.NumericUpDown ShamanBoundlessQuintessenceHpValue;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown RogueSealOfRavenholdtHpValue;
        private System.Windows.Forms.Label label5;
    }
}