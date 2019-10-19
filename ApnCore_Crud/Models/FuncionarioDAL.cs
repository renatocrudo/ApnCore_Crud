using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApnCore_Crud.Models
{
    public class FuncionarioDAL:IFuncionarioDAL
    {
        string connectionString = @"Data Source=DELL-RENATO\SQLEXPRESS;Initial Catalog=CadastroDB;Integrated Security=True";

        public IEnumerable<Funcionario> GetAllFuncionarios()
        {
            List<Funcionario> lstfuncionario = new List<Funcionario>();

            using(SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("Select FuncionarioId, Nome, Cidade, Departamento, Sexo From Funcionarios", con);
                cmd.CommandType = System.Data.CommandType.Text;

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Funcionario funcionario = new Funcionario();
                    funcionario.FuncionarioId = Convert.ToInt32(dr["FuncionarioId"]);
                    funcionario.Nome = dr["Nome"].ToString();
                    funcionario.Cidade = dr["Cidade"].ToString();
                    funcionario.Departamento = dr["Departamento"].ToString();
                    funcionario.Sexo = dr["Sexo"].ToString();

                    lstfuncionario.Add(funcionario);
                }
                con.Close();
            }
            return lstfuncionario;
        }

        //adicionando um funcionario
        public void AddFuncionario(Funcionario funcionario)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = "Insert Into Funcionarios (Nome, Cidade, Departamento, Sexo) values(@Nome, @Cidade, @Departamento, @Sexo)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.AddWithValue("@Nome", funcionario.Nome);
                cmd.Parameters.AddWithValue("@Cidade", funcionario.Cidade);
                cmd.Parameters.AddWithValue("@Departamento", funcionario.Departamento);
                cmd.Parameters.AddWithValue("@Sexo", funcionario.Sexo);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //Atualizando o Funcionario
        public void UpdateFuncionario(Funcionario funcionario)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = "Update Funcionarios set Nome = @Nome, Cidade = @Cidade, Departamento = @Departamento, Sexo = @Sexo Where FuncionarioId = @FuncionarioId";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.AddWithValue("@FuncionarioId", funcionario.FuncionarioId);
                cmd.Parameters.AddWithValue("@Nome", funcionario.Nome);
                cmd.Parameters.AddWithValue("@Cidade", funcionario.Cidade);
                cmd.Parameters.AddWithValue("@Departamento", funcionario.Departamento);
                cmd.Parameters.AddWithValue("@Sexo", funcionario.Sexo);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //Recuperando um funcionario
        public Funcionario GetFuncionario(int? id)
        {
            Funcionario funcionario = new Funcionario();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = "Select * From Funcionarios Where FuncionarioId= " + id;
                SqlCommand cmd = new SqlCommand(sql, con);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    funcionario.FuncionarioId = Convert.ToInt32(dr["FuncionarioId"]);
                    funcionario.Nome = dr["Nome"].ToString();
                    funcionario.Cidade = dr["Cidade"].ToString();
                    funcionario.Departamento = dr["Departamento"].ToString();
                    funcionario.Sexo = dr["Sexo"].ToString();
                }
            }
            return funcionario;
        }

        //Deletando um funcionario
        public void DeleteFuncionario(int? id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = "Delete From Funcionarios where FuncionarioId = @FuncionarioId";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.AddWithValue("@FuncionarioId", id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
