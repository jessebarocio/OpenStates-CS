using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenStatesApi.Tests
{
    [TestFixture(Category="Integration Tests")]
    public class OpenStatesClientTests
    {
        [Test]
        public async void GetLegislator_ReturnsCorrectData()
        {
            string apiKey = File.ReadAllText("apikey.txt");
            using (var client = new OpenStatesClient(apiKey))
            {
                var legislator = await client.GetLegislator("UTL000064");
                Assert.AreEqual("UTL000064", legislator.LegislatorId);
                Assert.AreEqual(State.UT, legislator.State);
                Assert.AreEqual(true, legislator.Active);
                Assert.AreEqual(Chamber.Lower, legislator.Chamber);
                Assert.AreEqual("51", legislator.District);
                Assert.AreEqual("Republican", legislator.Party);
                Assert.AreEqual("greghughes@le.utah.gov", legislator.Email);
                Assert.AreEqual("Gregory H Hughes", legislator.FullName);
                Assert.AreEqual("Gregory", legislator.FirstName);
                Assert.AreEqual("H", legislator.MiddleName);
                Assert.AreEqual("Hughes", legislator.LastName);
                Assert.AreEqual("", legislator.Suffixes);
                StringAssert.AreEqualIgnoringCase("http://le.utah.gov/images/legislator/hughegh.jpg", legislator.PhotoUrl);
                Assert.AreEqual("http://le.utah.gov/house2/detail.jsp?i=HUGHEGH", legislator.Url);
                Assert.AreEqual(DateTime.Parse("2011-01-14 22:24:08"), legislator.CreatedAt);
                Assert.AreEqual("de0ef9ead2d84d38a7bdcd9ffe2e53cc", legislator.TransparencyDataId);
                // Offices. Test the count and make sure the first one is correct.
                Assert.AreEqual(1, legislator.Offices.Count());
                var office = legislator.Offices.First();
                Assert.AreEqual(OfficeType.District, office.Type);
                Assert.AreEqual("Home", office.Name);
                Assert.AreEqual("472 MIDLAKE DR DRAPER, UT 84020", office.Address);
                Assert.AreEqual("801-432-0362", office.Phone);
                Assert.AreEqual(null, office.Fax);
                Assert.AreEqual("greghughes@le.utah.gov", office.Email);
                // Roles. Test the count and make sure the first couple are correct.
                // The data in this test will need to be updated with each session in
                // order to remain current. May want to investigate alternative ways
                // to test integration.
                Assert.AreEqual(8, legislator.Roles.Count());
                var role1 = legislator.Roles.ElementAt(0);
                Assert.AreEqual("2015-2016", role1.Term);
                Assert.AreEqual(Chamber.Lower, role1.Chamber);
                Assert.AreEqual(State.UT, role1.State);
                Assert.AreEqual(null, role1.StartDate);
                Assert.AreEqual(null, role1.EndDate);
                Assert.AreEqual(RoleType.Member, role1.Type);
                Assert.AreEqual("Republican", role1.Party);
                Assert.AreEqual("51", role1.District);
                Assert.AreEqual(null, role1.Committee);
                Assert.AreEqual(null, role1.Subcommittee);
                Assert.AreEqual(null, role1.CommitteeId);
                Assert.AreEqual(null, role1.Position);
                var role2 = legislator.Roles.ElementAt(1);
                Assert.AreEqual("2015-2016", role2.Term);
                Assert.AreEqual(Chamber.Joint, role2.Chamber);
                Assert.AreEqual(State.UT, role2.State);
                Assert.AreEqual(null, role2.StartDate);
                Assert.AreEqual(null, role2.EndDate);
                Assert.AreEqual(RoleType.CommitteeMember, role2.Type);
                Assert.AreEqual(null, role2.Party);
                Assert.AreEqual(null, role2.District);
                Assert.AreEqual("Legislative Management Committee", role2.Committee);
                Assert.AreEqual(null, role2.Subcommittee);
                Assert.AreEqual("UTC000085", role2.CommitteeId);
                Assert.AreEqual("Vice Chair", role2.Position);
                // Test the count on OldRoles. Role serialization was covered by the above tests.
                Assert.AreEqual(2, legislator.OldRoles.Count);
                Assert.AreEqual(3, legislator.OldRoles["2011-2012"].Count());
                Assert.AreEqual(8, legislator.OldRoles["2013-2014"].Count());
            }
        }
    }
}
