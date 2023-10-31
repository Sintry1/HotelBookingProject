using HotelBooking.Core;
using Moq;
using System;
using TechTalk.SpecFlow;

namespace SpecFlow.HotelBooking.StepDefinitions
{
    [Binding]
    public class BookingStepDefinitions
    {

        [Given(@"occupiedRange")]
        public void GivenOccupiedRange()
        {
            throw new PendingStepException();
        }

        [Given(@"I have a booking request with the following details")]
        public void GivenIHaveABookingRequestWithTheFollowingDetails(Table table)
        {
            throw new PendingStepException();
        }

        [When(@"I attempt booking before occupied range")]
        public void WhenIAttemptBookingBeforeOccupiedRange()
        {
            throw new PendingStepException();
        }

        [Then(@"Booking True")]
        public void ThenBookingTrue()
        {
            throw new PendingStepException();
        }

        [When(@"I attempt booking after occupied range")]
        public void WhenIAttemptBookingAfterOccupiedRange()
        {
            throw new PendingStepException();
        }

        [When(@"I attempt booking during occupied range")]
        public void WhenIAttemptBookingDuringOccupiedRange()
        {
            throw new PendingStepException();
        }

        [Then(@"Booking False")]
        public void ThenBookingFalse()
        {
            throw new PendingStepException();
        }
    }
}
