namespace BankSafe.Tests
{
    using System;
    using NUnit.Framework;   
    public class BankVaultTests
    {
        private BankVault banckVault;    
        private Item item;
        private readonly string owner = "Ivan";
        private readonly string itemId = "1";
        private readonly int vaultCellsCount = 12;
        private readonly string cellDoesntExists = "At2";
        private readonly string cellAlreadyTaken = "B1";
        private readonly string cellWithDifferentKey = "C1";       
        [SetUp]
        public void Setup()
        {
            this.item = new Item(owner, itemId); 
            this.banckVault = new BankVault();      
        }
        [Test]
        public void CheckItemConstructorAreWorksCorrectly()
        {
            Assert.AreEqual(this.owner, this.item.Owner);
            Assert.AreEqual(this.itemId, this.item.ItemId);
        }
        [Test]
        public void CheckBankConstructorWorksCorrectly()
        {
            Assert.That(this.banckVault.VaultCells.Count, Is.EqualTo(vaultCellsCount));
        }
        [Test]
        public void AddItemArgumentExceptionCellDoesntExists()
        {
            var result = this.banckVault.VaultCells.ContainsKey(cellDoesntExists);
            Assert.Throws<ArgumentException>(() =>
            {
                this.banckVault.AddItem(cellDoesntExists, item);
            }, "Cell doesn't exists!");
        }
        [Test]
        public void AddItemArgumentExceptionItemCellIsAlreadyTaken()
        {          
            var containsKey = this.banckVault.VaultCells.ContainsKey(cellAlreadyTaken);            
            this.banckVault.AddItem(cellAlreadyTaken, item);
            Assert.Throws<ArgumentException>(() =>
            {              
                this.banckVault.AddItem(cellAlreadyTaken, item);
            }, "Cell is already taken!");
        }
        [Test]
        public void AddItemArgumentExceptionItemIsAlreadyInCell()
        {            
            this.banckVault.AddItem(cellAlreadyTaken, item);         
            var throwOperationException = string.Format("Item is already in cell!");          
            var ex = Assert.Throws<InvalidOperationException>(()
                     => this.banckVault.AddItem(cellWithDifferentKey, item));
            Assert.That(ex.Message.Equals(throwOperationException));
        }
        [Test]
        public void AddItem()
        {
            var returnResultAddItem = this.banckVault.AddItem(cellAlreadyTaken, item);
            var resultStringFormat = string.Format("Item:{0} saved successfully!", item.ItemId);
            Assert.That(returnResultAddItem, Is.EqualTo(resultStringFormat));
        }
        [Test]
        public void AddItemSavedDeletedAndCheckCell()
        {      
            this.banckVault.AddItem(cellAlreadyTaken, item);
            string owner = null;
            string itemId = null;
            Item newItem = new Item(owner, itemId);
            bool getValueIfExistKey = this.banckVault.VaultCells.TryGetValue(cellAlreadyTaken, out newItem);
            bool keyExist = this.banckVault.VaultCells.ContainsKey(cellAlreadyTaken);
            Assert.That(getValueIfExistKey);
            Assert.That(keyExist);
            var returnResultRemoveItem = this.banckVault.RemoveItem(cellAlreadyTaken, newItem);
            var resultStringFormat = string.Format("Remove item:{0} successfully!", newItem.ItemId);
            Assert.That(returnResultRemoveItem, Is.EqualTo(resultStringFormat));
        }
        [Test]
        public void RemoveItem()
        {
            this.banckVault.AddItem(cellAlreadyTaken, item);
            var returnResultRemoveItem = this.banckVault.RemoveItem(cellAlreadyTaken, item);
            var resultStringFormat = string.Format("Remove item:{0} successfully!", item.ItemId);
            Assert.That(returnResultRemoveItem, Is.EqualTo(resultStringFormat));
        }
        [Test]
        public void RemoveItemArgumentExceptionCellDoesntExists()
        {
            this.banckVault.AddItem(cellAlreadyTaken, item);
            var result = this.banckVault.VaultCells.ContainsKey(cellDoesntExists);
            Assert.Throws<ArgumentException>(() =>
            {
                this.banckVault.RemoveItem(cellDoesntExists, item);
            }, "Cell doesn't exists!");
        }
        [Test]
        public void AddItemArgumentExceptionItemCellIsDoesntExists()
        {
            var containsKey = this.banckVault.VaultCells.ContainsKey(cellAlreadyTaken);
            this.banckVault.AddItem(cellAlreadyTaken, item);
            Assert.Throws<ArgumentException>(() =>
            {
                this.banckVault.RemoveItem(cellWithDifferentKey, item);
            }, "Item in that cell doesn't exists!");
        }
    }
}