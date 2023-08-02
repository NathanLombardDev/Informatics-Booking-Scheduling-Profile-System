namespace PedalProAPI.ViewModels
{
    public class ClientViewModel
    {
        public string UserId { get; set; }
        public string EmailAddress { get; set; }
        public string ClientName { get; set; }
        public string ClientSurname { get; set; }
        public int ClientTypeId { get; set; }
        public DateTime ClientDateOfBirth { get; set; }
        public string ClientPhoneNum { get; set; }
        public string ClientPhysicalAddress { get; set; }
        public string ClientProfilePicture { get; set; }
        public string ClientTitle { get; set; }
    }
}
