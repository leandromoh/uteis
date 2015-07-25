using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace Forum
{
    public class Connection
    {
        /// Dados de conexao para SqlServer
        public string ConnectionString { get; set; }
        private SqlConnection Conexao;


        public Connection() 
        {
            this.ConnectionString = @"Server=.\SQLExpress; Database=Forum; Trusted_Connection=Yes; MultipleActiveResultSets=True;";
            this.AbrirConexao();
        }

        /// Construtor que recebe como parametro a ConnectionString
        public Connection(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
            this.AbrirConexao();
        }

        /// Abre conexao
        public void AbrirConexao()
        {
            if (string.IsNullOrEmpty(this.ConnectionString)) throw new Exception("Não foi informado a ConnectionString.");

            if (Conexao == null)
            {
                Conexao = new SqlConnection();
                Conexao.ConnectionString = this.ConnectionString;
            }

            Conexao.Open();
        }

        /// Fecha conexao
        public void FechaConexao()
        {
            if (Conexao != null && Conexao.State == ConnectionState.Open)
            {
                Conexao.Close();
            }
        }

        //Retorna coleção de dados (IDataReader)
        public IDataReader RetornaDados(string sql)
        {
            if (string.IsNullOrEmpty(sql)) throw new Exception("Não foi informado a query SQL.");
            if (Conexao == null || Conexao.State == ConnectionState.Closed) throw new Exception("A conexão fechada. Execute o comando AbrirConexao e não se esqueça de FecharConexao no final.");

            SqlCommand command = new SqlCommand();
            command.Connection = this.Conexao;
            command.CommandText = sql;
            IDataReader reader = command.ExecuteReader();

            return reader;
        }

        //Executa comando e Retorna o total de linhas afetadas
        public int ExecutaComando(string sql)
        {
            if (string.IsNullOrEmpty(sql)) throw new Exception("Não foi informado a query SQL.");
            if (Conexao == null || Conexao.State == ConnectionState.Closed) throw new Exception("A conexão está fechada. Execute o método \"AbrirConexao\" e não se esqueça do \"FecharConexao\" no final.");

            SqlCommand command = new SqlCommand();
            command.Connection = this.Conexao;
            command.CommandText = sql;
            int result = command.ExecuteNonQuery();

            return result;
        }
    }
}