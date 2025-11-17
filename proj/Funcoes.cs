using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proj
{
    internal class Funcoes
    {
        public static void CadastrarUsuario()
        {
            Console.WriteLine("CADASTRO DE USUARIO");

            Console.Write("NOME: ");
            string nome = Console.ReadLine();

            Console.Write("IDADE: ");
            int idade = int.Parse(Console.ReadLine());

            Console.Write("PESO: ");
            float peso = float.Parse(Console.ReadLine());

            Console.Write("ALTURA: ");
            float altura = float.Parse(Console.ReadLine());

            Console.WriteLine($"BOA MLK ! USUARIO {nome} CADASTRADO COM SUCESSO!");
        }

        public static void CadastrarDieta()
        {
            Console.WriteLine("CADASTRO DE DIETA");

            Console.Write("OBJETIVO (EMAGRECER/FICAR FORTE/SAUDE): ");
            string objetivo = Console.ReadLine();

            Console.Write("CALORIAS DIARIAS: ");
            int calorias = int.Parse(Console.ReadLine());

            if (calorias > 700)
            {
                Console.WriteLine("ALERTA: dieta muito calorica!");
            }

            Console.WriteLine($"DIETA PARA {objetivo} COM {calorias} CALORIAS DIARIAS CADASTRADA COM SUCESSO!");
        }
    }
}


