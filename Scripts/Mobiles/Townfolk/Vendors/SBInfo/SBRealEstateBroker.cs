using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
    public class SBRealEstateBroker : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBRealEstateBroker()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( "Blank scroll", typeof( BlankScroll ), 5, 20, 0x0E34, 0 ) );
				Add( new GenericBuyInfo( "Pen and ink", typeof( ScribesPen ), 8,  20, 0xFBF, 0 ) );
				Add( new GenericBuyInfo( "Interior Decorator", typeof( InteriorDecorator ), 10000,  20, 0xFC1, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( ScribesPen ), 4 );
				Add( typeof( BlankScroll ), 2 );
			}
		}
	}
}