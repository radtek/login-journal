﻿using Journal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class Program
    {

        private static string CurrentUser
        {
            get
            {
                //var s1 = "";
                //try { s1 = "s1:" + System.DirectoryServices.AccountManagement.UserPrincipal.Current.DisplayName; }
                //catch (Exception e) { }
                var s2 = "";
                try { s2 = "s2:" + WindowsIdentity.GetCurrent().Name.ToString() + "|" + WindowsIdentity.GetCurrent().ToString(); }
                catch (Exception e) { }
                var s3 = "";
                try { s3 = "s3:" + Environment.UserName; }
                catch (Exception e) { }
                return s2 + s3;
            }
        }
        static void Main(string[] args)
        {
            if (!File.Exists("./file.txt"))
            {
                using (var sw = new StreamWriter("./file.txt", true))
                {
                    sw.WriteLine(",,,");
                }
            }
            else
            {

                using (var sw = new StreamWriter("./file.txt", true))
                {
                    sw.WriteLine(",,asd,");
                }
            }
            //var ji = new JournalItem(DateTime.Now, JournalItem.EventType.ВХОД, "asd", Environment.MachineName);
            //Console.WriteLine(ji);
            //var newj = JournalItem.StringToJournalItem(ji.ToString());
            //Console.WriteLine(newj);
            //Console.ReadKey();
        }
    }
}
