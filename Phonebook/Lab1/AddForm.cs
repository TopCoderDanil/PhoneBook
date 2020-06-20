using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1
{
    public partial class AddForm : Form
    {
        public Note MyRecord;
        public AddForm(Note _MyRecord)
        {
            InitializeComponent();
            MyRecord = _MyRecord;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            // определяем поля записи -
            // берем значения из соответствующих компонентов на форме
                MyRecord.LastName = LastNameTextBox.Text;
                MyRecord.Name = NameTextBox.Text;
                MyRecord.Patronymic = PatronymicTextBox.Text;
                MyRecord.Phone = PhoneMaskedTextBox.Text;
                MyRecord.Street = StreetTextBox.Text;
                MyRecord.House = (ushort)HouseNumericUpDown.Value;
                MyRecord.Flat = (ushort)FlatNumericUpDown.Value;
                Close();
        }

        public AddForm(Note _MyRecord, AddOrEdit MyType)
        {
            InitializeComponent();
            MyRecord = _MyRecord;
            // если форма открыта для добавления
            if (MyType == AddOrEdit.Add)
            {
                Text = "Добавление абонента";
                AddButton.Text = "Добавить";
            }
           else // если форма открыта для изменения записи
            {
                Text = "Изменение абонента";
                AddButton.Text = "Изменить";
                // определяем значение компонентов на форме
                LastNameTextBox.Text = MyRecord.LastName;
                NameTextBox.Text = MyRecord.Name;
                PatronymicTextBox.Text = MyRecord.Patronymic;
                PhoneMaskedTextBox.Text = MyRecord.Phone;
                StreetTextBox.Text = MyRecord.Street;
                HouseNumericUpDown.Value = MyRecord.House;
                FlatNumericUpDown.Value = MyRecord.Flat;
            }
        }
    }
}
