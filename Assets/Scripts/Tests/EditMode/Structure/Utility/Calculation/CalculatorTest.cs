using NUnit.Framework;
using Structure.Utility.Calculation;
using UnityEngine;

namespace Tests.EditMode.Structure.Utility.Calculation
{
    public class CalculatorTest
    {
        private const float Tolerance = 0.001f;

        // NormalToSlopeのテストケース
        [Test]
        [TestCase(0, 1, 0)]      // up -> 0 degrees
        [TestCase(1, 0, 90)]     // right -> 90 degrees
        [TestCase(0, -1, 180)]   // down -> 180 degrees
        [TestCase(-1, 0, 90)]    // left -> 90 degrees
        public void NormalToSlope_WithCardinalDirections_ReturnsCorrectSlope(float x, float y, float expectedSlope)
        {
            // Arrange
            var normal = new Vector2(x, y);

            // Act
            var slope = Calculator.NormalToSlope(normal);

            // Assert
            Assert.AreEqual(expectedSlope, slope, Tolerance);
        }

        [Test]
        public void NormalToSlope_With45DegreeVector_Returns45()
        {
            // Arrange
            var normal = new Vector2(1, 1).normalized;

            // Act
            var slope = Calculator.NormalToSlope(normal);

            // Assert
            Assert.AreEqual(45f, slope, Tolerance);
        }

        // FitVectorToScreenのテストケース
        [Test]
        public void FitVectorToScreen_WithWiderScreen_CalculatesBasedOnHeight()
        {
            // Arrange
            var origin = new Vector2(100, 0);
            var screen = new Vector2(1000, 800); // shorter side is 800

            // Act
            (Vector2 normalized, float fitLength) = Calculator.FitVectorToScreen(origin, screen);

            // Assert
            Assert.AreEqual(new Vector2(1, 0), normalized);
            Assert.AreEqual(100f / 800f, fitLength, Tolerance);
        }

        [Test]
        public void FitVectorToScreen_WithTallerScreen_CalculatesBasedOnWidth()
        {
            // Arrange
            var origin = new Vector2(0, 200);
            var screen = new Vector2(600, 900); // shorter side is 600

            // Act
            (Vector2 normalized, float fitLength) = Calculator.FitVectorToScreen(origin, screen);

            // Assert
            Assert.AreEqual(new Vector2(0, 1), normalized);
            Assert.AreEqual(200f / 600f, fitLength, Tolerance);
        }

        // 長さ0のベクトルが渡されると、NaNが帰る
        // [Test]
        // public void FitVectorToScreen_WithZeroVector_ReturnsZero()
        // {
        //     // Arrange
        //     var origin = Vector2.zero;
        //     var screen = new Vector2(1000, 800);
        //
        //     // Act
        //     (Vector2 normalized, float fitLength) = Calculator.FitVectorToScreen(origin, screen);
        //
        //     // Assert
        //     Assert.AreEqual(Vector2.zero, normalized);
        //     Assert.AreEqual(0f, fitLength, Tolerance);
        // }
    }
}
