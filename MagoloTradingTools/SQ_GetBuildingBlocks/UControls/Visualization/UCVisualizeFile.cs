
namespace MTT01_winforms.UControls.Visualization
{
    public partial class UCVisualizeFile : UserControl
    {
        private string FilePath = "";

        public UCVisualizeFile(string filePath)
        {
            InitializeComponent();

            FilePath = filePath;
        }

        private void VisualizeFileControl_Load(object sender, EventArgs e)
        {
            if (File.Exists(FilePath))
            {
                richTextBoxFile.Text = File.ReadAllText(FilePath);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            File.WriteAllText(FilePath, richTextBoxFile.Text);

            MessageBox.Show("Texto Salvado");
        }
    }
}
