using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechToText.WebJob
{
    public class User
    {
        public User(int id, String email, int balance)
        {
            Id = id;
            Email = email;
            Balance = balance;
        }

        public int Id { get; private set; }
        public string Email { get; private set; }
        public int Balance { get; private set; }
    }
}
