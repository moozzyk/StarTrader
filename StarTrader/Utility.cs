using System;

namespace StarTrader
{
	using System.Reflection;

	public static class AttributeHelper<TAttributeType, TType> where TAttributeType : Attribute, new()
	{
		private static readonly TAttributeType[] Attributes = Utility.GetAttributes<TAttributeType, TType>();

		public static TAttributeType GetAttibute(int value)
		{
			return Attributes[value];
		}
	}

	public class Utility
	{
		internal static TAttributeType[] GetAttributes<TAttributeType, TType>() where TAttributeType : Attribute, new()
		{
			FieldInfo[] fieldInfos = typeof(TType).GetFields();
			TAttributeType[] typeAttributes = new TAttributeType[fieldInfos.Length];

			foreach (FieldInfo fieldInfo in fieldInfos)
			{
				if (fieldInfo.FieldType.Equals(typeof(TType)))
				{
					object[] attributes = fieldInfo.GetCustomAttributes(typeof(TAttributeType), false);
					int index = (int)fieldInfo.GetValue(null);
					if (attributes.Length > 0)
					{
						typeAttributes[index] = (TAttributeType)attributes[0];
					}
					else
					{
						typeAttributes[index] = new TAttributeType();
					}
				}
			}

			return typeAttributes;
		}
	}
}
