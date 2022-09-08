using AutoFixture;
using FlareExam.Models;
using FlareExam.Tasks.Interfaces;
using FlareExam.Workers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FlareExam.Tests
{
    public class RectangleWorkerTests
    {
        private Fixture _fixture;

        public RectangleWorkerTests()
        {
            _fixture = new Fixture();

        }

        [Fact]
        public void AddRectangle_Success()
        {
            // Arrange
            var rectangles = _fixture.CreateMany<Rectangle>(2).ToList();
            var mockRectangle = _fixture.Create<Rectangle>();
            var expected = 3;

            // Act
            RectangleWorker sut = new RectangleWorker();
            sut.Rectangles = rectangles;
            sut.AddRectangle(mockRectangle);

            // Assert
            Assert.Equal(expected, sut.Rectangles.Count);
        }

        [Fact]
        public void RemoveRectangle_Success()
        {
            // Arrange
            var rectangles = _fixture.CreateMany<Rectangle>(2).ToList();
            var mockRectangle = rectangles[1];
            var expected = 1;

            // Act
            RectangleWorker sut = new RectangleWorker();
            sut.Rectangles = rectangles;
            sut.RemoveRectangle(mockRectangle);

            // Assert
            Assert.Equal(expected, sut.Rectangles.Count);
        }

        [Fact]
        public void FindRectangleViaCoordinate_Success()
        {
            // Arrange
            var rectangles = _fixture.CreateMany<Rectangle>(1).ToList();
            rectangles[0].Coordinates = new string[] { "0,0", "0,1", "1,0", "1,1" };

            // Act
            RectangleWorker sut = new RectangleWorker();
            sut.Rectangles = rectangles;
            var actual = sut.FindRectangleViaCoordinate("0,1");

            // Assert
            Assert.IsType<Rectangle>(actual);
            Assert.NotNull(actual);
        }

        [Fact]
        public void FindRectangleViaCoordinate_NoResult()
        {
            // Arrange
            var rectangles = _fixture.CreateMany<Rectangle>(1).ToList();
            rectangles[0].Coordinates = new string[] { "0,0", "0,1", "1,0", "1,1" };

            // Act
            RectangleWorker sut = new RectangleWorker();
            sut.Rectangles = rectangles;
            var actual = sut.FindRectangleViaCoordinate("5,6");

            // Assert
            Assert.Null(actual);
        }

        [Fact]

        public void IsValidRectangle_True_NoOtherRectangles()
        {
            // Arrange
            var mockRectangle = _fixture.Create<Rectangle>();
            mockRectangle.Coordinates = new string[] { "2,2", "3,2", "2,3", "3,3" };

            // Act
            RectangleWorker sut = new RectangleWorker();
            sut.Rectangles = new List<Rectangle>();
            var actual = sut.IsValidRectangle(mockRectangle);

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void IsValidRectangle_False_NotARectangle_NoOtherRectangles()
        {
            // Arrange
            var mockRectangle = _fixture.Create<Rectangle>();
            mockRectangle.Coordinates = new string[] { "2,2", "3,2", "2,3" };

            // Act
            RectangleWorker sut = new RectangleWorker();
            sut.Rectangles = new List<Rectangle>();
            var actual = sut.IsValidRectangle(mockRectangle);

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void IsValidRectangle_True_NoOverlappingRectangle()
        {
            // Arrange
            var mockRectangle = _fixture.Create<Rectangle>();
            mockRectangle.Coordinates = new string[] { "0,0", "1,0", "1,1", "0,1" };

            var mockRectangles = _fixture.CreateMany<Rectangle>(1).ToList();
            mockRectangles[0].Coordinates = new string[] { "2,2", "3,2", "2,3", "3,3" };

            // Act
            RectangleWorker sut = new RectangleWorker();
            sut.Rectangles = mockRectangles;
            var actual = sut.IsValidRectangle(mockRectangle);

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void IsValidRectangle_False_OverlappingRectangle()
        {
            // Arrange
            var mockRectangle = _fixture.Create<Rectangle>();
            mockRectangle.Coordinates = new string[] { "1,1", "2,1", "1,2", "2,2" };

            var mockRectangles = _fixture.CreateMany<Rectangle>(2).ToList();
            mockRectangles[0].Coordinates = new string[] { "2,2", "3,2", "2,3", "3,3" };
            mockRectangles[1].Coordinates = new string[] { "0,0", "1,0", "1,1", "0,1" };

            // Act
            RectangleWorker sut = new RectangleWorker();
            sut.Rectangles = mockRectangles;
            var actual = sut.IsValidRectangle(mockRectangle);

            // Assert
            Assert.False(actual);
        }
    }
}
