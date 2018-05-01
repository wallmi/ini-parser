using NUnit.Framework;
using IniParser.Model.Configuration;

namespace IniParser.Tests.Unit
{
    [TestFixture, Category("DataModel")]
    public class IniSchemeTests
    {        
        [Test] 
        public void check_deep_clone()
        {
            var scheme1 = new IniScheme();
            scheme1.CommentString = "/";
            Assert.That(scheme1.CommentString, Is.EqualTo("/"));

            var scheme2 = scheme1.DeepClone();
            Assert.That(scheme2.CommentString, Is.EqualTo("/"));
        }
        
        [Test]
        public void TestCommentRegex()
        {
            var scheme = new IniScheme();
            scheme.CommentString = ";";
            
            string strGoodTest1 = ";comment Test";
            string strGoodTest2 = "   ; comment Test";
            string strGoodTest3 = "   ; comment Test           ";
            string strGoodTest4 = " dfasdfasf ; comment Test ";

            Assert.That(strGoodTest1, Does.Match(scheme.CommentRegex.ToString()));
            Assert.That(strGoodTest2, Does.Not.Match(scheme.CommentRegex.ToString()));
            Assert.That(strGoodTest3, Does.Not.Match(scheme.CommentRegex.ToString()));
            Assert.That(strGoodTest4, Does.Not.Match(scheme.CommentRegex.ToString()));
        }

        [Test,]
        public void TestSectionRegex()
        {
            var scheme = new IniScheme();
            
            string strGoodTest1 = "[section]";
            string strGoodTest2 = "   [sec-tion]   ";
            string strGoodTest3 = "[ .section ]";
            string strGoodTest4 = "[ s_ection ]";

            string strBadTest1 = "  bad [section]";
            string strBadTest2 = "[section] bad";

            Assert.That(strGoodTest1, Does.Match(scheme.SectionRegex.ToString()));
            Assert.That(strGoodTest2, Does.Match(scheme.SectionRegex.ToString()));
            Assert.That(strGoodTest3, Does.Match(scheme.SectionRegex.ToString()));
            Assert.That(strGoodTest4, Does.Match(scheme.SectionRegex.ToString()));

            Assert.That(strBadTest1, Does.Not.Match(scheme.SectionRegex.ToString()));
            Assert.That(strBadTest2, Does.Not.Match(scheme.SectionRegex.ToString()));
        }

        [Test, Description("Test a regular expression for matching a section in an INI file given an specific delimiter")]
        public void TestNewSectionDelimiter()
        {
            var scheme = new IniScheme();
                    
            scheme.SectionStartString = "<";
            scheme.SectionEndString = ">";

            string strGoodTest1 = "<section>";
            string strGoodTest2 = "   <section>   ";
            string strGoodTest3 = "< section >";

            string strBadTest1 = "  bad <section>";
            string strBadTest2 = "<section> bad";


            Assert.That(strGoodTest1, Does.Match(scheme.SectionRegex.ToString()));
            Assert.That(strGoodTest2, Does.Match(scheme.SectionRegex.ToString()));
            Assert.That(strGoodTest3, Does.Match(scheme.SectionRegex.ToString()));

            Assert.That(strBadTest1, Does.Not.Match(scheme.SectionRegex.ToString()));
            Assert.That(strBadTest2, Does.Not.Match(scheme.SectionRegex.ToString()));

            //Restore default delimiters
            scheme.SectionStartString = "[";
            scheme.SectionEndString = "]";
        }
    }
}
