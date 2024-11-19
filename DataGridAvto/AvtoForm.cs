using DataGridAvto.Contracts.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using ComboBox = System.Windows.Forms.ComboBox;

namespace DataGridAvto
{
    /// <summary>
    /// Форма добавления данных авто
    /// </summary>
    public partial class AvtoForm : Form
    {
        private ValidAvto avto;

        /// <summary>
        /// Конструктор
        /// </summary>
        public AvtoForm(ValidAvto avto = null)
        {
            InitializeComponent();

            this.avto = avto == null
                ? new ValidAvto
                {
                    Id = Guid.NewGuid(),
                    Mark = Mark.Hunday_Creta,
                }
                : new ValidAvto
                {
                    Id = avto.Id,
                    Mark = Mark.Hunday_Creta,
                    Number = avto.Number,
                    Probeg = avto.Probeg,
                    AvgFuelCons = avto.AvgFuelCons,
                    CurrFuel = avto.CurrFuel,
                    CostRent = avto.CostRent,
                };

            foreach (var item in Enum.GetValues(typeof(Mark)))
            {
                comboBox1.Items.Add(item);
            }
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }

            comboBox1.AddBinding(x => x.SelectedItem, this.avto, x => x.Mark);
            textBoxNumber.AddBinding(x => x.Text, this.avto, x => x.Number, errorProvider1);
            textBoxProbeg.AddBinding(x => x.Text, this.avto, x => x.Probeg, errorProvider1);
            textBoxAvgFuelCons.AddBinding(x => x.Text, this.avto, x => x.AvgFuelCons, errorProvider1);
            textBoxCurrFuel.AddBinding(x => x.Text, this.avto, x => x.CurrFuel, errorProvider1);
            textBoxCostRent.AddBinding(x => x.Text, this.avto, x => x.CostRent, errorProvider1);  
        }

        public ValidAvto ValidAvto => avto;

        private void comboBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.Graphics.FillEllipse(Brushes.Red,
                new Rectangle(e.Bounds.X + 2, e.Bounds.Y + 2, e.Bounds.Height - 4, e.Bounds.Height - 4));
            if (e.Index > -1)
            {
                var value = (Mark)(sender as ComboBox).Items[e.Index];
                e.Graphics.DrawString(GetDisplayValue(value),
                    e.Font,
                    new SolidBrush(e.ForeColor),
                    e.Bounds.X + 20,
                    e.Bounds.Y);
            }
        }

        private string GetDisplayValue(Mark value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attributes = field.GetCustomAttributes<DescriptionAttribute>(false);
            return attributes.FirstOrDefault()?.Description ?? "ММ";
        }

        private void AvtoForm_Load(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(Pens.Silver, 0, 0, Width, 0);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidatableAvto(avto))
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool ValidatableAvto(ValidAvto avto)
        {
            var context = new ValidationContext(avto, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            return Validator.TryValidateObject(avto, context, results, true);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
