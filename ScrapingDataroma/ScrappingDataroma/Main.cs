using System.Net.Http;
using System.Text.RegularExpressions;
using ScrappingDataroma.Classes;

namespace ScrappingDataroma
{
    public partial class Main : Form
    {
        List<Portafolio> PortafoliosMAN;
        string FileName = "PortFoleo.txt"; //$"Dataroma_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
        string FileNameDataroma = "Dataroma.txt";
        int NumMaximoActivos = 20;

        public Main()
        {
            InitializeComponent();
        }


        private void Main_Load(object sender, EventArgs e)
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // Nombre del archivo          
            string filePath = Path.Combine(documentsPath, FileName);

            try
            {
                if (File.Exists(filePath))
                {
                    listBoxFINAL.Items.Clear();

                    foreach (string line in File.ReadAllLines(filePath))
                    {
                        listBoxFINAL.Items.Add(line);
                    }
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show($"Error al leer el archivo: {ex.Message}",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }

            filePath = Path.Combine(documentsPath, FileNameDataroma);
            if (File.Exists(filePath))
            {
                DateTime dt = File.GetLastWriteTimeUtc(filePath);
                lblLastDownload.Text = dt.ToShortDateString();
            }
        }

        private async void btnMAGIC_Click(object sender, EventArgs e)
        {
            listBoxMan.Enabled = true;
            listBoxActivos.Enabled = true;
            listBoxResult.Enabled = true;

            listBoxMan.Items.Clear();
            listBoxActivos.Items.Clear();

            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string filePath = Path.Combine(documentsPath, FileNameDataroma);

            if (File.Exists(filePath))
            {
                string htmlContent = File.ReadAllText(filePath);

                if (!string.IsNullOrEmpty(htmlContent))
                {
                    // Extraer los datos y formar la lista de Portafolio
                    PortafoliosMAN = ExtractPortfoliosFromHtml(htmlContent);

                    foreach (var item in PortafoliosMAN)
                        listBoxMan.Items.Add($"{item.Nombre}");
                    listBoxMan.SelectedValueChanged += listBoxMan_SelectedValueChanged;

                    listBoxMan.SelectedIndex = 0;

                    SetResult();

                    lblTotal.Text = listBoxMan.Items.Count.ToString();
                }
            }
        }

        private async void btnDownload_Click(object sender, EventArgs e)
        {
            string url = "https://www.dataroma.com/m/managers.php"; // "https://www.dataroma.com/m/home.php";
            string htmlContent = await FetchHtmlContentAsync(url);

            if (!string.IsNullOrEmpty(htmlContent))
            {
                // Guardar el HTML en un archivo local
                lblSaved.Text = SaveHtmlToFile(htmlContent, FileNameDataroma);

                btnMAGIC_Click(null, null);
            }
        }

        private void SetResult()
        {
            listBoxResult.Items.Clear();

            List<ActivoResult> result = CalcularActivosRepetidos();
            int count = 1;
            foreach (var item in result)
            {
                string pos = (count > 9 && count < 100) ? $"0{count}" : ((count < 10) ? $"00{count}" : count.ToString());

                listBoxResult.Items.Add($"{pos}:{item.Siglas} >> {item.Nombre} - {item.Count}");
                count++;
            }

            UpdateEntranSalen();
        }

        private async Task<string> FetchHtmlContentAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                // Opcional: Configurar el User-Agent para evitar bloqueos del servidor
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (compatible; MyApp/1.0)");

                try
                {
                    // Realizar la solicitud HTTP GET
                    string html = await client.GetStringAsync(url);
                    return html;
                }
                catch (HttpRequestException ex)
                {
                    MessageBox.Show($"Error al obtener el HTML: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return string.Empty;
                }
            }
        }

        private List<Portafolio> ExtractPortfoliosFromHtml(string html)
        {
            List<Portafolio> portafolios = new List<Portafolio>();
            string rowPattern = @"<tr>\s*<td class=""man""><a href=""(.*?)"".*?>(.*?)</a></td>\s*" +
                                @"<td class=""val"">(.*?)</td>\s*" +
                                @"<td class=""cnt"">(.*?)</td>\s*" +
                                @"(.*?)</tr>";

            foreach (Match rowMatch in Regex.Matches(html, rowPattern, RegexOptions.Singleline))
            {
                string link = "https://www.dataroma.com" + rowMatch.Groups[1].Value;
                string nombre = rowMatch.Groups[2].Value;
                string capitalStr = rowMatch.Groups[3].Value;
                double capitalValor = ConvertCapitalToDouble(capitalStr);
                int stocks = int.TryParse(rowMatch.Groups[4].Value, out int s) ? s : 0;
                string activosBlock = rowMatch.Groups[5].Value;

                Portafolio portafolio = new Portafolio
                {
                    Nombre = nombre,
                    Capital = capitalStr,
                    CapitalValor = capitalValor,
                    Stocks = stocks,
                    Link = link,
                    Activos = ExtractActivosFromBlock(activosBlock)
                };

                portafolios.Add(portafolio);
            }

            return portafolios.OrderByDescending(p => p.CapitalValor).ToList();
        }

        private List<Activo> ExtractActivosFromBlock(string activosBlock)
        {
            List<Activo> activos = new List<Activo>();
            string activoPattern = @"<a href=""(.*?)"">(.*?)</a>\s*<div>(.*?)<br/>(.*?)% of portfolio";

            foreach (Match match in Regex.Matches(activosBlock, activoPattern))
            {
                string link = "https://www.dataroma.com" + match.Groups[1].Value;
                string siglas = match.Groups[2].Value;
                string nombre = match.Groups[3].Value;
                string peso = match.Groups[4].Value;

                activos.Add(new Activo
                {
                    Siglas = siglas,
                    Nombre = nombre,
                    Link = link,
                    Peso = double.TryParse(peso.Replace("%", ""), out double p) ? p / 100 : 0
                });
            }

            // Ordenar la lista de activos por Peso de mayor a menor
            return activos.OrderByDescending(a => a.Peso).ToList();
        }

        private double ConvertCapitalToDouble(string capital)
        {
            if (capital.EndsWith("B"))
                return double.Parse(capital.Trim('$', 'B')) * 1_000_000_000;
            if (capital.EndsWith("M"))
                return double.Parse(capital.Trim('$', 'M')) * 1_000_000;
            return 0;
        }

        public List<ActivoResult> CalcularActivosRepetidos()
        {
            // Diccionario para contar los activos
            Dictionary<string, ActivoResult> contadorActivos = new Dictionary<string, ActivoResult>();

            // Tomar los primeros 'numeroPortafolios' portafolios en orden
            int numMANs = (txtBoxNumMANs.Text != "0") ? int.Parse(txtBoxNumMANs.Text) : listBoxMan.Items.Count;

            if (PortafoliosMAN == null)
                return new List<ActivoResult>();

            var portafoliosSeleccionados = PortafoliosMAN.Take(numMANs);

            foreach (var portafolio in portafoliosSeleccionados)
            {
                foreach (var activo in portafolio.Activos)
                {
                    // Filtrar activos con Peso mayor a 10
                    if (activo.Peso >= int.Parse(txtBoxPesoActivo.Text))
                    {
                        // Usar el Link como clave para evitar duplicados
                        if (contadorActivos.ContainsKey(activo.Link))
                        {
                            contadorActivos[activo.Link].Count++;
                        }
                        else
                        {
                            contadorActivos[activo.Link] = new ActivoResult
                            {
                                Siglas = activo.Siglas,
                                Nombre = activo.Nombre,
                                Link = activo.Link,
                                Count = 1
                            };
                        }
                    }
                }
            }

            // Ordenar por Count en orden descendente y devolver como lista
            return contadorActivos.Values.OrderByDescending(a => a.Count).ToList();
        }

        private void listBoxMan_SelectedValueChanged(object sender, EventArgs e)
        {
            if (PortafoliosMAN != null)
            {
                Portafolio portafoleo = PortafoliosMAN.Where(x => x.Nombre == listBoxMan.SelectedItem).FirstOrDefault();

                listBoxActivos.Items.Clear();

                if (portafoleo != null)
                {
                    foreach (var activo in portafoleo.Activos)
                        listBoxActivos.Items.Add($"{activo.Siglas} - {activo.Peso}%");
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (listBoxResult.Items.Count == 0)
                btnMAGIC_Click(null, null);

            SetResult();
        }

        private void UpdateEntranSalen()
        {
            listBoxEntran.Items.Clear();
            listBoxSalen.Items.Clear();

            int activos = (listBoxResult.Items.Count < NumMaximoActivos) ? listBoxResult.Items.Count : NumMaximoActivos;

            //  ACTIVOS QUE ENTRAN
            for (int x = 0; x < activos; x++)
            {
                var act = listBoxResult.Items[x].ToString().Split(":")[1].Trim().Split(" ")[0].Trim();

                bool exist = false;
                foreach (var actFinal in listBoxFINAL.Items)
                {
                    if (actFinal.ToString().Contains($"{act} "))
                    {
                        exist = true;
                        break;
                    }
                }
                if (!exist)
                    listBoxEntran.Items.Add(act);
            }

            //  ACTIVOS QUE SALEN
            for (int x1 = 0; x1 < listBoxFINAL.Items.Count; x1++)
            {
                bool exist = false;
                var act1 = listBoxFINAL.Items[x1].ToString().Trim().Split(" ")[0].Trim();

                for (int x2 = 0; x2 < activos; x2++)
                {
                    var act = listBoxResult.Items[x2].ToString().Split(":")[1].Trim().Split(" ")[0].Trim();
                    if (act.StartsWith(act1))
                    {
                        exist = true;
                        break;
                    }
                }

                if (!exist)
                    listBoxSalen.Items.Add(act1);
            }
        }

        private void ListBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            // Obtener el ListBox y el elemento actual
            ListBox listBox = sender as ListBox;
            string texto = listBox.Items[e.Index].ToString();

            // Definir colores: verde para los 10 primeros, gris para el resto
            Color colorTexto = e.Index < NumMaximoActivos ? Color.Blue : Color.Gray;

            if (e.Index < 12)
                colorTexto = Color.Green;

            // Dibujar fondo
            e.DrawBackground();

            // Dibujar texto con el color correspondiente
            using (Brush brush = new SolidBrush(colorTexto))
            {
                e.Graphics.DrawString(texto, e.Font, brush, e.Bounds);
            }

            // Dibujar borde de foco si es necesario
            e.DrawFocusRectangle();
        }

        private void listBoxResult_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            listBoxFINAL.Items.Add(((ListBox)sender).Text.Split(":")[1]);
        }

        private void listBoxFINAL_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            listBoxFINAL.Items.Remove(((ListBox)sender).Text);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string result = "";

            foreach (var item in listBoxFINAL.Items)
                result += item.ToString() + "\n";

            lblSaved.Text = SaveHtmlToFile(result, FileName);
        }

        // Método para guardar el HTML en un archivo local
        private string SaveHtmlToFile(string htmlContent, string fileName)
        {
            // Obtener el directorio Documentos del usuario
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // Nombre del archivo HTML con fecha y hora para evitar conflictos
            string filePath = Path.Combine(documentsPath, fileName);

            try
            {
                // Guardar el contenido en el archivo
                File.Delete(filePath);
                File.WriteAllText(filePath, htmlContent);

                return filePath;
            }
            catch (IOException ex)
            {
                MessageBox.Show($"Error al guardar el archivo: {ex.Message}",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return string.Empty;
            }
        }


    }
}
