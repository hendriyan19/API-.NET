using System;
using System.Collections.Generic;

namespace basic_programming59
{
    class Program
    {
        static void Main(string[] args)
        {

            int pilih;
            string yesNo;
            int harga = 0;
            int hargaDiskon;
            int totalHarga;
            List<String> pilihanMakanan = new List<String>();//List
            List<String> HistoryTransaksi = new List<String>();//List

            user user1 = new user();
            kasir ks = new kasir();
            try
            {
                
                MenuAwal:
                pilihanMakanan.Clear();
                Console.Clear();
                Console.WriteLine("Menu Kasir : ");
                Console.WriteLine("1. Menu Makanan");
                Console.WriteLine("2. History Transaksi");
                Console.Write("Masukan Pilihan: ");
                pilih = int.Parse(Console.ReadLine());
                switch (pilih)
                {
                    case 1:
                        goto z;
                    case 2:
                        goto a;
                        
                }

                z:
                Console.Write("Nama Pembeli: ");
                user1.Nama = Console.ReadLine();
                Console.Write("Uang Pembeli: Rp.");
                user1.Uang = int.Parse(Console.ReadLine());
                Console.Write("Nama Kasir: ");
                ks.Nama = Console.ReadLine();
                 x:
                Console.Clear();
                Console.WriteLine("Menu Rumah Makan Padang Murah"); //Menu
                menuMakanan();//Method Void
               
                Console.WriteLine("Masukan Pilihan :");
                pilih = int.Parse(Console.ReadLine());

                Console.WriteLine(pilih);//Input
                switch (pilih)
                {

                    case 1:

                        harga = harga + 5000;//Operator
                        pilihanMakanan.Add("Nasi Putih : Rp. 5000");//List
                        Console.WriteLine("Pilih Menu Lain Y/N");
                        yesNo = Console.ReadLine().ToUpper();//Input
                        if (yesNo.Equals("Y"))//Decision
                        {
                            goto x;
                        }
                        else if (yesNo.Equals("N"))//Decision
                        {

                            goto y;
                        }
                        break;

                    case 2:
                        harga = harga + 15000;//Operator
                        pilihanMakanan.Add("Rendang Rp. 15000");//List
                        Console.WriteLine("Pilih Menu Lain Y/N");
                        yesNo = Console.ReadLine().ToUpper();//Input
                        if (yesNo.Equals("Y"))//Decision
                        {
                            goto x;
                        }
                        else if (yesNo.Equals("N"))//Decision
                        {
                            goto y;
                        }
                        break;
                    case 3:
                        harga = harga + 8000;//Operator
                        pilihanMakanan.Add("Ati Ampela : Rp. 8000");//List
                        Console.WriteLine("Pilih Menu Lain Y/N");
                        yesNo = Console.ReadLine().ToUpper();//Input
                        if (yesNo.Equals("Y"))//Decision
                        {
                            goto x;
                        }
                        else if (yesNo.Equals("N"))//Decision
                        {
                            goto y;
                        }
                        break;
                    case 4:
                        harga = harga + 12000;//Operator
                        pilihanMakanan.Add("Dendeng : Rp. 12000");//List
                        Console.WriteLine("Pilih Menu Lain Y/N");
                        yesNo = Console.ReadLine().ToUpper();//Input
                        if (yesNo.Equals("Y"))
                        {
                            goto x;
                        }
                        else if (yesNo.Equals("N"))
                        {
                            goto y;
                        }
                        break;
                    }

            y:
                    Console.Clear();
                    Console.WriteLine("Nama kasir " + ks.Nama);
                    HistoryTransaksi.Add("Nama kasir "+ks.Nama);
                    Console.WriteLine("Makanan Anda : ");
                    foreach (String item in pilihanMakanan)//Looping
                    {
                    Console.WriteLine(item);
                    HistoryTransaksi.Add("Makanan :"+item);
                    }
                    
                    hargaDiskon = diskon(harga);//Non-Void
                    totalHarga = harga - hargaDiskon;//Operator

                    if (hargaDiskon != 0)
                    {
                    HistoryTransaksi.Add("Dengan Diskon 30%");
                    HistoryTransaksi.Add("Total Harga  : Rp." + harga);
                    HistoryTransaksi.Add("Diskon       : Rp." + hargaDiskon);
                    HistoryTransaksi.Add("Total Bayar  : Rp." + totalHarga);
                    HistoryTransaksi.Add("Nama Pembeli : "+user1.Nama);
                    HistoryTransaksi.Add("Uang Pembeli : " + user1.Uang);
                    Console.WriteLine("Selamat anda mendapatkan diskon 30%");
                    Console.WriteLine("Total Harga     : Rp." + harga);
                    Console.WriteLine("Diskon          : Rp." + hargaDiskon);
                    Console.WriteLine("Total Bayar     : Rp." + totalHarga);
                    user1.pembeli();
                    user1.kembalian(harga,hargaDiskon);
                    HistoryTransaksi.Add("Kembalian    : " + harga);
                    HistoryTransaksi.Add("\n");
                    Console.WriteLine("Kembali Y/N");
                    yesNo = Console.ReadLine().ToUpper();//Input
                    if (yesNo.Equals("Y"))
                    {
                        goto MenuAwal;
                    }
                    else if (yesNo.Equals("N"))
                    {

                    }

                }
                else
                {
                    HistoryTransaksi.Add("Total Bayar  : Rp." + totalHarga);
                    HistoryTransaksi.Add("Nama Pembeli : " + user1.Nama);
                    HistoryTransaksi.Add("Uang Pembeli : Rp." + user1.Uang);
                    Console.WriteLine("Total Bayar     : Rp." + totalHarga);
                    user1.pembeli();
                    user1.kembalian(harga);
                    HistoryTransaksi.Add("Kembalian    : Rp." + harga);
                    HistoryTransaksi.Add("\n");
                    Console.WriteLine("Kembali Y/N");
                    yesNo = Console.ReadLine().ToUpper();//Input
                    if (yesNo.Equals("Y"))
                    {
                        goto MenuAwal;
                    }
                    else if (yesNo.Equals("N"))
                    {

                    }
                }

                    a:
                    Console.Clear();
                    Console.WriteLine("History Transaksi ");
                    foreach (String item in HistoryTransaksi)//Looping
                    {
                        Console.WriteLine(""+item);
                    }
                    Console.WriteLine("Kembali Y/N");
                    yesNo = Console.ReadLine().ToUpper();//Input
                    if (yesNo.Equals("Y"))
                    {
                    goto MenuAwal;
                    }
                    else if (yesNo.Equals("N"))
                    {
                    
                    }




                    }
                    catch (FormatException e)
                    {
                    Console.WriteLine("INPUT ANDA TIDAK SESUAI FORMAT");
                    Console.WriteLine(e);

                    }
                    Console.ReadKey();
            }


        public static void menuMakanan()//Method Void
        {
            List<String> Makanan = new List<String>();//List
            Makanan.Add("1.Nasi Putih   Rp. 5000");
            Makanan.Add("2.Rendang      Rp. 15000");
            Makanan.Add("3.Ati Ampela   Rp. 8000");
            Makanan.Add("4.Dendeng      Rp. 12000");
            foreach (String item in Makanan)//Looping
            {
                Console.WriteLine(item);
            }
        }

        public static int diskon(int n)//Non-Void
        {
            if (n > 30000)
            {
                n = (n * 3) / 10;//Operator
                return n;
            }
            else
            {

                return 0;
            }

        }





    }
}
