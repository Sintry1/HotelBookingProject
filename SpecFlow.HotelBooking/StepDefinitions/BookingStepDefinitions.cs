using HotelBooking.Core;
using Moq;
using System;
using TechTalk.SpecFlow;

namespace HotelBooking.Specflow.Booking.StepDefinitions
{
    [Binding]
    public class BookingStepDefinitions
    {

       
        DateTime startDate, endDate;
        DateTime[] occupiedDates = new DateTime[]
        {
            new DateTime(2023,11,25),
            new DateTime(2023,11,30),
        };


       
        

        private ScenarioContext scenarioContext;

        [Given(@"occupiedRange")]
        public void GivenOccupiedRange(Table table)
        {
            throw new PendingStepException();
        }

        [Given(@"I have input a startDate before Occupied Range")]
        public void GivenIHaveInputAStartDateBeforeOccupiedRange(DateTime bookingStartDate)
        {
            startDate = bookingStartDate;
        }

        [Given(@"I have input a endDate before Occupied Range")]
        public void GivenIHaveInputAEndDateBeforeOccupiedRange(DateTime bookingEndDate)
        {
            endDate = bookingEndDate;
        }

        [When(@"I attempt booking before occupied range")]
        public void WhenIAttemptBookingBeforeOccupiedRange()
        {
            
        }

        [Then(@"A booking is made before occupied range")]
        public void ThenABookingIsMadeBeforeOccupiedRange()
        {
            throw new PendingStepException();
        }

        [Given(@"I have input a startDate after Occupied Range")]
        public void GivenIHaveInputAStartDateAfterOccupiedRange(DateTime startDate)
        {
            throw new PendingStepException();
        }

        [Given(@"I have input a endDate after Occupied Range")]
        public void GivenIHaveInputAEndDateAfterOccupiedRange(DateTime endDate)
        {
            throw new PendingStepException();
        }

        [When(@"I attempt booking after occupied range")]
        public void WhenIAttemptBookingAfterOccupiedRange()
        {
            throw new PendingStepException();
        }

        [Then(@"A booking is made after occupied range")]
        public void ThenABookingIsMadeAfterOccupiedRange()
        {
            throw new PendingStepException();
        }

        [Given(@"I have input a startDate during Occupied Range")]
        public void GivenIHaveInputAStartDateDuringOccupiedRange(DateTime startDate)
        {
            throw new PendingStepException();
        }

        [Given(@"I have input a endDate during Occupied Range")]
        public void GivenIHaveInputAEndDateDuringOccupiedRange(DateTime endDate)
        {
            throw new PendingStepException();
        }

        [When(@"I attempt booking during occupied range")]
        public void WhenIAttemptBookingDuringOccupiedRange()
        {
            throw new PendingStepException();
        }

        [Then(@"no booking is made")]
        public void ThenNoBookingIsMade()
        {
            throw new PendingStepException();
        }
    }
}
