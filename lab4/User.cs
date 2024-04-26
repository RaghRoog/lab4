using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lab4 {
    public class User {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        private int id;
        private string fname;
        private string lname;
        private string login;
        private string password;
        private string email;
        private string permissions;
        private string registration_date;

        public User(string fname, string lname, string login, string password, string email, string permissions, string registration_date) {
            this.fname = fname;
            this.lname = lname;
            this.login = login;
            this.password = password;
            this.email = email;
            this.permissions = permissions;
            this.registration_date = registration_date;
        }

        public int Id { get { return id; } set { id = value; } }
        public string Fname { get { return fname; } set { fname = value; } }
        public string Lname { get { return lname; } set { lname = value; } }
        public string Login { get { return login; } set { login = value; } }
        public string Password { get { return password; } set { password = value; } }
        public string Email { get { return email; } set { email = value; } }
        public string Permissions { get { return permissions; } set { permissions = value; } }
        public string Registration_date { get { return registration_date; } set { registration_date = value; } }
    }
}
