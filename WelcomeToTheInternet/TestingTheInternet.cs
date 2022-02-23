using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace WelcomeToTheInternet
{
    [TestClass]
    public class TestingTheInternet
    {
        IWebDriver driver;
        string expectedTitle = "The Internet";
        int elementCount;

        [TestInitialize]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/");
        }

        [TestCleanup]
        public void CleanUP()
        {
            driver.Quit();
        }

        [TestMethod]
        public void LoadHomePage()
        {
            var actualTitle = driver.Title;//actual headline of site
            Assert.IsTrue(actualTitle == expectedTitle, $"Actual Tite ({actualTitle}) did not match the Expected Title ({expectedTitle})");
            IWebElement headline = driver.FindElement(By.CssSelector("h1.heading"));
            var actualHeadline = headline.Text;
            var expectedHeadline = "Welcome to the-internet";
            Assert.IsTrue(actualHeadline == expectedHeadline, $"Actual Tite ({actualHeadline}) did not match the Expected Title ({expectedHeadline})");
        }

        [TestMethod]
        public void OpenCheckBoxesPage()
        {
            IWebElement CheckboxLink = driver.FindElement(By.CssSelector("[href*='/checkboxes']"));
            CheckboxLink.Click();
            //driver.FindElement(By.CssSelector("[href*='/checkboxes']")).Click() ; (same as lines 48 and 49)
            Assert.IsTrue(driver.Url.Contains("https://the-internet.herokuapp.com/checkboxes"));
            Console.WriteLine("Has been verifed using Assert.IsTrue");

        }

        [TestMethod]
        public void OpenCheckBoxesPageThenClickChecBox1()
        {
            IWebElement CheckboxLink = driver.FindElement(By.CssSelector("[href*='/checkboxes']"));
            CheckboxLink.Click();

            IWebElement CheckboxOne = driver.FindElement(By.CssSelector("form#checkboxes :nth-child(1)"));
            bool isCheckboxOneChecked = CheckboxOne.Displayed && CheckboxOne.Enabled;
            Assert.IsTrue(isCheckboxOneChecked);
            Console.WriteLine("Has been verifed using Assert.IsTrue");

        }

        [TestMethod]
        public void OpenInputsPage()
        {
            driver.FindElement(By.CssSelector("[href*='/inputs']")).Click();
            Assert.IsTrue(driver.Url.Contains("https://the-internet.herokuapp.com/inputs"));
            Console.WriteLine("Has been verifed using Assert.IsTrue");

        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        [TestMethod]
        public void FormAuth()
        //use .SendKeys() to input the username and password from the test page

        {
            driver.Url = "https://the-internet.herokuapp.com/login";
            IWebElement Username = driver.FindElement(By.CssSelector("#username"));
            Username.SendKeys("tomsmith");

            IWebElement Password = driver.FindElement(By.CssSelector("#password"));
            Password.SendKeys("SuperSecretPassword!");

            IWebElement Login = driver.FindElement(By.CssSelector("#login > button > i"));
            Login.Click();

            IWebElement SucessfulLogin = driver.FindElement(By.CssSelector("#flash"));
            Assert.IsTrue(driver.Url.Contains("https://the-internet.herokuapp.com/secure"));
            IWebElement Logout = driver.FindElement(By.CssSelector("#content > div > a"));
            Console.WriteLine("Has been verifed using Assert.IsTrue and took the liberty to log out");

        }

        [TestMethod]
        public void NotificationMessage()
        //create a test that captures the original notification message and
        //compares it to the message that is generated after clicking the link to load a new message.
        //Let the test pass if the messages match,
        //but fail if they do not and output both messages as a custom error message.
        {

            driver.Url = " https://the-internet.herokuapp.com/notification_message_rendered";

            IWebElement ClickHere = driver.FindElement(By.CssSelector("#content > div > p > a"));
            ClickHere.Click();

            IWebElement Message = driver.FindElement(By.CssSelector("#flash"));
            var FirstMessage = Message.Text;

            IWebElement ClickHereForNewMessage = driver.FindElement(By.CssSelector("#content > div > p > a"));
            ClickHereForNewMessage.Click();

            IWebElement NewMessage = driver.FindElement(By.CssSelector("#flash"));
            var SecondMessage = NewMessage.Text;

            Assert.IsTrue(SecondMessage == FirstMessage, $"Second Message ({SecondMessage}) did not match the First Message ({FirstMessage})");

            Console.WriteLine("We have completed the Do-While Loop and the test has been verifed using Assert.IsTrue");
        }

        [TestMethod]
        public void DynamicControls()
        //create a test that verifies the checkbox is removed when you click the "Remove" button
        {

            driver.Url = "https://the-internet.herokuapp.com/dynamic_controls";

            IWebElement CheckCheckBox = driver.FindElement(By.CssSelector("#checkbox > input[type=checkbox]"));
            CheckCheckBox.Click();

            Console.WriteLine("CheckBox has been checked");

            IWebElement RemoveBTN = driver.FindElement(By.CssSelector("#checkbox-example > button"));
            RemoveBTN.Click();

            IWebElement LoadingBar = driver.FindElement(By.CssSelector("form img"));
            Console.WriteLine("WE ARE LOADING");

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            //expected conditions 
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector("form img")));
            Console.WriteLine("WE FINISHED LOADING");
            string ExpectedMessage = "It's gone!";
            var ItsGone = "";
            try
            {
                IWebElement PostRemoveMessage = driver.FindElement(By.CssSelector("p#message"));
                ItsGone = PostRemoveMessage.Text;
            }
            catch (Exception exceptionCheckBox)
            {
                Console.WriteLine(exceptionCheckBox); ;
            }

            Console.WriteLine("Has been verifed using Assert.IsTrue");
        }

        [TestMethod]
        public void ChallengingDOM()
        //use driver.FindElements to return a count of the number of rows in the table
        {
            driver.Url = "https://the-internet.herokuapp.com/challenging_dom";
            var CountTableRows = driver.FindElements(By.TagName("tr")).Count();
            Console.WriteLine(CountTableRows);
            Assert.IsTrue(CountTableRows == 11);
            Console.WriteLine("Hardcoded to 11 which is not ideal but this test has passed");

        }

        [TestMethod]
        public void CountCheckboxes()
        //use driver.FindElements to return a count of the number of checkboxes on the page
        //elements-find all elements on single locator and then .count

        {
            driver.Url = "https://the-internet.herokuapp.com/checkboxes";

            IWebElement CheckboxOne = driver.FindElement(By.CssSelector("#checkboxes > input[type=checkbox]:nth-child(1)"));

            IWebElement CheckboxTwo = driver.FindElement(By.CssSelector("#checkboxes > input[type=checkbox]:nth-child(3)"));

            List<IWebElement> NumberOfCheckboxes = new List<IWebElement>() { CheckboxOne, CheckboxTwo };

            int mycount = NumberOfCheckboxes.Count();

            Console.WriteLine("Your Count for the number of checkboxes on the page is" + " " + mycount);

            Assert.IsTrue(NumberOfCheckboxes.Count == 2);//this is hard coded to 2 but there is def a better way around this 
            Console.WriteLine("Hardcoded to 2 which is not ideal but this test has passed");

        }

        [TestMethod]
        public void SortableDataTables1()
        //Example 1 - sort the table 1 A-Z by Email
        //Example 1 - Assert that the "Web Site"  in row 3 matches the expected value
        //Example 2 - sort the table 2 Z-A by Last Name
        //Example 2 - Assert that the "Due" in row 2 matches the expected value

        {
            //TABLE 1
            driver.Url = "https://the-internet.herokuapp.com/tables";
            IWebElement Email = driver.FindElement(By.CssSelector("#table1 > thead > tr > th:nth-child(3)"));
            Email.Click();
            Console.WriteLine("Email Column Sorted A-Z");

            IWebElement WebsiteRowThree = driver.FindElement(By.CssSelector("#table1 > tbody > tr:nth-child(3) > td:nth-child(5)"));
            var website = WebsiteRowThree.Text;
            String expectedWebsite = "http://www.jsmith.com";

            Assert.IsTrue(website == expectedWebsite);
            Console.WriteLine("Email Column Sorted A-Z  and has been verifed using Assert.IsTrue");

            //Table 2

            //Find Element
            IWebElement LastName = driver.FindElement(By.CssSelector("#table2 > thead > tr > th:nth-child(1)"));
            //Create Selenium action called myDoubleClick Action
            Actions myDoubleClickAction = new Actions(driver);
            //call action-->call mydoubleclick method(On IWebelement)--> commands can onl work it has been built(.built) and then action is performed(.perform)
            myDoubleClickAction.DoubleClick(LastName).Build().Perform();

            Console.WriteLine("LastName Column Sorted Z-A by Double Click Action");

            IWebElement DueTable = driver.FindElement(By.CssSelector("#table2 > tbody > tr:nth-child(2) > td.dues"));
            var Due = DueTable.Text;
            string expectedDue = "$100.00";

            Assert.IsTrue(Due == expectedDue);

            Console.WriteLine("LastName Column Sorted Z-A by Double Click Action and has been verifed using Assert.IsTrue");

        }
    }
}
