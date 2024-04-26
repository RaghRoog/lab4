using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace lab4 {
    public class RegisteredEvent {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string EventName { get; set; }
        public string EventAgenda { get; set; }
        public string EventDate {  get; set; }
        public string ParticipationType { get; set; }
        public string FoodType { get; set; }
        public bool IsAccepted { get; set; }
        public int UserID { get; set; }

        public RegisteredEvent(string eventName, string eventAgenda, string eventDate, string participationType, 
            string foodType, bool isAccepted, int userID) {
            EventName = eventName;
            EventAgenda = eventAgenda;
            EventDate = eventDate;
            ParticipationType = participationType;
            FoodType = foodType;
            IsAccepted = isAccepted;
            UserID = userID;
        }
    }
}
