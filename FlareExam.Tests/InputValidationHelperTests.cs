using AutoFixture;
using FlareExam.Models;
using FlareExam.Tasks.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FlareExam.Tests
{
    public class InputValidationHelperTests
    {
        private Fixture _fixture;

        public InputValidationHelperTests()
        {
            _fixture = new Fixture();

        }

        [Fact]
        public void GetValidIntegerInput_()
        {
            // Arrange

            // Act
            GridWorker sut = new GridWorker();
            sut.InitializeGrid();

            // Asset
            Assert.NotNull(sut.Grid);
            Assert.IsType<Grid>(sut.Grid);
        }
    }
}
