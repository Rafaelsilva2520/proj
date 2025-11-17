using proj;
using System;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine(">>>>>> SISTEMA DE NUTRIÇAO :) <<<<<<<<");
            Console.WriteLine("1- CADASTRAR");
            Console.WriteLine("2- CADASTRAR DIETA");
            Console.WriteLine("3- SAIR");
            Console.Write("ESCOLHE AI: ");
            string opc = Console.ReadLine();

            if (opc == "1")
            {
                Funcoes.CadastrarUsuario();
            }
            else if (opc == "2")
            {
                Funcoes.CadastrarDieta();
            }
            else if (opc == "3")
            {
                Console.WriteLine("SAINDO...");
                break;
            }
            else
            {
                Console.WriteLine("OPÇÃO INVÁLIDA, TENTE NOVAMENTE.");
            }

            Console.WriteLine();
        }
    }
}