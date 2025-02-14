
namespace MTT01_winforms.UControls.Visualization
{
    public partial class UCVisualizeAllFiles : UserControl
    {
        private string DirectoryPath = string.Empty;

        public UCVisualizeAllFiles(string directoryPath)
        {
            InitializeComponent();

            DirectoryPath = directoryPath;
        }

        private void VisualizeAllFiles_Load(object sender, EventArgs e)
        {
            if (Directory.Exists(DirectoryPath))
            {
                tabControl1.Controls.Clear();

                foreach (string file in Directory.GetFiles(DirectoryPath))
                {
                    FileInfo fileInfo = new FileInfo(file);
                    if (fileInfo.Extension != ".txt") continue;

                    TabPage tabPage = new TabPage();
                    tabPage.Location = new Point(4, 24);
                    tabPage.Padding = new Padding(3);
                    tabPage.Size = new Size(1050, 600);
                    tabPage.UseVisualStyleBackColor = true;
                    tabPage.Text = fileInfo.Name.Replace(".txt", "");
                    tabPage.Font = new Font(new FontFamily("Consolas"), 9);

                    TextBox txtBox = new TextBox();
                    txtBox.Dock = DockStyle.Fill;
                    txtBox.Multiline = true;
                    txtBox.Text = File.ReadAllText(file);

                    tabPage.Controls.Add(txtBox);

                    tabControl1.Controls.Add(tabPage);
                }
            }
        }
    }
}
