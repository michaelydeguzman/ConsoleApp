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
    public class GridWorkerTests
    { 
        private Fixture _fixture;

        public GridWorkerTests()
        {
            _fixture = new Fixture();

        }

        [Fact]
        public void InitializeGrid_Success()
        {
            // Arrange

            // Act
            GridWorker sut = new GridWorker();
            sut.InitializeGrid();

            // Asset
            Assert.NotNull(sut.Grid);
            Assert.IsType<Grid>(sut.Grid);
        }


        [Fact]
        public void SetGridWidth_Success()
        {
            // Arrange
            var expectedWidth = 5;

            // Act
            GridWorker sut = new GridWorker();
            sut.InitializeGrid();
            sut.SetGridWidth(expectedWidth);

            // Assert
            Assert.Equal(expectedWidth, sut.Grid.Width);
        }

        [Fact]
        public void SetGridHeight_Success()
        {
            // Arrange
            var expectedHeight = 5;

            // Act
            GridWorker sut = new GridWorker();
            sut.InitializeGrid();
            sut.SetGridHeight(expectedHeight);

            // Assert
            Assert.Equal(expectedHeight, sut.Grid.Height);
        }

        [Fact]
        public void IsRectangleInsideGrid_True()
        {
            // Arrange
            var mockRectangle = _fixture.Create<Rectangle>();
            mockRectangle.Name = "Test 1";
            mockRectangle.Coordinates = new string[] { "0,0", "0,1", "1,0", "1,1" };

            var mockGrid = _fixture.Create<Grid>();
            mockGrid.Width = 5;
            mockGrid.Height = 5;

            // Act
            GridWorker sut = new GridWorker();
            sut.Grid = mockGrid;
 
            var actual = sut.IsRectangleInsideGrid(mockRectangle);

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void IsRectangleInsideGrid_False()
        {
            // Arrange
            var mockRectangle = _fixture.Create<Rectangle>();
            mockRectangle.Name = "Test 1";
            mockRectangle.Coordinates = new string[] { "5,5", "5,6", "6,5", "6,6" };

            var mockGrid = _fixture.Create<Grid>();
            mockGrid.Width = 5;
            mockGrid.Height = 5;

            // Act
            GridWorker sut = new GridWorker();
            sut.Grid = mockGrid;

            var actual = sut.IsRectangleInsideGrid(mockRectangle);

            // Assert
            Assert.False(actual);
        }
    }
}
