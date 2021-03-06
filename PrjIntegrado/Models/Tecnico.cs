﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Windows.Forms;

namespace PrjIntegrado.Models
{
    public class Tecnico
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }

        public List<Tecnico> getTecnicos()
        {
            DbConnection dbConnection = new DbConnection();
            List<Tecnico> tecnicos = new List<Tecnico>();
            string tableName = "tecnicos";
            string fields = " id_tecnico, nome, cpf ";
            var result = dbConnection.Select(tableName, fields);
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Tecnico aux = new Tecnico();
                    aux.Id = result.GetInt32(0);
                    aux.Nome = result.GetString(1);
                    aux.Cpf = result.GetString(2);
                    tecnicos.Add(aux);
                }
            }
            return tecnicos;
        }

        public List<Tecnico> getTecnicos(string nome)
        {
            DbConnection dbConnection = new DbConnection();
            List<Tecnico> tecnicos = new List<Tecnico>();
            string table = "tecnicos";
            string like = " nome LIKE '%" + nome + "%'";
            var result = dbConnection.Search(table, like);
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Tecnico aux = new Tecnico();
                    aux.Id = result.GetInt32(0);
                    aux.Nome = result.GetString(1);
                    aux.Cpf = result.GetString(2);
                    tecnicos.Add(aux);
                }
            }
            return tecnicos;
        }

        public bool DeleteTecnico(int id)
        {
            DbConnection dbConnection = new DbConnection();
            string stmt = "SELECT * FROM MANUTENCOES WHERE ID_TECNICO = " + id;
            var result = dbConnection.GenericQuery(stmt);
            if (result.HasRows)
            {
                return false;
            }
            string tableName = "tecnicos";
            string condition = " id_tecnico = " + (id.ToString());
            DbConnection dbConnection1 = new DbConnection();
            dbConnection1.Delete(tableName, condition);
            return true;
        }

        public Tecnico selectById(int id)
        {
            DbConnection dbConnection = new DbConnection();            
            string table = "tecnicos";
            string fields = " id_tecnico, nome, cpf ";
            string id_field = "id_tecnico";
            int id_tecnico = id;
            var result = dbConnection.SelectById(table, fields, id_field, id_tecnico);
            Tecnico aux = new Tecnico();
            if (result.HasRows)
            {
                result.Read();
                aux.Id = result.GetInt32(0);
                aux.Nome = result.GetString(1);
                aux.Cpf = result.GetString(2);
            }
            return aux;
        }

        internal bool Insert(Tecnico aux)
        {
            DbConnection dbConnection = new DbConnection();
            string table = "tecnicos ";
            string fields = "nome, cpf";
            string values = "'" + aux.Nome + "'" + ", " + "'" + aux.Cpf + "'";
            dbConnection.Insert(table, fields, values);
            return true;
        }

        public bool Update(Tecnico aux)
        {
            DbConnection dbConnection = new DbConnection();
            string table = "tecnicos";
            string fields = "nome = '" + aux.Nome + "', cpf = '" + aux.Cpf + "'";
            string condition = "id_tecnico = " + (aux.Id.ToString());
            dbConnection.Update(table, fields, condition);
            return true;
        }
    }

}