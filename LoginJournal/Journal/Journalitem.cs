﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Journal
{
    public class JournalItem : IComparable
    {
        public enum EventType
        {
            ВХОД, ВЫХОД
        }
        public DateTime date { get; private set; }
        public EventType eventType { get; private set; }
        public string username { get; private set; }
        public string hash { get; private set; }
        public string machineName { get; private set; }
        public string purpose { get; private set; }
        public bool isValid
        {
            get
            {
                var _hash = sha256(date.ToString() + eventType.ToString() + username + machineName);
                return _hash == hash;
            }
        }
        public JournalItem(DateTime date, EventType eventType, string username, string machineName, string purpose = "")
        {
            this.date = date;
            this.eventType = eventType;
            this.username = username;
            this.machineName = machineName;
            this.purpose = purpose;
            hash = sha256(date.ToString() + eventType.ToString() + username + machineName);
        }
        public JournalItem(DateTime date, EventType eventType, string username, string machineName, string hash, string purpose = "")
        {
            this.date = date;
            this.eventType = eventType;
            this.username = username;
            this.machineName = machineName;
            this.purpose = purpose;
            this.hash = hash;
        }

        public static JournalItem StringToJournalItem(string line)
        {
            var _date = line.Substring(0, 30).Trim();
            var _eventType = line.Substring(30, 10).Trim();
            var username = line.Substring(40, 20).Trim();
            var machineName = line.Substring(60, 20).Trim();
            var hash = line.Substring(81, 64).Trim();
            var purpose = line.Substring(145).Trim();
            DateTime date;
            if (!DateTime.TryParse(_date, CultureInfo.CreateSpecificCulture("ru-RU"), DateTimeStyles.None, out date))
            {
                throw new FormatException(_date);
            }
            EventType eventType;
            if (!Enum.TryParse(_eventType, out eventType))
            {
                throw new FormatException(_eventType);
            }
            return new JournalItem(date, eventType, username, machineName, hash, purpose);
        }



        public override string ToString()
        {

            return string.Format("{0,30}{1,10}{2,20}{3,20}{4,65}{5}", date.ToString("F", CultureInfo.CreateSpecificCulture("ru-RU")), eventType, username, machineName, hash, purpose);
        }
        private string sha256(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public int CompareTo(JournalItem obj)
        {
            return date.CompareTo(obj.date);
        }

        public int CompareTo(object obj)
        {
            if (obj is JournalItem)
            {
                return this.CompareTo(obj as JournalItem);
            }
            else
                throw new NotImplementedException();
        }
    }
}
