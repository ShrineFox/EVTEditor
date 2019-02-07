using System.Windows.Forms;

namespace EVTEditor
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectedTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectedObjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddCommandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newCommandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newObjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.trackBar = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.dgv_EVTProperties = new System.Windows.Forms.DataGridView();
            this.Field = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txt_MsgEditor = new System.Windows.Forms.TextBox();
            this.lbl_bmdName = new System.Windows.Forms.Label();
            this.dgv_ObjectProperties = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.listBox_Objects = new System.Windows.Forms.ListBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_EVTProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ObjectProperties)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.AddCommandToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(994, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Visible = false;
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectedTabToolStripMenuItem,
            this.selectedObjectToolStripMenuItem});
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.deleteToolStripMenuItem.Text = "Delete...";
            // 
            // selectedTabToolStripMenuItem
            // 
            this.selectedTabToolStripMenuItem.Name = "selectedTabToolStripMenuItem";
            this.selectedTabToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.selectedTabToolStripMenuItem.Text = "Selected Tab";
            this.selectedTabToolStripMenuItem.Click += new System.EventHandler(this.selectedTabToolStripMenuItem_Click);
            // 
            // selectedObjectToolStripMenuItem
            // 
            this.selectedObjectToolStripMenuItem.Name = "selectedObjectToolStripMenuItem";
            this.selectedObjectToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.selectedObjectToolStripMenuItem.Text = "Selected Object";
            this.selectedObjectToolStripMenuItem.Click += new System.EventHandler(this.selectedObjectToolStripMenuItem_Click);
            // 
            // AddCommandToolStripMenuItem
            // 
            this.AddCommandToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newCommandToolStripMenuItem,
            this.newObjectToolStripMenuItem});
            this.AddCommandToolStripMenuItem.Name = "AddCommandToolStripMenuItem";
            this.AddCommandToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.AddCommandToolStripMenuItem.Text = "Add...";
            this.AddCommandToolStripMenuItem.Visible = false;
            // 
            // newCommandToolStripMenuItem
            // 
            this.newCommandToolStripMenuItem.Name = "newCommandToolStripMenuItem";
            this.newCommandToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.newCommandToolStripMenuItem.Text = "New Command...";
            // 
            // newObjectToolStripMenuItem
            // 
            this.newObjectToolStripMenuItem.Name = "newObjectToolStripMenuItem";
            this.newObjectToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.newObjectToolStripMenuItem.Text = "New Object";
            this.newObjectToolStripMenuItem.Click += new System.EventHandler(this.newObjectToolStripMenuItem_Click);
            // 
            // tabControl
            // 
            this.tabControl.Location = new System.Drawing.Point(13, 28);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(374, 328);
            this.tabControl.TabIndex = 1;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.TabControl_SelectedIndexChanged);
            // 
            // trackBar
            // 
            this.trackBar.Enabled = false;
            this.trackBar.Location = new System.Drawing.Point(17, 358);
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(332, 45);
            this.trackBar.TabIndex = 2;
            this.trackBar.ValueChanged += new System.EventHandler(this.TrackBar_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(351, 366);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 3;
            // 
            // dgv_EVTProperties
            // 
            this.dgv_EVTProperties.AllowUserToAddRows = false;
            this.dgv_EVTProperties.AllowUserToDeleteRows = false;
            this.dgv_EVTProperties.AllowUserToResizeColumns = false;
            this.dgv_EVTProperties.AllowUserToResizeRows = false;
            this.dgv_EVTProperties.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_EVTProperties.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Field,
            this.Value});
            this.dgv_EVTProperties.Location = new System.Drawing.Point(393, 31);
            this.dgv_EVTProperties.Name = "dgv_EVTProperties";
            this.dgv_EVTProperties.RowHeadersVisible = false;
            this.dgv_EVTProperties.Size = new System.Drawing.Size(203, 112);
            this.dgv_EVTProperties.TabIndex = 4;
            // 
            // Field
            // 
            this.Field.HeaderText = "Field";
            this.Field.Name = "Field";
            this.Field.ReadOnly = true;
            this.Field.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Value
            // 
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            this.Value.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // txt_MsgEditor
            // 
            this.txt_MsgEditor.AcceptsReturn = true;
            this.txt_MsgEditor.AcceptsTab = true;
            this.txt_MsgEditor.Enabled = false;
            this.txt_MsgEditor.Location = new System.Drawing.Point(393, 161);
            this.txt_MsgEditor.Multiline = true;
            this.txt_MsgEditor.Name = "txt_MsgEditor";
            this.txt_MsgEditor.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_MsgEditor.Size = new System.Drawing.Size(384, 219);
            this.txt_MsgEditor.TabIndex = 5;
            this.txt_MsgEditor.Leave += new System.EventHandler(this.txt_MsgEditor_Leave);
            // 
            // lbl_bmdName
            // 
            this.lbl_bmdName.AutoSize = true;
            this.lbl_bmdName.Location = new System.Drawing.Point(395, 145);
            this.lbl_bmdName.Name = "lbl_bmdName";
            this.lbl_bmdName.Size = new System.Drawing.Size(0, 13);
            this.lbl_bmdName.TabIndex = 6;
            // 
            // dgv_ObjectProperties
            // 
            this.dgv_ObjectProperties.AllowUserToAddRows = false;
            this.dgv_ObjectProperties.AllowUserToDeleteRows = false;
            this.dgv_ObjectProperties.AllowUserToResizeColumns = false;
            this.dgv_ObjectProperties.AllowUserToResizeRows = false;
            this.dgv_ObjectProperties.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_ObjectProperties.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.dgv_ObjectProperties.Location = new System.Drawing.Point(784, 35);
            this.dgv_ObjectProperties.Name = "dgv_ObjectProperties";
            this.dgv_ObjectProperties.RowHeadersVisible = false;
            this.dgv_ObjectProperties.Size = new System.Drawing.Size(203, 344);
            this.dgv_ObjectProperties.TabIndex = 7;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Field";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Value";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // listBox_Objects
            // 
            this.listBox_Objects.FormattingEnabled = true;
            this.listBox_Objects.Location = new System.Drawing.Point(602, 34);
            this.listBox_Objects.Name = "listBox_Objects";
            this.listBox_Objects.Size = new System.Drawing.Size(175, 108);
            this.listBox_Objects.TabIndex = 8;
            this.listBox_Objects.SelectedIndexChanged += new System.EventHandler(this.listBox_Objects_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 392);
            this.Controls.Add(this.listBox_Objects);
            this.Controls.Add(this.dgv_ObjectProperties);
            this.Controls.Add(this.lbl_bmdName);
            this.Controls.Add(this.txt_MsgEditor);
            this.Controls.Add(this.dgv_EVTProperties);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBar);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(1010, 431);
            this.MinimumSize = new System.Drawing.Size(410, 431);
            this.Name = "MainForm";
            this.Text = "EVTEditor";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_EVTProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ObjectProperties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TrackBar trackBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgv_EVTProperties;
        private System.Windows.Forms.ToolStripMenuItem AddCommandToolStripMenuItem;
        private System.Windows.Forms.TextBox txt_MsgEditor;
        private System.Windows.Forms.Label lbl_bmdName;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectedTabToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.DataGridView dgv_ObjectProperties;
        private System.Windows.Forms.ListBox listBox_Objects;
        private System.Windows.Forms.ToolStripMenuItem selectedObjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newCommandToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newObjectToolStripMenuItem;
        private DataGridViewTextBoxColumn Field;
        private DataGridViewTextBoxColumn Value;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    }
    }

