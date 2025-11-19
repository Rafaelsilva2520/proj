using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proj
{
    class Database
    {

        public static string sqlCreate = @"
        create database if not exists nutricao;
        use nutricao;

        create table paciente (
        id_paciente int auto_increment primary key,
        nome varchar(100) not null,
        idade int not null,
        cpf int not null unique,
        peso decimal(5,2) not null,
        altura decimal(3,2) not null
        );

        create table dieta(
        id_dieta int auto_increment primary key,
        id_paciente int,
        opcao int not null,
        calorias int not null,
        constraint dieta_pac foreign key (id_paciente) references paciente(id_paciente)
        );
        ";

        public static string sqlInsertPac = @"
        insert into paciente (nome, idade, cpf, peso, altura) 
        values (@nome, @idade, @cpf, @peso, @altura)";

        public static string sqlInsertDie = @"
        insert into dieta (id_paciente, opcao, calorias) 
        values (@idpac, @obj, @cal);";
     }
}
