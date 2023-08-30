using System;

namespace alkemyumsa.Entities
{
    public class Login
    {
        public Login() {
            this.Id = 1;
            this.Name = "Pepe";
            this.Password = "1234";
        }
      public int Id { get; set; }
      public string Name { get; set; }
      public string Password { get; set; }

    }
}
