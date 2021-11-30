using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Drawing;


namespace Authentication
{

    class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }


    }


    class Program

    {

        static void Main(string[] args)
        {

            List<User> listUser = new List<User>();
            int inputMenu = 0;

            do
            {


                try
                {
                    Console.Clear();
                    for (int i = 0; i <= 100; i++)
                    {
                        Console.Write($"\rProgress System: {i}%   ");
                        Thread.Sleep(25);
                    }

                    Console.WriteLine("\rDone!                  ");
                    Console.WriteLine("Please any Key to Continue");
                    Console.ReadKey();
                x:
                    Console.Clear();
                    Console.WriteLine(" 1. Create User ");
                    Console.WriteLine(" 2. Edit User ");
                    Console.WriteLine(" 3. Delete User ");
                    Console.WriteLine(" 4. Show User ");
                    Console.WriteLine(" 5. Search ");
                    Console.WriteLine(" 6. Login ");
                    Console.WriteLine(" 7. Exit");

                    Console.Write(" Pilih Menu :");
                    inputMenu = Convert.ToInt32(Console.ReadLine());

                    if (inputMenu < 1 || inputMenu > 7)
                    {
                        Console.Clear();
                        Console.WriteLine("!! Peringatan : Nomor yang anda masukan tidak ada dalam list\n");
                        Console.ReadKey();
                    }

                    switch (inputMenu)
                    {
                        case 1: //Create User
                            CreateUser(listUser);
                            goto x;

                        case 2: //Edit User
                            EditUser(listUser);
                            goto x;

                        case 3: //Delete User
                            DeleteUser(listUser);
                            break;

                        case 4: //Show User

                            showUser(listUser);
                            Console.ReadKey();
                            goto x;

                        case 5: //Search
                            Search(listUser);
                            break;

                        case 6: //Login
                            Login(listUser);
                            break;

                        case 7: //Exit
                            Exit();
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.Clear();
                    Console.WriteLine("!! Peringatan : Format input yang anda masukan tidak sesuai !!\n");
                }
            } while (inputMenu != 7);
        }


        static void CreateUser(List<User> listUser)
        {
            try
            {
                var rand = new Random();
                var randomGenerator = new Random();

                string fName, lName, password, username;
                bool validasiPassword = false;
                Console.Clear();
                Console.WriteLine("=======WELCOME========");
                Console.Write("First Name: ");
                fName = Console.ReadLine();
                Console.Write("Last Name: ");
                lName = Console.ReadLine();
                do
                {
                    Console.Write("Password: ");
                    password = Console.ReadLine();
                    Console.WriteLine("======================");
                    if (password.Any(char.IsUpper) && password.Any(char.IsSymbol) && password.Any(char.IsDigit))
                    {
                        validasiPassword = true;

                    }
                    else
                    {
                        validasiPassword = false;
                        Console.WriteLine("!!PERHATIAN Password harus mengandung huruf besar, symbol, dan juga angka");
                    }

                } while (validasiPassword == true);



                username = fName.Substring(0, 2) + lName.Substring(0, 2);

                string random1 = "";
                for (int i = 0; i < listUser.Count; i++)
                {
                    if (listUser[i].UserName == username)
                    {
                        random1 = Convert.ToString(randomGenerator.Next(1, 99));
                        username = username + random1;
                        i = 0;
                    }
                }

                listUser.Add(new User()
                {
                    FirstName = fName,
                    LastName = lName,
                    UserName = username,
                    Password = BCrypt.Net.BCrypt.HashPassword(password)
                });


                Console.Clear();

                Console.WriteLine("=======WELCOME========");
                Console.WriteLine("Your Account :");
                Console.WriteLine("");
                Console.WriteLine("First Name: {0}", fName);
                Console.WriteLine("Last Name: {0}", lName);
                Console.WriteLine("UserName: {0}", username);
                Console.WriteLine("Password: {0}", password);
                Console.WriteLine("======================");
                Console.WriteLine("Press any Key to Continue");
                Console.ReadKey();
            }

            catch (ArgumentOutOfRangeException)
            {
                Console.Clear();
                Console.WriteLine("!! PERHATIAN Inputan First Name atau Lastname anda kurang dari 2 karakter!!");
            }


        }


        static void showUser(List<User> listUser)
        {
            foreach (User item in listUser)
            {
                Console.WriteLine("Nama: " + item.FirstName + item.LastName);
                Console.WriteLine("Username :" + item.UserName);
                Console.WriteLine("Password :" + item.Password);
            }

        }

        static void DeleteUser(List<User> listUser)
        {

            string userName = "";
            Console.Clear();
            Console.WriteLine("=====================WELCOME=====================");
            Console.WriteLine(" ");
            Console.Write("Masukan User Name User yang ingin anda delete : ");
            userName = Console.ReadLine();

            for (int i = 0; i < listUser.Count; i++)
            {

                if (listUser[i].UserName == userName)
                {
                    Console.WriteLine("Mengdelete akun dengan username : " + userName);

                    listUser.RemoveAt(i);
                    Console.WriteLine("*User Berhasil Didelete !*\n");
                    Console.Write("Press any key to Continue... ");
                    Console.ReadLine();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("=====================WELCOME=====================");
                    Console.WriteLine("User yang anda ingin delete tidak ada");
                    Console.Write("Press any key to Continue... ");
                    Console.ReadLine();
                    Console.Clear();
                }

            }
        }

        static void EditUser(List<User> listUser)
        {
            string userName = "";
            Console.Clear();
            Console.WriteLine("=====================WELCOME=====================");
            Console.WriteLine(" ");
            Console.Write("Masukan User Name User yang ingin anda edit : ");
            userName = Console.ReadLine();

            for (int i = 0; i < listUser.Count; i++)
            {

                if (listUser[i].UserName == userName)
                {
                    Console.WriteLine("Mengedit User " + userName);

                    Console.Write("First Name Baru : ");
                    listUser[i].FirstName = Console.ReadLine();
                    Console.WriteLine(" ");

                    Console.Write("Last Name Baru : ");
                    listUser[i].LastName = Console.ReadLine();
                    Console.WriteLine(" ");

                    Console.Write("Password Name Baru : ");
                    listUser[i].Password = Console.ReadLine();
                    Console.WriteLine(" ");

                    Console.WriteLine("*User Berhasil Diedit !*\n");
                    Console.Write("Input apapun untuk kembali : ");
                    Console.ReadLine();
                    Console.Clear();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("=====================WELCOME=====================");
                    Console.WriteLine("User yang anda ingin edit tidak ada");
                    Console.Write("Press any key to Continue... ");
                    Console.ReadLine();
                    Console.Clear();
                }

            }


        }

        static void Search(List<User> listUser)
        {
            string userName = "";
            Console.Clear();
            Console.WriteLine("=====================WELCOME=====================");
            Console.WriteLine(" ");

            Console.Write("Masukan User Name User yang ingin anda cari : ");
            userName = Console.ReadLine();

            for (int i = 0; i < listUser.Count; i++)
            {
                if (listUser[i].UserName == userName)
                {
                    Console.WriteLine("User dengan User Name : " + listUser[i].UserName + " berhasil ditemukan ! ");

                    Console.WriteLine("First Name : " + listUser[i].FirstName);

                    Console.WriteLine("Last Name : " + listUser[i].LastName);

                    Console.WriteLine("Password Name : " + listUser[i].Password);

                    Console.Write("\nInput apapun untuk kembali : ");
                    Console.ReadLine();
                    Console.Clear();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("=====================WELCOME=====================");
                    Console.WriteLine("User dengan User Name" + userName + " tidak ditemukan");
                    Console.Write("Press any key to Continue... ");
                    Console.ReadLine();
                    Console.Clear();
                }

            }

        }

        static void Login(List<User> listUser)
        {


            string userName = "";
            string password = "";
            string passwordHash = "";

            Console.Clear();
            Console.WriteLine("=====================WELCOME=====================");
            Console.WriteLine(" ");

            Console.Write("Masukan User Name : ");
            userName = Console.ReadLine();
            Console.Write("Masukan password: ");
            password = Console.ReadLine();
            bool verifyPassword;

            for (int i = 0; i < listUser.Count; i++)
            {
                verifyPassword = BCrypt.Net.BCrypt.Verify(password, listUser[i].Password);

                if (listUser[i].UserName != userName)
                {
                    Console.Clear();
                    Console.WriteLine("Username from User" + userName + "is Incorrect");
                    Console.WriteLine("Access Denied");
                    Console.ReadLine();
                    Console.Clear();
                }
                else if (verifyPassword != true)
                {
                    Console.Clear();
                    Console.WriteLine("Password from User " + userName + "is Incorrect");
                    Console.WriteLine("Access Denied");
                    Console.ReadLine();
                    Console.Clear();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("User Name : " + listUser[i].UserName + " berhasil ditemukan ! ");


                    Console.WriteLine("Password : " + BCrypt.Net.BCrypt.Verify(password, listUser[i].Password));
                    Console.WriteLine(BCrypt.Net.BCrypt.HashPassword(password));

                    Console.Write("Press any key to Continue... ");
                    Console.ReadLine();
                    Console.Clear();
                }
            }


        }

        static void Exit()
        {
            Console.Clear();

            for (int i = 0; i < 1; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    Console.Clear();

                    // steam
                    Console.Write("       . . . . o o o o o o", Color.LightGray);
                    for (int s = 0; s < j / 2; s++)
                    {
                        Console.Write(" o", Color.LightGray);
                    }
                    Console.WriteLine();

                    var margin = "".PadLeft(j);
                    Console.WriteLine(margin + "                _____      o", Color.LightGray);
                    Console.WriteLine(margin + "       ____====  ]OO|_n_n__][.", Color.DeepSkyBlue);
                    Console.WriteLine(margin + "      [________]_|__|________)< ", Color.DeepSkyBlue);
                    Console.WriteLine(margin + "       oo    oo  'oo OOOO-| oo\\_", Color.Blue);
                    Console.WriteLine("   +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+-", Color.Silver);
                    Console.WriteLine("\n\n Terimakasih Telah Menggunakan Program Kami \n\n");
                    Thread.Sleep(50);


                }
            }

        }

    }


}