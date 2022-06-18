using Domain.Entities;
using System.Collections.Generic;
using Xunit;

namespace Tests.Domain.Entities;
public class CategoryEntityTest
{
    [Fact(DisplayName = "Should sum entrances values with success")]
    public void Should_Sum_Entrances_Values_With_Success()
    {
        // Arrange
        var category = new Category
        {
            Entrances = new List<Entrance>
            {
                new () { Value = 100 },
                new () { Value = 300 },
                new () { Value = 400 },
                new () { Value = 500 },
                new () { Value = 600 },
            }
        };

        // Act
        var result = category.GetTotalValues();

        // Assert
        Assert.Equal(1900, result);
    }

    [Fact(DisplayName = "Should not sum entrances values with succes when is null")]
    public void Should_Not_Sum_Entrances_Values_With_Success()
    {
        // Arrange
        var category = new Category { };

        // Act
        var result = category.GetTotalValues();

        // Assert
        Assert.Equal(0.0, result);
    }
}
