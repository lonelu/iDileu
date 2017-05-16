using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DotNumerics;
using DotNumerics.LinearAlgebra;


namespace iDileu
{
    public partial class Form1 : Form
    {
        double[] result;
        double[] InputArray = new double[15];
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            
            string[] lines = TxtInput.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            try
            {
                for (int i = 0; i <= 14; i++)
                {
                    InputArray[i] = double.Parse(lines[i]);
                }
                result = iDileu_Calculation(InputArray);
            }
            catch(Exception)
            {
                MessageBox.Show("Please input validated varibles and calulate again!\nBefore reuse it, clear all the old data.");
            }
            


            //TxtOutput.Text = (result[0].ToString() + "/n" + result[1].ToString() + "," +
            //result[2].ToString() + "," + result[3].ToString() + "," + result[4].ToString());
            TxtOutput.Clear();
            TxtOutput.Text =  result[0].ToString();
            TxtOutput.Text += System.Environment.NewLine + result[1].ToString();
            TxtOutput.Text += System.Environment.NewLine + result[2].ToString();
            TxtOutput.Text += System.Environment.NewLine + result[3].ToString();
            TxtOutput.Text += System.Environment.NewLine + result[4].ToString();
        }

        private double[] iDileu_Calculation(double[] InputArray)
        {
            double id0 = InputArray[10] / InputArray[0];
            double id3 = (InputArray[11] - InputArray[1] * id0) / InputArray[2];
            double id6 = (InputArray[12] - InputArray[3] * id3) / InputArray[4];

            //A = new double[] { InputArray[6], InputArray[9] };
            //A = new double[] { InputArray[7], InputArray[8] };
            //B = new double[] { (InputArray[13]-InputArray[5]*id6), InputArray[14] };
            Matrix A = new Matrix(2, 2);
            A[0, 0] = InputArray[6]; A[0, 1] = InputArray[9];
            A[1, 0] = InputArray[7]; A[1, 1] = InputArray[8];
            Matrix B = new Matrix(2, 1);
            B[0, 0] = InputArray[13] - InputArray[5] * id6;
            B[1, 0] = InputArray[14];
            LinearEquations leq = new LinearEquations();
            Matrix X = leq.Solve(A, B);
            double id9 = X[0, 0];
            double id12 = X[1, 0];
            double[] result = {id0, id3, id6, id9, id12};
            return result;
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            TxtInput.Clear();
            TxtOutput.Clear();
        }
    }
}
