using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;

namespace MTT01_winforms.UControls.StrategyQuant
{
    public partial class UCSqxBblockAnalizer : UserControl
    {
        List<string> ListLongOriginLines = new List<string>();
        List<string> ListShortOriginLines = new List<string>();

        List<string> ListLongLines = new List<string>();
        List<string> ListShortLines = new List<string>();

        public UCSqxBblockAnalizer()
        {
            InitializeComponent();
        }

        private void btnGetFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txtFolder.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void btnAnalizar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFolder.Text) && Directory.Exists(txtFolder.Text))
            {
                string[] files = Directory.GetFiles(txtFolder.Text);
                if (files != null && files.Length > 0)
                {
                    ListLongLines = new List<string>();
                    ListShortLines = new List<string>();

                    foreach (string file in files)
                    {
                        string[] lines = File.ReadAllLines(file);
                        string? longLine = lines.FirstOrDefault(x => x.StartsWith("LongEntrySignal = "));
                        string? shortLine = lines.FirstOrDefault(x => x.StartsWith("ShortEntrySignal = "));

                        if (!string.IsNullOrEmpty(longLine))
                        {
                            if (!longLine.EndsWith(";"))
                                GetExtraLinesIfExist(ref longLine, lines);

                            CleanCurrentLine(ref longLine, "LongEntrySignal");

                            ListLongLines.Add(longLine);
                        }

                        if (!string.IsNullOrEmpty(shortLine))
                        {
                            if (!shortLine.EndsWith(";"))
                                GetExtraLinesIfExist(ref shortLine, lines);

                            CleanCurrentLine(ref shortLine, "ShortEntrySignal");

                            ListShortLines.Add(shortLine);
                        }
                    }

                    ListLongOriginLines = ListLongLines.ToList();
                    ListShortOriginLines = ListShortLines.ToList();

                    ListLongLines = ListLongLines.Distinct().ToList();
                    ListShortLines = ListShortLines.Distinct().ToList();

                    listBoxLongEntry.Items.Clear();
                    listBoxLongEntry.Items.AddRange(ListLongLines.ToArray());
                    listBoxLongEntry.Sorted = true;

                    listBoxShortEntry.Items.Clear();
                    listBoxShortEntry.Items.AddRange(ListShortLines.ToArray());
                    listBoxShortEntry.Sorted = true;
                }
            }
        }

        private void btnSaveLongs_Click(object sender, EventArgs e)
        {
            saveFileDialog.DefaultExt = "*.txt";
            saveFileDialog.Title = "Guardar archivo";
            saveFileDialog.FileName = "_LONGs.txt";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllLines(saveFileDialog.FileName, ListLongLines.ToArray(), Encoding.UTF8);
            }
        }

        private void btnSaveShorts_Click(object sender, EventArgs e)
        {
            saveFileDialog.DefaultExt = "*.txt";
            saveFileDialog.Title = "Guardar archivo";
            saveFileDialog.FileName = "_SHORTs.txt";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllLines(saveFileDialog.FileName, ListShortLines.ToArray(), Encoding.UTF8);
            }
        }

        private void btnDeleteLongs_Click(object sender, EventArgs e)
        {
            if (listBoxLongEntry.SelectedItems.Count > 0)
            {
                List<string> borrados = listBoxLongEntry.SelectedItems.Cast<string>().ToList();

                ListLongLines.RemoveAll(x => borrados.Contains(x));

                listBoxLongEntry.Items.Clear();
                listBoxLongEntry.Items.AddRange(ListLongLines.ToArray());
            }
        }

        private void btnDeleteShorts_Click(object sender, EventArgs e)
        {
            if (listBoxShortEntry.SelectedItems.Count > 0)
            {
                List<string> borrados = listBoxShortEntry.SelectedItems.Cast<string>().ToList();

                ListShortLines.RemoveAll(x => borrados.Contains(x));

                listBoxShortEntry.Items.Clear();
                listBoxShortEntry.Items.AddRange(ListShortLines.ToArray());
            }
        }

        private void GetExtraLinesIfExist(ref string longLine, string[] lines)
        {
            bool exist = false;

            foreach (string line in lines)
            {
                if (exist)
                {
                    string l = line.Trim();
                    if (l.StartsWith("and ("))
                        l = l.Replace("and (", "& ");
                    if (l.StartsWith("("))
                        l = l.Substring(1, l.Length - 1);
                    longLine += " " + l;
                    if (line.EndsWith(";"))
                        break;
                }

                if (line == longLine)
                    exist = true;
            }
        }

        private void CleanCurrentLine(ref string linea, string lineType)
        {
            linea = linea.Replace(lineType + " = (", lineType + " = ")
                     .Replace(lineType + " = (", lineType + " = ")
                     .Replace(lineType + " = ", "");

            linea = linea.Replace("[0]", "").Replace("[1]", "").Replace("[2]", "")
                       .Replace("[3]", "").Replace("[4]", "").Replace("[5]", "")
                       .Replace("[6]", "").Replace("[7]", "").Replace("[8]", "")
                       .Replace("[9]", "");

            linea = Regex.Replace(linea, @"\([^)]*\)", "");

            linea = linea.Replace("(", "").Replace(")", "").Replace(";", "")
                         .Replace("\t", "").Replace("\r", "").Replace("\n", "");
        }

        private void btnLongContiene_Click(object sender, EventArgs e)
        {
            listBoxLongEntry.Items.Clear();

            if (!string.IsNullOrEmpty(txtLongContiene.Text.Trim()))
            {
                string[] claves = txtLongContiene.Text.Split("|");

                List<string> list = new List<string>();

                foreach (string clave in claves)
                {
                    List<string> listConClave = ListLongLines
                                .Where(elemento => elemento.ToLower().Contains(clave.Trim().ToLower()))
                                .ToList();

                    if (listConClave.Count > 0)
                        list.AddRange(listConClave);
                }

                listBoxLongEntry.Items.AddRange(list.ToArray());
            }
            else
            {
                listBoxLongEntry.Items.AddRange(ListLongLines.ToArray());
            }
        }

        private void btnShortContiene_Click(object sender, EventArgs e)
        {
            listBoxShortEntry.Items.Clear();

            if (!string.IsNullOrEmpty(txtShortContiene.Text.Trim()))
            {
                string[] claves = txtShortContiene.Text.Split("|");

                List<string> list = new List<string>();

                foreach (string clave in claves)
                {
                    List<string> listConClave = ListShortLines
                                .Where(elemento => elemento.ToLower().Contains(clave.Trim().ToLower()))
                                .ToList();

                    if (listConClave.Count > 0)
                        list.AddRange(listConClave);
                }

                listBoxShortEntry.Items.AddRange(list.ToArray());
            }
            else
            {
                listBoxShortEntry.Items.AddRange(ListShortLines.ToArray());
            }
        }

        private void btnSaveAll_Click(object sender, EventArgs e)
        {
            saveFileDialog.DefaultExt = "*.txt";
            saveFileDialog.Title = "Guardar archivo";
            saveFileDialog.FileName = "Bblocks_.txt";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    List<string> listBblocks_nums = new List<string>();
                    listBblocks_nums.Add($"Bloques totales: [{ListLongOriginLines.Count} Longs] y [{ListShortOriginLines.Count} Shorts]");
                    listBblocks_nums.Add("------------------------------------------------------------------------");

                    if (!string.IsNullOrEmpty(txtContarBblocks.Text.Trim()))
                    {
                        string[] claves = txtContarBblocks.Text.Split("|");

                        List<string> list = new List<string>();

                        foreach (string clave in claves)
                        {
                            List<string> listConClaveLong = ListLongOriginLines
                                        .Where(elemento => elemento.ToLower().Contains(clave.Trim().ToLower()))
                                        .ToList();

                            List<string> listConClaveShort = ListShortOriginLines
                                        .Where(elemento => elemento.ToLower().Contains(clave.Trim().ToLower()))
                                        .ToList();

                            listBblocks_nums.Add($"Bloque: {clave.ToUpper()} contiene [{listConClaveLong.Count} Longs] y [{listConClaveShort.Count} Shorts]");
                        }
                    }
                    listBblocks_nums.Add("------------------------------------------------------------------------");

                    List<string> combinedList = listBblocks_nums;
                    List<string> listLong = listBoxLongEntry.Items.Cast<string>().ToList();
                    List<string> listShort = listBoxShortEntry.Items.Cast<string>().ToList();
                    combinedList.AddRange(listLong.Concat(listShort).Distinct().OrderBy(s => s).ToList());

                    File.WriteAllLines(saveFileDialog.FileName, combinedList.ToArray(), Encoding.UTF8);

                    MessageBox.Show("Fichero creado");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnSaveUniqueFile_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string allFile = string.Empty;

                foreach (string file in Directory.GetFiles(folderBrowserDialog.SelectedPath))
                {
                    FileInfo fileInfo = new FileInfo(file);
                    if (fileInfo.Extension != ".txt") continue;

                    allFile += File.ReadAllText(file);
                    allFile += "\r-----------------------------------------------------------------------------\r";
                }

                string path = folderBrowserDialog.SelectedPath + "\\ALL_FILES.txt";
                File.WriteAllText(path, allFile);

                MessageBox.Show($"Fichero creado en '{path}'");
            }
        }
    }
}