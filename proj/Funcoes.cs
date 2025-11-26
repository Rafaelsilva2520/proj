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
        static string conexaoString = "server=localhost;uid=root;pwd=root;database=nutricao;port=3306";

        public static void CadastrarUsuario()
        {
            while (true)
            { 
                try
                {
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
                        }
                    }
                    //aqui acaba uma funçao
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Opção inválida, tente novamente. Erro: {e.Message}");
                }
            }
        }
        //msg de opç inválida




        public static void CadastrarDieta()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("CADASTRO DE DIETA");
                    Console.WriteLine("OLA " + Pessoa.nome + " vamos cadastrar sua dieta!");
                    Console.WriteLine("-------------------------------");
                    Console.WriteLine("Informe os dados abaixo:");
                    Console.Write("CPF do paciente: ");
                    long cpf = long.Parse(Console.ReadLine());

                    Console.Write("Carboidratos consumidos por dia (g): ");
                    Dieta.carbo = int.Parse(Console.ReadLine());

                    Console.Write("Proteínas consumidas por dia (g): ");
                    Dieta.prot = int.Parse(Console.ReadLine());

                    Console.Write("Lipídios (gorduras) consumidos por dia (g): ");
                    Dieta.lip = int.Parse(Console.ReadLine());

                    // Cálculo das calorias
                    Dieta.calorias = (Dieta.carbo * 4) + (Dieta.prot * 4) + (Dieta.lip * 9);

                    Console.WriteLine($"\nCALORIAS TOTAIS CONSUMIDAS: {Dieta.calorias} kcal");

                    string nutrienteMaisConsumido;

                    if (Dieta.carbo > Dieta.prot && Dieta.carbo > Dieta.lip)
                        nutrienteMaisConsumido = "Carboidratos";
                    else if (Dieta.prot > Dieta.carbo && Dieta.prot > Dieta.lip)
                        nutrienteMaisConsumido = "Proteínas";
                    else if (Dieta.lip > Dieta.carbo && Dieta.lip > Dieta.prot)
                        nutrienteMaisConsumido = "Lipídios";
                    else
                        nutrienteMaisConsumido = "Equilíbrio";

                    Console.WriteLine($"\nNutriente mais consumido: {nutrienteMaisConsumido}");

                    string dietaRecomendada = nutrienteMaisConsumido switch
                    {
                        "Carboidratos" => "Dieta recomendada: reduzir carboidratos refinados e aumentar proteínas magras.",
                        "Proteínas" => "Dieta recomendada: manter proteínas moderadas e adicionar carboidratos complexos.",
                        "Lipídios" => "Dieta recomendada: diminuir gorduras saturadas e aumentar gorduras boas.",
                        _ => "Dieta recomendada: manter equilíbrio entre os macronutrientes."
                    };
                        //aqui acaba outra funçao



                    Console.WriteLine(dietaRecomendada);

                    using (MySqlConnection conexao = new MySqlConnection(conexaoString))
                    {
                        conexao.Open();

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

                        MySqlCommand cmd = new MySqlCommand(Database.sqlInsertDie, conexao);
                        cmd.Parameters.AddWithValue("@idpac", idPaciente);
                        cmd.Parameters.AddWithValue("@carb", Dieta.carbo);
                        cmd.Parameters.AddWithValue("@prot", Dieta.prot);
                        cmd.Parameters.AddWithValue("@lip", Dieta.lip);
                        cmd.Parameters.AddWithValue("@cal", Dieta.calorias);
                        cmd.ExecuteNonQuery();
                    }
                        //funçao de recomendar uma dieta
                    Console.WriteLine("\nDieta cadastrada com sucesso!");
                    break;
                }



                catch (Exception e)
                {
                    Console.WriteLine($"Erro: {e.Message}");
                }
            }
        }

        public static void ListarPacientes()
        {
            try
            {
                Console.WriteLine("LISTA DE PACIENTES CADASTRADOS:");
                using (MySqlConnection conexao = new MySqlConnection(conexaoString))
                {
                    conexao.Open();
                    string sqlLista = "select id_paciente, nome, idade, cpf, peso, altura from paciente";
                    MySqlCommand cmd = new MySqlCommand(sqlLista, conexao);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(
                                $"ID: {reader["id_paciente"]}, " +
                                $"Nome: {reader["nome"]}, " +
                                $"Idade: {reader["idade"]}, " +
                                $"Peso: {reader["peso"]} kg, " +
                                $"Altura: {reader["altura"]} m, " +
                                $"CPF: {reader["cpf"]}"
                            );
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao listar pacientes: {e.Message}");
            }
        }
    }
}



