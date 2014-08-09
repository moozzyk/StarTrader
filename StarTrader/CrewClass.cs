namespace StarTrader
{
	using System;

	public class CrewAttribute : Attribute
	{
		public int OneTimePay { get; set; }

		public int Salary { get; set; }

		public int SafeJumpModifier { get; set; }

		internal static CrewAttribute GetAttibute(CrewClass crewClass)
		{
			return AttributeHelper<CrewAttribute, CrewClass>.GetAttibute((int)crewClass);
		}
	}

	public enum CrewClass
	{
		[Crew(OneTimePay = 20, Salary = 4, SafeJumpModifier = 5)]
		A,

		[Crew(OneTimePay = 10, Salary = 2, SafeJumpModifier = 3)]
		B,

		[Crew(OneTimePay = 6, Salary = 1, SafeJumpModifier = 1)]
		C,

		[Crew(OneTimePay = 2, Salary = 0)]
		D
	}
}