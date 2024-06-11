using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Refactoring_CaloriesCalculator
{
    public partial class Form1 : Form
    {
        public Patient Patient { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            Patient = new Patient();

            ClearResults();

            if (!UserInputValid()) return;

            Patient.Gender = rbtnMale.Checked
                ? Gender.Male
                : Gender.Female;

            Patient.WeightInPounds = Convert.ToDouble(txtWeight.Text);
            Patient.HeightInInches = (Convert.ToDouble(txtFeet.Text) * 12)
                + Convert.ToDouble(txtInches.Text);
            Patient.Age = Convert.ToDouble(txtAge.Text);

            if (rbtnMale.Checked)
            {
                txtCalories.Text = DailyCaloriesRecommendedMale(Convert.ToDouble(txtWeight.Text), ((Convert.ToDouble(txtFeet.Text) * 12)
                    + Convert.ToDouble(txtInches.Text)), Convert.ToDouble(txtAge.Text)).ToString();
                txtIdealWeigth.Text = IdealBodyWeightMale(
                    (Convert.ToDouble(txtFeet.Text) * 12)
                    + Convert.ToDouble(txtInches.Text)).ToString();
            }
            else
            {
                txtCalories.Text = DailyCaloriesRecommendedFemale(Convert.ToDouble(txtWeight.Text), ((Convert.ToDouble(txtFeet.Text) * 12)
                    + Convert.ToDouble(txtInches.Text)), Convert.ToDouble(txtAge.Text)).ToString();
                txtIdealWeigth.Text = IdealBodyWeightFemale(
                    (Convert.ToDouble(txtFeet.Text) * 12)
                    + Convert.ToDouble(txtInches.Text)).ToString();
            }

            txtDistance.Text = DistanceFromIdealWeight(
                Convert.ToDouble(txtWeight.Text),
                Convert.ToDouble(txtIdealWeigth.Text))
                .ToString();
        }

        private bool UserInputValid()
        {
            double result;
            if (!double.TryParse(txtFeet.Text, out result))
            {
                MessageBox.Show("Altura (Pies) debe ser un valor numerico");
                txtFeet.Select();
                return false;
            }

            if (!double.TryParse(txtInches.Text, out result))
            {
                MessageBox.Show("Altura (Pulgadas) debe ser un valor numerico");
                txtInches.Select();
                return false;
            }

            if (!double.TryParse(txtWeight.Text, out result))
            {
                MessageBox.Show("Peso debe ser un valor numerico");
                txtWeight.Select();
                return false;
            }

            if (!double.TryParse(txtAge.Text, out result))
            {
                MessageBox.Show("Edad debe ser un valor numerico");
                txtAge.Select();
                return false;
            }

            if (!(Convert.ToDouble(txtFeet.Text) >= 5))
            {
                MessageBox.Show("La altura debe ser igual o mayor a 5 Piés");
                txtFeet.Select();
                return false;
            }

            return true;
        }

        private void ClearResults()
        {
            txtDistance.Clear();
            txtIdealWeigth.Clear();
            txtCalories.Clear();
        }
    }
}
