using System.Collections.Generic;
using System.Linq;

namespace lab4 {
    public class DatabaseHandler {
        private static DatabaseContext db;

        public DatabaseHandler() {
            SQLitePCL.Batteries.Init();
            db = new DatabaseContext();
        }

        public static void Create<T>(T item) where T : class {
            db.Add(item);
            db.SaveChanges();
        }

        public static List<T> ReadAll<T>() where T : class {
            return db.Set<T>().ToList();
        }

        public static User ReadUserWithLogin(string login) {
            return db.Users.FirstOrDefault(u => u.Login == login);
        }

        public static User ReadUserWithId(int id) {
            return db.Users.FirstOrDefault(u => u.Id == id);
        }

        public static RegisteredEvent ReadRegisteredEventWithId(int id) {
            return db.RegisteredEvents.FirstOrDefault(re => re.Id == id);
        }

        public static void Delete<T>(T item) where T : class {
            db.Remove(item); 
            db.SaveChanges();
        }

        public static void ResetPassword(User user, string password) {
            user.Password = password;
            db.SaveChanges();
        }

        public static Event ReadEventWithName(string name) {
            return db.Events.FirstOrDefault(e => e.EventName == name);
        }

        public static void ModifyEvent(Event eventToModify,  string name, string agenda) {
            eventToModify.EventName = name;
            eventToModify.Agenda = agenda;
            db.SaveChanges();
        }

        public static void ModifyRegisteredEvent(RegisteredEvent registeredEvent, bool isAccepted) { 
            registeredEvent.IsAccepted = isAccepted;
            db.SaveChanges();
        }
    }
}
