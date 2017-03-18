using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RBSector.Entry.Entry;
using RBSector.DataBase.Models;
using RBSector.Entry.Tools;
using System.Collections.Generic;
using OpenQA.Selenium.Remote;

namespace RBSectorUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        private Tabs Tab;
        UniversalCRUD<Tabs> crud_tabs;
        public UnitTest1()
        {
            Tab = new Tabs();
            Tab.TbName = "TabsTest";
            crud_tabs = new UniversalCRUD<Tabs>();
        }
        [TestMethod]
        public void SaveOrUpdateTab()
        {
            bool result = false;
            result = crud_tabs.isExist(Tab);
            if (!result)
            {
                result = crud_tabs.SaveOrUpdate(Tab);
                Assert.IsTrue(result, "Should be true!");
            }
        }
        [TestMethod]
        public void IsExist()
        {
            bool result = false;
            result = crud_tabs.isExist(Tab);
            Assert.IsTrue(result, "Should be true!");
        }
        [TestMethod]
        public void GetObjName()
        {
            bool result = false;
            object obj = null;
            obj = crud_tabs.GetObj_Name(Tab.TbName);
            result = obj != null;
            Assert.IsNotNull(obj, "Should not null!");
            Assert.IsTrue(result, "Should be true!");
        }
        [TestMethod]
        public void DeleteTab()
        {
            bool result = false;
            object obj = null;

            obj = crud_tabs.GetObj_Name(Tab.TbName);
            result = obj != null;
            Assert.IsNotNull(obj, "Should not null!");
            Assert.IsTrue(result, "Should be true!");
            result = crud_tabs.Delete<Tabs>((obj as Tabs).RECID);
            Assert.IsTrue(result, "Should be true!");
        }
        [TestMethod]
        public void JSonTest()
        {
            List<Tabs> tab = new List<Tabs>();
            tab.Add(new Tabs() { TbName = "TabsTest1" });
            tab.Add(new Tabs() { TbName = "TabsTest2" });
            tab.Add(new Tabs() { TbName = "TabsTest3" });
            Assert.IsNotNull(tab, "Should not null!");
            string json = JsonTools.Serialize(tab);
            Assert.AreNotEqual(string.Empty, json);
            List<Tabs> newtab = JsonTools.Deserelize(json);
            Assert.AreEqual(tab.Count, newtab.Count);

        }
        [TestMethod]
        public void SeleniumTest()
        {
            const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723";
            RemoteWebDriver CalculatorSession;
            RemoteWebElement CalculatorResult;

            var appCapabilities = new DesiredCapabilities();
            appCapabilities.SetCapability("app", @"D:\DYPLOM\GitHub\trunk\RBSector\RBSector\bin\x86\Debug\AppX\RBSector.exe");
            CalculatorSession = new RemoteWebDriver(new Uri(WindowsApplicationDriverUrl), appCapabilities);
            Assert.IsNotNull(CalculatorSession);
            CalculatorSession.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(2));

            /*CalculatorSession.FindElementByName("Clear").Click();
            CalculatorSession.FindElementByName("Seven").Click();
            CalculatorResult = CalculatorSession.FindElementByName("Display is 7 ") as RemoteWebElement;
            Assert.IsNotNull(CalculatorResult);
            CalculatorSession.FindElementByName("Clear").Click();

            CalculatorSession.FindElementByName("One").Click();
            CalculatorSession.FindElementByName("Plus").Click();
            CalculatorSession.FindElementByName("Seven").Click();
            CalculatorSession.FindElementByName("Equals").Click();
            Assert.AreEqual("Display is 8 ", CalculatorResult.Text);

            CalculatorResult = null;
            CalculatorSession.Dispose();
            CalculatorSession = null;*/
        }
    }
}
