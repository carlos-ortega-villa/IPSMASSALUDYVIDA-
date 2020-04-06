using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
     public class RegimenSubsidiado:LiquidacionCuotaModeradora
    {
        public RegimenSubsidiado(int NLiquidacion,int NIdentificacion,decimal VServicioPrestado) : 
            base(NLiquidacion, NIdentificacion, "Subsidiado", 0, VServicioPrestado)
        { }

        public override void FijarTarifaYTope()
        {
            TopeMaximo = 200000;
            TarifaServicio = (decimal)0.05;
        }
    }
}
