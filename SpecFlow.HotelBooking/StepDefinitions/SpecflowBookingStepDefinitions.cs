using System;
using TechTalk.SpecFlow;

namespace HotelBooking;
{
    [Binding]
    public class SpecflowBookingStepDefinitions
    {
        [Given(@"occupiedRange")]
        public void GivenOccupiedRange()
        {
            throw new PendingStepException();
        }

        [Given(@"I have input a startDate")]
        public void GivenIHaveInputAStartDate()
        {
            throw new PendingStepException();
        }

        [Given(@"I have input an endDate")]
        public void GivenIHaveInputAnEndDate()
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

        [Given(@"I have input a startDate during Occupied Range")]
        public void GivenIHaveInputAStartDateDuringOccupiedRange()
        {
            throw new PendingStepException();
        }

        [Given(@"I have input a endDate during Occupied Range")]
        public void GivenIHaveInputAEndDateDuringOccupiedRange()
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

        [Given(@"I have input a startDate before Occupied Range")]
        public void GivenIHaveInputAStartDateBeforeOccupiedRange()
        {
            throw new PendingStepException();
        }
    }
}
