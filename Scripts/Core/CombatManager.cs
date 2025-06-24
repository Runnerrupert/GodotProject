using Godot;
using System;

public partial class CombatManager : Node
{	
	public static void DealDamage(Character attacker, Character defender) {
		int finalDamage = CalculateDamage(attacker, defender);
		defender.TakeDamage(finalDamage);
	}
	
	private static int CalculateDamage(Character attacker, Character defender) {
		return attacker.BaseDamage;
	}
}
