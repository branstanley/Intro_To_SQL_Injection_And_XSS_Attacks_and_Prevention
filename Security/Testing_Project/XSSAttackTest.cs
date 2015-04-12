// <copyright file="XSSAttackTest.cs" company="engi">
// XSS Attack Testing Class.
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
    /// This is the XSS Attack Testing Class.  It performs an attack which checks if our HTML Encoding is working properly.
    /// </summary>
    [TestClass]
    public class XSSAttackTest
    {
        /// <summary>
        /// The XSS Attack method.
        /// This method sends the malicious JavaScript code through the SafeWebForm which will load an image.
        /// The ReadUnsafe file is then checked to make sure that the image has been successfully sent.
        /// The ReadSafe file is then checked to make sure that the image has not been successfully sent.
        /// </summary>
        [TestMethod]
        public void XSSAttackMethod()
        {
            ChromeDriver chrome = new ChromeDriver();

            // Set up the attack
			chrome.Navigate().GoToUrl("http://localhost/Security/SafeWebForm.aspx");
            chrome.FindElement(By.XPath("//input[@type = 'text'][@name = 'nameBox']")).SendKeys("<script>document.getElementsByTagName(\"td\")[0].innerHTML = \"<img src='http://i.imgur.com/s4DdGEx.jpg'>\";</script>");
            chrome.FindElement(By.XPath("//input[@type = 'text'][@name = 'gpaBox']")).SendKeys("2");
            chrome.FindElement(By.XPath("//input[@type = 'submit'][@name = 'submit']")).Click();

            // Check if the ReadUnsafe page is vulnerable to XSS attacks, which it should be
			chrome.Navigate().GoToUrl("http://localhost/Security/UnsafeRead.aspx");

            try
            {
                chrome.FindElementByTagName("img");
            }
            catch (Exception e)
            {
                Assert.Fail("Image tag not found in the ReadUnsafe.aspx\nThis page is supposed to be vulnerable to XSS attacks");
            }

            // Check if the ReadSafe page is vulnerable to XSS attacks, which it should not be
			chrome.Navigate().GoToUrl("http://localhost/Security/SafeRead.aspx");

            try
            {
                chrome.FindElementByTagName("img");
                Assert.Fail("Image tag found in the ReadUSafe.aspx\nThis page is not supposed to be vulnerable to XSS attacks");
            }
            catch (Exception e)
            {
            }
        }
    }
}
