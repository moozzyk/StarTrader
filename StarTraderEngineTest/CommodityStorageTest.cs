namespace StarTrader
{
	using Xunit;

	public class CommodityStorageTest
	{
		[Fact]
		public void Size_Any_CangGetSet()
		{
			var commodity = new CommodityStorage { Size = 5 };

			Assert.Equal(5, commodity.Size);

		}
	}
}
