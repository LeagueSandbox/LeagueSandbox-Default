using LeagueSandbox.GameServer.API;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.GameObjects.Missiles;
using LeagueSandbox.GameServer.GameObjects.Spells;
using LeagueSandbox.GameServer.Scripting.CSharp;

namespace LeagueSandbox_Default.Champions.Akali
{
    public class AkaliSmokeBomb : IGameScript
    {
        public void OnActivate(Champion owner)
        {
        }

        public void OnDeactivate(Champion owner)
        {
        }

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            var smokeBomb = ApiFunctionManager.AddParticle(owner, "akali_smoke_bomb_tar.troy", owner.X, owner.Y);
            /*
             * TODO: Display green border (akali_smoke_bomb_tar_team_green.troy) for the own team,
             * display red border (akali_smoke_bomb_tar_team_red.troy) for the enemy team
             * Currently only displaying the green border for everyone
            */
            var smokeBombBorder = ApiFunctionManager.AddParticle(owner, "akali_smoke_bomb_tar_team_green.troy", owner.X, owner.Y);
            //TODO: Add invisibility

            ApiFunctionManager.CreateTimer(8.0f, () =>
            {
                ApiFunctionManager.LogInfo("8 second timer finished, removing smoke bomb");
                ApiFunctionManager.RemoveParticle(smokeBomb);
                ApiFunctionManager.RemoveParticle(smokeBombBorder);
                //TODO: Remove invisibility
            });
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
        }

        public void OnUpdate(double diff)
        {
        }
    }
}
