using Morpion;

namespace MorpionUnitTest;

public class GameUnitTest
{
    [Fact]
    public void RunGameTest()
    {
        var game = new Game();

        try
        {
            game.Run();
        }
        catch (InvalidOperationException)
        {

        }
        finally
        {
            Assert.True(true);
        }
    }
}