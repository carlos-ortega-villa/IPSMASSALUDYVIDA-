using ENTITY;
using System.Collections.Generic;
using System.IO;

namespace DAL
{
    public class LCuotaModeradoraRepository
    {
        private string Ruta = @"LiquidaciondeCuotas.txt";
        public List<LiquidacionCuotaModeradora> LiquidacionDeCuotas;
        public LCuotaModeradoraRepository()
        {
            LiquidacionDeCuotas = new List<LiquidacionCuotaModeradora>();
        }
        public void Guardar(LiquidacionCuotaModeradora lCuotaModeradora)
        {
            FileStream fileStream = new FileStream(Ruta, FileMode.Append);
            StreamWriter streamWriter = new StreamWriter(fileStream);
            streamWriter.WriteLine(lCuotaModeradora.ToString());
            streamWriter.Close();
            fileStream.Close();
        }

        public List<LiquidacionCuotaModeradora> Consultar()
        {
            LiquidacionDeCuotas.Clear();
            FileStream filestream = new FileStream(Ruta, FileMode.OpenOrCreate);
            StreamReader streamreader = new StreamReader(filestream);
            string linea = string.Empty;
            while ((linea = streamreader.ReadLine()) != null)
            {
                LiquidacionCuotaModeradora lCuotaModeradora = MapearLCM(linea);
                LiquidacionDeCuotas.Add(lCuotaModeradora);
            }
            filestream.Close();
            streamreader.Close();
            return LiquidacionDeCuotas;
        }

        public LiquidacionCuotaModeradora MapearLCM(string linea)
        {
            LiquidacionCuotaModeradora lCuotaModeradora;
            string[] Datos = linea.Split(';');
            int NLiquidacion = int.Parse(Datos[0]);
            int NIdentificacion = int.Parse(Datos[1]);
            decimal Salario = decimal.Parse(Datos[3]);
            decimal VServicioPrestado = decimal.Parse(Datos[4]);
            if (Datos[2] == "Subsidiado")
            {
                lCuotaModeradora = new RegimenSubsidiado(NLiquidacion, NIdentificacion, VServicioPrestado);
            }
            else
            {
                lCuotaModeradora = new RegimenContributivo(NLiquidacion, NIdentificacion, Salario, VServicioPrestado);
            }
            lCuotaModeradora.TopeMaximo = decimal.Parse(Datos[5]);
            lCuotaModeradora.TarifaServicio = decimal.Parse(Datos[6]);
            lCuotaModeradora.VCuotaModeradora = decimal.Parse(Datos[7]);
            lCuotaModeradora.Tope = (Datos[8]);
            lCuotaModeradora.VCuotaTotal = decimal.Parse(Datos[9]);
            return lCuotaModeradora;
        }

        public LiquidacionCuotaModeradora Buscar(int IDLiquidacion)
        {
            LiquidacionDeCuotas.Clear();
            LiquidacionDeCuotas = Consultar();
            foreach (var item in LiquidacionDeCuotas)
            {
                if (item.NLiquidacion.Equals(IDLiquidacion))
                { return item; }
            }
            return null;
        }
        public void Eliminar(int IDLiquidacion)
        {
            LiquidacionDeCuotas.Clear();
            LiquidacionDeCuotas = Consultar();
            FileStream file = new FileStream(Ruta, FileMode.Create);
            file.Close();
            foreach (var item in LiquidacionDeCuotas)
            {
                if (item.NLiquidacion != IDLiquidacion)
                {
                    Guardar(item);
                }
            }

        }
        public void Modificar(LiquidacionCuotaModeradora lCuotaModeradora)
        {
            LiquidacionDeCuotas.Clear();
            LiquidacionDeCuotas = Consultar();
            FileStream file = new FileStream(Ruta, FileMode.Create);
            file.Close();
            foreach (var item in LiquidacionDeCuotas)
            {
                if (item.NLiquidacion != lCuotaModeradora.NLiquidacion)
                {
                    Guardar(item);
                }
                else
                {
                    Guardar(lCuotaModeradora);
                }
            }

        }
    }
}
