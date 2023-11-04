using HotelBooking.Core;
using Moq;
using System;
using TechTalk.SpecFlow;
using SpecFlow.HotelBooking.Support;

namespace SpecFlow.HotelBooking.StepDefinitions
{
    [Binding]
    public class BookingStepDefinitions
    {

        private IBookingManager bookingManager;
        IRepository<Booking> bookingRepository;
        private Booking booking = new Booking();
        private bool canBook;

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
            booking.StartDate = DateTime.Today.AddDays(p0);
        }

        [Given(@"I have entered an end date in (.*)")]
        public void GivenIHaveEnteredAnEndDateIn(int p0)
        {
            booking.EndDate = DateTime.Today.AddDays(p0);
        }

        [When(@"I press Create New Booking")]
        public void WhenIPressCreateNewBooking()
        {
            canBook = bookingManager.CreateBooking(booking);
        }

        [Then(@"The result should be (.*)")]
        public void ThenTheResultShouldBe(bool expectedReturn)
        {
            Assert.Equal(expectedReturn, canBook);
        }
    }
}
