using ReaLTaiizor.Controls;
using ReaLTaiizor.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace AcademicoAppV1
{
    public partial class FormAluno : MaterialForm
    {
        #region Variaveis
        string alunosFileName = "alunos.txt";
        bool isEditMode = false;
        int indexSelecionado = 0;


        #endregion

        #region Metodos
        public FormAluno()
        {
            InitializeComponent();
        }

        private void LimpaCampos()
        {
            isEditMode = false;
            foreach (var control in tabPageCadastro.Controls)
            {
                if (control is MaterialTextBoxEdit textBox)
                {
                    textBox.Clear();
                }

                if (control is MaterialMaskedTextBox masketTextBox)
                {
                    masketTextBox.Clear();
                }
            }
        }

        private void Salvar()
        {
            var line = $"{txtMatricula.Text};"
                + $"{txtData.Text};"
                + $"{txtNome.Text};"
                + $"{txtEndereco.Text};"
                + $"{txtBairro.Text};"
                + $"{txtCidade.Text};"
                + $"{cboEstado.Text};"
                + $"{txtSenha.Text};";

            if (!isEditMode)
            {
                using (StreamWriter sw = new StreamWriter(alunosFileName, true))
                {
                    sw.WriteLine(line);
                }

            }

            else
            {
                var fileLines = File.ReadAllLines(alunosFileName).ToList();
                fileLines[indexSelecionado] = line;
                File.WriteAllLines(alunosFileName, fileLines);
            }
        }

        private bool ValidaFormulario()
        {
            var erro = "";
            if (string.IsNullOrEmpty(txtMatricula.Text))
            {
                erro += "Matricula deve ser informada\n";
            }

            if (string.IsNullOrEmpty(txtNome.Text))
            {
                erro += "Nome deve ser informada\n";
            }
            if (string.IsNullOrEmpty(txtEndereco.Text))
            {
                erro += "Endereco deve ser informada";
            }
            if (string.IsNullOrEmpty(txtCidade.Text))
            {
                erro += "Cidade deve ser informada\n";
            }

            if (string.IsNullOrEmpty(txtSenha.Text))
            {
                erro += "Senha deve ser informada\n";
            }

            if (string.IsNullOrEmpty(txtData.Text))
            {
                erro += "Data deve ser informada\n";
            }

            if (!DateTime.TryParse(txtData.Text, out _))
            {
                erro += "Data deve ser informada\n";
            }

            if (!string.IsNullOrEmpty(erro))
            {
                MessageBox.Show(erro, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                return false;
            }
        }

        private void CarregaLista()
        {
            Cursor.Current = Cursors.WaitCursor;
            listViewConsulta.Columns.Clear();
            listViewConsulta.Items.Clear();
            listViewConsulta.Columns.Add("Matricula");
            listViewConsulta.Columns.Add("Data");
            listViewConsulta.Columns.Add("Nome");
            listViewConsulta.Columns.Add("Endereco");
            listViewConsulta.Columns.Add("Bairro");
            listViewConsulta.Columns.Add("Cidade");
            listViewConsulta.Columns.Add("Estado");
            listViewConsulta.Columns.Add("Senha");
            var fileLines = File.ReadLines(alunosFileName);
            foreach (var line in fileLines)
            {
                var campos = line.Split(";");
                listViewConsulta.Items.Add(new ListViewItem(campos));
            }
            listViewConsulta.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            Cursor.Current = Cursors.Default;
        }

        private void Editar()
        {
            if (listViewConsulta.SelectedIndices.Count > 0)
            {
                indexSelecionado = listViewConsulta.SelectedItems[0].Index;
                isEditMode = true;
                var item = listViewConsulta.SelectedItems[0];
                txtMatricula.Text = item.SubItems[0].Text;
                txtData.Text = item.SubItems[1].Text;
                txtNome.Text = item.SubItems[2].Text;
                txtEndereco.Text = item.SubItems[3].Text;
                txtBairro.Text = item.SubItems[4].Text;
                txtCidade.Text = item.SubItems[5].Text;
                cboEstado.Text = item.SubItems[6].Text;
                txtSenha.Text = item.SubItems[7].Text;
                tabControlCadastro.SelectedIndex = 0;
                txtMatricula.Focus();
            }

            else
            {
                MessageBox.Show("Selecione algum regsitro!", "Atencao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Deletar()
        {
            if (listViewConsulta.SelectedIndices.Count > 0)
            {
                if (MessageBox.Show("Deseja realmente deletar?", "Pergunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    indexSelecionado=listViewConsulta.SelectedItems[0].Index;
                    var fileLines = File.ReadAllLines(alunosFileName).ToList();
                    fileLines.RemoveAt(indexSelecionado);
                    File.WriteAllLines(alunosFileName, fileLines);
                }
            }

            else
            {
                MessageBox.Show("Selecione algum registro!", "Atencao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region Eventos

        private void btnNovo_Click(object sender, EventArgs e)
        {
            LimpaCampos();
            tabControlCadastro.SelectedIndex = 0;
            txtMatricula.Focus();

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Informacoes nao solvas serao perdidas\n" + "Deseja realmente cancelar?", "Atencao", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                LimpaCampos();
                tabControlCadastro.SelectedIndex = 1;
            }
        }



        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!ValidaFormulario())
            {
                Salvar();
                tabControlCadastro.SelectedIndex += 1;
            }
        }

        private void tabPageConsulta_Enter(object sender, EventArgs e)
        {
            CarregaLista();
        }

        #endregion

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Editar();
        }

        private void listViewConsulta_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Editar();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            Deletar();
            CarregaLista();

        }
    }
}
