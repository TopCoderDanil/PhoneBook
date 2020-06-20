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
    public partial class SearchPhoneForm : Form
    {
        List<Note> PhoneNote;
        public SearchPhoneForm()
        {
            InitializeComponent();
        }
        public SearchPhoneForm(List<Note> _PhoneNote)
        {
            InitializeComponent();
            PhoneNote = _PhoneNote;
        }
        private void поискПоТелефонуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchPhoneForm _Search = new SearchPhoneForm(PhoneNote);
            _Search.ShowDialog();
        }
        private void SearchButton_Click(object sender, EventArgs e)
        {
            // очищаем окно для вывода результатов
            ResultsTextBox.Text = "";
            // количество найденных результатов
            int i = 0;
            // цикл for для каждого элемента списка - foreach
            foreach (Note MyRecord in PhoneNote)
            {
                if (MyRecord.Phone.Contains(PhoneT.Text))
                {
                    // увеличиваем счетчик найденных записей
                    i++;
                    // дописываем элемент и его номер к результату
                    ResultsTextBox.Text = ResultsTextBox.Text + i.ToString() + ". " + MyRecord.LastName + " " + MyRecord.Name + " " + MyRecord.Patronymic +
                        ", ул. " + MyRecord.Street + ", д." + MyRecord.House + ", кв. " + MyRecord.Flat + ", тел. " + MyRecord.Phone + "\r\n";
                }
            }
            // если не найдено ни одной записи, выводим сообщение
            if (i == 0) ResultsTextBox.Text = "Записей, удовлетворяющих поставленным условиям, в списке абонентов нет! ";
        }
    }
}
