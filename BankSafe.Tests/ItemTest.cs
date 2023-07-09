namespace BankSafe.Tests
{
    using NUnit.Framework;  
    class ItemTest
    {
        private Item item;
        private readonly string owner = "Ivan";
        private readonly string itemId = "1";
        [SetUp]
        public void Setup()
        {
            this.item = new Item(owner, itemId);
        }
        [Test]
        public void CheckItemConstructorAreWorksCorrectly()
        {
            Assert.AreEqual(this.owner, this.item.Owner);
            Assert.AreEqual(this.itemId, this.item.ItemId);
        }
    }
}
