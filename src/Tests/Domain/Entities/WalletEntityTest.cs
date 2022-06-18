using Domain.Entities;
using System;
using Xunit;

namespace Tests.Domain.Entities;
public class WalletEntityTest
{
    [Theory(DisplayName = "Should update wallet currentValue with success")]
    [InlineData(1, 1100)]
    [InlineData(2, 900)]
    public void Should_Update_Wallet_CurrentValue_With_Success(int type, double result)
    {
        // Arrange
        var wallet = new Wallet
        {
            CurrentValue = 1000
        };

        // Act
        wallet.UpdateValue(type, 100.0);

        // Assert
        Assert.Equal(result, wallet.CurrentValue);
    }

    [Fact(DisplayName = "Should not update wallet currentValue with success")]
    public void Should_Not_Update_Wallet_CurrentValue_With_Success()
    {
        // Arrange
        var wallet = new Wallet
        {
            CurrentValue = 1000
        };

        // Act
        Assert.Throws<Exception>(
            () => wallet.UpdateValue(2, 2000.0)
        ).Message.Equals("Insuficient founds");

        Assert.Throws<ArgumentException>(
            () => wallet.UpdateValue(3, 2000.0)
        ).Message.Equals("Type not found");
    }    
}
