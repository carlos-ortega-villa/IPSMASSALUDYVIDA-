using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
   public class RegimenContributivo : LiquidacionCuotaModeradora
    {
        public RegimenContributivo(int nLiquidacion, int nIdentificacion, decimal salario, decimal vServicioPrestado) :
            base(nLiquidacion, nIdentificacion,"Contributivo", salario, vServicioPrestado)
        {
        }
        public override void  FijarTarifaYTope()
        {
            if (Salario > (SalarioM * 5))
            {
                TopeMaximo = 1500000;
                TarifaServicio = (decimal)0.25;
            }
           else if (Salario < (SalarioM * 2))
            {
                TopeMaximo = 250000;
                TarifaServicio = (decimal)0.15;
            }
            else
            {
                TopeMaximo = 900000;
                TarifaServicio = (decimal)0.20;
            }
        }
    }
}
