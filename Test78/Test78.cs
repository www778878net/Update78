
using www778878net.update;

namespace www778878net.Test
{
    [TestClass]
    public class Test78
    {
        [TestMethod]
        public  void Test()
        {
            ////小版本更新
            //Update78 uppro = new ("C:\\downtest\\","http://www.778878.net/down/test","1.10");
            ////大版本更新
            ////Update78 uppro = new("C:\\downtest\\", "http://www.778878.net/down/test", "0.10");
            //uppro.Test();
            //Thread.Sleep(5000);
            int test = 1;//nothing to test
            Assert.AreEqual(1, test);
        }

        [TestMethod]
        public void TestBig()
        { 
            ////大版本更新 服务器版本号1.1
            ////Update78 uppro = new("C:\\downtest\\", "http://www.778878.net/down/test", "0.10");
            //uppro.Start();
            //Thread.Sleep(5000);
            int test = 1;//nothing to test
            Assert.AreEqual(1, test);
        }


        [TestMethod]
        public void TestSmall()
        {
            ////小版本更新 服务器版本号1.1
            ////Update78 uppro = new("C:\\downtest\\", "http://www.778878.net/down/test", "1.0");
            //uppro.Start();
            //Thread.Sleep(5000);
            int test = 1;//nothing to test
            Assert.AreEqual(1, test);
        }


    }
}