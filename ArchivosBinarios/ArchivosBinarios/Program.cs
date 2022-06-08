using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ArchivosBinarios
{
    class ArchivosBinarioEjempleados
    {
        //Declaracion de flujos
        BinaryWriter bw = null; // flujo de salida --> escritura de datos
        BinaryReader br = null; // flujo de entrada --> lectura de datos

        // Campos de la clase
        string Nombre, Direccion;
        long Telefono;
        int NumEmp, DiasTrabajados;
        float SalarioDiario;

        public void CrearArchivo(string Archivo)
        {
            //variable local metodo
            char resp;
            try
            {
                //Creacion del flujo para escribir datos en el archivo
                bw = new BinaryWriter(new FileStream(Archivo, FileMode.Create, FileAccess.Write));
                //captura de datos
                do
                {
                    Console.Clear();
                    Console.Write("Numero del Empleado: ");
                    NumEmp = int.Parse(Console.ReadLine());
                    Console.Write("Nombre del Empleado: ");
                    Nombre = Console.ReadLine();
                    Console.Write("Direccion del empleado: ");
                    Direccion = Console.ReadLine();
                    Console.Write("Telefono del Empleado: ");
                    Telefono = long.Parse(Console.ReadLine());
                    Console.Write("Dias trabajados del Empleado: ");
                    DiasTrabajados = int.Parse(Console.ReadLine());
                    Console.Write("Salario diario del Empleado: ");
                    SalarioDiario = float.Parse(Console.ReadLine());

                    //escrive los datos en el archivo
                    bw.Write(NumEmp);
                    bw.Write(Nombre);
                    bw.Write(Direccion);
                    bw.Write(Telefono);
                    bw.Write(DiasTrabajados);
                    bw.Write(SalarioDiario);
                    Console.Write("\n\nDesea almacesar otro registro (s/n)");
                    resp = Char.Parse(Console.ReadLine());

                } while ((resp == 's') || (resp == 's'));
                
            }
            catch(IOException e)
            {
                Console.WriteLine("\nError : " + e.Message);
                Console.WriteLine("\nRuta  : " + e.StackTrace);
            }
            finally
            {
                if (bw != null)
                    bw.Close(); //Cierra el flujo - escritura
                Console.WriteLine("\nPrecione <enter> para terminar la Escritura de datos y regresar al menu");
                Console.ReadKey();
            }
        }
        public void MostrarArchivo(string Archivo)
        {
            try
            {
                //verifica si existe el archivo
                if (File.Exists(Archivo))
                {
                    //Creacion de flujo para leer datos del archivo
                    br = new BinaryReader(new FileStream(Archivo, FileMode.Open, FileAccess.Read));
                    //despliegue de datos en pantalla
                    Console.Clear();
                    do
                    {
                        //lectura de registros no llehue a EndOfFile
                        NumEmp = br.ReadInt32();
                        Nombre = br.ReadString();
                        Direccion = br.ReadString();
                        Telefono = br.ReadInt64();
                        DiasTrabajados = br.ReadInt32();
                        SalarioDiario = br.ReadSingle();
                        //Muestra datos
                        Console.WriteLine("Numero de empleados          : " + NumEmp);
                        Console.WriteLine("Nombre del Empleado          : " + Nombre);
                        Console.WriteLine("Direccion del Empleado       : " + Direccion);
                        Console.WriteLine("Telefono del Empleado        : " + Telefono);
                        Console.WriteLine("Dias Trabajados del Empleado : " + DiasTrabajados);
                        Console.WriteLine("Salario diario del Empleado  : " + SalarioDiario);
                        Console.WriteLine("Sueldo total del empleado : {0:c} " + (DiasTrabajados * SalarioDiario));
                        Console.WriteLine("\n");

                    } while (true);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\n\nEl Arichivo " + Archivo + "No existe en el disco");
                    Console.Write("\nPrecione <enter> para continuar...");
                    Console.ReadKey();
                }
            }
            catch (EndOfStreamException)
            {
                Console.WriteLine("\n\nFin del listado de Empleado");
                Console.Write("\nPrecione <enter> para continuar...");
                Console.ReadKey();
            }
            finally
            {
                if (br != null) br.Close(); //Cierra el flujo
                Console.Write("\nPrecione <enter> para terminar la lectura de datos y regresar al menu");
                Console.ReadKey();
            }

        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //declaracion de variables auxiliares
            string Arch = null;
            int opc;
            //Creacion del objero
            ArchivosBinarioEjempleados Al = new ArchivosBinarioEjempleados();

            //Menu de opcciones 
            do
            {

                Console.Clear();
                Console.WriteLine("\n*** ARCHIVO BINARIO EMPLEADOS***");
                Console.WriteLine("1.- Creacion de un Archivo");
                Console.WriteLine("2.- Lectura de un Archivo");
                Console.WriteLine("3.- Salida del programa");
                Console.Write("\nQue opccion deseas: ");
                opc = int.Parse(Console.ReadLine());

                switch (opc)
                {
                    case 1: //bloque de escritura 
                        try
                        {
                            //Captura nombre archivo
                            Console.Write("\nAalimenta el nombre del archivo a Crear: ");
                            Arch = Console.ReadLine();
                            //Verifica si existe el archivo
                            char resp = 's';
                            if (File.Exists(Arch))
                            {
                                Console.WriteLine("\nEl archivo Existe!!, Desea Sobreescribirlo (s/n) ? ");
                                resp = Char.Parse(Console.ReadLine());
                                if ((resp == 's') || (resp == 's'))
                                    Al.CrearArchivo(Arch);

                            }
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("\nError : " + e.Message);
                            Console.WriteLine("\nRuta  : " + e.StackTrace);

                        }
                        break;
                    case 2:
                        //Bloque de lectura 
                        try
                        {
                            //Captura nombre de archivo
                            Console.Write("\nAalimenta el nombre del archivo a Leer: ");
                            Arch = Console.ReadLine();
                            Al.MostrarArchivo(Arch);
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("\nError : " + e.Message);
                            Console.WriteLine("\nRuta  : " + e.StackTrace);
                        }
                        break;
                    case 3:
                        Console.WriteLine("\nPrecione <enter> para salir del programa");
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("\nEsa opcción no Existe!!, Precione <enter> para continuar...");
                        Console.ReadKey();
                        break;

                }

            } while (opc != 3);
        }
    }
}
