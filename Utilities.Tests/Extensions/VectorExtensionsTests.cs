namespace Utilities.Tests.Extensions;

public class VectorExtensionsTests
{
    [Test]
    public void Copy_ShouldReturnCopyOfVector()
    {
        var vector = new double[] { 0, 1, 2, 3 };

        var vectorCopy = vector.Copy();

        vectorCopy.Should().BeEquivalentTo(vector);
        vectorCopy.Should().NotBeSameAs(vector);
    }

    [Test]
    public void OuterProduct_ShouldCalculateOuterProduct()
    {
        var lhs = new double[] { -2, -1, 0, 1, 2 };
        var rhs = new double[] { 1, 2, 3 };

        var result = new double[,]
        {
            { -2, -4, -6 },
            { -1, -2, -3 },
            { 0, 0, 0 },
            { 1, 2, 3 },
            { 2, 4, 6 },
        };

        var actualResult = lhs.OuterProduct(rhs);

        actualResult.Should().BeEquivalentTo(result);
    }

    [Test]
    public void Dot_ShouldThrowArgumentException_WhenDimensionsDoNotMatch()
    {
        var lhs = new double[] { 1, 2, 3 };
        var rhs = new double[] { 1, 2, 3, 4 };

        var func = () => lhs.Dot(rhs);

        func.Should().Throw<ArgumentException>()
            .WithMessage("Dot product arguments must have same dimension");
    }

    [Test]
    public void Dot_ShouldCalculateDotProduct()
    {
        var lhs = new double[] { 1, 2, 3 };
        var rhs = new double[] { 4, 5, 6 };

        var actualResult = lhs.Dot(rhs);

        actualResult.Should().Be(32);
    }

    [Test]
    public void Magnitude_ShouldCalculateMagnitude()
    {
        var vector = new double[] { -3, 4 };

        var actualResult = vector.Magnitude();

        actualResult.Should().BeApproximately(5.0, 0.0001);
    }

    [Test]
    public void Scale_ShouldCalculateScale()
    {
        var vector = new double[] { -1, 0, 1 };
        var factor = 2;

        var result = new double[] { -2, 0, 2 };

        var actualResult = vector.Scale(factor);

        actualResult.Should().BeEquivalentTo(result);
    }

    [Test]
    public void ToColumnVector_ShouldReturnColumnVector()
    {
        var vector = new double[] { 1, 2, 3, 4 };
        var result = new double[,] { { 1 }, { 2 }, { 3 }, { 4 } };

        var actualResult = vector.ToColumnVector();

        actualResult.Should().BeEquivalentTo(result);
    }

    [Test]
    public void ToRowVector_ShouldThrowInvalidOperationException_WhenSourceIsNotAColumnVector()
    {
        var source = new double[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } };

        var func = () => source.ToRowVector();

        func.Should().Throw<InvalidOperationException>()
            .WithMessage("The column vector should have only 1 element in width.");
    }

    [Test]
    public void ToRowVector_ShouldReturnRowVector()
    {
        var source = new double[,] { { 1 }, { 2 }, { 3 }, { 4 } };
        var result = new double[] { 1, 2, 3, 4 };

        var actualResult = source.ToRowVector();

        actualResult.Should().BeEquivalentTo(result);
    }

    [Test]
    public void ToDiagonalMatrix_ShouldReturnDiagonalMatrix()
    {
        var source = new double[] { 1, 2, 3, 4 };
        var result = new double[,]
        {
            { 1, 0, 0, 0 },
            { 0, 2, 0, 0 },
            { 0, 0, 3, 0 },
            { 0, 0, 0, 4 },
        };

        var actualResult = source.ToDiagonalMatrix();

        actualResult.Should().BeEquivalentTo(result);
    }
}
