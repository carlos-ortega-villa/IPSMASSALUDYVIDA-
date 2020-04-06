using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
   public abstract class LiquidacionCuotaModeradora
    {
        public int NLiquidacion { get; set; }
        public int NIdentificacion { get; set; }
        public string TAfiliacion { get; set; }
        public decimal Salario { get; set; }
        public decimal VServicioPrestado { get; set; }
        public decimal VCuotaModeradora{ get; set; }
        public decimal TopeMaximo { get; set; }
        public decimal TarifaServicio { get; set; }
        public decimal VCuotaTotal { get; set; }
        public String Tope { get; set; }


        public decimal SalarioM = (decimal)980.657;

        public LiquidacionCuotaModeradora(int nLiquidacion, int nIdentificacion, string tAfiliacion, decimal salario, decimal vServicioPrestado) 
        {
           NLiquidacion  = nLiquidacion;
            NIdentificacion = nIdentificacion;
            TAfiliacion = tAfiliacion;
            Salario = salario;
            VServicioPrestado = vServicioPrestado;
        }

        public void CalcularCuotaModeradora()
        {
            VCuotaModeradora = VServicioPrestado * TarifaServicio;
           VCuotaTotal = VCuotaModeradora ;
            Tope = "false";
            if(VCuotaModeradora>TopeMaximo)
            {
                VCuotaTotal = TopeMaximo;
                Tope = "true";
            }
           
           
        }
        public abstract void FijarTarifaYTope();

        public override string ToString()
        {
            return $"{NLiquidacion};{NIdentificacion};{TAfiliacion};{Salario};{VServicioPrestado};{TopeMaximo};" +
                $"{TarifaServicio};{VCuotaModeradora};{Tope};{VCuotaTotal}";
        }

    }
}
