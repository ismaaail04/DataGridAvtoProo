using DataGridAvto.Contracts;
using DataGridAvto.Contracts.Models;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataGridAvto
{
    /// <summary>
    /// Главная форма приложения
    /// </summary>
    public partial class Form1 : Form
    {
        private ICarManager carManager;
        private BindingSource bindingSource;

        /// <summary>
        /// Конструктор
        /// </summary>
        public Form1(ICarManager carManager)
        {
            this.carManager = carManager;
            bindingSource = new BindingSource();

            InitializeComponent();

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = bindingSource;
        }

        private async void toolStripAdd_Click(object sender, EventArgs e)
        {
            var avtoForm = new AvtoForm();
            if (avtoForm.ShowDialog(this) == DialogResult.OK)
            {
                await carManager.AddAsync(ValidConverter.ToValidAvto(avtoForm.ValidAvto));
                bindingSource.ResetBindings(false);
                await SetStats();
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].DataPropertyName == "FuelReserve")
            {
                var data = (Avto)dataGridView1.Rows[e.RowIndex].DataBoundItem;
                e.Value = data.CurrFuel / data.AvgFuelCons;
            }

            if (dataGridView1.Columns[e.ColumnIndex].DataPropertyName == "RentalAmount")
            {
                var data = (Avto)dataGridView1.Rows[e.RowIndex].DataBoundItem;
                e.Value = data.CurrFuel / data.AvgFuelCons * data.CostRent;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private async void toolStripEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                var data = (Avto)dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].DataBoundItem;
                var avtoForm = new AvtoForm(ValidConverter.ToAvto(data));
                if (avtoForm.ShowDialog(this) == DialogResult.OK)
                {
                    await carManager.EditAsync(ValidConverter.ToValidAvto(avtoForm.ValidAvto));
                    bindingSource.ResetBindings(false);
                    await SetStats();
                }
            }

        }

        private async void toolStripDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                var data = (Avto)dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].DataBoundItem;
                if (MessageBox.Show($"Вы действительно хотите удалить {data.Mark}?", "Удаление записи", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    await carManager.DeleteAsync(data.Id);
                    bindingSource.ResetBindings(false);
                    await SetStats();
                }
            }

        }
        public async Task SetStats()
        {
            var stats = await carManager.GetStatsAsync();

            toolStripLabel1.Text = $"Всего: {stats.Count}";
            toolStripLabel2.Text = $"Критически низкий уровень запаса хода: {stats.LowCount}";
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            bindingSource.DataSource = await carManager.GetAllAsync();
            await SetStats();
        }
    }
}
