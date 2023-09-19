using System;
using HotelBooking.Core;
using HotelBooking.UnitTests.Fakes;
using Xunit;

namespace HotelBooking.UnitTests
{
    public class BookingManagerTests
    {
        private IBookingManager bookingManager;

        public BookingManagerTests(){
            DateTime start = DateTime.Today.AddDays(10);
            DateTime end = DateTime.Today.AddDays(20);
            IRepository<Booking> bookingRepository = new FakeBookingRepository(start, end);
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
        public void FindAvailbleRoom_DateOverLapsExistingBooking_RoomIdMinusOne()
        {
            // Arrange
            DateTime date = DateTime.Today.AddDays(8);
            DateTime endDate = date.AddDays(22);
            // Act
            int roomId = bookingManager.FindAvailableRoom(date, endDate);
            // Assert
            Assert.Equal(-1, roomId);

        }

        [Fact]
        public void FindAvailbleRoom_endDateOverlapsFullyBooked_RoomIdMinusOne()
        {
            // Arrange
            DateTime date = DateTime.Today.AddDays(8);
            DateTime endDate = DateTime.Today.AddDays(14);
            // Act
            int roomId = bookingManager.FindAvailableRoom(date, endDate);
            // Assert
            Assert.Equal(-1, roomId);

        }

        [Fact]
        public void FindAvailbleRoom_startDateOverlapsFullyBooked_RoomIdMinusOne()
        {
            // Arrange
            DateTime date = DateTime.Today.AddDays(18);
            DateTime endDate = DateTime.Today.AddDays(22);
            // Act
            int roomId = bookingManager.FindAvailableRoom(date, endDate);
            // Assert
            Assert.Equal(-1, roomId);

        }

        [Fact]
        public void FindAvailbleRoom_RoomAvailable_RoomIdNotMinusOne()
        {
            // Arrange
            DateTime date = DateTime.Today.AddDays(5);
            DateTime endDate = DateTime.Today.AddDays(9);
            // Act
            int roomId = bookingManager.FindAvailableRoom(date, endDate);
            // Assert
            Assert.NotEqual(-1, roomId);

        }
        
        [Fact]
        public void FindAvailbleRoom_RoomAvailableAfterFullyBooked_RoomIdNotMinusOne()
        {
            // Arrange
            DateTime date = DateTime.Today.AddDays(21);
            DateTime endDate = DateTime.Today.AddDays(28);
            // Act
            int roomId = bookingManager.FindAvailableRoom(date, endDate);
            // Assert
            Assert.NotEqual(-1, roomId);
        }

        [Fact]
        public void FindAvailbleRoom_RoomAvailableDuringFullyBooked_ThrowsArgumentExpection()
        {
            // Arrange
            DateTime date = DateTime.Today.AddDays(14);
            DateTime endDate = DateTime.Today.AddDays(18);
            // Act
            int roomId = bookingManager.FindAvailableRoom(date, endDate);
            // Assert
            Assert.Equal(-1, roomId);
        }

        [Fact]
        public void FindAvailbleRoom_EndDateBeforeStartDAte_ThrowsArgumentExpection()
        {
            // Arrange
            DateTime date = DateTime.Today.AddDays(8);
            DateTime endDate = DateTime.Today.AddDays(2);
            // Act
            Action act = () => bookingManager.FindAvailableRoom(date, endDate);
            // Assert
            Assert.Throws<ArgumentException>(act);
        }
    }
}
