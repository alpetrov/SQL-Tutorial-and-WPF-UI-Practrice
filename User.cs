using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;

namespace SQLFirstTutorial
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        string Name { get; set; }
        string Surname { get; set; }
        
        public User(int id, string username)
        {
            Id = id;
            Username = username;
        }

        public User(DataRow row)
        {
            Id = Convert.ToInt32(row["ID"]);
            Username = row["Username"].ToString();
        }
    }
}
