using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;

namespace Mercurial_Med
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //CONEXÃO COM O BANCO DE DADOS 
        IFirebaseConfig ifc = new FirebaseConfig() {
            AuthSecret = "CJANfTZXV9kOpL7XONkRjBUgaBLS4pvL8sRu8hI2",
        BasePath="https://mercurial-med.firebaseio.com/"
        };
        IFirebaseClient client;
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                client = new FireSharp.FirebaseClient(ifc);
            }
            catch {
                MessageBox.Show("Erro ao conectar por favor verifique a sua internet ");
            }
        }
        //SALVANDO DADOS NO BANCO DE DADOS 
        private void BtnSalvar_Click(object sender, EventArgs e)
        {//CLASSE Q GERA A TABELA NO BANCO
            Dados salvar = new Dados()
            {
                Nome = txtNome.Text,
                Idade = txtIdade.Text,
                Sexo = txtSexo.Text
            };
            var set = client.Set(@"Dados/"+ txtNome.Text,salvar);
            MessageBox.Show("Dados salvos com sucesso");
        }
        //PESQUISANDO DADOS NO BANCO DE DADOS 
        private void BtnPesquisar_Click(object sender, EventArgs e)
        {
            var pesquisa = client.Get(@"Dados/" + txtNome.Text);
            Dados salvar = pesquisa.ResultAs<Dados>();
            txtIdade.Text = salvar.Idade;
            txtSexo.Text = salvar.Sexo;
            MessageBox.Show("Pesquisa realizada com sucesso");

        }
        //ATUALIZANDO DADOS NO BANCO DE DADOS 
        private void BtnAtualizar_Click(object sender, EventArgs e)
        {
            Dados salvar = new Dados()
            {
                Nome = txtNome.Text,
                Idade = txtIdade.Text,
                Sexo = txtSexo.Text
            };
            var set = client.Update(@"Dados/" + txtNome.Text, salvar);
            MessageBox.Show("Dados atalizados com sucesso");
        }
        //DELETANDO DADOS NO BANCO DE DADOS 
        private void BtnDeletar_Click(object sender, EventArgs e)
        {
            var set = client.Delete(@"Dados/" + txtNome.Text);
            MessageBox.Show("Dados deletados com sucesso");
        }
    }
}
