using ReaLTaiizor.Forms;

namespace AcademicoAppV1
{
    public partial class FormPrincipal : MaterialForm
    {
        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void alunosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAluno formAluno = new FormAluno();
            //definir FormPrinciapl como pai
            formAluno.MdiParent = this;
            formAluno.Show();

        }
    }
}
