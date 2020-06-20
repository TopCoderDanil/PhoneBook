using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
namespace Lab1
{
    public partial class MainForm : Form
    {
        private List<Note> PhoneNote;
        private int current;
        bool bob;
        bool Check;
        public MainForm()
        {
            InitializeComponent();
            PhoneNote = new List<Note>();
            current = -1;
            bob = true;
            Check = false;
        }
        private void PrintElement()
        {
            if ((current >= 0) && (current < PhoneNote.Count)) {    // если есть что выводить
                                                                    // MyRecord - запись списка PhoneNote номер current
                Note MyRecord = PhoneNote[current];
                // записываем в соответствующие элементы на форме 
                // поля из записи MyRecord 
                if (MyRecord.House != 0)
                {
                    LastNameTextBox.Text = MyRecord.LastName;
                    NameTextBox.Text = MyRecord.Name;
                    PatronymicTextBox.Text = MyRecord.Patronymic;
                    PhoneMaskedTextBox.Text = MyRecord.Phone;
                    StreetTextBox.Text = MyRecord.Street;
                    HouseNumericUpDown.Value = MyRecord.House;
                    FlatNumericUpDown.Value = MyRecord.Flat;
                }
            }
            else // если current равно -1, т. е. список пуст
            {   // очистить поля формы 
                LastNameTextBox.Text = "";
                NameTextBox.Text = "";
                PatronymicTextBox.Text = "";
                PhoneMaskedTextBox.Text = "";
                StreetTextBox.Text = "";
                HouseNumericUpDown.Value = 1;
                FlatNumericUpDown.Value = 1;
            }
            // обновление строки состояния
            NumberToolStripStatusLabel.Text = (current + 1).ToString();
            QuantityToolStripStatusLabel.Text = PhoneNote.Count.ToString();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Check = true;
            // создаем запись - экземпляр класса Note
            Note MyRecord = new Note();
            // создаем экземпляр формы AddForm
            AddForm _AddForm = new AddForm(MyRecord);
            // открываем форму для добавления записи
            _AddForm.ShowDialog();
            // текущей записью становится последняя
            current = PhoneNote.Count;
            Note ob = _AddForm.MyRecord;
            bool baba = false;
            if (MyRecord.House != 0)
            {
                foreach (Note biob in PhoneNote)
                {
                    if (ob.Equals(biob)) baba = true;
                    if (baba) break;
                }
                // добавляем к списку PhoneN
                if (!baba)
                {
                    PhoneNote.Add(_AddForm.MyRecord);
                    PrintElement();
                }
                else MessageBox.Show("Повтор");
                // выводим текущий элемент
            }
        }

        private void PreviousButton_Click(object sender, EventArgs e)
        {
            int a = Convert.ToInt32(NumberToolStripStatusLabel.Text);
            if (a > 1) {
                current--;      // уменьшаем номер текущей записи на 1
                PrintElement();
            }
            else {
                MessageBox.Show("Предыдущей записи не существует!");
            }
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            int a = Convert.ToInt32(NumberToolStripStatusLabel.Text);
            int b = Convert.ToInt32(QuantityToolStripStatusLabel.Text);
            if (a < b) {
                current++;      // увеличиваем номер текущей записи на 1
                PrintElement();
            }
            else {
                MessageBox.Show("Следующей записи не существует!");
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK && saveFileDialog1.FilterIndex == 1)
            // Если в диалоговом окне нажали ОК
            {
                try         // обработчик исключительных ситуаций
                {
                    // используя sw (экземпляр класса StreamWriter),
                    // создаем файл с заданным в диалоговом окне именем
                    using (StreamWriter sw =
                    new StreamWriter(saveFileDialog1.FileName))
                    {
                        // проходим по всем элементам списка
                        foreach (Note MyRecord in PhoneNote)
                        {
                            // записываем каждое поле в отдельной строке
                            sw.WriteLine(MyRecord.LastName);
                            sw.WriteLine(MyRecord.Name);
                            sw.WriteLine(MyRecord.Patronymic);
                            sw.WriteLine(MyRecord.Street);
                            sw.WriteLine(MyRecord.House);
                            sw.WriteLine(MyRecord.Flat);
                            sw.WriteLine(MyRecord.Phone);
                        }
                    }
                }
                catch (Exception ex)    // перехватываем ошибку
                {
                    // выводим информацию об ошибке
                    MessageBox.Show("Не удалось сохранить данные! Ошибка: " +
                    ex.Message);
                }
            }
            else if (/*saveFileDialog1.ShowDialog() == DialogResult.OK && */saveFileDialog1.FilterIndex == 2)
            {
                //создание xml документа со строкой <?xml version="1.0" encoding="utf-8"?>
                XmlTextWriter textWritter = new XmlTextWriter(saveFileDialog1.FileName, Encoding.UTF8);
                textWritter.WriteStartDocument();//запись заголовка документа
                textWritter.WriteStartElement("Notes");//создание головы
                textWritter.WriteEndElement();//закрываем голову
                textWritter.Close();//закрываем документ
                XmlDocument document = new XmlDocument();//открываем документ  
                document.Load(saveFileDialog1.FileName);//загружаем из xml файла
                int i = 0;
                foreach (Note x in PhoneNote)
                {
                    //Создаём XML-запись
                    XmlElement element = document.CreateElement("Note");
                    document.DocumentElement.AppendChild(element); // указываем родителя
                    XmlAttribute attribute = document.CreateAttribute("id"); // создаём атрибут
                    attribute.Value = i.ToString(); // устанавливаем значение атрибута
                    element.Attributes.Append(attribute); // добавляем атрибут

                    //Добавляем в запись данные
                    XmlNode lastnameElem = document.CreateElement("Lastname"); // даём имя
                    lastnameElem.InnerText = x.LastName; // и значение
                    element.AppendChild(lastnameElem); // и указываем кому принадлежит

                    lastnameElem = document.CreateElement("Name"); // даём имя
                    lastnameElem.InnerText = x.Name; // и значение
                    element.AppendChild(lastnameElem); // и указываем кому принадлежит

                    lastnameElem = document.CreateElement("Patronymic"); // даём имя
                    lastnameElem.InnerText = x.Patronymic; // и значение
                    element.AppendChild(lastnameElem); // и указываем кому принадлежит

                    lastnameElem = document.CreateElement("Street"); // даём имя
                    lastnameElem.InnerText = x.Street; // и значение
                    element.AppendChild(lastnameElem); // и указываем кому принадлежит

                    lastnameElem = document.CreateElement("House"); // даём имя
                    lastnameElem.InnerText = Convert.ToString(x.House); // и значение
                    element.AppendChild(lastnameElem); // и указываем кому принадлежит

                    lastnameElem = document.CreateElement("Flat"); // даём имя
                    lastnameElem.InnerText = Convert.ToString(x.Flat); // и значение
                    element.AppendChild(lastnameElem); // и указываем кому принадлежит

                    lastnameElem = document.CreateElement("Phone"); // даём имя
                    lastnameElem.InnerText = x.Phone; // и значение
                    element.AppendChild(lastnameElem); // и указываем кому принадлежит

                    i++;
                }
                document.Save(saveFileDialog1.FileName + ".xml");
            }
            bob = false;
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Note MyRecord;
            if (openFileDialog1.ShowDialog() == DialogResult.OK && openFileDialog1.FilterIndex == 1)
            // если в диалоговом окне нажали ОК
            {
                try         // обработчик исключительных ситуаций
                {
                    // считываем из указанного в диалоговом окне файла
                    using (StreamReader sr =
                    new StreamReader(openFileDialog1.FileName))
                    {
                        // пока не дошли до конца файла
                        while (!sr.EndOfStream)
                        {
                            //выделяем место под запись
                            MyRecord = new Note();
                            // считываем значения полей записи из файла
                            MyRecord.LastName = sr.ReadLine();
                            MyRecord.Name = sr.ReadLine();
                            MyRecord.Patronymic = sr.ReadLine();
                            MyRecord.Street = sr.ReadLine();
                            MyRecord.House = ushort.Parse(sr.ReadLine());
                            MyRecord.Flat = ushort.Parse(sr.ReadLine());
                            MyRecord.Phone = sr.ReadLine();
                            //добавляем запись в список
                            PhoneNote.Add(MyRecord);
                        }
                    }
                    // если список пуст, то current устанавливаем в -1,
                    // иначе текущей является первая с начала запись (номер 0)
                    if (PhoneNote.Count == 0) current = -1;
                    else current = 0;
                    // выводим текущий элемент
                    PrintElement();
                }
                catch (Exception ex)    // если произошла ошибка
                {
                    // выводим сообщение об ошибке
                    MessageBox.Show("При открытии файла произошла ошибка: " +
                    ex.Message);
                }
            }
            else if (/*openFileDialog1.ShowDialog() == DialogResult.OK && */openFileDialog1.FilterIndex == 2)
            {
                try
                {
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.Load(openFileDialog1.FileName);
                    XmlElement xRoot = xDoc.DocumentElement;
                    foreach (XmlElement xnode in xRoot)
                    {
                        Note node = new Note();
                        foreach (XmlNode cnode in xnode.ChildNodes)
                        {
                            if (cnode.Name == "Lastname") node.LastName = cnode.InnerText;
                            if (cnode.Name == "Name") node.Name = cnode.InnerText;
                            if (cnode.Name == "Patronymic") node.Patronymic = cnode.InnerText;
                            if (cnode.Name == "Street") node.Street = cnode.InnerText;
                            if (cnode.Name == "House") node.House = Convert.ToByte(cnode.InnerText);
                            if (cnode.Name == "Flat") node.Flat = Convert.ToByte(cnode.InnerText);
                            if (cnode.Name == "Phone") node.Phone = cnode.InnerText;
                        }
                        PhoneNote.Add(node);
                    }
                    if (PhoneNote.Count == 0) current = -1;
                    else current = 0;
                    // выводим текущий элемент
                    PrintElement();
                }
                catch (Exception)
                {
                    MessageBox.Show("xml файла не существуе");
                }
            }

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Check)
            {
                int b = Convert.ToInt32(QuantityToolStripStatusLabel.Text);
                if (b > 0 && bob)
                {
                    DialogResult result = MessageBox.Show(
            "Yes - Сохранить \n No - Не сохранять изменения",
            "Сообщение",
            MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        if (saveFileDialog1.ShowDialog() == DialogResult.OK && saveFileDialog1.FilterIndex == 1)
                        // Если в диалоговом окне нажали ОК
                        {
                            try         // обработчик исключительных ситуаций
                            {
                                // используя sw (экземпляр класса StreamWriter),
                                // создаем файл с заданным в диалоговом окне именем
                                using (StreamWriter sw =
                                new StreamWriter(saveFileDialog1.FileName))
                                {
                                    // проходим по всем элементам списка
                                    foreach (Note MyRecord in PhoneNote)
                                    {
                                        // записываем каждое поле в отдельной строке
                                        sw.WriteLine(MyRecord.LastName);
                                        sw.WriteLine(MyRecord.Name);
                                        sw.WriteLine(MyRecord.Patronymic);
                                        sw.WriteLine(MyRecord.Street);
                                        sw.WriteLine(MyRecord.House);
                                        sw.WriteLine(MyRecord.Flat);
                                        sw.WriteLine(MyRecord.Phone);
                                    }
                                }
                            }
                            catch (Exception ex)    // перехватываем ошибку
                            {
                                // выводим информацию об ошибке
                                MessageBox.Show("Не удалось сохранить данные! Ошибка: " +
                                ex.Message);
                            }
                        }
                        else if (saveFileDialog1.ShowDialog() == DialogResult.OK && saveFileDialog1.FilterIndex == 2)
                        {
                            //создание xml документа со строкой <?xml version="1.0" encoding="utf-8"?>
                            XmlTextWriter textWritter = new XmlTextWriter(saveFileDialog1.FileName, Encoding.UTF8);
                            textWritter.WriteStartDocument();//запись заголовка документа
                            textWritter.WriteStartElement("Notes");//создание головы
                            textWritter.WriteEndElement();//закрываем голову
                            textWritter.Close();//закрываем документ
                            XmlDocument document = new XmlDocument();//открываем документ  
                            document.Load(saveFileDialog1.FileName);//загружаем из xml файла
                            int i = 0;
                            foreach (Note x in PhoneNote)
                            {
                                //Создаём XML-запись
                                XmlElement element = document.CreateElement("Note");
                                document.DocumentElement.AppendChild(element); // указываем родителя
                                XmlAttribute attribute = document.CreateAttribute("id"); // создаём атрибут
                                attribute.Value = i.ToString(); // устанавливаем значение атрибута
                                element.Attributes.Append(attribute); // добавляем атрибут

                                //Добавляем в запись данные
                                XmlNode lastnameElem = document.CreateElement("Lastname"); // даём имя
                                lastnameElem.InnerText = x.LastName; // и значение
                                element.AppendChild(lastnameElem); // и указываем кому принадлежит

                                lastnameElem = document.CreateElement("Name"); // даём имя
                                lastnameElem.InnerText = x.Name; // и значение
                                element.AppendChild(lastnameElem); // и указываем кому принадлежит

                                lastnameElem = document.CreateElement("Patronymic"); // даём имя
                                lastnameElem.InnerText = x.Patronymic; // и значение
                                element.AppendChild(lastnameElem); // и указываем кому принадлежит

                                lastnameElem = document.CreateElement("Street"); // даём имя
                                lastnameElem.InnerText = x.Street; // и значение
                                element.AppendChild(lastnameElem); // и указываем кому принадлежит

                                lastnameElem = document.CreateElement("House"); // даём имя
                                lastnameElem.InnerText = Convert.ToString(x.House); // и значение
                                element.AppendChild(lastnameElem); // и указываем кому принадлежит

                                lastnameElem = document.CreateElement("Flat"); // даём имя
                                lastnameElem.InnerText = Convert.ToString(x.Flat); // и значение
                                element.AppendChild(lastnameElem); // и указываем кому принадлежит

                                lastnameElem = document.CreateElement("Phone"); // даём имя
                                lastnameElem.InnerText = x.Phone; // и значение
                                element.AppendChild(lastnameElem); // и указываем кому принадлежит

                                i++;
                            }
                            document.Save(saveFileDialog1.FileName + ".xml");
                        }
                    }
                }
            }


        }
        private void поиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchNameForm ob = new SearchNameForm(PhoneNote);
            ob.Show();
        }

        private void поискПоАдресуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchAddressForm ob = new SearchAddressForm(PhoneNote);
            ob.Show();
        }

        private void поискПоТелефонуToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            SearchPhoneForm ob = new SearchPhoneForm(PhoneNote);
            ob.Show();
        }

        private void поФамилииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PhoneNote.Count > 0) // если список не пуст
            {
                // сортировка списка по фамилии
                PhoneNote.Sort(new CompareByLastName());
                current = 0; // задаем номер текущего элемента
                PrintElement(); // вывод текущего элемента
            }
        }

        private void поКвартиреToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PhoneNote.Count > 0)
            {
                PhoneNote.Sort(new CompareByFlat());
                current = 0;
                PrintElement();
            }
        }

        private void поВозрастаниюПоИмениToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PhoneNote.Count > 0)
            {
                PhoneNote.Sort(new CompareByName());
                current = 0;
                PrintElement();
            }
        }

        private void поВозрастаниюПоОтчествуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PhoneNote.Count > 0)
            {
                PhoneNote.Sort(new CompareByPatronymic());
                current = 0;
                PrintElement();
            }
        }

        private void поВозрастаниюПоУлицеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PhoneNote.Count > 0)
            {
                PhoneNote.Sort(new CompareByStreet());
                current = 0;
                PrintElement();
            }
        }

        private void поВозрастаниюПоДомуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PhoneNote.Count > 0)
            {
                PhoneNote.Sort(new CompareByHouse());
                current = 0;
                PrintElement();
            }
        }

        private void поВозрастаниюПоТелефонуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PhoneNote.Count > 0)
            {
                PhoneNote.Sort(new CompareByPhone());
                current = 0;
                PrintElement();
            }
        }

        private void поУбывваниюПоФамилииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PhoneNote.Count > 0) // если список не пуст
            {
                // сортировка списка по фамилии
                PhoneNote.Sort(new WaneCompareByLastName());
                current = 0; // задаем номер текущего элемента
                PrintElement(); // вывод текущего элемента
            }
        }

        private void поУбываниюПоИмениToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PhoneNote.Count > 0) // если список не пуст
            {
                // сортировка списка по фамилии
                PhoneNote.Sort(new WaneCompareByName());
                current = 0; // задаем номер текущего элемента
                PrintElement(); // вывод текущего элемента
            }
        }

        private void поУбываниюПоОтчествуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PhoneNote.Count > 0)
            {
                PhoneNote.Sort(new WaneCompareByPatronymic());
                current = 0;
                PrintElement();
            }
        }

        private void поУбываниюПоУлицеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PhoneNote.Count > 0)
            {
                PhoneNote.Sort(new WaneCompareByStreet());
                current = 0;
                PrintElement();
            }
        }

        private void поУбываниюПоДомуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PhoneNote.Count > 0)
            {
                PhoneNote.Sort(new WaneCompareByHouse());
                current = 0;
                PrintElement();
            }
        }

        private void поУбываниюПоКвартиреToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PhoneNote.Count > 0)
            {
                PhoneNote.Sort(new WaneCompareByFlat());
                current = 0;
                PrintElement();
            }
        }

        private void поУбываниюПоТелефонуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PhoneNote.Count > 0)
            {
                PhoneNote.Sort(new WaneCompareByPhone());
                current = 0;
                PrintElement();
            }
        }

        private void изменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PhoneNote.Count > 0)
{
                // создаем запись - экземпляр класса Note
                Note MyRecord = new Note();
                // определяем поля записи
                // (берем значения из соответствующих компонентов на форме)
                MyRecord.LastName = LastNameTextBox.Text;
                MyRecord.Name = NameTextBox.Text;
                MyRecord.Patronymic = PatronymicTextBox.Text;
                MyRecord.Phone = PhoneMaskedTextBox.Text;
                MyRecord.Street = StreetTextBox.Text;
                MyRecord.House = (ushort)HouseNumericUpDown.Value;
                MyRecord.Flat = (ushort)FlatNumericUpDown.Value;
                // создаем экземпляр формы и открываем эту форму
                AddForm _AddForm = new AddForm(MyRecord, AddOrEdit.Edit);
                _AddForm.ShowDialog();
                // изменяем текущую запись
                PhoneNote[current] = _AddForm.MyRecord;
                Check = true;
            }
            PrintElement();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PhoneNote.Count > 0)
            {
                PhoneNote.RemoveAt(current);
                current--;
                Check = true;
            }
            PrintElement();
        }
    }

    class CompareByLastName : IComparer<Note>
    {
        public int Compare(Note x, Note y)
        {
            return string.Compare(x.LastName, y.LastName);
        }
    }

    class CompareByFlat : IComparer<Note>
    {
        public int Compare(Note x, Note y)
        {
            return x.Flat.CompareTo(y.Flat);
        }
    }

    class CompareByName : IComparer<Note>
    {
        public int Compare(Note x, Note y)
        {
            return string.Compare(x.Name, y.Name);
        }
    }

    class CompareByPatronymic : IComparer<Note>
    {
        public int Compare(Note x, Note y)
        {
            return string.Compare(x.Patronymic, y.Patronymic);
        }
    }

    class CompareByStreet : IComparer<Note>
    {
        public int Compare(Note x, Note y)
        {
            return string.Compare(x.Street, y.Street);
        }
    }

    class CompareByHouse : IComparer<Note>
    {
        public int Compare(Note x, Note y)
        {
            return x.House.CompareTo(y.House);
        }
    }

    class CompareByPhone : IComparer<Note>
    {
        public int Compare(Note x, Note y)
        {
            return string.Compare(x.Phone, y.Phone);
        }
    }

    class WaneCompareByLastName : IComparer<Note>
    {
        public int Compare(Note y, Note x)
        {
            return string.Compare(x.LastName, y.LastName);
        }
    }

    class WaneCompareByName : IComparer<Note>
    {
        public int Compare(Note y, Note x)
        {
            return string.Compare(x.Name, y.Name);
        }
    }

    class WaneCompareByPatronymic : IComparer<Note>
    {
        public int Compare(Note y, Note x)
        {
            return string.Compare(x.Patronymic, y.Patronymic);
        }
    }

    class WaneCompareByStreet : IComparer<Note>
    {
        public int Compare(Note y, Note x)
        {
            return string.Compare(x.Street, y.Street);
        }
    }

    class WaneCompareByHouse : IComparer<Note>
    {
        public int Compare(Note y, Note x)
        {
            return x.House.CompareTo(y.House);
        }
    }

    class WaneCompareByFlat : IComparer<Note>
    {
        public int Compare(Note y, Note x)
        {
            return x.Flat.CompareTo(y.Flat);
        }
    }

    class WaneCompareByPhone : IComparer<Note>
    {
        public int Compare(Note y, Note x)
        {
            return string.Compare(x.Phone, y.Phone);
        }
    }

    public enum AddOrEdit
    {
        Add,
        Edit
    }
}
