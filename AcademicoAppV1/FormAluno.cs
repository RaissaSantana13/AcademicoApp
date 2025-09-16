using ReaLTaiizor.Controls;
using ReaLTaiizor.Forms;

namespace AcademicoAppV1
{
    public partial class FormAluno : MaterialForm
    {
        string alunosFileName = "alunos.txt";
        bool isEditMode = false;
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

        private void btnNovo_Click(object sender, EventArgs e)
        {
            LimpaCampos();
            tabControlCadastro.SelectedIndex = 0;
            txtMatricula.Focus();

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Informacoes nao solvas serao perdidas\n"+"Deseja realmente cancelar?", "Atencao", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) 
            {
                LimpaCampos();
                tabControlCadastro.SelectedIndex=1;
            }
        }
    }
}
