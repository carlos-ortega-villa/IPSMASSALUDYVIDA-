using DAL;
using ENTITY;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class LiquidacionCuotaModeradoraService
    {
        private LCuotaModeradoraRepository lCuotaModeradoraRepository;
        public LiquidacionCuotaModeradoraService()
        {
            lCuotaModeradoraRepository = new LCuotaModeradoraRepository();
        }
        public string Guardar(LiquidacionCuotaModeradora liquidacionCuotaModeradora)
        {
            try
            {
                if (lCuotaModeradoraRepository.Buscar(liquidacionCuotaModeradora.NLiquidacion) == null)
                {
                    lCuotaModeradoraRepository.Guardar(liquidacionCuotaModeradora);
                    return $"Los datos obtenidos del numero de liquidacion {liquidacionCuotaModeradora.NLiquidacion} " +
                        $"han sido guardados satisfactoriamente";
                }
                return $"Ha ocurrido un erron el sistema al registrar el numero de liquidacion " +
                    $"{liquidacionCuotaModeradora.NLiquidacion}, ya que este archivo ya esta en el sistema";
            }
            catch (Exception e)
            {
                return "Error al leer o escribir el archivo" + e.Message;
            }
        }
        public LiquidacionCuotaModeradora Buscar(int IDLiquidacion)
        {
            try
            {
                LiquidacionCuotaModeradora liquidacionCuotaModeradora = lCuotaModeradoraRepository.Buscar(IDLiquidacion);
                if (liquidacionCuotaModeradora == null)
                {
                    Console.WriteLine($"El numero de liquidacion {IDLiquidacion} no se encuentra en el archivo registrado");
                }
                return liquidacionCuotaModeradora;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al leer o escribir el archivo" + e.Message);
            }
            return null;
        }
        public void Consultar()
        {
            try
            {
                List<LiquidacionCuotaModeradora> LiquidacionDeCuotas = lCuotaModeradoraRepository.Consultar();
                if (LiquidacionDeCuotas.Count != 0)
                {
                    Mostrar(LiquidacionDeCuotas);
                }
                else
                {
                    Console.WriteLine("Este archivo esta vacio, NO hay registro de liquidaciones");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al leer y o escribir el archivo" + e.Message);
            }
        }

        public void Mostrar(List<LiquidacionCuotaModeradora> LiquidacionDeCuotas)
        {
            Console.WriteLine("|°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°|");
            Console.WriteLine("{1,21}{2,20}{3,22}{4,23}{5,21}{6,12}{7,16}{8,19}{9,18}", "N. Liquidacion", "N. IDentificacion", "Afiliacion-Tipo", "Salario", "Tarifa",
                "Tope Maximo", "Valor del servicio", "Cuota", "Valor Total");
            Console.WriteLine("|°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°|");
            Console.WriteLine("|°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°|");
            foreach (var item in LiquidacionDeCuotas)
            {
                Console.WriteLine("{1,21}{2,20}{3,22}{4,23}{5,21}{6,12}{7,16}{8,19}{9,18}", item.NLiquidacion, item.NIdentificacion, item.TAfiliacion, item.Salario, item.TarifaServicio,
                item.TopeMaximo, item.VServicioPrestado, item.VCuotaModeradora, item.VCuotaTotal);
            }
            Console.WriteLine("\n\n");
        }
        public string Eliminar(int IDLiquidacion)
        {
            try
            {
                LiquidacionCuotaModeradora liquidacionCuotaModeradora = lCuotaModeradoraRepository.Buscar(IDLiquidacion);
                if (liquidacionCuotaModeradora != null)
                {
                    lCuotaModeradoraRepository.Eliminar(IDLiquidacion);
                    return $"Los datos han sido eliminados correctamente del archivo," +
                        $" el numero de liquidacion {IDLiquidacion} ya no se encuentra en el archivo";
                }
                return $"El numero de liquidacion {IDLiquidacion} no se encuentra en el archivo, por lo tanto no se puede eliminar";
            }
            catch (Exception e)
            {
                return "Error al leer y/o escribir el archivo" + e.Message;
            }
        }
        public string Modificar(LiquidacionCuotaModeradora liquidacionCuotaModeradora)
        {
            try
            {
                lCuotaModeradoraRepository.Modificar(liquidacionCuotaModeradora);

                return $"Los datos han sido modificados correctamente del archivo," +
                    $" el numero de liquidacion {liquidacionCuotaModeradora.NLiquidacion} ha sido actualizado";
            }
            catch (Exception e)
            {
                return "Error al leer y/o escribir el archivo" + e.Message;
            }
        }
    }
}
