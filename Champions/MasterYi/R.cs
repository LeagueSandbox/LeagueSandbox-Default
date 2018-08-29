using LeagueSandbox.GameServer.API;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.GameObjects.Missiles;
using LeagueSandbox.GameServer.GameObjects.Spells;
using LeagueSandbox.GameServer.Scripting.CSharp;

namespace Spells
{
	public class MasterYiHighlander : IGameScript
	{
		public void OnActivate(Champion owner)
		{
		}

		private void ReduceCooldown(AttackableUnit unit, bool isCrit)
		{
		}

		public void OnDeactivate(Champion owner)
		{
		}

		public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
		{
			var p = ApiFunctionManager.AddParticleTarget(owner, "Highlander_buf.troy", target, 1);
			var buff = ((ObjAiBase) target).AddBuffGameScript("Highlander", "Highlander", spell);
			var visualBuff = ApiFunctionManager.AddBuffHudVisual("Highlander", 10.0f, 1, owner);
			ApiFunctionManager.CreateTimer(10.0f, () =>
			{
				ApiFunctionManager.RemoveParticle(p);
				ApiFunctionManager.RemoveBuffHudVisual(visualBuff);
				((ObjAiBase) target).RemoveBuffGameScript(buff);
			});
		}

		public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
		{
		}

		public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
		{
		}

		public void OnUpdate(double diff)
		{
		}
	}
}
