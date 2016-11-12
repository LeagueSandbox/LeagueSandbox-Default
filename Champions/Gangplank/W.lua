function onStartCasting()
    local newHealth = owner:GetStats().CurrentHealth + 10 + (70*spellLevel) + 1*owner:GetStats().AbilityPower.Total
    local maxHealth = owner:GetStats().HealthPoints.Total
    
    if newHealth >= maxHealth then
        owner:GetStats().CurrentHealth = maxHealth
    else
        owner:GetStats().CurrentHealth = newHealth
    end    
    addBuff("PirateScurvy", 1, 1, owner, owner)
end

function onFinishCasting()

end

function applyEffects()

end

function onUpdate(diff)

end
