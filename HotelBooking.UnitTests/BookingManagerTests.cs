using System;
using HotelBooking.Core;
using HotelBooking.UnitTests.Fakes;
using Xunit;
using System.Linq;


namespace HotelBooking.UnitTests
{
    public class BookingManagerTests
    {
        private IBookingManager bookingManager;
        IRepository<Booking> bookingRepository;

        public BookingManagerTests(){
            DateTime start = DateTime.Today.AddDays(10);
            DateTime end = DateTime.Today.AddDays(20);
            bookingRepository = new FakeBookingRepository(start, end);
            IRepository<Room> roomRepository = new FakeRoomRepository();
            bookingManager = new BookingManager(bookingRepository, roomRepository);
        }

        [Fact]
        public void FindAvailableRoom_StartDateNotInTheFuture_ThrowsArgumentException()
        {
            // Arrange
            DateTime date = DateTime.Today;

            // Act
            Action act = () => bookingManager.FindAvailableRoom(date, date);

            // Assert
            Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void FindAvailableRoom_RoomAvailable_RoomIdNotMinusOne()
        {
            // Arrange
            DateTime date = DateTime.Today.AddDays(1);
            // Act
            int roomId = bookingManager.FindAvailableRoom(date, date);
            // Assert
            Assert.NotEqual(-1, roomId);
        }

        [Fact]
        public void FindAvailableRoom_RoomAvailable_ReturnsAvailableRoom()
        {
            // This test was added to satisfy the following test design
            // principle: "Tests should have strong assertions".

            // Arrange
            DateTime date = DateTime.Today.AddDays(1);
            // Act
            int roomId = bookingManager.FindAvailableRoom(date, date);

            // Assert
            var bookingForReturnedRoomId = bookingRepository.GetAll().Where(
                b => b.RoomId == roomId
                && b.StartDate <= date
                && b.EndDate >= date
                && b.IsActive);

            Assert.Empty(bookingForReturnedRoomId);
        }

        // #1
        [Fact]
        public void FindAvailableRoom_RoomAvailable_StartAndEndInPast()
        {
            // Arrange
            DateTime startDate = DateTime.Today.AddDays(-5);
            DateTime endDate = DateTime.Today.AddDays(-1);

            // Act
            Action act = () => bookingManager.FindAvailableRoom(startDate, endDate);
            // Assert
            Assert.Throws<ArgumentException>(act);
        }

        // #2
        [Fact]
        public void FindAvailableRoom_RoomAvailable_StartInPast()
        {
            // Arrange
            DateTime startDate = DateTime.Today.AddDays(-1);
            DateTime endDate = DateTime.Today.AddDays(5);
            // Act
            Action act = () => bookingManager.FindAvailableRoom(startDate, endDate);
            // Assert
            Assert.Throws<ArgumentException>(act);
        }

        // #3
        [Fact]
        public void FindAvailableRoom_RoomAvailable_NotFullyBooked()
        {
            // Arrange
            DateTime startDate = DateTime.Today.AddDays(1);
            DateTime endDate = DateTime.Today.AddDays(5);
            // Act
            int roomId = bookingManager.FindAvailableRoom(startDate, endDate);
            // Assert
            Assert.NotEqual(-1, roomId);
        }

        // #4
        [Fact]
        public void FindAvailableRoom_RoomAvailable_StartBeforeEndAfterFullyBooked()
        {
            // Arrange
            DateTime startDate = DateTime.Today.AddDays(8);
            DateTime endDate = DateTime.Today.AddDays(22);
            // Act
            int roomId = bookingManager.FindAvailableRoom(startDate, endDate);
            // Assert
            Assert.Equal(-1, roomId);
        }

        // #5
        [Fact]
        public void FindAvailableRoom_RoomAvailable_StartAndEndAfterFullyBooked()
        {
            // Arrange
            DateTime startDate = DateTime.Today.AddDays(22);
            DateTime endDate = DateTime.Today.AddDays(26);
            // Act
            int roomId = bookingManager.FindAvailableRoom(startDate, endDate);
            // Assert
            Assert.NotEqual(-1, roomId);
        }
        //testing
        // #6
        [Fact]
        public void FindAvailableRoom_RoomAvailable_StartBeforeEndDuringFullyBooked()
        {
            // Arrange
            DateTime startDate = DateTime.Today.AddDays(8);
            DateTime endDate = DateTime.Today.AddDays(15);
            // Act
            int roomId = bookingManager.FindAvailableRoom(startDate, endDate);
            // Assert
            Assert.Equal(-1, roomId);
        }

        // #7
        [Fact]
        public void FindAvailableRoom_RoomAvailable_StartAndEndDuringFullyBooked()
        {
            // Arrange
            DateTime startDate = DateTime.Today.AddDays(11);
            DateTime endDate = DateTime.Today.AddDays(15);
            // Act
            int roomId = bookingManager.FindAvailableRoom(startDate, endDate);
            // Assert
            Assert.Equal(-1, roomId);
        }

        // #8
        [Fact]
        public void FindAvailableRoom_RoomAvailable_StartDuringEndAfterFullyBooked()
        {
            // Arrange
            DateTime startDate = DateTime.Today.AddDays(15);
            DateTime endDate = DateTime.Today.AddDays(22);
            // Act
            int roomId = bookingManager.FindAvailableRoom(startDate, endDate);
            // Assert
            Assert.Equal(-1, roomId);
        }

        // Putting a comment here so I have something to commit and check the Azure Pipelines is working

    }
}
