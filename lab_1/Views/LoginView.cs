﻿using System;
using System.Diagnostics;
using System.Threading;
using lab_1.Models;
using lab_1.Workers;

namespace lab_1.Views
{
    class LoginView : IView
    {
        public void Start()
        {
            string login = "";
            string pass = "";
            Console.WriteLine("Введите логин: ");
            TypeLogin(ref login);
            Console.WriteLine("Введите пароль: ");
            TypePassword(ref pass);
            if (CheckLoginAndPass(login, pass))
                Console.WriteLine("Пользователь успешно вошел в ЧАТ");
            else
            {
                Console.WriteLine("Некорректные данные! Даю еще одну попытку..");
                Start();
            }
        }

        private void TypePassword(ref string pass)
        {
            string typed = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(typed))
                Start();
            pass = typed;
        }

        private void TypeLogin(ref string login)
        {
            string typed = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(typed) || !CheckLogin(typed))
                Start();
            login = typed;
        }

        private bool CheckLogin(string login)
        {
            var list = FileWorker.GetProfilesFromFile();
            Profile profile = list.Find(x => x.Login == login);
            if (profile == null)
            {
                Console.WriteLine("Такого логина нет!");
                return false;
            }
            Console.WriteLine("Такой логин есть!");
            return true;
        }

        private bool CheckLoginAndPass(string login, string pass)
        {
            var list = FileWorker.GetProfilesFromFile();
            Profile profile = list.Find(x => x.Login == login && x.Password == pass);
            if (profile == null)
            {
                Console.WriteLine("А пароль то.. неправильный!");
                Console.WriteLine("Консоль закроется через 5 секунд");
                Thread.Sleep(5000);
                Process.GetCurrentProcess().CloseMainWindow();
            }

            return true;
        }
    }
}
