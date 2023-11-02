using HotelBooking.Core;
using Moq;
using System;
using TechTalk.SpecFlow;

namespace SpecFlow.HotelBooking.StepDefinitions
{
    [Binding]
    public class BookingStepDefinitions
    {

        private IBookingManager bookingManager;
        IRepository<Booking> bookingRepository;
        private DateTime startDate, endDate;
        private int roomID;

        [Given(@"I have a repository of booked dates")]
        public void GivenIHaveARepositoryOfBookedDates()
        {
            DateTime start = DateTime.Today.AddDays(10);
            DateTime end = DateTime.Today.AddDays(20);
            bookingRepository = new FakeBookingRepository(start, end);
            IRepository<Room> roomRepository = new FakeRoomRepository();
            bookingManager = new BookingManager(bookingRepository, roomRepository);
        }

        [Given(@"I have entered a start date in (.*)")]
        public void GivenIHaveEnteredAStartDateIn(int p0)
        {
            startDate = DateTime.Today.AddDays(p0);
        }

        [Given(@"I have entered an end date in (.*)")]
        public void GivenIHaveEnteredAnEndDateIn(int p0)
        {
            endDate = DateTime.Today.AddDays(p0);
        }

        [When(@"I press Create New Booking")]
        public void WhenIPressCreateNewBooking()
        {
            roomID = bookingManager.FindAvailableRoom(startDate, endDate);
        }

        [Then(@"The result should be (.*)")]
        public void ThenTheResultShouldBe(int expectedReturn)
        {
            Assert.Equal(expectedReturn, roomID);
        }
    }
}
