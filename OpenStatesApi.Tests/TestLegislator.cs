﻿using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OpenStatesApi.Tests
{
    [TestFixture( Category = "Json.Net Deserialization" )]
    public class TestLegislator
    {
        [Test(Description="Verifies mappings between OpenStates json and Legislator class are correct.")]
        public void LegislatorDeserialization()
        {
            string jsonFile = "OpenStatesApi.Tests.TestData.Legislator1.json";
            using ( var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream( jsonFile ) )
            using ( StreamReader reader = new StreamReader( stream ) )
            {
                var json = reader.ReadToEnd();
                Legislator legislator = JsonConvert.DeserializeObject<Legislator>( json );
                Assert.AreEqual( "UTL000064", legislator.LegislatorId );
                Assert.AreEqual( State.UT, legislator.State );
                Assert.AreEqual( true, legislator.Active );
                Assert.AreEqual( Chamber.Lower, legislator.Chamber );
                Assert.AreEqual( "51", legislator.District );
                Assert.AreEqual( "Republican", legislator.Party );
                Assert.AreEqual( "greghughes@le.utah.gov", legislator.Email );
                Assert.AreEqual( "Gregory H Hughes", legislator.FullName );
                Assert.AreEqual( "Gregory", legislator.FirstName );
                Assert.AreEqual( "H", legislator.MiddleName );
                Assert.AreEqual( "Hughes", legislator.LastName );
                Assert.AreEqual( "", legislator.Suffixes );
                Assert.AreEqual( "http://le.utah.gov/images/legislator/hughegh.jpg", legislator.PhotoUrl );
                Assert.AreEqual( "http://le.utah.gov/house2/detail.jsp?i=HUGHEGH", legislator.Url );
                Assert.AreEqual( DateTime.Parse( "2011-01-14 22:24:08" ), legislator.CreatedAt );
                Assert.AreEqual( DateTime.Parse( "2014-06-20 09:23:19" ), legislator.UpdatedAt );
                Assert.AreEqual( "de0ef9ead2d84d38a7bdcd9ffe2e53cc", legislator.TransparencyDataId );
                // Offices. Test the count and make sure the first one is correct.
                Assert.AreEqual( 1, legislator.Offices.Count() );
                var office = legislator.Offices.First();
                Assert.AreEqual( OfficeType.District, office.Type );
                Assert.AreEqual( "Home", office.Name );
                Assert.AreEqual( "472 MIDLAKE DR DRAPER, UT 84020", office.Address );
                Assert.AreEqual( null, office.Phone );
                Assert.AreEqual( null, office.Fax );
                Assert.AreEqual( null, office.Email );
                // Roles. Test the count and make sure the first couple are correct.
                Assert.AreEqual( 8, legislator.Roles.Count() );
                var role1 = legislator.Roles.ElementAt( 0 );
                Assert.AreEqual( "2013-2014", role1.Term );
                Assert.AreEqual( Chamber.Lower, role1.Chamber );
                Assert.AreEqual( State.UT, role1.State );
                Assert.AreEqual( null, role1.StartDate );
                Assert.AreEqual( null, role1.EndDate );
                Assert.AreEqual( RoleType.Member, role1.Type );
                Assert.AreEqual( "Republican", role1.Party );
                Assert.AreEqual( "51", role1.District );
                Assert.AreEqual( null, role1.Committee );
                Assert.AreEqual( null, role1.Subcommittee );
                Assert.AreEqual( null, role1.CommitteeId );
                Assert.AreEqual( null, role1.Position );
                var role2 = legislator.Roles.ElementAt( 1 );
                Assert.AreEqual( "2013-2014", role2.Term );
                Assert.AreEqual( Chamber.Joint, role2.Chamber );
                Assert.AreEqual( State.UT, role2.State );
                Assert.AreEqual( null, role2.StartDate );
                Assert.AreEqual( null, role2.EndDate );
                Assert.AreEqual( RoleType.CommitteeMember, role2.Type );
                Assert.AreEqual( null, role2.Party );
                Assert.AreEqual( null, role2.District );
                Assert.AreEqual( "Occupational and Professional Licensure Review Committee", role2.Committee );
                Assert.AreEqual( null, role2.Subcommittee );
                Assert.AreEqual( "UTC000053", role2.CommitteeId );
                Assert.AreEqual( "member", role2.Position );
                // Test the count on OldRoles. Role serialization was covered by the above tests.
                Assert.AreEqual( 1, legislator.OldRoles.Count );
                Assert.AreEqual( 3, legislator.OldRoles["2011-2012"].Count() );
            }
        }
    }
}
