namespace StarTrader
{
	using System;
	using System.Collections;
	using System.Collections.Generic;

	class Spaceship : IEnumerable<ShipModule>
	{
		private readonly HullAttribute m_hull;
		private readonly List<ShipModule> m_modules = new List<ShipModule>();

		private CrewAttribute m_crew;

		public Spaceship(HullType hull, CrewClass crew)
		{
			m_hull = HullAttribute.GetAttibute(hull);
			SetCrew(crew);
		}

		public int AvailableCapacity
		{
			get { return m_hull.Capacity - m_modules.Count; }
		}

		public void SetCrew(CrewClass crew)
		{
			m_crew = CrewAttribute.GetAttibute(crew);
		}

		public int GetJumpSuccessModifier()
		{
			int modifier = m_crew.SafeJumpModifier;
			if (m_modules.Contains(ShipModule.SafeJump))
			{
				modifier += 2;
			}

			return modifier;
		}

		public IEnumerator<ShipModule> GetEnumerator()
		{
			return m_modules.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<ShipModule>)this).GetEnumerator();
		}

		public void Add(ShipModule module)
		{
			if (m_modules.Count < m_hull.Capacity)
			{
				m_modules.Add(module);
			}
			else
			{
				throw new InvalidOperationException("Ship doesn't have required capacity to add a new module");
			}
		}
	}
}
