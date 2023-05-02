using Sumativa2Progra.Comunicacion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Sumativa2Progra
{
    public class Program
    {
        static void Main(string[] args)
        {
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);

            Console.WriteLine("Iniciando servidor en puerto {0}", puerto);
            ServerSocket servidor = new ServerSocket(puerto);

            if (servidor.Iniciar())
            {
                Console.WriteLine("Servidor iniciado");
                Console.WriteLine("Esperando Cliente...");
                Socket socketCliente = servidor.ObtenerCliente();
                Console.WriteLine("Cliente Conectado");
                ClienteCom cliente = new ClienteCom(socketCliente);
                cliente.Escribir("Bienvenido cliente ");
                while (true)
                {
                    string respuesta = cliente.Leer();
                    Console.WriteLine("El cliente envio: {0}", respuesta);
                    {
                        if (respuesta != "Chao")
                        {
                            string mensaje = Console.ReadLine();
                            cliente.Escribir(mensaje);
                            Console.WriteLine("Enviaste: " + mensaje);
                        }
                        else
                        {
                            cliente.Desconectar();
                        }
                    }


                }
            }

            else
            {
                Console.WriteLine("ERROR, EL PUERTO ESTA {0} ESTA EN USO", puerto);
            }
        }
    }
}
