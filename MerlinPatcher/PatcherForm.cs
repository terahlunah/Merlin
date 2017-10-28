using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MerlinPatcher
{
    public partial class PatcherForm : Form
    {
        private string targetPath;
        private string managedPath;

        public PatcherForm()
        {
            InitializeComponent();
        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    SetRootFolder(fbd.SelectedPath);
                }
            }
        }

        private void SetRootFolder(string rootPath)
        {
            foreach (var dir in Directory.GetDirectories(rootPath))
            {
                if (dir.EndsWith("_Data"))
                {
                    foreach (var dir2 in Directory.GetDirectories(dir))
                    {
                        if (dir2.EndsWith("Managed"))
                        {
                            managedPath = dir2;
                            targetPath = Path.Combine(dir2, "Assembly-CSharp.dll");
                            if (File.Exists(targetPath))
                                patchButton.Enabled = true;
                        }
                    }
                }
            }
        }

        private void patchButton_Click(object sender, EventArgs e)
        {
            MerlinPatcher.Patch(targetPath, "Merlin.dll");
        }
    }
}
