
namespace ScrappingDataroma.Classes
{
    public class Activo
    {
        public string Nombre { get; set; }
        public string Siglas { get; set; }
        public string Link { get; set; }
        public double Peso { get; set; }

        public override string ToString()
        {
            return $"Siglas: {Siglas}, Nombre: {Nombre}, Peso: {Peso}, Link: {Link}";
        }
    }
}
