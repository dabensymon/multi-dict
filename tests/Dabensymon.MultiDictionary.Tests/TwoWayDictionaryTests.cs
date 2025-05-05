using FluentAssertions;

namespace Dabensymon.MultiDictionary.Tests
{
    public class TwoWayDictionaryTests
    {
        [Fact]
        public void Constructor_Returns_ValidInstance()
        {
            // arrange/act
            Func<object> action = () => new TwoWayDictionary<Guid, Guid>();
        
            // assert
            action.Should().NotThrow();
            action().Should().BeOfType<TwoWayDictionary<Guid, Guid>>();
        }
    
        [Fact]
        public void Constructor_Returns_UniqueInstance()
        {
            // arrange/act
            var instance1 = new TwoWayDictionary<Guid, Guid>();
            var instance2 = new TwoWayDictionary<Guid, Guid>();

            // assert
            instance1.Should().NotBeSameAs(instance2);
        }
    
        [Fact]
        public void Add_NewItems_Is_Successful()
        {
            // arrange
            var dict = new TwoWayDictionary<Guid, Guid>();
            
            // act
            Action action = () => dict.Add(Guid.NewGuid(), Guid.NewGuid());
        
            // assert
            action.Should().NotThrow();
        }

        [Fact] 
        public void Add_Item_WithExistingFirstKey_Fails()
        {
            // arrange
            var dict = new TwoWayDictionary<string, Guid> 
            {
                { "test1stKey", Guid.NewGuid() } 
            };

            // act
            Action action = () => dict.Add("test1stKey", Guid.NewGuid());
        
            // assert
            action.Should().ThrowExactly<ArgumentException>();
        }
    
        [Fact] 
        public void Add_Item_WithExistingSecondKey_Fails()
        {
            // arrange
            var dict = new TwoWayDictionary<Guid, string> 
            {
                {
                    Guid.NewGuid(), "test2ndKey"
                }
            };

            // act
            Action action = () => dict.Add(Guid.NewGuid(), "test2ndKey");
        
            // assert
            action.Should().ThrowExactly<ArgumentException>();
        }

    }
}