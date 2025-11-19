using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;

namespace proj
{
    class Funcoes
    {
        static string conexaoString = "server=localhost;uid=root;pwd=;database=nutricao;port=3306";
        public static void CadastrarUsuario()
        {
            while (true)
            { 
                try {

                    Console.WriteLine("CADASTRO DE USUARIO");

                    Console.Write("NOME: ");
                    Pessoa.nome = Console.ReadLine();

                    Console.Write("IDADE: ");
                    Pessoa.idade = int.Parse(Console.ReadLine());

                    Console.Write("CPF: ");
                    Pessoa.cpf = int.Parse(Console.ReadLine());

                    Console.Write("PESO: ");
                    Pessoa.peso = float.Parse(Console.ReadLine());

                    Console.Write("ALTURA: ");
                    Pessoa.altura = float.Parse(Console.ReadLine());

                    using (MySqlConnection conexao = new MySqlConnection(conexaoString))
                    {
                        conexao.Open();
                        Console.WriteLine($"BOA MLK ! USUARIO {Pessoa.nome} CADASTRADO COM SUCESSO!");


                        using (MySqlCommand cmd = new MySqlCommand(Database.sqlInsertPac, conexao))
                        {
                            cmd.Parameters.AddWithValue("@nome", Pessoa.nome);
                            cmd.Parameters.AddWithValue("@idade", Pessoa.idade);
                            cmd.Parameters.AddWithValue("@cpf", Pessoa.cpf);
                            cmd.Parameters.AddWithValue("@peso", Pessoa.peso);
                            cmd.Parameters.AddWithValue("@altura", Pessoa.altura);

                            cmd.ExecuteNonQuery();
                            break;
                        }
                    } 

                } catch (Exception e)
                {
                    Console.WriteLine($"Opção inválida, tente novamente {e}");
                    continue;
                }
            }
        }   
        public static void CadastrarDieta()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("CADASTRO DE DIETA");
                    Console.Write("CPF do paciente: ");
                    long cpf = long.Parse(Console.ReadLine());

                    Console.WriteLine("\nOBJETIVO: \n1 - Emagrecer \n2 - Ficar forte \n3 - Saúde");
                    Console.Write("Opção: ");
                    Dieta.Objetivo = int.Parse(Console.ReadLine());

                    if (Dieta.Objetivo < 1 || Dieta.Objetivo > 3)
                    {
                        Console.WriteLine("Digite entre 1 e 3!");
                        continue;
                    }

                    Console.Write("Calorias diárias: ");
                    Dieta.Calorias = int.Parse(Console.ReadLine());

                    using (MySqlConnection conexao = new MySqlConnection(conexaoString))
                    {
                        conexao.Open();

                        // Buscar id_paciente
                        string sqlBusca = "select id_paciente from paciente where cpf = @cpf";
                        MySqlCommand cmdBusca = new MySqlCommand(sqlBusca, conexao);
                        cmdBusca.Parameters.AddWithValue("@cpf", cpf);

                        object result = cmdBusca.ExecuteScalar();

                        if (result == null)
                        {
                            Console.WriteLine("CPF não encontrado!");
                            continue;
                        }

                        int idPaciente = Convert.ToInt32(result);

                        // Inserir dieta
                        MySqlCommand cmd = new MySqlCommand(Database.sqlInsertDie, conexao);
                        cmd.Parameters.AddWithValue("@idpac", idPaciente);
                        cmd.Parameters.AddWithValue("@obj",  Dieta.Objetivo);
                        cmd.Parameters.AddWithValue("@cal", Dieta.Calorias);
                        cmd.ExecuteNonQuery();

                        Console.WriteLine("\nDieta cadastrada com sucesso!");
                    }

                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Erro: {e.Message}");
                }
            }
        }
    }
}



