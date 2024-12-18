using System;

namespace ScrappingDataroma.Classes
{
    public class Portafolio
    {
        public string Nombre { get; set; }
        public string Capital { get; set; }
        public double CapitalValor { get; set; } // Para ordenar correctamente
        public int Stocks { get; set; }
        public string Link { get; set; }
        public List<Activo> Activos { get; set; }

        public Portafolio()
        {
            Activos = new List<Activo>();
        }

        public override string ToString()
        {
            return $"Nombre: {Nombre}, Capital: {Capital}, Stocks: {Stocks}, Link: {Link}, Activos: {string.Join(", ", Activos)}";
        }
    }
}
