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
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            ClearResults();

            if (!UserInputValid()) return;

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

        private double IdealBodyWeightFemale(double heightInInches)
        {
            return (45.5 + (2.3 * (heightInInches - 60))) * 2.2046;
        }

        private double IdealBodyWeightMale(double heightInInches)
        {
            return (50 + (2.3 * (heightInInches - 60))) * 2.2046;
        }

        private double DailyCaloriesRecommendedFemale(double weightInPounds, double heightInInches, double age)
        {
            return (655
                + (4.3 * weightInPounds)
                + (4.7 * heightInInches)
                - 4.7 * age);
        }

        private double DailyCaloriesRecommendedMale(double weightInPounds, double heightInInches, double age)
        {
            return (66
                + (6.3 * weightInPounds)
                + (12.9 * heightInInches)
                - 6.8 * age);
        }

        private double DistanceFromIdealWeight(double actualWeightInPounds, double idealWeightInPounds)
        {
            return actualWeightInPounds
                   - idealWeightInPounds;
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
