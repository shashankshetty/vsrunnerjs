This is a to run qUnit tests in Visual Studio.

## Add the following test fixture to your project and run qUnit tests.

[TestFixture]
public class When_asked_to_run_QUnit_tests
{
	private readonly IJSTestRunner _testRunner = new JSTestRunner();

	[Test]
	[TestCaseSource("RunQUnitTests")]
	public void Should_run_the_tests_and_display_the_results(JSTest jsTest)
	{
		Assert.True(jsTest.Passed, jsTest.Message);
	}
	
	private IEnumerable<JSTest> RunQUnitTests()
	{
		// <Test Files Directory> is the directory where you have your html files that link to your JS tests like C:\\Tests
		return _testRunner.RunTests("<Test Files Directory>"));
	}
}
