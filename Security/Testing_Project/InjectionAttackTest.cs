// <copyright file="InjectionAttackTest.cs" company="engi">
// injection Attack Testing Class.
// </copyright>
namespace Testing_Project
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using OpenQA.Selenium;
	using OpenQA.Selenium.Chrome;
	using OpenQA.Selenium.IE;
    
	/// <summary>
	/// This class is used to perform the Injection Attacks on the test
	/// </summary>
	[TestClass]
	public class InjectionAttackTest
	{
		/// <summary>
		/// This is the Injection Attack method.
		/// The Test performs an injection attack attempt on the SafeWebForm and then tests if the attack has worked, which it should not
		/// The Test then performs an injection attack attempt on the UnsafeWebForm and then tests if the attack has worked, which it should
		/// </summary>
		[TestMethod]
		public void InjectionAttackMethod()
		{
			ChromeDriver chrome = new ChromeDriver();

			// Test attack on the Safe page, attack should fail
			chrome.Navigate().GoToUrl("http://localhost/Security/SafeWebForm.aspx");
			
			chrome.FindElement(By.XPath("//input[@type = 'text'][@name = 'nameBox']")).SendKeys("Bob', 2); update students set Name='All your base are belong to us';--");
			chrome.FindElement(By.XPath("//input[@type = 'text'][@name = 'gpaBox']")).SendKeys("2");
			chrome.FindElement(By.XPath("//input[@type = 'submit'][@name = 'submit']")).Click();
			
			chrome.Navigate().GoToUrl("http://localhost/Security/SafeRead.aspx");

			foreach (IWebElement iwe in chrome.FindElementsByName("nameBox"))
			{
				Assert.AreEqual("All your base are belong to us", iwe.Text, "Injection attack parameter has been found: prepared statements have failed, this page is not safe vs Injection attacks.");
			}

			// Test attack on the Unsafe page, attack should pass
			chrome.Navigate().GoToUrl("http://localhost/Security/UnsafeWebForm.aspx");

			chrome.FindElement(By.XPath("//input[@type = 'text'][@name = 'nameBox']")).SendKeys("Bob', 2); update students set Name='All your base are belong to us';--");
			chrome.FindElement(By.XPath("//input[@type = 'text'][@name = 'gpaBox']")).SendKeys("2");
			chrome.FindElement(By.XPath("//input[@type = 'submit'][@name = 'submit']")).Click();

			chrome.Navigate().GoToUrl("http://localhost/Security/SafeRead.aspx");

			foreach (IWebElement iwe in chrome.FindElementsByName("nameBox"))
			{
				Assert.AreNotEqual("All your base are belong to us", iwe.Text, "Injection attack parameter has not been found: this page is safe vs Injection attacks and should not be.");
			}
		}
	}
}
