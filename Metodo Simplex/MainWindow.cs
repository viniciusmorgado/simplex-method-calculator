using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Metodo_Simplex
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        int V = 0;
        int ren = 1, col = 0;
        int var, rest, Dmenor = 0, g;
        int Colpivote = 0;
        bool M;

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnNRP_Click(object sender, EventArgs e)
        {
            btntt.Enabled = true;
            btnrenycol.Enabled = false;
            double ele;
            double elpib = Convert.ToDouble(dgv1[col, ren].Value);
            for (int i = 0; i < rest + var + 2; i++)
            {
                ele = Convert.ToInt32(dgv1[i, ren].Value);
                dgv1[i, ren].Value = (ele / elpib).ToString("0.##");
            }
        }

        private void btntt_Click(object sender, EventArgs e)
        {
            btnNRP.Enabled = false;
            Dmenor *= -1;
            int nColumnas = dgv1.ColumnCount;
            int nRenglo = dgv1.RowCount;
            double[] rPiv = new double[nColumnas];
            for (int i = 0; i < nColumnas; i++)
                rPiv[i] = Convert.ToDouble(dgv1[i, ren].Value) * Dmenor;
            for (int i = 0; i < nColumnas; i++)
                dgv1[i, 0].Value = Convert.ToDouble(dgv1[i, 0].Value) + rPiv[i];

            double[] Checar = new double[nColumnas];
            double[] colPivo = new double[nRenglo];
            double[] nrenPivo = new double[nColumnas];
            for (int i = 0; i < nRenglo; i++)
            {
                colPivo[i] = Convert.ToDouble(dgv1.Rows[i].Cells[col].Value);
            }
            for (int i = 0; i < nColumnas; i++)
            {
                nrenPivo[i] = Convert.ToDouble(dgv1.Rows[ren].Cells[i].Value);
            }
            double nrpiv = 0;
            int conta = 1;
            int y = 0, z = 0;
            double elePiv = Convert.ToDouble(dgv1[col, ren].Value);
            for (int i = 1; i < nRenglo; i++)
            {
                y = 0;
                for (int j = 0; j < nColumnas; j++)
                {
                    if (i != ren)
                    {
                        nrpiv = colPivo[i] * -1;
                        Checar[y] = nrpiv * nrenPivo[y];
                        dgv1[j, i].Value = Checar[y] + Convert.ToDouble(dgv1[j, i].Value);
                        y++;
                    }
                }
                conta++;
            }
            double numero = 0.0;
            for (int i = 0; i < nColumnas; i++)
            {
                numero = Convert.ToDouble(dgv1.Rows[0].Cells[i].Value);
                if (numero < 0)
                {
                    M = true;
                    break;
                }
                V++;
            }
            btnValop.Enabled = true;
        }

        private void btnValop_Click(object sender, EventArgs e)
        {

            listBox1.Items.Clear();
            double[] nuevaCpivote = new double[dgv1.RowCount];
            double[] sol = new double[dgv1.RowCount];
            double[] resultado = new double[dgv1.RowCount];
            if (M == true)
            {
                for (int i = 0; i < dgv1.RowCount; i++)
                {
                    for (int j = 0; j < dgv1.ColumnCount; j++)
                    {
                        dgv1.Rows[i].Cells[j].Style.BackColor = Color.White;
                    }
                }
                for (int i = 0; i < dgv1.RowCount; i++)
                {
                    nuevaCpivote[i] = Convert.ToDouble(dgv1.Rows[i].Cells[V].Value);
                }
                for (int i = 0; i < dgv1.RowCount; i++)
                {
                    dgv1.Rows[i].Cells[V].Style.BackColor = Color.Green;
                }
                for (int i = 0; i < sol.Length; i++)
                {
                    sol[i] = Convert.ToDouble(dgv1.Rows[i].Cells[dgv1.ColumnCount - 1].Value);
                }
                for (int i = 0; i < resultado.Length; i++)
                {
                    resultado[i] = sol[i] / nuevaCpivote[i];
                }
                double peque = 999999.99;
                for (int i = 1; i < resultado.Length; i++)
                {
                    if (resultado[i] < peque)
                    {
                        peque = resultado[i];
                    }
                }
                int t;
                for (t = 0; t < dgv1.RowCount; t++)
                {
                    if (peque == resultado[t])
                        break;
                }
                for (int i = 0; i < dgv1.ColumnCount; i++)
                {
                    dgv1.Rows[t].Cells[i].Style.BackColor = Color.Yellow;
                }
                dgv1.Rows[t].Cells[V].Style.BackColor = Color.Red;
                double nElementoPivote = Convert.ToDouble(dgv1.Rows[t].Cells[V].Value);
                int z = 0;
                int ncdt = dgv1.ColumnCount;
                int nrdt = dgv1.RowCount;
                double vari;
                for (int i = 0; i < ncdt; i++)
                {
                    vari = Convert.ToDouble(dgv1.Rows[t].Cells[i].Value);
                    dgv1.Rows[t].Cells[i].Value = (vari / nElementoPivote).ToString("0.##");
                }
                double ch;
                double[] Pri = new double[dgv1.ColumnCount];
                double[] Jir = new double[dgv1.ColumnCount];
                for (int i = 0; i < Jir.Length; i++)
                {
                    Jir[i] = Convert.ToDouble(dgv1.Rows[t].Cells[i].Value);
                }
                for (int i = 0; i < dgv1.RowCount; i++)
                {
                    z = 0;
                    for (int j = 0; j < dgv1.ColumnCount; j++)
                    {
                        if (i != t)
                        {
                            ch = nuevaCpivote[i] * -1;
                            Pri[z] = ch * Jir[z];
                            dgv1[j, i].Value = (Pri[z] + Convert.ToDouble(dgv1[j, i].Value)).ToString("0.##");
                            z++;
                        }
                    }
                }
                listBox1.Items.Add("Z= " + dgv1.Rows[0].Cells[dgv1.ColumnCount - 1].Value);
                for (int i = 1; i < (var + 1); i++)
                {
                    listBox1.Items.Add("X" + i + "= " + dgv1.Rows[i].Cells[dgv1.ColumnCount - 1].Value);
                }
            }
            else
            {
                listBox1.Items.Add("Z= " + dgv1.Rows[0].Cells[dgv1.ColumnCount - 1].Value);
                for (int i = 1; i < (var + 1); i++)
                {
                    listBox1.Items.Add("X" + i + "= " + dgv1.Rows[i].Cells[dgv1.ColumnCount - 1].Value);
                }
            }
        }

        private void dgv1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            cmbvari.SelectedIndex = 0;
            cmbres.SelectedIndex = 0;
            dgv1.Columns.Clear();
            dgv2.Rows.Clear();
            dgv2.Columns.Clear();
            dgv1.Rows.Clear();
            ren = 1;
            col = 0;
            var = 0;
            rest = 0;
            V = 0;
            listBox1.Items.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btncreartabla_Click(object sender, EventArgs e)
        {
            try
            {
                btnrenycol.Enabled = true;
                dgv2.Rows.Clear();
                dgv1.Columns.Clear();
                dgv2.Rows.Clear();
                dgv2.Columns.Clear();
                dgv1.Rows.Clear();
                int v = Convert.ToInt32(cmbvari.Text);
                int r = Convert.ToInt32(cmbres.Text);
                dgv2.ColumnCount = v;
                dgv2.RowCount = r + 1;

                for (int i = 0; i < v; i++)
                    dgv2.Columns[i].Name = ("X" + (i + 1));
                dgv2.Rows[0].HeaderCell.Value = "Max Z =";

                DataGridViewComboBoxColumn cmb = new DataGridViewComboBoxColumn();
                cmb.Items.Add("<=");
                cmb.Items.Add(">=");
                cmb.Items.Add("=");
                cmb.HeaderText = "";
                cmb.Width = 50;

                dgv2.Columns.Add(cmb);
                for (int i = 1; i <= r; i++)
                    dgv2.Rows[i].HeaderCell.Value = "Rest. " + i;
                for (int i = 0; i < v; i++)
                    dgv2.Columns[i].Width = 50;
                dgv2.RowHeadersWidth = 90;
                dgv2.Columns.Add("Result", "Result");
                dgv2.Rows[0].Cells[v + 1].Style.BackColor = Color.Gray;
                dgv2.Rows[0].Cells[v].ReadOnly = true;
                for (int i = 1; i <= r; i++)
                    dgv2.Rows[i].Cells[v].Value = "<=";
            }
            catch
            {
                MessageBox.Show("Por favor, insirar os valores!");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnrenycol_Click(object sender, EventArgs e)
        {
            try
            {
                dgv1.Rows.Clear();
                btnNRP.Enabled = true;
                var  = Convert.ToInt32(cmbvari.Text);
                rest = Convert.ToInt32(cmbres.Text);
                dgv1.ColumnCount = (1 + var + rest + 1);
                dgv1.Columns[0].Name = "Z";

                for (int i = 1; i <= var; i++)
                    dgv1.Columns[i].Name = ("X" + i);

                for (int i = 1; i <= rest; i++)
                    dgv1.Columns[i + var].Name = ("S" + i);
                dgv1.Columns[var + rest + 1].Name = "Solucion";
                dgv1.RowCount = rest + 1;
                dgv1.Rows[0].HeaderCell.Value = "Z";

                for (int i = 1; i <= rest; i++)
                    dgv1.Rows[i].HeaderCell.Value = "S" + i;

                for (int i = 0; i <= rest + var; i++)
                    dgv1.Columns[i].Width = 40;
                dgv1.Columns[var + rest + 1].Width = 55;
                dgv1.RowHeadersWidth = 50;

                int[] z = new int[var];
                for (int i = 0; i < var; i++)
                    z[i] = (Convert.ToInt32(dgv2.Rows[0].Cells[i].Value) * -1);
                dgv1.Rows[0].Cells[0].Value = 1;

                for (int i = 0; i < var; i++)
                {
                    dgv1[i + 1, 0].Value = z[i];
                }

                for (int i = (var + 1); i < (rest + var + 2); i++)
                {
                    dgv1[i, 0].Value = 0;
                }

                for (int i = 0; i < rest; i++)
                {
                    for (int j = 0; j < var; j++)
                    {
                        dgv1[j + 1, i + 1].Value = dgv2[j, i + 1].Value;
                    }
                }

                for (int i = 0; i < rest; i++)
                {
                    dgv1[0, i + 1].Value = 0;
                }

                for (int i = 0; i < rest; i++)
                {
                    for (int j = 0; j < rest; j++)
                    {
                        if (i == j)
                            dgv1[j + (var + 1), i + 1].Value = 1;
                        else
                            dgv1[j + (var + 1), i + 1].Value = 0;
                    }
                }

                for (int i = 0; i < rest; i++)
                {
                    dgv1[var + rest + 1, i + 1].Value = dgv2[var + 1, i + 1].Value;
                }

                Dmenor = 999;
                int num;
                for (int i = 0; i < (rest + var + 2); i++)
                {
                    num = Convert.ToInt32(dgv1[i, 0].Value);
                    if (num < Dmenor)
                        Dmenor = num;
                }

                for (int i = 0; i < (rest + var + 2); i++)
                {
                    if (Convert.ToInt32(dgv1[i, 0].Value) == Dmenor)
                    {
                        dgv1.Columns[i].DefaultCellStyle.BackColor = Color.Red;
                        break;
                    }
                    Colpivote++;
                    col++;
                }
                int R = rest + var + 1;
                int n1, n2, S = 0;
                for (int i = 0; i < rest + 1; i++)
                {
                    n1 = Convert.ToInt32(dgv1[R, i].Value);
                    n2 = Convert.ToInt32(dgv1[col, i].Value);
                    if (n1 > 0 & n2 > 0)
                        S++;
                }
                double[] comp = new double[S];
                int M = 0;
                double ra = 0.0, r2 = 0.0;
                for (int i = 0; i < rest + 1; i++)
                {
                    ra = Convert.ToDouble(dgv1[R, i].Value);
                    r2 = Convert.ToDouble(dgv1[col, i].Value);
                    if (ra > 0 & r2 > 0)
                    {
                        comp[M] = ra / r2;
                        M++;
                    }
                }
                double and = 999999;
                for (int i = 0; i < comp.Length; i++)
                {
                    if (comp[i] < and)
                    {
                        and = comp[i];
                    }
                }
                g = 1;
                for (int i = 0; i < (rest + 1); i++)
                {
                    if (comp[i] == and)
                    {
                        dgv1.Rows[g].DefaultCellStyle.BackColor = Color.Black;
                        break;
                    }
                    g++;
                    ren++;
                }
                dgv1.Rows[ren].Cells[col].Style.BackColor = Color.Yellow;
            }
            catch (Exception)
            {
                MessageBox.Show("Por favor, insira os valores!");
            }
        }
    }
 }

