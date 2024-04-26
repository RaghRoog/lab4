using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lab4 {
    public class Event {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string EventName { get; set; }
        public string Agenda { get; set; }

       public Event(string eventName, string agenda) {
            EventName = eventName;
            Agenda = agenda;
        }
    }
}
