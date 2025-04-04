using RunLengthEncoding.Encoding;

namespace RunLengthEncodingTest.Encoding
{
    [TestClass]
    public sealed class RLETest
    {
        [TestMethod]
        [DataRow("AABBBCCCC", "2A3B4C")]
        [DataRow("AaBBBCCCc", "1A1a3B3C1c")]
        public void RLEEncode_WithSimpleText_ShouldReturn(string text, string shouldBe)
        {
            Assert.AreEqual(RLE.Encode(text), shouldBe);
        }

        [TestMethod]
        [DataRow("AA33CC555555ttUU", "2A23|2C65|2t2U")]
        public void RLEEncode_IncludingNumbers_ShouldReturn(string text, string shouldBe)
        {
            Assert.AreEqual(RLE.Encode(text), shouldBe);
        }
    }
}
