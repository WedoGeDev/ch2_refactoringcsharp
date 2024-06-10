using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Refactoring_CaloriesCalculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            txtDistance.Clear();
            txtIdealWeigth.Clear();
            txtCalories.Clear();

            double result;
            if (!double.TryParse(txtFeet.Text, out result))
            {
                MessageBox.Show("Altura (Pies) debe ser un valor numerico");
                txtFeet.Select();
                return;
            }

            if (!double.TryParse(txtInches.Text, out result))
            {
                MessageBox.Show("Altura (Pulgadas) debe ser un valor numerico");
                txtInches.Select();
                return;
            }

            if (!double.TryParse(txtWeight.Text, out result))
            {
                MessageBox.Show("Peso debe ser un valor numerico");
                txtWeight.Select();
                return;
            }

            if (!double.TryParse(txtAge.Text, out result))
            {
                MessageBox.Show("Edad debe ser un valor numerico");
                txtAge.Select();
                return;
            }

            if (!(Convert.ToDouble(txtFeet.Text) >= 5))
            {
                MessageBox.Show("La altura debe ser igual o mayor a 5 Piés");
                txtFeet.Select();
                return;
            }

            if (rbtnMale.Checked)
            {
                txtCalories.Text = (66
                    + (6.3 * Convert.ToDouble(txtWeight.Text))
                    + (12.9 * ((Convert.ToDouble(txtFeet.Text) * 12)
                        + Convert.ToDouble(txtInches.Text)))
                    - 6.8 * Convert.ToDouble(txtAge.Text)).ToString();
                
                txtIdealWeigth.Text = ((50 +
                    (2.3 * (((Convert.ToDouble(txtFeet.Text) - 5) * 12)
                    + Convert.ToDouble(txtInches.Text)))) * 2.2046).ToString();
            }
            else
            {
                txtCalories.Text = (655
                    + (4.3 * Convert.ToDouble(txtWeight.Text))
                    + (4.7 * ((Convert.ToDouble(txtFeet.Text) * 12)
                        + Convert.ToDouble(txtInches.Text)))
                    - 4.7 * Convert.ToDouble(txtAge.Text)).ToString();

                txtIdealWeigth.Text = ((45.5 +
                    (2.3 * (((Convert.ToDouble(txtFeet.Text) - 5) * 12)
                    + Convert.ToDouble(txtInches.Text)))) * 2.2046).ToString();
            }

            txtDistance.Text = (Convert.ToDouble(txtWeight.Text)
                - Convert.ToDouble(txtIdealWeigth.Text)).ToString();
        }
    }
}
