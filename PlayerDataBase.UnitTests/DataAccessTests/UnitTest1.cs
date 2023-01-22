using PlayerDataBase.DataAccess;

namespace PlayerDataBase.UnitTests.DataAccess;

public class PlayerTests
{
    [Fact]
    public void Create_ReturnNewPlayer()
    {
        //arrange
        var invalidName = "";
        var invalidLevel = 3;
        //act - assert
        Assert.Throws<ArgumentException>(() => new Player(invalidName,invalidLevel));
    }

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Create_InvalidName_Return(string invalidName)
    {
        //arrange
        var invalidLevel = 3;
        //act - assert 
        Assert.Throws<ArgumentException>(() => new Player(invalidName, invalidLevel));
    }

}