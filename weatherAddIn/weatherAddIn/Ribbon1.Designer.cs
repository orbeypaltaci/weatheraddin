namespace weatherAddIn
{
    partial class Ribbon1 : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public Ribbon1()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ribbon1));
            this.tab1 = this.Factory.CreateRibbonTab();
            this.weather_data = this.Factory.CreateRibbonGroup();
            this.Forecast_Button = this.Factory.CreateRibbonButton();
            this.Weather_Button = this.Factory.CreateRibbonButton();
            this.Settings_Group = this.Factory.CreateRibbonGroup();
            this.temp_menu = this.Factory.CreateRibbonMenu();
            this.celsius_toggle = this.Factory.CreateRibbonToggleButton();
            this.kelvin_toggle = this.Factory.CreateRibbonToggleButton();
            this.fahren_toggle = this.Factory.CreateRibbonToggleButton();
            this.wind_menu = this.Factory.CreateRibbonMenu();
            this.spm_toggle = this.Factory.CreateRibbonToggleButton();
            this.spft_toggle = this.Factory.CreateRibbonToggleButton();
            this.settingsButton = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.weather_data.SuspendLayout();
            this.Settings_Group.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Groups.Add(this.weather_data);
            this.tab1.Groups.Add(this.Settings_Group);
            resources.ApplyResources(this.tab1, "tab1");
            this.tab1.Name = "tab1";
            // 
            // weather_data
            // 
            this.weather_data.Items.Add(this.Forecast_Button);
            this.weather_data.Items.Add(this.Weather_Button);
            resources.ApplyResources(this.weather_data, "weather_data");
            this.weather_data.Name = "weather_data";
            // 
            // Forecast_Button
            // 
            resources.ApplyResources(this.Forecast_Button, "Forecast_Button");
            this.Forecast_Button.Name = "Forecast_Button";
            this.Forecast_Button.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.Forecast_Button_Click);
            // 
            // Weather_Button
            // 
            resources.ApplyResources(this.Weather_Button, "Weather_Button");
            this.Weather_Button.Name = "Weather_Button";
            this.Weather_Button.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.Weather_Button_Click);
            // 
            // Settings_Group
            // 
            this.Settings_Group.Items.Add(this.temp_menu);
            this.Settings_Group.Items.Add(this.wind_menu);
            this.Settings_Group.Items.Add(this.settingsButton);
            resources.ApplyResources(this.Settings_Group, "Settings_Group");
            this.Settings_Group.Name = "Settings_Group";
            // 
            // temp_menu
            // 
            this.temp_menu.Items.Add(this.celsius_toggle);
            this.temp_menu.Items.Add(this.kelvin_toggle);
            this.temp_menu.Items.Add(this.fahren_toggle);
            resources.ApplyResources(this.temp_menu, "temp_menu");
            this.temp_menu.Name = "temp_menu";
            // 
            // celsius_toggle
            // 
            resources.ApplyResources(this.celsius_toggle, "celsius_toggle");
            this.celsius_toggle.Name = "celsius_toggle";
            this.celsius_toggle.ShowImage = true;
            // 
            // kelvin_toggle
            // 
            resources.ApplyResources(this.kelvin_toggle, "kelvin_toggle");
            this.kelvin_toggle.Name = "kelvin_toggle";
            this.kelvin_toggle.ShowImage = true;
            // 
            // fahren_toggle
            // 
            resources.ApplyResources(this.fahren_toggle, "fahren_toggle");
            this.fahren_toggle.Name = "fahren_toggle";
            this.fahren_toggle.ShowImage = true;
            this.fahren_toggle.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.fahren_toggle_Click);
            // 
            // wind_menu
            // 
            this.wind_menu.Items.Add(this.spm_toggle);
            this.wind_menu.Items.Add(this.spft_toggle);
            resources.ApplyResources(this.wind_menu, "wind_menu");
            this.wind_menu.Name = "wind_menu";
            // 
            // spm_toggle
            // 
            resources.ApplyResources(this.spm_toggle, "spm_toggle");
            this.spm_toggle.Name = "spm_toggle";
            this.spm_toggle.ShowImage = true;
            // 
            // spft_toggle
            // 
            resources.ApplyResources(this.spft_toggle, "spft_toggle");
            this.spft_toggle.Name = "spft_toggle";
            this.spft_toggle.ShowImage = true;
            // 
            // settingsButton
            // 
            resources.ApplyResources(this.settingsButton, "settingsButton");
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.settingsButton_Click_1);
            // 
            // Ribbon1
            // 
            this.Name = "Ribbon1";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.Ribbon1_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.weather_data.ResumeLayout(false);
            this.weather_data.PerformLayout();
            this.Settings_Group.ResumeLayout(false);
            this.Settings_Group.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup weather_data;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton Weather_Button;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup Settings_Group;
        internal Microsoft.Office.Tools.Ribbon.RibbonMenu temp_menu;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton celsius_toggle;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton kelvin_toggle;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton fahren_toggle;
        internal Microsoft.Office.Tools.Ribbon.RibbonMenu wind_menu;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton spm_toggle;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton spft_toggle;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton settingsButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton Forecast_Button;
    }

    partial class ThisRibbonCollection
    {
        internal Ribbon1 Ribbon1
        {
            get { return this.GetRibbon<Ribbon1>(); }
        }
    }
}
