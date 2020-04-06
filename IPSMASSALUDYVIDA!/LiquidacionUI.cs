using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using ENTITY;


namespace IPSMASSALUDYVIDA_
{
    class LiquidacionUI
    {
        public static LiquidacionCuotaModeradoraService liquidacionCuotaModeradoraService = new LiquidacionCuotaModeradoraService();
        static string DATO;
        static void Main(string[] args)
        {
         
        }
        public static void Menu()
        {
            int OP=9;
            do
            {
                Console.Clear();
                Console.WriteLine("|°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°|");
                Console.WriteLine("                                 Menu                                ");
                Console.WriteLine("1<- Registro de Liquidacion");
                Console.WriteLine("2<- Consultar Liquidacion  ");
                Console.WriteLine("3<- Eliminar Liquidacion");
                Console.WriteLine("4<- Modificar Liquidacion");
                Console.WriteLine("5<-Visualizar Lista de Liquidaciones");
                Console.WriteLine("0<- EXIT");
                Console.WriteLine("Elija una opcion");
                OP = Opcion("Los valores ingresados no estan disponibles",0,5);
                switch (OP)
                {
                    case 0: break;
                    case 1:RegistroLiquidaciones(); break;
                    case 2:BuscarLiquidacion(); break;
                    case 3: EliminarLiquidacion() ; break;
                    case 4:ModificarLiquidacion(); break;
                    case 5:ListadoLiquidaciones(); break;

                    default:
                        Console.WriteLine("Error, digite solamente los numeros correspondientes");
                        break;
                }
            } while (OP!=0);
        }
        public static LiquidacionCuotaModeradora RegistroDatos()
        {
            LiquidacionCuotaModeradora liquidacionCuotaModeradora;
          Console.WriteLine("A que tipo de regimen pertenece CONTRIBUTIVO->(C) o SUBSIDIADO->(S)");
            string regimen = Opcion1("Digite solamente las opciones planteadas", "C", "S");
            Console.WriteLine("Digite el numero de Liquidacion");
            int NLiquidacion = int.Parse(Console.ReadLine());
            Console.WriteLine("Digite el numero de identificacion");
            int NIdentificacion= int.Parse(Console.ReadLine());
            decimal Salario;
            Console.WriteLine("Digite el valor del servicio prestado por el Hospital");
            decimal VServicioPrestado = decimal.Parse(Console.ReadLine());
            if ( regimen == "S")
            {
                liquidacionCuotaModeradora = new RegimenSubsidiado(NLiquidacion, NIdentificacion, VServicioPrestado);
            }
            else
            {
                liquidacionCuotaModeradora = new RegimenContributivo(NLiquidacion, NIdentificacion, Salario, VServicioPrestado);
            }
            return liquidacionCuotaModeradora;
        }
        public static void RegistroLiquidaciones()
        {
            string OPC;
            do
            {
                Console.Clear();
                LiquidacionCuotaModeradora liquidacionCuotaModeradora = RegistroDatos();
                liquidacionCuotaModeradora.FijarTarifaYTope();
                liquidacionCuotaModeradora.CalcularCuotaModeradora();
                DATO = liquidacionCuotaModeradoraService.Guardar(liquidacionCuotaModeradora);
                Console.WriteLine($"{DATO}");
                Console.WriteLine("El costo de la cuota moderada es de : {0}",liquidacionCuotaModeradora.VCuotaTotal);
                Console.WriteLine("¡¿Desea realizar nuevamente otra Liquidacion?! S/N");
                OPC = Opcion1("Elija solamente las opciones disponibles S/N", "S", "N");

            } while (OPC!="N");
        }
        public static void ListadoLiquidaciones()
        {
            Console.Clear();
            liquidacionCuotaModeradoraService.Consultar();
            Console.ReadKey();
        }

        public static string Opcion1(string msj,string char1, string char2)
        {
            string opc;
            do
            {
                opc = Console.ReadLine().ToUpper();
                if (opc != char1 && opc != char2)
                {
                    Console.WriteLine(msj + "\n");
                    Console.ReadKey();
                }
            } while (opc != char1 && opc != char2);
            return opc;
        }
        public static int Opcion(string msj,int menor, int mayor)
        {
            int op;
            do
            {
                op = int.Parse(Console.ReadLine());
                if (op < menor|| op > mayor)
                {
                    Console.WriteLine(msj);
                    Console.ReadKey();
                }
            } while (op < menor && op > mayor);
            return op;
        }
        public static void EliminarLiquidacion()
        {
            string OPC;
            do
            {
                Console.Clear();
                Console.WriteLine("Digite el numero de la Liquidacion a Eliminar: ");
                int NLiquidacion = int.Parse(Console.ReadLine());
                DATO = liquidacionCuotaModeradoraService.Eliminar(NLiquidacion);
                Console.WriteLine($"{DATO}");
                Console.WriteLine("Desea Eliminar otra Liquidacion S/N");
                OPC = Opcion1("Elija solamente las opciones disponibles S/N", "S", "N");
            } while (OPC!="N");
        }
        public static void ModificarLiquidacion()
        {
            string OPC;
            do
            {
                Console.Clear();
                Console.WriteLine("Digite el numero de la Liquidacion a Eliminar: ");
                int NLiquidacion = int.Parse(Console.ReadLine());
                LiquidacionCuotaModeradora liquidacionCuotaModeradora = liquidacionCuotaModeradoraService.Buscar(NLiquidacion);
                if( liquidacionCuotaModeradora!= null)
                {
                    Console.WriteLine("Digite el nuevo dato de liquidacion que corresponde al valor del servicio ");
                    liquidacionCuotaModeradora.VServicioPrestado = decimal.Parse(Console.ReadLine());
                    liquidacionCuotaModeradora.CalcularCuotaModeradora();
                    DATO = liquidacionCuotaModeradoraService.Modificar(liquidacionCuotaModeradora);
                    Console.WriteLine($"{DATO}");
                    Console.WriteLine("El nuevo valor ha sido actualizado ahora es: {0}",liquidacionCuotaModeradora.VCuotaTotal);
                }
                
                Console.WriteLine("Desea Modificar otra Liquidacion S/N");
                OPC = Opcion1("Elija solamente las opciones disponibles S/N", "S", "N");
            } while (OPC != "N");
        }
        public static void BuscarLiquidacion()
        {
            string OPC;
            do
            {
                Console.Clear();
                List<LiquidacionCuotaModeradora> LiquidacionDeCuotas = new List<LiquidacionCuotaModeradora>();
                Console.WriteLine("Digite el numero de la Liquidacion a Buscar: ");
                int NLiquidacion = int.Parse(Console.ReadLine());
                LiquidacionCuotaModeradora liquidacionCuotaModeradora = liquidacionCuotaModeradoraService.Buscar(NLiquidacion);
                if (liquidacionCuotaModeradora != null)
                {
                    Console.WriteLine("!!!Se ha encontrado el dato!!!");
                    LiquidacionDeCuotas.Add(liquidacionCuotaModeradora);
                    liquidacionCuotaModeradoraService.Mostrar(LiquidacionDeCuotas);
               
                }
                else
                {
                    Console.WriteLine("No hay registro del dato");
                }


                Console.WriteLine("Desea Buscar otra Liquidacion S/N");
            OPC = Opcion1("Elija solamente las opciones disponibles S/N", "S", "N");
        } while (OPC != "N");
        }
}
}
