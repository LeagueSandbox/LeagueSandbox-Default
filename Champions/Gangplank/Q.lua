Vector2 = require 'Vector2' -- include 2d vector lib 

function onStartCasting()   
    
end

function onFinishCasting()
    local target = castTarget
    addProjectileTarget("pirate_parley_mis", target, false) 	
end

function applyEffects()
    local target = castTarget
    local damage = owner:GetStats().AttackDamage.Total + -5 + (25*spellLevel)
    local newGold = owner:GetStats().Gold + 3 + (1*spellLevel)
	
    if ((not (target == 0)) and (not isDead(target))) then
        if castTarget:GetStats().CurrentHealth > damage then
            owner:dealDamageTo(target, damage, TYPE_PHYSICAL, SOURCE_ATTACK, false)
        else
            owner:GetStats().Gold = newGold
            owner:dealDamageTo(target, damage, TYPE_PHYSICAL, SOURCE_ATTACK, false)
        end    
    end

    destroyProjectile()
end

function onUpdate(diff)

end
