namespace PedalProAPI.ViewModels
{
    public class ComplexBookingViewModel
    {
        public int timeslotId { get; set; }
        public int bookingTypeID { get; set; }

        public int bicycleId { get; set; }
        public string Description { get; set; }

        public int BicyclePartId { get; set; }
    }
}
