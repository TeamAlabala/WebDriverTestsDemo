using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Diagnostics;

namespace NUnitWebDriverTests
{
    public class CalculatorSeleniumTests
    {
        private WebDriver driver;
        IWebElement textBoxFirstNum;
        IWebElement textBoxSecondNum;
        IWebElement dropDownOperation;
        IWebElement calcBtn;
        IWebElement resetBtn;
        IWebElement divResult;

        [OneTimeSetUp]
        public void OpenBrowserAndNavigate()
        {
            var options = new ChromeOptions();
            if (!Debugger.IsAttached)
                options.AddArguments("--headless", "--window-size=1920,1200");
            this.driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            driver.Url = "https://number-calculator.nakov.repl.co/";

            textBoxFirstNum = driver.FindElement(By.Id("number1"));
            textBoxSecondNum = driver.FindElement(By.Id("number2")); ;
            dropDownOperation = driver.FindElement(By.Id("operation")); ;
            calcBtn = driver.FindElement(By.Id("calcButton")); ;
            resetBtn = driver.FindElement(By.Id("resetButton")); ;
            divResult = driver.FindElement(By.Id("result")); ;

        }

        [OneTimeTearDown]
        public void ShutDown()
        {
            driver.Quit();
        }

        [TestCase("5", "+", "3", "Result: 8")]
        [TestCase("5", "-", "3", "Result: 2")]
        [TestCase("", "/", "3", "Result: invalid input")]
        public void TestCalculatorWebApp(string num1, string op, string num2, string expectedResult)
        {
            //Arrange
            resetBtn.Click();
            if (num1 != "")
                textBoxFirstNum.SendKeys(num1);
            if (op != "")
                dropDownOperation.SendKeys(op);
            if (num2 != "")
                textBoxSecondNum.SendKeys(num2);

            //Act
            calcBtn.Click();

            //Assert
            Assert.AreEqual(expectedResult, divResult.Text);

        }
    }
}