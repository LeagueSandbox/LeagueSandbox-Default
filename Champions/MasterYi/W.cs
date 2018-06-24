using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Spells
{
    public class Meditate : GameScript
    {
        public void OnActivate(Champion owner)
        {
        }

        public void OnDeactivate(Champion owner)
        {
        }

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            for (int i = 0; i < 5; i++)
            {
                
                ApiFunctionManager.CreateTimer(i * 0.5f, () =>
                {
                    
                    ApiFunctionManager.AddParticleTarget(owner, "Meditate_eff.troy", owner, 1);
                    spell.spellAnimation("SPELL2", owner);
                    
                    

                    float healthPercentage = owner.GetStats().CurrentHealth / owner.GetStats().HealthPoints.Total;
                    //Example : 50/100= 0.5 
                    float missingHealthPercentage = 1.0f - healthPercentage;
                    //Right now the result would be 0.5 missingHP
                    //next we have to heal yi for 30/50/70/90/110 (+0.3) ap * 1% for every 1% missing hp
                    float ap = owner.GetStats().AbilityPower.Total * 0.3f;
                    float hptorecover = new float[] { 30f, 50f, 70f, 90f, 110f }[spell.Level - 1] + ap;
                    float bonushealth = hptorecover * missingHealthPercentage;

                    float totalhealthtorecover = (hptorecover + bonushealth);
                    float hptorecoverinhalfasec = totalhealthtorecover / 4f;
                    owner.RestoreHealth(hptorecoverinhalfasec);
                });
                var buff = ((ObjAIBase)target).AddBuffGameScript("MeditateBuff", "MeditateBuff", spell, -1, true);

                ApiFunctionManager.CreateTimer(4.0f, () =>
                {
                    owner.RemoveBuffGameScript(buff);
                });

            }
           




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
