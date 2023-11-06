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
        // Moq starts here
        // Moq allows us to isolate test dependencies by creating mock implementations of them so that we can focus on testing behavour of the code under test without relying on the repos themselves
        public void CreateBooking_CallOnce_And_ReturnTrue()
        {
            // Arrange
            var rooms = new List<Room>{new Room { Id = 1, Description = "single room" }, new Room { Id = 2, Description = "double room" }};
            var bookingManager = new BookingManager(bookingRepositoryMock.Object, roomRepositoryMock.Object);
            var booking = new Booking { StartDate = DateTime.Today.AddDays(2), EndDate = DateTime.Now.AddDays(4) };

            // Set up an expectation for the Add method of bookingRepositoryMock
            roomRepositoryMock.Setup(repo => repo.GetAll()).Returns(rooms);
            bookingRepositoryMock.Setup(repo => repo.Add(It.IsAny<Booking>()));

            // Act
            // Will return true if the booking is created, false otherwise
            bool result = bookingManager.CreateBooking(booking);

            // Assert
            // Verifies that the Add method is called once and only once
            bookingRepositoryMock.Verify(repo => repo.Add(booking), Times.Once());
            Assert.True(result);
        }

        // Moq actually makes unit testing more simple, by making things more readable and maintainable
        // Moq also avoids errors and unintended side effects because it doesn't use the real dependencies


        [Fact]
        public void GetFullyOccupiedDates_Return5FullyOccupiedDates()
        {
            // Arrange
            var bookingManager = new BookingManager(bookingRepositoryMock.Object, roomRepositoryMock.Object);
            bookingRepositoryMock.Setup(repo => repo.Add(It.IsAny<Booking>()));

            // Set up the booking repository mock to return bookings when GetAll is called
            var bookings = new List<Booking>
            {
                new Booking { Id = 1, StartDate = DateTime.Today.AddDays(1), EndDate = DateTime.Today.AddDays(4), IsActive = true, RoomId = 1, CustomerId = 1 },
                new Booking { Id = 2, StartDate = DateTime.Today.AddDays(1), EndDate = DateTime.Now.AddDays(4), IsActive = true, RoomId = 2, CustomerId = 2 },
                new Booking { Id = 1, StartDate = DateTime.Today.AddDays(5), EndDate = DateTime.Now.AddDays(7), IsActive = true, RoomId = 1, CustomerId = 1 },
                new Booking { Id = 2, StartDate = DateTime.Today.AddDays(5), EndDate = DateTime.Now.AddDays(9), IsActive = true, RoomId = 2, CustomerId = 2 },
            };
            bookingRepositoryMock.Setup(repo => repo.GetAll()).Returns(bookings);

            // Set up the room repository mock to return rooms when GetAll is called
            var rooms = new List<Room>
            {
                new Room { Id = 1, Description = "Single Room" },
                new Room { Id = 2, Description = "Double Room" },
            };
            roomRepositoryMock.Setup(repo => repo.GetAll()).Returns(rooms.AsQueryable());

            // Act
            var fullyOccupiedDates = bookingManager.GetFullyOccupiedDates(DateTime.Now, DateTime.Now.AddDays(10));

            // Assert
            //Asserts that it's a collections of dates that matches the dates we added to the booking
            Assert.Collection(fullyOccupiedDates,
                date => Assert.Equal(DateTime.Today.AddDays(1).Date, date.Date),
                date => Assert.Equal(DateTime.Today.AddDays(2).Date, date.Date),
                date => Assert.Equal(DateTime.Today.AddDays(3).Date, date.Date),
                date => Assert.Equal(DateTime.Today.AddDays(5).Date, date.Date),
                date => Assert.Equal(DateTime.Today.AddDays(6).Date, date.Date) 
            );
            //it only returns 5 dates even tho it looks like there should be 7, due to how bookings work
            //books don't count the day of the END date, so it's technically only days 1-3 and 5-6
            //Today + 7 and 8 aren't counted, as it's only one, of the two rooms, that is booked
        }

        [Fact]
        public void FindAvailableRooms_returnRoomIdOfFirstAvailableRoom()
        {
            // Arrange
            var bookingManager = new BookingManager(bookingRepositoryMock.Object, roomRepositoryMock.Object);
            bookingRepositoryMock.Setup(repo => repo.Add(It.IsAny<Booking>()));

            // Set up the booking repository mock to return bookings when GetAll is called
            var bookings = new List<Booking>
            {
                new Booking { Id = 1, StartDate = DateTime.Today.AddDays(1), EndDate = DateTime.Today.AddDays(4), IsActive = true, RoomId = 1, CustomerId = 1 },
            };
            bookingRepositoryMock.Setup(repo => repo.GetAll()).Returns(bookings);

            // Set up the room repository mock to return rooms when GetAll is called
            var rooms = new List<Room>
            {
                new Room { Id = 1, Description = "Single Room" },
                new Room { Id = 2, Description = "Double Room" },
                new Room { Id = 3, Description = "Double Room" },
            };
            roomRepositoryMock.Setup(repo => repo.GetAll()).Returns(rooms.AsQueryable());

            // Act
            var availableRooms = bookingManager.FindAvailableRoom(DateTime.Today.AddDays(2), DateTime.Today.AddDays(10));

            // Assert
            Assert.Equal(2, availableRooms);
            //This is slightly weird, as you would expect a list of rooms or something, since there are two available rooms in the mock repository
            //But GetAvailableRooms only returns the first available room that it finds
            // it's expected based on the code, but seems like a major oversight for the hotel and customers
        }

            [Fact]
        public void FindAvailableRoom_RoomAvailable_StartDuringEndAfterFullyBooked2()
        {
            // Arrange
            DateTime startDate = DateTime.Today.AddDays(15);  // Set a start date 15 days in the future.
            DateTime endDate = DateTime.Today.AddDays(22);   // Set an end date 22 days in the future.

            var bookingManager = new BookingManager(bookingRepositoryMock.Object, roomRepositoryMock.Object);
            bookingRepositoryMock.Setup(repo => repo.Add(It.IsAny<Booking>()));

            // Mock the behavior of bookingRepository to return a list of fully booked rooms during the specified date range
            bookingRepositoryMock.Setup(repo => repo.GetAll()).Returns(new List<Booking>
            {
                // Include bookings that fully occupy the specified date range
                new Booking { Id = 1, StartDate = DateTime.Today.AddDays(10), EndDate = DateTime.Today.AddDays(20), IsActive = true, RoomId = 1, CustomerId = 1 },
                new Booking { Id = 2, StartDate = DateTime.Today.AddDays(12), EndDate = DateTime.Today.AddDays(18), IsActive = true, RoomId = 2, CustomerId = 2 },
            });

            // Mock the behavior of roomRepository to return a list of all available rooms
            roomRepositoryMock.Setup(repo => repo.GetAll()).Returns(new List<Room>
            {
                new Room { Id = 1, Description = "Single Room" },
                new Room { Id = 2, Description = "Double Room" },
                new Room { Id = 3, Description = "Double Room" },
            });

            // Act: Call the FindAvailableRoom method with a start date within a fully booked date range
            // and an end date in the future, and retrieve the room ID assigned to the available room (if any).
            int roomId = bookingManager.FindAvailableRoom(startDate, endDate);

            // Assert
            // We expect that -1 will be returned, indicating that no available rooms exist within the specified date range.
            Assert.NotEqual(-1, roomId);
        }
    }
}
