using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Enquete.Business;
using Enquete.DTO;

namespace Enquete.Tests
{
    [TestClass]
    public class PollTest
    {
        private pollBusiness pollBusiness;
        private const long POLL_ID = 26;
        private const short OPTION_ID = 65;

        public PollTest()
        {
            pollBusiness = new pollBusiness();
        }

        [TestMethod]
        public void Poll()
        {
            poll poll = new poll();

            poll.poll_description = $"Poll_{DateTime.Now}";
            poll.options.Add(new optionDTO() { option_description = "Opção 1" });
            poll.options.Add(new optionDTO() { option_description = "Opção 2" });
            poll.options.Add(new optionDTO() { option_description = "Opção 3" });

            var poll_id = pollBusiness.Poll(poll);

            Assert.IsTrue(poll_id > 0);
        }

        [TestMethod]
        public void Get()
        {
            var poll = pollBusiness.Get(new poll() { poll_id = POLL_ID });

            Assert.IsTrue(poll != null);
        }

        [TestMethod]
        public void Vote()
        {
            poll poll = new poll();

            poll.poll_id = POLL_ID;
            poll.options.Add(new optionDTO() { option_id = OPTION_ID });

            pollBusiness.Vote(poll);

            Assert.IsNotNull(poll);
        }

        [TestMethod]
        public void Stats()
        {
            var poll = pollBusiness.Stats(new poll() { poll_id = POLL_ID });

            Assert.IsTrue(poll != null);
        }
    }
}
