using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Gangplank
{
    public class E : GameScript
    {
        public void OnActivate(Champion owner) { }
        public void OnDeactivate(Champion owner) { }
        public void OnStartCasting(Champion owner, Spell spell, Unit target)
        {
            ApiFunctionManager.PlaySpellAnimation(owner, "Spell3");
            ApiFunctionManager.AddParticleTarget(owner, "pirate_raiseMorale_cas.troy", owner, 1, "L_HAND");
            ApiFunctionManager.AddParticleTarget(owner, "pirate_raiseMorale_tar.troy", owner, 1, "Gun");

            foreach (var ally in ApiFunctionManager.GetChampionsInRange(owner, 1250, true))
            {
                if (ally != owner)
                {
                    ally.AddBuffGameScript("GangplankETeam", "GangplankETeam", spell, 3.0f);
                    ApiFunctionManager.AddBuffHUDVisual("RaiseMoraleTeamBuff", 3.0f, 1, ally, 3.0f);
                }

                if (ally == owner)
                {
                    ally.AddBuffGameScript("GangplankE", "GangplankE", spell, 3.0f);
                    ApiFunctionManager.AddBuffHUDVisual("RaiseMorale", 3.0f, 1, ally, 3.0f);
                }
            }
            

        }
        public void OnFinishCasting(Champion owner, Spell spell, Unit target) {

        }
        public void ApplyEffects(Champion owner, Unit target, Spell spell, Projectile projectile) {

        }
        public void OnUpdate(double diff) {

        }
     }
}