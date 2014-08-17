namespace StarTraderTest
{
	using StarTrader;

	using Xunit;

	public class CommodityStorageTest
	{
		[Fact]
		public void Can_get_set_size()
		{
			var commodity = new CommodityStorage { Size = 5 };

			Assert.Equal(5, commodity.Size);

		}
	}
}
