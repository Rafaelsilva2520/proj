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
            create table if not exists paciente (
                id_paciente int auto_increment primary key,
                nome varchar(100) not null,
                idade int not null,
                peso decimal(5,2) not null,
                altura decimal(3,2) not null,
                cpf bigint not null unique
            );

            create table if not exists dieta (
                id_dieta int auto_increment primary key,
                id_paciente int not null,
                qtd_carboidratos int not null,
                qtd_proteinas int not null,
                qtd_lipidios int not null,
                calorias int not null,
                constraint fk_dieta_paciente 
                    foreign key (id_paciente) 
                    references paciente(id_paciente)
                    on delete cascade
                    on update cascade
            );
        ";

        public static string sqlInsertPac = @"
            insert into paciente (nome, idade, cpf, peso, altura) 
            values (@nome, @idade, @cpf, @peso, @altura);
        ";

        public static string sqlInsertDie = @"
            insert into dieta (id_paciente, qtd_carboidratos, qtd_proteinas, qtd_lipidios, calorias) 
            values (@idpac, @carb, @prot, @lip, @cal);
        ";
    }

}
