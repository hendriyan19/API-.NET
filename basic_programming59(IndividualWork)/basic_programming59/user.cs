using System;
using System.Collections.Generic;
using System.Text;

namespace basic_programming59
{
    class user
    {
        public string Nama { get; set; }
        public int Uang{ get; set; }

        public void pembeli() {
        Console.WriteLine("Nama Pembeli  : "+Nama);
        Console.WriteLine("Uang          : Rp. " +Uang);
        }

        public void kembalian(int total) {
            int kembalian;
            kembalian = Uang-total;
            Console.WriteLine("Kembalian : Rp. "+kembalian);
            Uang = 0;
        }

        

        public void kembalian(int total, int diskon)
        {
            int kembalian;
            kembalian = Uang-(total-diskon);
            Console.WriteLine("Kembalian : Rp. " + kembalian);
            Uang = 0;
        }

    }
}
