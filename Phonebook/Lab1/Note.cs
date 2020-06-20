using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class Note
    {
        public override bool Equals(object obj)
        {
            Note temp = (Note)obj;
            return (Name == temp.Name) && (LastName == temp.LastName) && 
                (Patronymic == temp.Patronymic) && (Street == temp.Street) && 
                (House == temp.House) && (Flat == temp.Flat) && (Phone == temp.Phone);
       }

        public override int GetHashCode()
        {
            return LastName.Length; ;
        }
        public string LastName;
        public string Name;
        public string Patronymic;
        public string Street;
        public ushort House;
        public ushort Flat;
        public string Phone;
    }

}
