
namespace MTT01_winforms.UControls.EAs
{
    public partial class UCGeneraEA_mt4_RRR : UserControl
    {
        private string FilePath { get; set; }
        private string FileName { get; set; }

        public UCGeneraEA_mt4_RRR()
        {
            InitializeComponent();
        }

        [STAThread]
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            string new_ea = string.Empty;
            string num_rules = "," + txtRules.Text + ",";
            string last_rule = string.Empty;
            string inputRules = "input bool:";
            int counter = 1;

            foreach (string line in richTextBoxFile.Lines)
            {
                string newline = line;

                if (newline.StartsWith("input int TotalRules"))
                    newline = "{RRR}";

                if (!newline.StartsWith("(TotalRules == "))
                    new_ea += newline + "\n";
                else
                {
                    string number = newline.Split("&&")[0].Trim().Split(" ")[2].Trim();

                    if (num_rules.Contains("," + number + ","))
                    {
                        if (last_rule.Length > 0)
                            new_ea += " || \n";

                        last_rule = newline
                                .Replace($"TotalRules == {number}", $"R{counter}")
                                .Replace(")) || ", "))") + "\n";

                        inputRules = inputRules.Replace(":", $" R{counter},:");
                        counter++;

                        new_ea += last_rule;
                    }
                }
            }

            inputRules = inputRules.Replace(",:", " = false;");
            new_ea = new_ea.Replace("{RRR}", inputRules);

            richTextBoxFile.Text = new_ea;

            btnSalvarEA.Visible = true;

        }

        private void btnCargarReglas_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(openFileDialog1.FileName))
                {
                    txtRules.Text = string.Empty;
                    richTextBoxFile.Text = File.ReadAllText(openFileDialog1.FileName);

                    FilePath = openFileDialog1.FileName;
                    FileName = openFileDialog1.SafeFileName;
                }
            }
        }

        private void btnSalvarEA_Click(object sender, EventArgs e)
        {
            File.WriteAllText(FilePath.Replace(".mq4", "_RRR.mq4"), richTextBoxFile.Text);

            MessageBox.Show($"EA creado en '{FilePath}'!!");

            btnSalvarEA.Visible = false;
        }
    }
}
