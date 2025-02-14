using System.Net;
using System.Text.RegularExpressions;

namespace MTT01_winforms.UControls.Visualization
{
    public partial class UCCompareImages : UserControl
    {
        public UCCompareImages()
        {
            InitializeComponent();
        }

        private void btnsearchFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtUrlFolder.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnSearchIcon_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtUrlIcon.Text = openFileDialog1.FileName;
            }
        }

        #region Comparación de iconos

        private void button3_Click(object sender, EventArgs e)
        {
            // Ruta de la carpeta que contiene los iconos
            string folderPath = txtUrlFolder.Text;
            // URL del nuevo icono a comparar
            string iconUrl = txtUrlIcon.Text;

            // Descargar la imagen desde la URL
            Bitmap newIcon = DownloadImageFromUrl(iconUrl);

            // Recorrer todas las imágenes de la carpeta
            foreach (string filePath in Directory.GetFiles(folderPath, "*.png"))
            {
                using (Bitmap existingIcon = new Bitmap(filePath))
                {
                    // Comparar si las imágenes son iguales
                    if (AreImagesEqual(existingIcon, newIcon))
                    {
                        Console.WriteLine("El icono ya existe en la carpeta.");
                        return;  // Detener la ejecución si ya existe
                    }
                }
            }

            // Si no existe, puedes guardar la nueva imagen en la carpeta
            string newFilePath = Path.Combine(folderPath, "nuevo_icono.png");
            newIcon.Save(newFilePath);
            Console.WriteLine("El icono ha sido copiado a la carpeta.");
        }

        // Método para descargar una imagen desde una URL
        static Bitmap DownloadImageFromUrl(string url)
        {
            using (WebClient webClient = new WebClient())
            {
                using (Stream stream = webClient.OpenRead(url))
                {
                    return new Bitmap(stream);
                }
            }
        }

        // Método para comparar dos imágenes pixel por pixel
        static bool AreImagesEqual(Bitmap img1, Bitmap img2)
        {
            // Verificar si tienen las mismas dimensiones
            if (img1.Width != img2.Width || img1.Height != img2.Height)
                return false;

            // Comparar cada pixel
            for (int x = 0; x < img1.Width; x++)
            {
                for (int y = 0; y < img1.Height; y++)
                {
                    if (img1.GetPixel(x, y) != img2.GetPixel(x, y))
                        return false;
                }
            }

            return true;
        }

        #endregion

        #region Descarga de iconos

        private void btnDownloadImages_Click(object sender, EventArgs e)
        {
            // URL base donde se encuentran las imágenes
            string baseUrl = "https://s2.coinmarketcap.com/static/img/coins/64x64/{0}.png";

            // Ruta de la carpeta donde se guardarán las imágenes
            string folderPath = txtUrlFolder.Text;

            // Asegurarse de que la carpeta existe
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Descargar las imágenes en un bucle desde 1 hasta 10
            int i = 1;
            string errors = "";

            for (i = 1; i <= 30000; i++)
            {
                // Construir la URL con el número sustituido
                string imageUrl = string.Format(baseUrl, i);

                // Descargar la imagen desde la URL
                Bitmap image = DownloadImageFromWebUrl(imageUrl);

                if (image != null)
                {
                    // Crear el nombre del archivo de destino
                    string filePath = Path.Combine(folderPath, $"{i}.png");

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }

                    // Guardar la imagen en la carpeta
                    image.Save(filePath);
                    //Console.WriteLine($"Imagen {i} descargada y guardada en: {filePath}");
                }
                else
                {
                    //Console.WriteLine($"Error al descargar la imagen desde la URL: {imageUrl}");
                    errors += $"{i};";
                }
            }

            MessageBox.Show($"Descarga completada, última imagen en descargar: {i - 1}\n\rErrores:\n\r{errors}");
        }

        // Método para descargar una imagen desde una URL
        static Bitmap DownloadImageFromWebUrl(string url)
        {
            try
            {
                int width = 64;
                int height = 64;

                using (WebClient webClient = new WebClient())
                {
                    using (Stream stream = webClient.OpenRead(url))
                    {
                        // Cargar la imagen original
                        using (Bitmap originalImage = new Bitmap(stream))
                        {
                            // Verificar si la imagen ya es de 64x64
                            if (originalImage.Width == width && originalImage.Height == height)
                            {
                                // La imagen ya tiene el tamaño correcto, devolver tal cual
                                return new Bitmap(originalImage);
                            }

                            // Si no tiene el tamaño correcto, redimensionar la imagen
                            Bitmap resizedImage = new Bitmap(width, height);

                            using (Graphics g = Graphics.FromImage(resizedImage))
                            {
                                // Alta calidad para la redimensión
                                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                                g.DrawImage(originalImage, 0, 0, width, height);
                            }

                            return resizedImage;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al descargar o redimensionar la imagen: {ex.Message}");
                return null;
            }
        }

        #endregion

        #region Descarga imágenes y texto

        private async void btnDownloadImagesTexto_Click(object sender, EventArgs e)
        {
            // URL base con paginación donde se buscan las imágenes
            string baseUrl = "https://www.binance.com/es/price?page={0}";

            // Carpeta donde se guardarán las imágenes descargadas
            string folderPath = txtUrlFolder.Text;

            // Asegurarse de que la carpeta existe
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Definir el rango de páginas a descargar
            int totalPages = 100; // Por ejemplo, descargar hasta la página 10

            // Bucle para recorrer las páginas
            for (int i = 1; i <= totalPages; i++)
            {
                // Construir la URL de la página con la paginación
                string pageUrl = string.Format(baseUrl, i);
                //Console.WriteLine($"Descargando página: {pageUrl}");

                // Descargar y analizar la página
                string pageHtml = await DownloadPageHtmlAsync(pageUrl);

                if (pageHtml != null)
                {
                    // Extraer todas las imágenes y sus atributos
                    ExtractAndDownloadImages(pageHtml, folderPath);
                }
                else
                {
                    //Console.WriteLine($"Error al descargar la página {pageUrl}");
                }
            }
        }

        // Método para descargar el HTML de una página web
        static async Task<string> DownloadPageHtmlAsync(string url)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    return await httpClient.GetStringAsync(url);
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error al descargar la página: {ex.Message}");
                return null;
            }
        }

        // Método para extraer imágenes y descargarlas desde el HTML de la página
        static void ExtractAndDownloadImages(string html, string folderPath)
        {
            // Expresión regular para encontrar las etiquetas <img>
            string imgPattern = @"<img\s+[^>]*src\s*=\s*[""']([^""']+)[""'][^>]*alt\s*=\s*[""']([^""']+)[""'][^>]*>";

            // Buscar todas las coincidencias de imágenes en el HTML
            MatchCollection matches = Regex.Matches(html, imgPattern, RegexOptions.IgnoreCase);

            foreach (Match match in matches)
            {
                // La primera coincidencia es la URL de la imagen (src)
                string imgUrl = match.Groups[1].Value;

                // La segunda coincidencia es el nombre de la imagen (alt)
                string imgAlt = match.Groups[2].Value;

                // Solo descargar si la URL de la imagen es del dominio correcto
                if (imgUrl.StartsWith("https://s2.coinmarketcap.com/static/img/coins/64x64/"))
                {
                    // Construir el nombre del archivo con el valor de "alt" y la extensión ".png"
                    string fileName = imgAlt + ".png";
                    string filePath = Path.Combine(folderPath, SanitizeFileName(fileName));

                    // Descargar la imagen y guardarla
                    DownloadImage(imgUrl, filePath);
                }
            }
        }

        // Método para descargar una imagen desde una URL y guardarla en un archivo
        static void DownloadImage(string url, string filePath)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.DownloadFile(url, filePath);
                    //Console.WriteLine($"Imagen descargada: {filePath}");
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error al descargar la imagen: {ex.Message}");
            }
        }

        // Método para sanear el nombre del archivo (remover caracteres no permitidos)
        static string SanitizeFileName(string fileName)
        {
            // Elimina caracteres no válidos en nombres de archivo
            return Regex.Replace(fileName, @"[\\\/:*?""<>|]", string.Empty);
        }

        #endregion

    }
}
