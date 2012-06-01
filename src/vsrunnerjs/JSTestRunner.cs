using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using FluentWebUITesting;
using NUnit.Framework;
using WatiN.Core;

namespace vsrunnerjs
{
	public interface IJSTestRunner
	{
		IEnumerable<JSTest> RunTests(string jsFilePath, BrowserSetUp browserSetUp);
	}

	public class JSTestRunner : IJSTestRunner
	{
		private readonly List<JSTest> _jSTests = new List<JSTest>();


		public IEnumerable<JSTest> RunTests(string jsFilePath, BrowserSetUp browserSetUp)
		{
			var filesBeingTested =
				new List<string> {"*.htm", "*.html"}.SelectMany(
					filter => Directory.EnumerateFiles(jsFilePath, filter, SearchOption.AllDirectories));

			foreach (string fileBeingTested in filesBeingTested)
			{
				string url = fileBeingTested;
				var actions = UITestRunner.InitializeWorkFlowContainer(
					b => b.ElementsOfType<ListItem>()
					     	.Where(x => x.InnerHtml.StartsWith("<strong>"))
					     	.ForEach(x => GetJSTestSummary(x, url)));

				var list = new List<Action<Browser>>
				           	{
				           		(b => b.GoTo(url)),
				           		(b => b.WaitForComplete()),
				           	};
				if (actions != null)
				{
					list.AddRange(actions);
				}

				var notification = new TestRunner(new BrowserProvider(browserSetUp), browserSetUp).PassesTest(list) ??
				                   new Notification();

				Assert.True(notification.Success, notification.ToString());
			}
			return _jSTests;
		}

		private void GetJSTestSummary(Element item, string fileBeingTested)
		{
			if (item != null)
			{
				var regex = new Regex(@"\([0-9, ]+\)Rerun");
				var testSummary = regex.Split(item.Text);

				Console.WriteLine(fileBeingTested);
				_jSTests.Add(new JSTest
				             	{
				             		FileName = fileBeingTested,
				             		Test = testSummary[0],
				             		Passed = item.ClassName == "pass",
				             		Message = testSummary[1]
				             	});
			}
		}
	}
}