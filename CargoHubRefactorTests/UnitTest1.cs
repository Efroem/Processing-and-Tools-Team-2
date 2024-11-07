// using CargoHubRefactor;


[TestClass]
public class AdminEditsTests
{
    private string filePath = "../../../Room1.json";

    [TestInitialize]
    public void TestInitialize()
    {
        // Stel de omgevingsvariabele in voor de testomgeving
        Environment.SetEnvironmentVariable("IS_TEST_ENVIRONMENT", "true");

    }

    [TestCleanup]
    public void TestCleanup()
    {
        // Verwijder de omgevingsvariabele na de test
        Environment.SetEnvironmentVariable("IS_TEST_ENVIRONMENT", null);

        // Verwijder het testbestand na de test
        // if (File.Exists(filePath))
        // {
        //     File.Delete(filePath);
        // }
    }

    private void SuppressConsoleOutput(Action action)
    {
        var originalOut = Console.Out;
        var originalError = Console.Error;

        try
        {
            using (var writer = new StringWriter())
            {
                Console.SetOut(writer);
                Console.SetError(writer);
                action();
            }
        }
        finally
        {
            Console.SetOut(originalOut);
            Console.SetError(originalError);
        }
    }

    [TestMethod]
    public void EditRoomSize_ShouldUpdateRoomDimensions()
    {
        // Arrange

    }
}
