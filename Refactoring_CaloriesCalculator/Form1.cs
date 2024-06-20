using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
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
            ClearResults();

            if (!ValidatePatientPersonalData() || !UserInputValid()) return;

            if (rbtnMale.Checked)
                Patient = new MalePatient();
            else
                Patient = new FemalePatient();

            Patient.WeightInPounds = Convert.ToDouble(txtWeight.Text);
            Patient.HeightInInches = (Convert.ToDouble(txtFeet.Text) * 12)
                + Convert.ToDouble(txtInches.Text);
            Patient.Age = Convert.ToDouble(txtAge.Text);

            txtCalories.Text = Patient.DailyCaloriesRecommended().ToString();
            txtIdealWeigth.Text = Patient.IdealBodyWeight().ToString();
            txtDistance.Text = Patient.DistanceFromIdealWeight().ToString();
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

        private bool ValidatePatientPersonalData()
        {
            int result;
            if (!int.TryParse(txtSSNFirstPart.Text, out result) ||
                !int.TryParse(txtSSNSecondPart.Text, out result) ||
                !int.TryParse(txtSSNThirdPart.Text, out result))
            {
                MessageBox.Show("Debes ingresar un número de Seguridad social válido");
                txtSSNFirstPart.Select();
                return false;
            }

            if (txtFirstName.Text.Length < 1)
            {
                MessageBox.Show("Debes ingresar un Nombre propio del paciente.");
                txtFirstName.Select();
                return false;
            }

            if (txtLastName.Text.Length < 1)
            {
                MessageBox.Show("Debes ingresar un apellido del paciente.");
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidatePatientPersonalData() || !UserInputValid()) return;

            btnCalculate_Click(null, null);

            bool fileCreated = true;
            var document = new XmlDocument();

            try
            {
                document.Load(Assembly
                        .GetEntryAssembly().Location
                        .Replace("Refactoring_CaloriesCalculator.exe", "PatientsHistory.xml"));
            }
            catch (FileNotFoundException)
            {
                fileCreated = false;
            }

            if (!fileCreated)
            {
                document.LoadXml($@"<?xml version=""1.0"" encoding=""utf-8"" ?> 
                <PatientsHistory>
                    <patient ssn=""{Patient.SSN}"" firstName=""{Patient.FirstName}"" lastName=""{Patient.LastName}""> 
                        <measurement date=""{DateTime.Now}"">
                            <height>{Patient.HeightInInches}</height>
                            <weight>{Patient.WeightInPounds}</weight>
                            <age>{Patient.Age}</age> 
                            <dailyCaloriesRecommended>{Patient.DailyCaloriesRecommended()}</dailyCaloriesRecommended> 
                            <idealBodyWeight>{Patient.IdealBodyWeight()}</idealBodyWeight> 
                            <distanceFromIdealWeight>{Patient.DistanceFromIdealWeight()}</distanceFromIdealWeight> 
                        <!--Another measurement -->
                        </measurement> 
                    </patient>
                <!--Another patient --> 
                </PatientsHistory>
                ");
            }
            else
            {
                XmlNode patientNode = null;

                foreach (XmlNode node in document.FirstChild.ChildNodes)
                {
                    foreach (XmlAttribute attrib in node.Attributes)
                    {
                        if (attrib.Name == "ssn" && attrib.Value == Patient.SSN)
                        {
                            patientNode = node;
                        }
                    }
                }

                if (patientNode == null)
                {
                    XmlNode thisPatient = document.DocumentElement.FirstChild.CloneNode(false);
                    thisPatient.Attributes["ssn"].Value = Patient.SSN;
                    thisPatient.Attributes["firstName"].Value = Patient.FirstName;
                    thisPatient.Attributes["lastName"].Value = Patient.LastName;

                    XmlNode measurement = document.DocumentElement.FirstChild["measurement"].CloneNode(true);
                    measurement.Attributes["date"].Value = DateTime.Now.ToString();
                    measurement["height"].FirstChild.Value = Patient.HeightInInches.ToString();
                    measurement["weight"].FirstChild.Value = Patient.WeightInPounds.ToString();
                    measurement["age"].FirstChild.Value = Patient.Age.ToString();
                    measurement["dailyCaloriesRecommended"].FirstChild.Value = Patient.DailyCaloriesRecommended().ToString();
                    measurement["idealBodyWeight"].FirstChild.Value = Patient.IdealBodyWeight().ToString();
                    measurement["distanceFromIdealWeight"].FirstChild.Value = Patient.DistanceFromIdealWeight().ToString();
                    thisPatient.AppendChild(measurement);
                    document.FirstChild.AppendChild(thisPatient);
                }
                else
                {
                    XmlNode measurement = patientNode.FirstChild.CloneNode(true);
                    measurement.Attributes["date"].Value = DateTime.Now.ToString();
                    measurement["height"].FirstChild.Value = Patient.HeightInInches.ToString();
                    measurement["weight"].FirstChild.Value = Patient.WeightInPounds.ToString();
                    measurement["age"].FirstChild.Value = Patient.Age.ToString();
                    measurement["dailyCaloriesRecommended"].FirstChild.Value = Patient.DailyCaloriesRecommended().ToString();
                    measurement["idealBodyWeight"].FirstChild.Value = Patient.IdealBodyWeight().ToString();
                    measurement["distanceFromIdealWeight"].FirstChild.Value = Patient.DistanceFromIdealWeight().ToString();
                    patientNode.AppendChild(measurement);
                }
            }

            document.Save(Assembly
                .GetEntryAssembly().Location
                .Replace("Refactoring_CaloriesCalculator.exe", "PatientsHistory.xml"));
        }
    }
}
