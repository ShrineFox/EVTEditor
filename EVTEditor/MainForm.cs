using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EvtTool;
using Newtonsoft.Json;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Numerics;
using PAKPack;
using System.IO;
using AtlusFileSystemLibrary.FileSystems.PAK;
using AtlusFileSystemLibrary.Common.IO;
using System.Threading;
using System.Diagnostics;

namespace EVTEditor
{
    public partial class MainForm : Form
    {
        EvtFile evt = new EvtFile();
        string evtPath = "";
        string pakPath = "";
        string bmdPath = "";
        string bmd = "";
        DataGridView dgv = new DataGridView();
        DataGridView dgv2 = new DataGridView();
        string[] commandTypes = new string[] { "AlEf", "CAA_", "CAR_", "CClp", "Chap", "Cht_",
            "CMCn", "CMD_", "CQuk", "CSA_", "CSD_", "CSEc", "CShk", "CwCl", "CwHt", "CwP_",
            "Date", "EAlp", "ELd_", "EMD_", "EnBc", "EnCc", "EnDf", "EnFD", "EnFH", "EnHd",
            "EnL0", "EnLI", "EnOl", "EnPh", "EnSh", "Env_", "ERgs", "EScl", "ESD_", "ESH_",
            "FAA_", "FAB_", "FbEn", "Fd__", "FDFl", "FdS_", "FGFl", "Flbk", "FOD_", "FrJ_",
            "FS__", "GCAP", "GGGg", "GPoe", "ImDp", "LBX_", "MAA_", "MAAB", "MAB_", "MAI_",
            "MAlp", "MAt_", "MAtO", "MAI_", "MDt_", "MFts", "MIc_", "ML__", "MLa_", "MLd_",
            "MLw_", "MMD_", "MRgs", "MRot", "MScl", "MSD_", "Msg_", "MsgR", "MSSs", "MvCt",
            "MvPl", "PBDs", "PBNs", "PBRd", "PBSt", "PCc_", "PLf_", "PRum", "Scr_", "SFlt",
            "SsCp", "TCol", "TRgs", "TrMt", "TScl" };

        public MainForm()
        {
            InitializeComponent();

            //Set up dropdown for adding commands
            ToolStripMenuItem[] items = new ToolStripMenuItem[commandTypes.Count()];
            for (int i = 0; i < items.Length; i++)
            {
                items[i] = new ToolStripMenuItem();
                items[i].Name = commandTypes[i];
                items[i].Tag = commandTypes[i];
                items[i].Text = commandTypes[i];
                items[i].Click += new EventHandler(AddNewCommandTypeClickHandler);
            }
            newCommandToolStripMenuItem.DropDownItems.AddRange(items);

            dgv.CellEndEdit += new DataGridViewCellEventHandler((sender, e) => UpdateEVTCommand(sender, e));
            dgv2.CellEndEdit += new DataGridViewCellEventHandler((sender, e) => UpdateEVTCommandData(sender, e));
            dgv_EVTProperties.CellEndEdit += new DataGridViewCellEventHandler((sender, e) => UpdateEVTProperties(sender, e));
            dgv_ObjectProperties.CellEndEdit += new DataGridViewCellEventHandler((sender, e) => UpdateEVTObject(sender, e));
        }

        //Update Object's property value in EVT to the changed cell's value
        private void UpdateEVTObject(object sender, DataGridViewCellEventArgs e)
        {
            //Narrow down the selected property in the evt so we can update it
            EvtObject obj = evt.Objects.Find(o => Convert.ToInt32(GetSubstringByString("[", "]", listBox_Objects.SelectedItem.ToString())) == evt.Objects.IndexOf(o));
            PropertyInfo prop = obj.GetType().GetProperty(dgv_ObjectProperties.CurrentCell.OwningRow.Cells[0].Value.ToString());
            
            //Try to update value using the proper type
            var cellValue = dgv_ObjectProperties.CurrentCell.Value;
            UpdateProperty(prop, obj, cellValue);
            BeginInvoke(new MethodInvoker(RefreshEVT));
        }

        //Attempt to deduce proper type of new property value and set it
        private void UpdateProperty(PropertyInfo prop, object obj, object cellValue)
        {
            try
            {
                if (prop.PropertyType.IsEnum)
                {
                    //Try to set enum value
                    var newEnumValue = Enum.Parse(prop.PropertyType, cellValue.ToString());
                    prop.SetValue(obj, newEnumValue);
                }
                else if (cellValue.ToString().Contains("<"))
                {
                    //Try to set the value as a vector
                    prop.SetValue(obj, StringToVec(cellValue.ToString()));
                }
                else
                {
                    //Try to automatically deduce type if all else fails
                    var newCellValue = Convert.ChangeType(cellValue, prop.PropertyType);
                    prop.SetValue(obj, newCellValue);
                }
            }
            catch { }
        }

        private void GridViewRefresh(DataGridView dg)
        {
            dg.Rows.Clear();
            dg.Refresh();
        }

        //Give generated datagridviews the following properties
        private void GridViewSetup(DataGridView dg)
        {
            dg.ColumnCount = 2;

            dg.Columns[0].ReadOnly = true;
            dg.AllowUserToAddRows = false;
            dg.AllowUserToResizeColumns = false;
            dg.AllowUserToResizeRows = false;
            dg.AllowUserToDeleteRows = false;
            dg.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dg.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            dg.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            dg.Name = "Command Parameters";
            dg.AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            dg.ColumnHeadersBorderStyle =
                DataGridViewHeaderBorderStyle.Single;
            dg.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dg.GridColor = Color.Black;
            dg.RowHeadersVisible = false;

            dg.Columns[0].Name = "Field";
            dg.Columns[1].Name = "Value";
            dg.Columns[0].Width = 80;
            dg.Columns[1].Width = 97;

            dg.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dg.MultiSelect = false;
            dg.Dock = DockStyle.Left;
            dg.Width = 180;
        }

        //Add a command at the selected Time
        private void AddNewCommandTypeClickHandler(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            var command = new Command();
            command.Type = clickedItem.Name;
            command.Time = trackBar.Value;
            command.Data = CommandDataFactory.Create(clickedItem.Name);

            evt.Commands.Add(command);
            RefreshEVT();
        }

        //Open an EVT or Event PAK and enable controls
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                string path = dialog.FileName;

                if (path.EndsWith("evt", StringComparison.InvariantCultureIgnoreCase))
                    evt = new EvtFile(path);
                else if (path.EndsWith("pak", StringComparison.InvariantCultureIgnoreCase))
                    ReadPAK(path);
                else
                    return;

                //If EVT successfully opened, enable controls
                this.Text = $"EVTEditor - {Path.GetFileName(path)}";
                EVTSetup();
            }
        }

        //Enable controls and populate with data from EVT
        private void EVTSetup()
        {
            //Set up gridviews
            GridViewSetup(dgv);
            GridViewSetup(dgv2);
            dgv2.Dock = DockStyle.Right;

            //Populate gridviews
            RefreshEVT();

            //Enable controls
            AddCommandToolStripMenuItem.Visible = true;
            saveToolStripMenuItem.Visible = true;
            trackBar.Enabled = true;
        }

        //Get an EVT from a PAK and decompile event's referenced BMD
        private void ReadPAK(string path)
        {
            pakPath = path;
            using (var pak = new PAKFileSystem(FormatVersion.Version3BE))
            {
                pak.Load(path);
                foreach (string file in pak.EnumerateFiles())
                {
                    var normalizedFilePath = file.Replace("../", ""); // Remove backwards relative path
                    string filePath = Path.GetDirectoryName(path) + Path.DirectorySeparatorChar + normalizedFilePath;
                    using (var stream = FileUtils.Create(filePath))
                    using (var inputStream = pak.OpenFile(file))
                        inputStream.CopyTo(stream);
                    if (file.EndsWith("evt", StringComparison.InvariantCultureIgnoreCase))
                    {
                        evtPath = filePath;
                        evt = new EvtFile(evtPath);
                    }
                    else if (file.EndsWith("bmd", StringComparison.InvariantCultureIgnoreCase))
                    {
                        bmdPath = filePath;
                        DecompileBMD();
                    }
                }
            }
        }

        //Replace a PAK with new BMD and EVT and save as new PAK
        private void SavePAK(string path)
        {
            using (var pak = new PAKFileSystem(FormatVersion.Version3BE))
            {
                pak.Load(pakPath);
                string evtHandle = "";
                string bmdHandle = "";
                foreach (string file in pak.EnumerateFiles())
                {
                    if (file.EndsWith("evt", StringComparison.InvariantCultureIgnoreCase))
                        evtHandle = file;
                    else if (file.EndsWith("bmd", StringComparison.InvariantCultureIgnoreCase))
                        bmdHandle = file;
                }

                if (evtHandle != "")
                    pak.AddFile(evtHandle, evtPath, AtlusFileSystemLibrary.ConflictPolicy.Replace);
                if (bmdHandle != "")
                    pak.AddFile(bmdHandle, bmdPath, AtlusFileSystemLibrary.ConflictPolicy.Replace);

                pak.Save(path);
            } 
        }
        

        //Update Command's property value in EVT to the changed cell's value
        private void UpdateEVTCommand(object sender, DataGridViewCellEventArgs e)
        {
            //Narrow down the selected property in the evt so we can update it
            Command command = evt.Commands.Find(cmd => Convert.ToInt32(GetSubstringByString("[", "]", tabControl.SelectedTab.Text)) == evt.Commands.IndexOf(cmd));
            PropertyInfo prop = command.GetType().GetProperty(dgv.CurrentCell.OwningRow.Cells[0].Value.ToString());

            //Try to update value using the proper type
            var cellValue = dgv.CurrentCell.Value;
            UpdateProperty(prop, command, cellValue);
            BeginInvoke(new MethodInvoker(RefreshEVT));
        }

        //Update EVT Property value in EVT to the changed cell's value
        private void UpdateEVTProperties(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                evt.Version = Convert.ToUInt32(dgv_EVTProperties.Rows[0].Cells[1].Value);
                evt.MajorId = Convert.ToInt16(dgv_EVTProperties.Rows[1].Cells[1].Value);
                evt.MinorId = Convert.ToInt16(dgv_EVTProperties.Rows[2].Cells[1].Value);
                evt.Duration = Convert.ToInt32(dgv_EVTProperties.Rows[3].Cells[1].Value);
            }
            catch { }

            BeginInvoke(new MethodInvoker(RefreshEVT));
        }

        //Update CommandData's property value in EVT to the changed cell's value
        private void UpdateEVTCommandData(object sender, DataGridViewCellEventArgs e)
        {
            //Narrow down the selected property in the evt so we can update it
            Command command = evt.Commands.Find(cmd => Convert.ToInt32(GetSubstringByString("[", "]", tabControl.SelectedTab.Text)) == evt.Commands.IndexOf(cmd));
            var cmdData = CommandDataFactory.Create(GetSubstringByString("[", "]", tabControl.SelectedTab.Text));
            cmdData = command.Data;

            //Try to update value using the proper type
            PropertyInfo prop = cmdData.GetType().GetProperty(dgv2.CurrentCell.OwningRow.Cells[0].Value.ToString());
            var cellValue = dgv2.CurrentCell.Value;
            UpdateProperty(prop, cmdData, cellValue);

            //Update tabs and datagridview with new data
            BeginInvoke(new MethodInvoker(RefreshEVT));
        }

        //Update EVT command/commandData datagridview tabs based on Time
        private void TrackBar_ValueChanged(object sender, EventArgs e)
        {
            tabControl.TabPages.Clear();
            foreach (var command in evt.Commands)
            {
                if (command.Time == trackBar.Value)
                {
                    //Add tab for each Command that matches the selected Time value
                    TabPage tp = new TabPage($"{command.Type}[{evt.Commands.IndexOf(command)}]");
                    tabControl.TabPages.Add(tp);
                }
            }
            if (tabControl.TabPages.Count != 0)
            {
                //Select first command in tab if there are any
                tabControl.SelectTab(0);
                //Update tab with command's info
                TabControl_SelectedIndexChanged(sender, e);
            }
                

            label1.Text = $"{trackBar.Value}/{evt.Duration}";
        }

        //Update tabs based on currently selected Command
        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.TabPages.Count != 0)
            {
                TabPage tp = tabControl.SelectedTab;
                GridViewRefresh(dgv);
                GridViewRefresh(dgv2);
                tp.Controls.Add(dgv);
                tp.Controls.Add(dgv2);

                foreach (Command command in evt.Commands.Where(cmd => Convert.ToInt32(GetSubstringByString("[", "]", tabControl.SelectedTab.Text)) == evt.Commands.IndexOf(cmd)))
                {
                    //Update datagridview with command and command data properties
                    foreach (PropertyInfo prop in typeof(Command).GetProperties())
                    {
                        if (prop.Name != "Data" )
                            dgv.Rows.Add(new string[] { $"{prop.Name}", $"{prop.GetValue(command, null)}" });
                        else
                        {
                            var cmdData = CommandDataFactory.Create(GetSubstringByString("[", "]", tabControl.SelectedTab.Text));
                            cmdData = command.Data;
                            var cmdDataType = cmdData.GetType();
                            foreach (PropertyInfo cmdDataProp in cmdDataType.GetProperties())
                            {
                                if (prop.Name != "Entries")
                                    dgv2.Rows.Add(new string[] { $"{cmdDataProp.Name}", $"{cmdDataProp.GetValue(cmdData, null)}" });
                            }
                        }
                    }

                    //Decompile and show BMD text if possible
                    if (File.Exists(bmdPath) && command.Type == "Msg_")
                    {
                        txt_MsgEditor.Enabled = true;
                        txt_MsgEditor.Text = bmd;
                    }
                    else
                    {
                        txt_MsgEditor.Enabled = false;
                        txt_MsgEditor.Clear();
                    }
                        
                }

            }
        }

        //Update tabs and datagridview to reflect new values
        private void RefreshEVT()
        {
            //Save indices to try to restore location after refresh
            int oldTabIndex = tabControl.SelectedIndex;
            int oldTrackBarValue = trackBar.Value;
            int oldObjectSelection = listBox_Objects.SelectedIndex;

            //Reorder commands by time value before refreshing tabs
            evt = ReindexCommands(evt);

            //Make sure trackbar can be moved (needs a better solution later)
            if (evt.Duration >= 2)
                trackBar.Maximum = evt.Duration;
            else
                trackBar.Maximum = 2;
            trackBar.Minimum = 1;

            //Clear and refresh datagridviews/tabpages
            GridViewRefresh(dgv_EVTProperties);
            GridViewRefresh(dgv);
            GridViewRefresh(dgv2);
            GridViewRefresh(dgv_ObjectProperties);
            tabControl.TabPages.Clear();
            tabControl.Refresh();

            //Change tab index to force tabs to update, hacky workaround
            trackBar.Value = 2;
            trackBar.Value = 1;

            //Restore positions if possible
            if (trackBar.Maximum >= oldTrackBarValue && oldTrackBarValue > 0)
                trackBar.Value = oldTrackBarValue;
            if (tabControl.TabCount >= oldTabIndex && oldTabIndex > 0)
                tabControl.SelectedIndex = oldTabIndex;

            //Update EVT Properties
            dgv_EVTProperties.Rows.Add(new string[] { $"Version", $"{evt.Version}" });
            dgv_EVTProperties.Rows.Add(new string[] { $"MajorId", $"{evt.MajorId}" });
            dgv_EVTProperties.Rows.Add(new string[] { $"MinorId", $"{evt.MinorId}" });
            dgv_EVTProperties.Rows.Add(new string[] { $"Duration", $"{evt.Duration}" });

            //Reorder objects by Id value
            evt = ReindexObjects(evt);
            //Change selected index of objects list (needs a better solution later)
            if (evt.Objects.Count() >= 2)
            {
                listBox_Objects.SelectedIndex = 1;
                listBox_Objects.SelectedIndex = 0;
            }
            //Restore Object selection position if possible
            if (listBox_Objects.Items.Count >= oldObjectSelection && oldObjectSelection >= 0)
                listBox_Objects.SelectedIndex = oldObjectSelection;
        }

        //Reassign Time values and update Duration
        public static EvtFile ReindexCommands(EvtFile evt)
        {
            int previousValue = evt.Commands.OrderBy(x => x.Time).First().Time;
            int newTime = 1;

            foreach (var command in evt.Commands.OrderBy(x => x.Time))
            {
                if (command.Time != previousValue)
                {
                    ++newTime;
                    previousValue = command.Time;
                }
                command.Time = newTime;
            }

            evt.Duration = newTime + 1;
                
            return evt;
        }

        //Assign new indices to Objects
        public EvtFile ReindexObjects(EvtFile evt)
        {
            listBox_Objects.Items.Clear();
            int i = 0;
            foreach (EvtObject obj in evt.Objects)
            {
                listBox_Objects.Items.Add($"{obj.Type}[{i}]");
                i++;
            }

            return evt;
        }

        //Get string value between two string values
        public static string GetSubstringByString(string a, string b, string c)
        {
            return c.Substring((c.IndexOf(a) + a.Length), (c.IndexOf(b) - c.IndexOf(a) - a.Length));
        }

        //Convert string to vector
        public Vector3 StringToVec(string s)
        {
            string[] temp = s.Substring(1, s.Length - 2).Split(',');
            return new Vector3(float.Parse(temp[0]), float.Parse(temp[1]), float.Parse(temp[2]));
        }

        //Remove selected tab's Command from EVT
        private void selectedTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (evt.Commands.Count > 1)
            {
                try
                {
                    Command command = evt.Commands.Find(cmd => Convert.ToInt32(GetSubstringByString("[", "]", tabControl.SelectedTab.Text)) == evt.Commands.IndexOf(cmd));
                    evt.Commands.Remove(command);
                    RefreshEVT();
                }
                catch { }
            }
        }

        //Start new EVT from scratch
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Re-initialize some things that may have changed if you already opened a PAK
            evtPath = "";
            pakPath = "";
            bmdPath = "";
            bmd = "";
            this.Text = "EVTEditor";
            txt_MsgEditor.Enabled = false;
            evt = new EvtFile();
            //Add field command to EVT to start with
            var command = new Command();
            command.Type = "Fd__";
            command.Time = trackBar.Value;
            command.DataSize = 64;
            command.Data = CommandDataFactory.Create(command.Type);
            evt.Commands.Add(command);
            //Add field object to EVT to start with
            var obj = new EvtObject();
            obj.Type = EvtObjectType.Field;
            evt.Objects.Add(obj);

            //Enable controls
            AddCommandToolStripMenuItem.Visible = true;
            trackBar.Enabled = true;

            //Enable datagridviews
            EVTSetup();
        }

        //Update Objects datagridview based on selected Object
        private void listBox_Objects_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRefresh(dgv_ObjectProperties);
            if (listBox_Objects.SelectedIndex >= 0)
            {
                EvtObject obj = evt.Objects[listBox_Objects.SelectedIndex];

                //Update datagridview with object properties
                foreach (PropertyInfo prop in typeof(EvtObject).GetProperties())
                    dgv_ObjectProperties.Rows.Add(new string[] { $"{prop.Name}", $"{prop.GetValue(obj, null)}" });
            }
        }

        //Add example Field Object to EVT
        private void newObjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var obj = new EvtObject();
            obj.Type = EvtObjectType.Character;
            obj.Id = listBox_Objects.Items.Count;
            evt.Objects.Add(obj);
            RefreshEVT();
        }

        private void DecompileBMD()
        {
            string compilerPath = $"{Directory.GetCurrentDirectory()}\\AtlusScriptCompiler\\AtlusScriptCompiler.exe";
            if (File.Exists(bmdPath) && File.Exists(compilerPath))
            {
                string msgPath = $"{Path.GetDirectoryName(bmdPath)}\\{Path.GetFileName(bmdPath)}.msg";
                //Decompile
                Process cmd = new Process();
                cmd.StartInfo.FileName = compilerPath;
                cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                cmd.StartInfo.Arguments = $"\"{bmdPath}\" -Decompile -Library P5 -Encoding P5";
                cmd.Start();
                cmd.Close();
                Thread.Sleep(1000);
                //Display message
                if (File.Exists(msgPath))
                    bmd = File.ReadAllText(msgPath);
            }
    }

        //Display updated text
        private void txt_MsgEditor_Leave(object sender, EventArgs e)
        {
            if (txt_MsgEditor.Text != "")
                bmd = txt_MsgEditor.Text;
        }

        //Recompile BMD with edited text
        private void CompileBMD()
        {
            string compilerPath = $"{Directory.GetCurrentDirectory()}\\AtlusScriptCompiler\\AtlusScriptCompiler.exe";
            string msgPath = $"{Path.GetDirectoryName(bmdPath)}\\{Path.GetFileName(bmdPath)}.msg";
            if (txt_MsgEditor.Enabled && File.Exists(msgPath) && File.Exists(compilerPath) && bmd != "")
            {
                //Overwrite msg with txtbox text
                File.WriteAllText(msgPath, txt_MsgEditor.Text);
                //Compile
                Process cmd = new Process();
                cmd.StartInfo.FileName = compilerPath;
                cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                cmd.StartInfo.Arguments = $"\"{msgPath}\" -Compile -Library P5 -Encoding P5 -OutFormat V1BE";
                cmd.Start();
                Thread.Sleep(1000);
                cmd.Close();
                //Overwrite original bmd
                File.Copy($"{bmdPath}.msg.bmd", bmdPath, true);
            }
        }

        //Remove selected Object from EVT
        private void selectedObjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (evt.Objects.Count > 1)
            {
                try
                {
                    EvtObject obj = evt.Objects.Find(o => Convert.ToInt32(GetSubstringByString("[", "]", listBox_Objects.SelectedItem.ToString())) == evt.Objects.IndexOf(o));
                    evt.Objects.Remove(obj);
                    RefreshEVT();
                }
                catch { }
            }
        }

        //Save EVT or PAK
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommonSaveFileDialog dialog = new CommonSaveFileDialog();
            
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                string path = dialog.FileName;
                if (path.EndsWith("evt", StringComparison.InvariantCultureIgnoreCase))
                    evt.Save(path);
                else if (path.EndsWith("pak", StringComparison.InvariantCultureIgnoreCase) && pakPath.EndsWith("pak", StringComparison.InvariantCultureIgnoreCase))
                {
                    CompileBMD();
                    evt.Save($"{Path.GetDirectoryName(path)}\\{Path.GetFileNameWithoutExtension(path)}.EVT");
                    SavePAK(path);
                }
                else
                    return;
            }
        }
    }
}
