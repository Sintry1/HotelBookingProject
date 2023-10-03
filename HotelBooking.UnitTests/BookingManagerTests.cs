using System;
using HotelBooking.Core;
using HotelBooking.UnitTests.Fakes;
using Xunit;
using System.Linq;
using Moq;
using System.Collections.Generic;



namespace HotelBooking.UnitTests
{
    public class BookingManagerTests
    {
        private IBookingManager bookingManager;
        IRepository<Booking> bookingRepository;
        private Mock<IRepository<Booking>> bookingRepositoryMock;
        private Mock<IRepository<Room>> roomRepositoryMock;

        public BookingManagerTests(){
            DateTime start = DateTime.Today.AddDays(10);
            DateTime end = DateTime.Today.AddDays(20);
            bookingRepository = new FakeBookingRepository(start, end);
            IRepository<Room> roomRepository = new FakeRoomRepository();
            bookingManager = new BookingManager(bookingRepository, roomRepository);
            bookingRepositoryMock = new Mock<IRepository<Booking>>();
            roomRepositoryMock = new Mock<IRepository<Room>>();
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

        // #1: This unit test is checking the behavior of the FindAvailableRoom method
        // when the start date is in the past and the end date is also in the past.
        [Fact]
        public void FindAvailableRoom_RoomAvailable_StartAndEndInPast()
        {
            // Arrange
            DateTime startDate = DateTime.Today.AddDays(-5);  // Set a start date 5 days in the past.
            DateTime endDate = DateTime.Today.AddDays(-1);  // Set an end date 1 day in the past.

            // Act: Call the FindAvailableRoom method with the specified start and end dates
            // and capture any exception that might be thrown during this action.
            Action act = () => bookingManager.FindAvailableRoom(startDate, endDate);

            // Assert
            // We expect that calling FindAvailableRoom with invalid input dates will throw an ArgumentException.
            Assert.Throws<ArgumentException>(act);
        }

        // #2: This unit test is checking the behavior of the FindAvailableRoom method
        // when the start date is in the past, but the end date is in the future.
        [Fact]
        public void FindAvailableRoom_RoomAvailable_StartInPast()
        {
            // Arrange
            DateTime startDate = DateTime.Today.AddDays(-1); // Set a start date 1 day in the past.
            DateTime endDate = DateTime.Today.AddDays(5);    // Set an end date 5 days in the future.

            // Act: Call the FindAvailableRoom method with a start date in the past and an end date in the future,
            // and capture any exception that might be thrown during this action.
            Action act = () => bookingManager.FindAvailableRoom(startDate, endDate);

            // Assert
            // We expect that calling FindAvailableRoom with an invalid start date will throw an ArgumentException.
            Assert.Throws<ArgumentException>(act);
        }

        // #3: This unit test is checking the behavior of the FindAvailableRoom method
        // when there is a room available that is not fully booked within the specified date range.
        [Fact]
        public void FindAvailableRoom_RoomAvailable_NotFullyBooked()
        {
            // Arrange
            DateTime startDate = DateTime.Today.AddDays(1);  // Set a start date 1 day in the future.
            DateTime endDate = DateTime.Today.AddDays(5);   // Set an end date 5 days in the future.

            // Act: Call the FindAvailableRoom method with a start date in the future and an end date in the future,
            // and retrieve the room ID assigned to the available room (if any).
            int roomId = bookingManager.FindAvailableRoom(startDate, endDate);

            // Assert
            // We expect that a room ID other than -1 will be returned,
            // indicating that there is an available room for the specified date range
            Assert.NotEqual(-1, roomId);
        }

        // #4: This unit test is checking the behavior of the FindAvailableRoom method
        // when the start date is before the end date, but all rooms are fully booked within the specified date range.
        [Fact]
        public void FindAvailableRoom_RoomAvailable_StartBeforeEndAfterFullyBooked()
        {
            // Arrange
            DateTime startDate = DateTime.Today.AddDays(8);  // Set a start date 8 days in the future.
            DateTime endDate = DateTime.Today.AddDays(22);  // Set an end date 22 days in the future.

            // Act: Call the FindAvailableRoom method with a start date in the future and an end date further in the future,
            // and retrieve the room ID assigned to the available room (if any).
            int roomId = bookingManager.FindAvailableRoom(startDate, endDate);

            // Assert
           // We expect that -1 will be returned, indicating that no available rooms exist within the specified date range.
            Assert.Equal(-1, roomId);
        }

        // #5: This unit test is checking the behavior of the FindAvailableRoom method
        // when both the start date and end date are after the specified date range is fully booked.
        [Fact]
        public void FindAvailableRoom_RoomAvailable_StartAndEndAfterFullyBooked()
        {
            // Arrange
            DateTime startDate = DateTime.Today.AddDays(22);  // Set a start date 22 days in the future.
            DateTime endDate = DateTime.Today.AddDays(26);   // Set an end date 26 days in the future.

            // Act: Call the FindAvailableRoom method with both a start date and an end date in the future,
            // and retrieve the room ID assigned to the available room (if any).
            int roomId = bookingManager.FindAvailableRoom(startDate, endDate);

            // Assert
            // We expect that a room ID other than -1 will be returned,
            // indicating that there is an available room for the specified date range.
            Assert.NotEqual(-1, roomId);
        }

        // #6: This unit test is checking the behavior of the FindAvailableRoom method
        // when the start date is before the end date, but all rooms are fully booked during the specified date range.
        [Fact]
        public void FindAvailableRoom_RoomAvailable_StartBeforeEndDuringFullyBooked()
        {
            // Arrange
            DateTime startDate = DateTime.Today.AddDays(8); // Set a start date 8 days in the future.
            DateTime endDate = DateTime.Today.AddDays(15);  // Set an end date 15 days in the future.

            // Act: Call the FindAvailableRoom method with a start date in the future and an end date within the future,
            // and retrieve the room ID assigned to the available room (if any).
            int roomId = bookingManager.FindAvailableRoom(startDate, endDate);

            // Assert
            // We expect that -1 will be returned, indicating that no available rooms exist within the specified date range.
            Assert.Equal(-1, roomId);
        }

        // #7: This unit test is checking the behavior of the FindAvailableRoom method
        // when both the start date and end date fall within a specified date range that is fully booked.
        [Fact]
        public void FindAvailableRoom_RoomAvailable_StartAndEndDuringFullyBooked()
        {
            // Arrange
            DateTime startDate = DateTime.Today.AddDays(11);  // Set a start date 11 days in the future.
            DateTime endDate = DateTime.Today.AddDays(15);   // Set an end date 15 days in the future.

            // Act: Call the FindAvailableRoom method with both a start date and an end date in the future,
            // and retrieve the room ID assigned to the available room (if any).
            int roomId = bookingManager.FindAvailableRoom(startDate, endDate);

            // Assert
            // We expect that -1 will be returned, indicating that no available rooms exist within the specified date range.
            Assert.Equal(-1, roomId);
        }

        // #8: This unit test is checking the behavior of the FindAvailableRoom method
        // when the start date falls within a specified date range that is fully booked,
        // but the end date is after the specified date range.
        [Fact]
        public void FindAvailableRoom_RoomAvailable_StartDuringEndAfterFullyBooked()
        {
            // Arrange
            DateTime startDate = DateTime.Today.AddDays(15);  // Set a start date 15 days in the future.
            DateTime endDate = DateTime.Today.AddDays(22);   // Set an end date 22 days in the future.

            // Act: Call the FindAvailableRoom method with a start date within a fully booked date range
            // and an end date in the future, and retrieve the room ID assigned to the available room (if any).
            int roomId = bookingManager.FindAvailableRoom(startDate, endDate);

            // Assert
            // We expect that -1 will be returned, indicating that no available rooms exist within the specified date range.
            Assert.Equal(-1, roomId);
        }


        [Fact]
        public void CreateBooking_CallOnce_ReturnTrue()
        {
            // Arrange
            var rooms = new List<Room>{new Room { Id = 1, Description = "single room" }, new Room { Id = 2, Description = "double room" }, new Room { Id = 3, Description = "presidential suite" },};
            var bookingManager = new BookingManager(bookingRepositoryMock.Object, roomRepositoryMock.Object);
            var booking = new Booking { StartDate = DateTime.Today.AddDays(2), EndDate = DateTime.Now.AddDays(4) };

            // Set up an expectation for the Add method of bookingRepositoryMock
            roomRepositoryMock.Setup(repo => repo.GetAll()).Returns(rooms);
            bookingRepositoryMock.Setup(repo => repo.Add(It.IsAny<Booking>()));

            // Act
            bool result = bookingManager.CreateBooking(booking);

            // Assert
            // You can assert that the Add method was called with the expected parameters
            bookingRepositoryMock.Verify(repo => repo.Add(booking), Times.Once());
            Assert.True(result);
        }


    }

}
