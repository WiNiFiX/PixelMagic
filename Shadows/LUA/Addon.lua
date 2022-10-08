-- Configurable Variables
local size = 1;	-- this is the size of the "pixels" at the top of the screen that will show stuff, currently 5x5 because its easier to see and debug with

---Tables
local npcDetect = {PlatesOn = 0,enemiesPlate = 0, activeUnitPlates = {}, npcCountFrame = nil}
local EquipmentManger = {
invSlots = {
1,3,5,7,10,15
},
setItemsT19 = {
	--[DK-T19]
138355,138349,138361,138352,138358,138364,
--[DH-T19]
138378,138376,138380,138377,138379,138375,

--[Druid-T19]
138330,138324,138336,138327,138333,138366,

--[Hunter-T19]
138342,138339,138347,138340,138344,138368,

--[Mage-T19]
138312,138318,138321,138309,138315,138365,

--[Monk-T19]
138331,138325,138337,138328,138334,138367,

--[Paladin-T19]
138356,138350,138362,138353,138359,138369,

--[Priest-T19]
138313,138319,138322,138310,138316,138370,

--[Rogue-T19]
138332,138326,138338,138329,138335,138371,

--[Shaman-T19]
138343,138346,138348,138341,138345,138372,

--[Warlock-T19]
138314,138320,138323,138311,138317,138373,

--[Warrior-T19]
138357,138351,138363,138354,138360,138374,
},
 setItemsT20 = {
	147178, 174180, 147176, 138346, 147177, 147179, -- T20 Shaman
	147187, 147188, 147189, 147190, 147191, 147192, -- T20 warrior
},
T19 = 0,
T20 = 0,
Legendary1 = 0,
legendary2 = 0,
}


local Race = {
	["Human"] = 0.01,
	["Dwarf"]= 0.02,
	["NightElf"]= 0.03,
	["Gnome"]= 0.04,
	["Draenei"]= 0.05,
	["Pandaren"]= 0.06,
	["Orc"]= 0.07,
	["Undead"]= 0.08,
	["Tauren"]= 0.09,
	["Troll"]= 0.10,
	["BloodElf"]= 0.11,
	["Goblin"]= 0.12,
    ["Worgen"] = 0.13,
}

local Spec = { Id ={
[0] = 0,
	[250] = 0.01,
	[251] = 0.02,
	[252] =0.03,
	[577] = 0.04,
	[581] =0.05,
	[102] = 0.06,
	[103] =0.07,
	[104] =0.08,
	[105] =0.09,
	[253] = 0.10,
	[254] =0.11,
	[255] = 0.12,
	[62] =0.13,
	[63] = 0.14,
	[64] =0.15,
	[268] = 0.16,
	[269] =0.17,
	[270] = 0.18,
	[65] = 0.19,
	[66] = 0.20,
	[70] =0.21,
	[256] = 0.22,
	[257]=.23,
	[258] =0.24,
	[259] =0.25,
	[260] =0.26,
	[261] = 0.27,
	[262] = 0.28,
    [263] = 0.29,
	[264] = 0.30,
	[265] = 0.31,
	[71] = 0.32,
	[72] = 0.33,
	[73] = 0.34,
	[266] = 0.35,
    [267] = 0.36},
	Spell = {
	[0] = "Death Strike",
	[250] = "Death Strike",
	[251] ="Frost Strike",
	[252] = "Festering Strike",
	[577] = "Demon's Bite",
	[581] ="Shear",
	[102] = "Solar Wrath",
	[103] ="Shred",
	[104] ="Mangle",
	[105] ="Solar Wrath",
	[253] ="Counter Shot",
	[254] ="Counter Shot",
	[255] ="Mongoose Bite",
	[62] ="Arcane Blast",
	[63] ="Fireball",
	[64] ="Frostbolt",
	[268] ="Blackout Strike",
	[269] ="Rising Sun Kick",
	[270] ="Rising Sun Kick",
	[65] = "Crusader Strike",
	[66] = "Shield of the Righteous",
	[70] ="Crusader Strike",
	[256] ="Penance",
	[257]="Smite",
	[258] ="Mind Blast",
	[259] ="Mutilate",
	[260] ="Saber Slash",
	[261] ="Backstab",
	[262] ="Lightning Bolt",
    [263] ="Rockbiter",
	[264] ="Lightning Bolt",
	[265] ="Agony",
	[71] = "Mortal Strike",
	[72] ="Bloodthirst",
	[73] = "Devastate",
	[266] ="Doom",
    [267] ="Incinerate"}
}

local index = GetCurrentResolution();
local currentResolution = select(index, GetScreenResolutions());  -- this will return a value in the format 1920x1080
local resWidth, resHeight = strsplit("x", currentResolution, 2)
local scale = GetScreenWidth() / resWidth 

local parent = CreateFrame("frame", "Recount", UIParent)
parent:SetSize(55 * size, 20 * size);  -- Width, Height
parent:SetPoint("TOPLEFT", 0, 0)
parent:SetScale(scale) 
parent:RegisterEvent("ADDON_LOADED")
parent:RegisterEvent("NAME_PLATE_UNIT_ADDED")
parent:RegisterEvent("PLAYER_ENTERING_WORLD")
parent:RegisterEvent("PLAYER_REGEN_DISABLED")
parent:RegisterEvent("PLAYER_REGEN_ENABLED")
parent.t = parent:CreateTexture()
parent.t:SetColorTexture(0, 0, 0, 1)
parent.t:SetAllPoints(parent)
parent:SetFrameStrata("TOOLTIP");

local flagFrame = {}
local cooldownframes = {}
local updateSpellChargesFrame = {}
local buffFrames = {}
local playerDebuffFrames = {}
local petBuffFrames = {}
local lastSpellFrame = {}
local lastPower = 0
local currentSpec = GetSpecialization()
local currentSpecId = 0
local lastPlayerDebuffState = {}
local targetLastSpellFrame = {}
local targetArena1Frame = {}
local targetArena2Frame = {}
local targetArena3Frame = {}
local targetDebuffFrames = {}
local spellInRangeFrames = {}
local damageModifierFrames = {}
local IsDispellableFrame = {}
local healthFrame = nil
local wildPetsFrame = nil
local targetHealthFrames = {}
local isTargetFriendlyFrame = nil
local hasTargetFrame = nil
local powerFrames = {}
local hasteFrame = nil
local playerIsCastingFrame = nil
local targetIsCastingFrame = nil
local unitIsVisibleFrame = nil
local unitPetFrame = nil
local lastCooldownState = {}
local lastSpellChargeCharges = {}
local lastBuffState = {}
local lastBuffStatePet = {}
local lastDebuffState = {}
local TargetBuffs = { }
local itemframes = { }
local buffLastState  = { }
local runePrev = 0
local ssPrev = 0
local ccPrev = 0
local hpPrev = 0
local lastUnitPower = 0
local lastCombat = nil
local alphaColor = 1
local spellOverlayedFrames = {}
local PlayerStatFrame = {}
local playerClass, englishClass, classIndex = UnitClass("player");

local function TurnOnPlates()
	if GetCVar("nameplateShowEnemies") == "1" then
		npcDetect.PlatesOn= 1
	else
		npcDetect.PlatesOn= 0
	end
end
local function activeEnemies()
	currentSpecId = currentSpec and select(1, GetSpecializationInfo(GetSpecialization())) or 0
    npcDetect.enemiesPlate = 0
	local spell = Spec.Spell[currentSpecId]
	for k, v in pairs(npcDetect.activeUnitPlates) do
        if v ~= nil then
            if UnitCanAttack("player", k) and LibStub("SpellRange-1.0").IsSpellInRange(spell, k) and 
            UnitIsDead(k) == false and UnitAffectingCombat(k) then
                npcDetect.enemiesPlate = npcDetect.enemiesPlate + 1
            --print("what is k:",k,"Is ",v," In range ",LibStub("SpellRange-1.0").IsSpellInRange("Mongoose Bite", v)," Is unit dead :",UnitIsDead(v))
        
            end
        end
    end
    npcDetect.enemiesPlate = npcDetect.enemiesPlate/100
    
end
local function NameplateFrameUPDATE()
	activeEnemies()
	TurnOnPlates()
	--TurnOnPlates()
	--print("Npc counT",npcDetect.enemiesPlate)
	npcDetect.npcCountFrame.t:SetColorTexture(1, npcDetect.enemiesPlate, npcDetect.PlatesOn, alphaColor)
end

local function AddNameplate(unitID)
	local GUID = false
	for k,v in pairs(npcDetect.activeUnitPlates)do
		 npcDetect.activeUnitPlates[k] = nil
	end	
	for i=1, 30 do
		if	UnitGUID ("nameplate"..i) ~= nil and not UnitIsPlayer("nameplate".. i) then
			for k, v in pairs(npcDetect.activeUnitPlates) do
			--print("Nameplate :","nameplate"..i,"GUID",UnitGUID ("nameplate"..i)  )
				if v == UnitGUID ("nameplate"..i) then
					GUID = true
				end
			end
			if GUID == false then
				--print("SET Nameplate :","nameplate"..i,"GUID",UnitGUID ("nameplate"..i)  )
				npcDetect.activeUnitPlates["nameplate"..i] = UnitGUID ("nameplate"..i)
				GUID = false
			end
		end
		GUID = false
	end
end
local function ClearNamePlates()
	for k,v in pairs(npcDetect.activeUnitPlates)do
		if  k ~=UnitGUID("target") then 
			npcDetect.activeUnitPlates[k] = nil
		end 
	end	
end

local function updateCombat(self, event, ...)	
    if event == "PLAYER_REGEN_ENABLED" then  
		--print("Leaving combat")
        unitCombatFrame.t:SetColorTexture(1, 1, 1, alphaColor)  
    end
	if event == "PLAYER_REGEN_DISABLED" then
		--print("Entering combat")
        unitCombatFrame.t:SetColorTexture(1, 0, 0, alphaColor)
    end
end

local function roundNumber(num)
    under = math.floor(num)
    upper = math.floor(num) + 1
    underV = -(under - num)
    upperV = upper - num
    if (upperV > underV) then
        return under
    else
        return upper
    end
end
local function updateLegendary()
	local count = 0
	EquipmentManger.Legendary1 = 0
	EquipmentManger.Legendary2 = 0
    for i =1, 18 do
		if GetInventoryItemID("player", i) ~= nil then
			local _, _, itemRarity, _, _, _,_, _, _, _, _ = GetItemInfo(GetInventoryItemID("player", i))
			if itemRarity == 5 then
				if count == 0 then
			        count = count +1
				    EquipmentManger.Legendary1 = i/100
            	else
					count = count + 1
					EquipmentManger.Legendary2 = i/100
				end
			end
		end
	end    
end
local function updateSetBonus()
	EquipmentManger.T19 = 0
	EquipmentManger.T20 = 0
	for _, v in pairs(EquipmentManger.invSlots) do
		itemID = GetInventoryItemID("player", v);
		for _,z in pairs(EquipmentManger.setItemsT19) do
			if(itemID == z)then
			--print(v, " ", z)
				EquipmentManger.T19= EquipmentManger.T19 +1
			end
		end
		for _,z in pairs(EquipmentManger.setItemsT20) do
			if(itemID == z)then
			--print(v, " ", z)
				EquipmentManger.T20= EquipmentManger.T20 + 1
			end
		end
	end
end


local function Talents()
		currentSpec = GetSpecialization()
		currentSpecId = currentSpec and select(1, GetSpecializationInfo(currentSpec)) or 0

		updateSetBonus()	
		updateLegendary()
		local Tier = {}
   		AllSeven = false
		for i = 1, GetMaxTalentTier() do 
		talenetSelected = false
			for z = 1, 3 do 
				local  selected = select(4, GetTalentInfo(i, z,1))
				if(selected == true) then
					Tier[i] = z/100
					talenetSelected = true
				end
				if talenetSelected == false and z == 3 then
					Tier[i]=0
				end
			end
			if i == 7 then
				AllSeven = true 
			end
		end
	if not AllSeven then
		for i = GetMaxTalentTier(), 7 do
			Tier[i] = 0	
		end
	end
	PlayerStatFrame[1].t:SetColorTexture(Tier[1], Tier[2],Tier[3], alphaColor)
	PlayerStatFrame[2].t:SetColorTexture(Tier[4],Tier[5],Tier[6], alphaColor)
	PlayerStatFrame[3].t:SetColorTexture(Tier[7],Race[select(2,UnitRace("player"))],0, alphaColor)
	PlayerStatFrame[4].t:SetColorTexture(Spec.Id[currentSpecId],EquipmentManger.T19/100,EquipmentManger.T20/100, alphaColor)
	PlayerStatFrame[5].t:SetColorTexture(EquipmentManger.Legendary1,EquipmentManger.Legendary2,0, alphaColor)
end

function GetUnitBuff(unit, buff)
    for i=1,40 do
        local name, _, _, count, _, _, expirationTime, _, _,_, spellId, _, _, _, _, _ = UnitBuff(unit,i)
        if name == buff then
            return name, count, expirationTime, spellId
        end
    end
    return nil
end


local function updateTargetBuffs()
	for _, auraId in pairs(buffs) do
		local auraName = GetSpellInfo(auraId)
		
		if auraName == nil then
            if (buffLastState[auraId] ~= "BuffOff") then
                TargetBuffs[auraId].t:SetColorTexture(1, 1, 1, alphaColor)
                TargetBuffs[auraId].t:SetAllPoints(false)
                buffLastState[auraId] = "BuffOff"          
            end    
            return
        end

        local name, count, expirationTime, spellId = GetUnitBuff("target", auraName)

		if (name == auraName) then -- We have Aura up and Aura ID is matching our list
                local remainingTime = 0
                if(expirationTime~=0) then
                    remainingTime = expirationTime -GetTime() - select(4,GetNetStats())/1000
                end
                if (buffLastState[auraId] ~= "BuffOn" .. count .. remainingTime) then
                buffLastState[auraId] = "BuffOn" .. count 
                remainingTime = string.format("%00.2f", tostring(remainingTime))
			    local green = tonumber(strsub(tostring(remainingTime), 1, 2)) / 100
                local blue = tonumber(strsub(tostring(remainingTime), -2, -1)) / 100

               
              --  if spellId == 194084 then
                --    print("Remaining CD: ",remainingTime," Count : ",red, " Seconds :",green," tenths : ",blue)
                --end
		     TargetBuffs[auraId].t:SetColorTexture(count/100, green, blue, alphaColor)
             TargetBuffs[auraId].t:SetAllPoints(false)
		end
        else
            if (buffLastState[auraId] ~= "BuffOff") then
                TargetBuffs[auraId].t:SetColorTexture(1, 1, 1, alphaColor)
                TargetBuffs[auraId].t:SetAllPoints(false)
                buffLastState[auraId] = "BuffOff"              
            end
        end
    end
end

local function updateUnitPower()
    local power = 0
    
    if classIndex == 2 then                                 -- Paladin
        power = UnitPower("player", SPELL_POWER_HOLY_POWER)
    end

    if classIndex == 9 then                                 -- Warlock
        power = UnitPower("player", SPELL_POWER_SOUL_SHARDS)
    end

    if classIndex == 8 then                                 -- Mage
        power = UnitPower("player", SPELL_POWER_ARCANE_CHARGES)
    end

    if classIndex == 4 then                                 -- Rogue 
        power = UnitPower("player", SPELL_POWER_COMBO_POINTS)
    end

    if classIndex == 11 then                                -- Druid Feral
        power = UnitPower("player", SPELL_POWER_COMBO_POINTS)
    end

    if classIndex == 6 then                                 -- DeathKnight                        
        local maxRunes = UnitPower("player", SPELL_POWER_RUNES)
        local selectedRunes = 6
        local i = 1
        for i = 1, maxRunes do
            local runeReady = select(3, GetRuneCooldown(i))
            if not runeReady then
                selectedRunes = selectedRunes - 1
            end
        end
        power = selectedRunes        
    end

    if classIndex == 10 then                                -- Monk
        power = UnitPower("player", SPELL_POWER_CHI)
    end

    if power ~= lastUnitPower then        
        --print("UnitPower: " .. green)        
        unitPowerFrame.t:SetColorTexture(0, power/100, 0, alphaColor)
        unitPowerFrame.t:SetAllPoints(false)
        lastUnitPower = power
    end
end

local lastPet = nil
local function updateUnitPet(self, event)
    local pet = UnitExists("pet")
	if (pet == false or UnitIsDead("pet")) then			
        if (pet ~= lastPet) then
            --print("Pet Is Not Up")
            unitPetFrame.t:SetColorTexture(1, 1, 1, alphaColor)
            lastPet = pet
        end
	else
		if pet ~= lastPet and not UnitIsDead("player")  then
            --print("Pet is up!")
            unitPetFrame.t:SetColorTexture(1, 0, 0, alphaColor)
            lastPet = pet
        end

    end
end


local function updateSpellCooldowns(self, event) 
    for _, spellId in pairs(cooldowns) do
		-- start is the value of GetTime() at the point the spell began cooling down
		-- duration is the total duration of the cooldown, NOT the remaining
		local start, duration, _ = GetSpellCooldown(spellId)
        --print(" " .. spellId .. " is currently active, use it and wait " .. duration .. " seconds for the next one.")
        local remainingTime = (start + duration - GetTime() - select(4,GetNetStats() )/1000 )
        if remainingTime < 0 then
			 remainingTime = 0
		 end
        
		if remainingTime ~= 0 then -- the spell is not ready to be cast
            	--print("Spell with Id = " .. spellId .. " is on CD")
                --print(" " ..spellId.. " remaining time: " ..math.floor(remainingTime)..  " ")
				remainingTime = string.format("%00.2f",tostring(remainingTime) )
				local green = tonumber(strsub(tostring(remainingTime), 1, 2))/100
				local blue = tonumber(strsub( tostring(remainingTime), -2,-1))/100
				cooldownframes[spellId].t:SetColorTexture(0, green, blue, alphaColor)				
				cooldownframes[spellId].t:SetAllPoints(false)
		else
				cooldownframes[spellId].t:SetColorTexture(1, 1, 1, alphaColor)
				cooldownframes[spellId].t:SetAllPoints(false)
		end						
	end
end

local lastItemCooldownState = { }

local function updateItemCooldowns(self, event) 
  for _, itemId in pairs(items) do
    local start, duration, _ = GetItemCooldown(itemId)
    local remainingTime =  start + duration - GetTime()
	local count = 0
    if remainingTime < 0 then remainingTime = 0 end
   
    local itemCount = GetItemCount(itemId)/100
    if itemCount >= 100 then count = 1 end

    if remainingTime == 0 or count == 0  then
	           
      if (lastItemCooldownState[itemId] ~= "onCD" .. itemCount .. remainingTime) then
	   lastItemCooldownState[itemId] = "onCD" .. itemCount .. remainingTime
        itemframes[itemId].t:SetColorTexture(0, 0, count, alphaColor)
        itemframes[itemId].t:SetAllPoints(false)
       
        --print("Count = " .. itemCount)
        --print("Remaining Time = " .. remainingTime)
        --print("OnCD = false")
      end
    else    
      if (lastItemCooldownState[itemId] ~= "offCD" .. itemCount .. remainingTime) then
        itemframes[itemId].t:SetColorTexture(1, 0, count, alphaColor)
        itemframes[itemId].t:SetAllPoints(false)
        lastItemCooldownState[itemId] = "offCD" .. itemCount .. remainingTime
        --print("Count = " .. itemCount)
        --print("Remaining Time = " .. remainingTime)
        --print("OnCD = true")
      end
    end
  end
end

local function updateSpellCharges(self, event) 
    
	for _, spellId in pairs(cooldowns) do
        charges, _, _, _ = GetSpellCharges(spellId)

        if (lastSpellChargeCharges[spellId] ~= charges) then
            --print("Spell with Id = " .. spellId .. " has charges: " .. charges .. " Green = " .. green)

            updateSpellChargesFrame[spellId].t:SetColorTexture(0, charges/100, 0, alphaColor)
		    updateSpellChargesFrame[spellId].t:SetAllPoints(false)
		    		
		    lastSpellChargeCharges[spellId] = charges		
        end        
	end
end

local function updateMyBuffs(self, event)
	for _, auraId in pairs(buffs) do
		local auraName = GetSpellInfo(auraId)
		
		if auraName == nil then
	        if (lastBuffState[auraId] ~= "BuffOff") then
                buffFrames[auraId].t:SetColorTexture(1, 1, 1, alphaColor)
                buffFrames[auraId].t:SetAllPoints(false)
                 lastBuffState[auraId] = "BuffOff" 
            end
			return
		end

        local name, count, duration, expirationTime, spellId = GetUnitBuff("player", auraName)

		if (name == auraName) then -- We have Aura up and Aura ID is matching our list
                local remainingTime = 0
                if(expirationTime~=0) then
                    remainingTime = expirationTime -GetTime() - select(4,GetNetStats())/1000
                end
                if (lastBuffState[auraId] ~= "BuffOn" .. count ) then
                lastBuffState[auraId] = "BuffOn" .. count 
                remainingTime = string.format("%00.2f", tostring(remainingTime))
                local green = tonumber(strsub(tostring(remainingTime), 1, 2)) / 100
                local blue = tonumber(strsub(tostring(remainingTime), -2, -1)) / 100

               
				--  if spellId == 194386 then
                --    print("Remaining CD: ",remainingTime," Count : ",red, " Seconds :",green," tenths : ",blue)
                --end
		     buffFrames[auraId].t:SetColorTexture(count/100, green, blue, alphaColor)
             buffFrames[auraId].t:SetAllPoints(false)

        end
        else
            if (lastBuffState[auraId] ~= "BuffOff") then
             buffFrames[auraId].t:SetColorTexture(1, 1, 1, alphaColor)
             buffFrames[auraId].t:SetAllPoints(false)
             lastBuffState[auraId] = "BuffOff"
            end
        end
    end
end

function GetDebuff(unit, debuff)
    for i=1,40 do
        local name, _, _, count, _, _, expirationTime, _, _,_, spellId, _, _, _, _, _ = UnitDebuff(unit,i,nil,"PLAYER|HARMFUL")
        if name == debuff then
            return name, count, expirationTime, spellId
        end
    end
    return nil
end


local function updateTargetDebuffs(self, event)
    
	for _, auraId in pairs(debuffs) do
        local auraName = GetSpellInfo(auraId)

        if auraName == nil then
            if (lastDebuffState[auraId] ~= "DebuffOff") then
                targetDebuffFrames[auraId].t:SetColorTexture(1, 1, 1, alphaColor)
                targetDebuffFrames[auraId].t:SetAllPoints(false)
                lastDebuffState[auraId] = "DebuffOff"               
            end
    
            return
        end
 		--print("Getting debuff for Id = " .. auraName)
		local name, count, duration, expirationTime, spellId = GetDebuff("target", auraName)
	    if name == "Unstable Affliction" and (name == auraName) then
              count = 0
                index = 1
                
                UA = true
                while UA do
                    name2, _, _, count2,_, duration, expirationTime2, _, _, _, spellId2, _, _, _, _, _ = UnitDebuff("target", index, "PLAYER|HARMFUL")
                    if name2 ~= nil then
                        index = index +1
                        if auraId == spellId2 then
                            UA = false
                            expirationTime = expirationTime2
                            count = count2
                        end
                    else 
                            UA = false
                    end
                end
                
            end
		if (name == auraName) then -- We have Aura up and Aura ID is matching our list
                local remainingTime = 0
                if(expirationTime ~=0) then
                     remainingTime = expirationTime - GetTime()
                end
                remainingTime = string.format("%00.2f", tostring(remainingTime))

			if (lastDebuffState[auraId] ~= "DebuffOn" .. count..remainingTime) then
                local green = tonumber(strsub(tostring(remainingTime), 1, 2)) / 100
                local blue = tonumber(strsub(tostring(remainingTime), -2, -1)) / 100

                targetDebuffFrames[auraId].t:SetColorTexture(count/100, green, blue, alphaColor)
                targetDebuffFrames[auraId].t:SetAllPoints(false)
                --print("[" .. buff.. "] " .. auraName.. " " .. count.. " Green: " .. green.. " Blue: " .. blue)
                lastDebuffState[auraId] = "DebuffOn" .. count..remainingTime
            end
        else
            if (lastDebuffState[auraId] ~= "DebuffOff") then
                targetDebuffFrames[auraId].t:SetColorTexture(1, 1, 1, alphaColor)
                targetDebuffFrames[auraId].t:SetAllPoints(false)
                lastDebuffState[auraId] = "DebuffOff"
                --print("[" .. buff.. "] " .. auraName.. " Off")
            end
        end

    end
end

local lastSpellInRange = {}

local function updateSpellInRangeFrames(self, event) 
    
	for _, spellId in pairs(cooldowns) do		
		local inRange = 0		
		local name, _, _, _, _, _ = GetSpellInfo(spellId)
		
		if (name == nil) then		
			inRange = 0		
		else
			-- http://wowwiki.wikia.com/wiki/API_IsSpellInRange	
			inRange = LibStub("SpellRange-1.0").IsSpellInRange(spellId, "target")  -- '0' if out of range, '1' if in range, or 'nil' if the unit is invalid.	
		end
								
		if lastSpellInRange[spellId] ~= inRange then
			if (inRange == 1) then
				spellInRangeFrames[spellId].t:SetColorTexture(1, 0, 0, alphaColor)
			else
				spellInRangeFrames[spellId].t:SetColorTexture(1, 1, 1, alphaColor)
			end 
			spellInRangeFrames[spellId].t:SetAllPoints(false)
			
			--print("Spell: " .. name .. " InRange = " .. inRange)
			
			lastSpellInRange[spellId] = inRange	
		end				
	end
end

local function PlayerNotMove()
    if GetUnitSpeed("Player") == 0 then
		Movetime = GetTime()
		PlayerMovingFrame.t:SetColorTexture(1, 1, 1, alphaColor)
    else 
		PlayerMovingFrame.t:SetColorTexture(1, 0, 0, alphaColor)
    end
end
local function PlayerNotMove()
	mountedplayer = 0
	moveTime = 1
	if IsMounted() then
		mountedplayer = .5
	end
	if GetUnitSpeed("Player") == 0 then
		moveTime = 0
	end
        PlayerMovingFrame.t:SetColorTexture(moveTime, mountedplayer, 1, alphaColor)
end

local function AutoAtacking()
    if IsCurrentSpell(6603) then
        AutoAtackingFrame.t:SetColorTexture(1, 0, 0, alphaColor)
    else 
		AutoAtackingFrame.t:SetColorTexture(1, 1, 1, alphaColor)
    end
end

local function updateIsPlayer()
    if UnitIsPlayer("target")  then        
        targetIsPlayerFrame.t:SetColorTexture(1, 0, 0, alphaColor)
    else 
		targetIsPlayerFrame.t:SetColorTexture(1, 1, 1, alphaColor)
    end
end

local function updateOutdoors()
    if IsOutdoors() then        
        flagFrame.t:SetColorTexture(1, 0, 0, alphaColor) -- red = outside
    else 
		flagFrame.t:SetColorTexture(1, 1, 1, alphaColor) -- white = inside
    end
end

local lastHealth = 0

local function updateHealth(self, event)
    local level = UnitLevel("player")    

	local red = 0             
    local green = 0
    local blue = 0
	local strLevel = "0.0" .. level;
			
	if (level >= 10) then
		strLevel = "0." .. level;
	end
       
	green = tonumber(strLevel)
    if (level > 100) then
        green = 0
        level = level - 100
        local strlevel2 = "0.0" .. level;	
	    if (level >= 10) then
	        strlevel2 = "0." .. level
	    end
        blue = tonumber(strlevel2)
    end
   
	local health = UnitHealth("player");		
	local maxHealth = UnitHealthMax("player");
	local percHealth = ceil((health / maxHealth) * 100)
	
	if (percHealth ~= lastHealth) then			
		local strHealth = "0.0" .. percHealth;
				
		if (percHealth >= 10) then
			strHealth = "0." .. percHealth;
		end
		red = tonumber(strHealth)
        if (percHealth == 100) then
            if (level == 100) then
		       healthFrame.t:SetColorTexture(1, 1, 0, alphaColor)
            else
               healthFrame.t:SetColorTexture(1, green, blue, alphaColor)
            end
        else
            healthFrame.t:SetColorTexture(red, green, blue, alphaColor)
        end
	    lastHealth = percHealth
	end
end

local lastPetHealth = 0

local function updatePetHealth(self, event)
    
	local health = UnitHealth("pet");		
	local maxHealth = UnitHealthMax("pet");
    if maxHealth == nil then maxHealth = 1 end
    if maxHealth == 0 then maxHealth = 1 end

	local percHealth = ceil((health / maxHealth) * 100)
	
	if (lastPetHealth ~= percHealth) then			
		local red = 0             
		local strHealth = "0.0" .. percHealth;
				
		if (percHealth >= 10) then
			strHealth = "0." .. percHealth;
		end
		red = tonumber(strHealth)
        if (percHealth == 100) then
		    petHealthFrame.t:SetColorTexture(1, 0, 0, alphaColor)
        else
            petHealthFrame.t:SetColorTexture(red, 0, 0, alphaColor)
        end

		--print ("Pet Health = " .. percHealth .. " strHealth = ".. strHealth)
			
	lastPetHealth = percHealth
	end
end

local lastTargetHealth = 0
local function updateTargetHealth(self, event)
    
	local guid = UnitGUID("target")

    local health = 0		
    local maxHealth = 100
    local percHealth = 0

    if (guid ~= nil) then
	    health = UnitHealth("target");		
	    maxHealth = UnitHealthMax("target");
	    percHealth = ceil((health / maxHealth) * 100)
    end	
	
	if (percHealth ~= lastTargetHealth) then						
		local red = 0             
		local strHealth = "0.0" .. percHealth;
				
		if (percHealth >= 10) then
			strHealth = "0." .. percHealth;
		end
		red = tonumber(strHealth)
        if (percHealth == 100) then
		    targetHealthFrame.t:SetColorTexture(1, 0, 0, alphaColor)
        else
            targetHealthFrame.t:SetColorTexture(red, 0, 0, alphaColor)
        end		
		lastTargetHealth = percHealth
	end
end

local function updatePower(self, event)
    
	local power = UnitPower("player");		
	local maxPower = UnitPowerMax("player");

	
	if (power ~= lastPower) then
		lastPower = power
			
		-- If the class uses mana, then we need to calculate percent mana to show its power
		-- http://wowwiki.wikia.com/wiki/API_UnitClass
		-- http://wowwiki.wikia.com/wiki/SpecializationID
		if (
			((classIndex == 7) and (currentSpecId == 264))  -- Shaman Restoration
		 or (classIndex == 2)  -- Paladin
		 or (classIndex == 5)  -- Priest 
		 or (classIndex == 8)  -- Mage
		 or (classIndex == 9)  -- Lock
		 or ((classIndex == 11) and (currentSpecId == 102)) -- Druid Balance
		 or ((classIndex == 11) and (currentSpecId == 105)) -- Druid Resto 
		   ) 
		then 
			power = ceil((power / maxPower) * 100)
		end
		
		local red = 0             
        local green = 0
		local strPower = "0.0" .. power;
				
		if (power >= 10) then
			strPower = "0." .. power;
		end
        
		red = tonumber(strPower)
        if (power > 100) then
            red = 0
            power = power - 100

            local strPower2 = "0.0" .. power;
				
		    if (power >= 10) then
			    strPower2 = "0." .. power
		    end

            green = tonumber(strPower2)
        end
    
        if (power == 100) then
		    powerFrame.t:SetColorTexture(1, 0, 0, alphaColor)
        else
            powerFrame.t:SetColorTexture(red, green, 0, alphaColor)
        end

		--print ("Power = " .. power .. " R = ".. red .. " G = " .. green)
	end
end

local lastHaste = 0;

local function updateHaste()
	local ratingBonus = math.floor(GetHaste())
	local green = 0
	if lasthaste == ratingBonus then return end
	lastehaste = ratingBonus
	local red = 0
    if ratingBonus == math.abs(ratingBonus) then
		red = 1
	else
        red = 0
    end
	if(ratingBonus >=100) then
		green =	math.floor(ratingBonus/100)
		ratingBonus = ratingBonus - (green*100)
	end
	local blue = math.abs(tonumber(strsub(tostring(ratingBonus), 1,2))/100)
    hasteFrame.t:SetColorTexture(red,green,blue, alphaColor)
end
local lastIsFriend = true 
 
local function updateIsFriendly(self, event)
    
	isFriend = UnitIsFriend("player","target");
	
	if (isFriend ~= lastIsFriend) then
	
		if (isFriend == true) then
			--print ("Unit is friendly: True")
			
			isTargetFriendlyFrame.t:SetColorTexture(0, 1, 0, alphaColor)
		else
			--print ("Unit is friendly: False")
			
			isTargetFriendlyFrame.t:SetColorTexture(0, 0, 1, alphaColor)
		end
	
		lastIsFriend = isFriend
	end
end

local lastTargetGUID = ""

local function hasTarget()
	local guid = UnitGUID("target")
    local LibBoss = LibStub("LibBossIDs-1.0")
    local isDead = UnitIsDead("target")
    local mylevel = UnitLevel("player")  

	if (guid ~= lastTargetGUID) then
	    if guid == nil then
            hasTargetFrame.t:SetColorTexture(0, 0, 0, alphaColor)
	    else
	        if guid ~= nil then local type, _, _, _, _, mobID = strsplit(" - ", UnitGUID("target"))
	            if LibBoss.BossIDs[tonumber(mobID)] then
                    hasTargetFrame.t:SetColorTexture(0, 0, 1, alphaColor)
                    --print('|cFFFF0000[BOSS] ID: ' .. mobID)
                elseif type == "Player" then
                        hasTargetFrame.t:SetColorTexture(1, 0, 0, alphaColor)
                elseif not LibBoss.BossIDs[tonumber(mobID)] then               
                        if mobID ~= nil then
                            if tonumber(mobID) == 114631 or   -- NPC outside Kara registers as enemy for some reason ignore it
                               tonumber(mobID) == 114822 or   -- NPC outside Kara registers as enemy for some reason ignore it
							   tonumber(mobID) == 90463  or   -- NPC in Violet Citedal [Archmage Karlain] registers as enemy for some reason ignore it
							   tonumber(mobID) == 90418  or   -- NPC in Violet Citedal [Archmage Modera] registers as enemy for some reason ignore it
							   tonumber(mobID) == 90431  then -- NPC in Violet Citedal [Archmage Ansirem Runeweaver] registers as enemy for some reason ignore it
                                --print('|cFFFFFF00[NPC] ID: ' .. mobID)
                                hasTargetFrame.t:SetColorTexture(0, 0, 0, alphaColor)
                            else
                                --print('|cFFFFFF00[NPC] ID: ' .. mobID)
                                hasTargetFrame.t:SetColorTexture(1, 0, 0, alphaColor)        
                            end
                        end
	            elseif (guid ~= nil and isDead == true) then
                    hasTargetFrame.t:SetColorTexture(0, 0, 0, alphaColor)
                end
            end
        end
        lastTargetGUID = guid
    end
end

local lastCastID = 0
local lastChanneling = ""

local function updatePlayerIsCasting(self, event)
    
    spell, _, _, _, _,_, _, castID, _ = UnitCastingInfo("player")
    name, _, text, _, _, _, _, _ = UnitChannelInfo("player")
	

	if castID ~= nil then	
		if castID ~= lastCastID then
			--print("Casting spell: " .. spell)
		
			playerIsCastingFrame.t:SetColorTexture(1, 0, 0, alphaColor)
		
			lastCastID = castID		
		end
	else
		if castID ~= lastCastID then
			--print("Not casting")
			
			playerIsCastingFrame.t:SetColorTexture(1, 1, 1, alphaColor)
			
			lastCastID = castID		
		end	
	end		

    if name ~= nil then
        if text ~= lastChanneling then
        playerIsCastingFrame.t:SetColorTexture(0, 1, 0, alphaColor)
        --   print(text)
        lastChanneling = text
        end

	else
	    if text ~= lastChanneling then
        playerIsCastingFrame.t:SetColorTexture(1, 1, 1, alphaColor)
        lastChanneling = text
        end

    end
end

local lastTargetCastID = 0

local function InteruptSpellTime(startTime, endTime)
    return  math.abs(endTime - GetTime() * 1000 ) / math.abs (endTime - startTime)
    --print("Cast time: ", castTime, "Cast PCT: ", castPCT,"remaining :", remainingTime)
end

local function updateTargetIsCasting(self, event)	    
	spell, _, _, _, startTime, endTime, _, _, notInterrupt = UnitCastingInfo("target")
    if(spell == nil) then        
        spell, _, _, _, startTime, endTime, _,  notInterrupt = UnitChannelInfo("target")
    end
    
    if spell ~= nil then    
        --print(InteruptSpellTime(startTime, endTime))
        if notInterrupt == false then
            targetIsCastingFrame.t:SetColorTexture(1, InteruptSpellTime(startTime, endTime), 1, alphaColor)
        else
            targetIsCastingFrame.t:SetColorTexture(0, InteruptSpellTime(startTime, endTime), 1, alphaColor)
        end
    else
        if spell == nil then
            targetIsCastingFrame.t:SetColorTexture(0, 0, 0, alphaColor)
        end
    end
end

local function updateWildPetsFrame()
  local wild = 0;
  local retard = 0;
  for i = 1, 4 do
    haveTotem, name, startTime, duration, _ = GetTotemInfo(i)
    local Quesatime =  startTime + duration
    if haveTotem
    then
      if (name == "Wild Imps" or name == "Spirit Wolf" or name == "Totem Mastery")
         and(startTime + duration - GetTime() > 1.6) 
      then
        wild = wild + 1;
        end
      if name == "Dreadstalkers"
      and(startTime + duration - GetTime() > 1.6) 
      then
        retard = retard + 1;
        end
      end
    wilds = tonumber("0.0"..wild)
    retards = tonumber("0.0"..retard)
    wildPetsFrame.t:SetColorTexture(wilds, 0, retards, alphaColor)
  end
end

local lastVis = nil

local function updateUnitIsVisible(self, event)
    
	local vis = UnitIsVisible("target")
		
	if vis == nil then			
        if (vis ~= lastVis) then
		    --print("Target Is Not Visible")		

	        unitIsVisibleFrame.t:SetColorTexture(1, 1, 1, alphaColor)
		    lastVis = vis				
        end
	else
		if vis ~= lastVis then
			--print("Target Is Visible")			

			unitIsVisibleFrame.t:SetColorTexture(1, 0, 0, alphaColor)			
			lastVis = vis		
		end	
	end
end

local function updateLastSpell(self, event, ...)
        
    local _, _, _, _, spellID = ...
    
    if (event == "UNIT_SPELLCAST_SUCCEEDED" and spellID ~= nil) then
        local strRed = ""
		local strBlue = ""
		local strGreen = ""
        
        for i = 1, 2 do        
		    if(i==1) then
                strRedTemp = strsub( spellID, 1, 1)
                if (strRedTemp~=nil) then
                    strRed = "0.".. strRedTemp
                else strRed = 1 end

                strGreenTemp = strsub( spellID, 2, 2)
                if (strGreenTemp~=nil) then
                    strGreen = "0.".. strGreenTemp
                else strGreen = 1 end
                            
                strBlueTemp = strsub( spellID, 3, 3)
                if (strBlueTemp~=nil) then
                    strBlue = "0.".. strBlueTemp
                else strBlue = 1 end
                            
            end
            if(i==2) then
                strRedTemp = strsub( spellID, 4, 4)
                if (strRedTemp~=nil) then
                    strRed = "0.".. strRedTemp
                else strRed = 1 end

                strGreenTemp = strsub( spellID, 5, 5)
                if (strGreenTemp~=nil) then
                    strGreen = "0.".. strGreenTemp
                else strGreen = 1 end

                strBlueTemp = strsub( spellID, 6, 6)
                if (strBlueTemp~=nil) then
                strBlue = "0.".. strBlueTemp
                else strBlue = 1 end
            end
                    
            local red = tonumber(strRed)
            local green = tonumber(strGreen)
            local blue = tonumber(strBlue)

            --print (strRed)
            lastSpellFrame[i].t:SetColorTexture(red, green, blue, alphaColor)
        end
    end
end
 
local function updateTargetCurrentSpell()            
    local spellID = select(10, UnitCastingInfo("target"))

    if spellID ~= nil then
        local strRed = ""
	    local strBlue = ""
	    local strGreen = ""
            
        for i = 1, 2 do        
	    	if(i==1) then
                strRedTemp = strsub( spellID, 1, 1)
                if (strRedTemp~=nil) then
                    strRed = "0.".. strRedTemp
                else strRed = 1 end

                strGreenTemp = strsub( spellID, 2, 2)
                if (strGreenTemp~=nil) then
                    strGreen = "0.".. strGreenTemp
                else strGreen = 1 end
                                
                strBlueTemp = strsub( spellID, 3, 3)
                if (strBlueTemp~=nil) then
                    strBlue = "0.".. strBlueTemp
                else strBlue = 1 end
                                
            end
            if(i==2) then
                strRedTemp = strsub( spellID, 4, 4)
                if (strRedTemp~=nil) then
                    strRed = "0.".. strRedTemp
                else strRed = 1 end

                strGreenTemp = strsub( spellID, 5, 5)
                if (strGreenTemp~=nil) then
                    strGreen = "0.".. strGreenTemp
                else strGreen = 1 end

                strBlueTemp = strsub( spellID, 6, 6)
                if (strBlueTemp~=nil) then
                strBlue = "0.".. strBlueTemp
                else strBlue = 1 end
            end
                        
            local red = tonumber(strRed)
            local green = tonumber(strGreen)
            local blue = tonumber(strBlue)

            --print (strRed)
            targetLastSpellFrame[i].t:SetColorTexture(red, green, blue, alphaColor)
        end
    else
        for i = 1, 2 do  
            targetLastSpellFrame[i].t:SetColorTexture(0, 0, 0, alphaColor)
        end
    end
end

local function updateArena1Spell()            
    local spellID = select(10, UnitCastingInfo("arena1"))

    if spellID ~= nil then
        --print("Arena 1: " .. spellID)
        local strRed = ""
	    local strBlue = ""
	    local strGreen = ""
            
        for i = 1, 2 do        
	    	if(i==1) then
                strRedTemp = strsub( spellID, 1, 1)
                if (strRedTemp~=nil) then
                    strRed = "0.".. strRedTemp
                else strRed = 1 end

                strGreenTemp = strsub( spellID, 2, 2)
                if (strGreenTemp~=nil) then
                    strGreen = "0.".. strGreenTemp
                else strGreen = 1 end
                                
                strBlueTemp = strsub( spellID, 3, 3)
                if (strBlueTemp~=nil) then
                    strBlue = "0.".. strBlueTemp
                else strBlue = 1 end
                                
            end
            if(i==2) then
                strRedTemp = strsub( spellID, 4, 4)
                if (strRedTemp~=nil) then
                    strRed = "0.".. strRedTemp
                else strRed = 1 end

                strGreenTemp = strsub( spellID, 5, 5)
                if (strGreenTemp~=nil) then
                    strGreen = "0.".. strGreenTemp
                else strGreen = 1 end

                strBlueTemp = strsub( spellID, 6, 6)
                if (strBlueTemp~=nil) then
                strBlue = "0.".. strBlueTemp
                else strBlue = 1 end
            end
                        
            local red = tonumber(strRed)
            local green = tonumber(strGreen)
            local blue = tonumber(strBlue)

            --print (strRed)
            targetArena1Frame[i].t:SetColorTexture(red, green, blue, alphaColor)
        end
    else
        for i = 1, 2 do  
            targetArena1Frame[i].t:SetColorTexture(0, 0, 0, alphaColor)
        end
    end
end

local function updateArena2Spell()            
    local spellID = select(10, UnitCastingInfo("arena2"))

    if spellID ~= nil then
        --print("Arena 2: " .. spellID)
        local strRed = ""
	    local strBlue = ""
	    local strGreen = ""
            
        for i = 1, 2 do        
	    	if(i==1) then
                strRedTemp = strsub( spellID, 1, 1)
                if (strRedTemp~=nil) then
                    strRed = "0.".. strRedTemp
                else strRed = 1 end

                strGreenTemp = strsub( spellID, 2, 2)
                if (strGreenTemp~=nil) then
                    strGreen = "0.".. strGreenTemp
                else strGreen = 1 end
                                
                strBlueTemp = strsub( spellID, 3, 3)
                if (strBlueTemp~=nil) then
                    strBlue = "0.".. strBlueTemp
                else strBlue = 1 end
                                
            end
            if(i==2) then
                strRedTemp = strsub( spellID, 4, 4)
                if (strRedTemp~=nil) then
                    strRed = "0.".. strRedTemp
                else strRed = 1 end

                strGreenTemp = strsub( spellID, 5, 5)
                if (strGreenTemp~=nil) then
                    strGreen = "0.".. strGreenTemp
                else strGreen = 1 end

                strBlueTemp = strsub( spellID, 6, 6)
                if (strBlueTemp~=nil) then
                strBlue = "0.".. strBlueTemp
                else strBlue = 1 end
            end
                        
            local red = tonumber(strRed)
            local green = tonumber(strGreen)
            local blue = tonumber(strBlue)

            --print (strRed)
            targetArena2Frame[i].t:SetColorTexture(red, green, blue, alphaColor)
        end
    else
        for i = 1, 2 do  
            targetArena2Frame[i].t:SetColorTexture(0, 0, 0, alphaColor)
        end
    end
end

local function updateArena3Spell()            
    local spellID = select(10, UnitCastingInfo("arena3"))

    if spellID ~= nil then
        --print("Arena 3: " .. spellID)
        local strRed = ""
	    local strBlue = ""
	    local strGreen = ""
            
        for i = 1, 2 do        
	    	if(i==1) then
                strRedTemp = strsub( spellID, 1, 1)
                if (strRedTemp~=nil) then
                    strRed = "0.".. strRedTemp
                else strRed = 1 end

                strGreenTemp = strsub( spellID, 2, 2)
                if (strGreenTemp~=nil) then
                    strGreen = "0.".. strGreenTemp
                else strGreen = 1 end
                                
                strBlueTemp = strsub( spellID, 3, 3)
                if (strBlueTemp~=nil) then
                    strBlue = "0.".. strBlueTemp
                else strBlue = 1 end
                                
            end
            if(i==2) then
                strRedTemp = strsub( spellID, 4, 4)
                if (strRedTemp~=nil) then
                    strRed = "0.".. strRedTemp
                else strRed = 1 end

                strGreenTemp = strsub( spellID, 5, 5)
                if (strGreenTemp~=nil) then
                    strGreen = "0.".. strGreenTemp
                else strGreen = 1 end

                strBlueTemp = strsub( spellID, 6, 6)
                if (strBlueTemp~=nil) then
                strBlue = "0.".. strBlueTemp
                else strBlue = 1 end
            end
                        
            local red = tonumber(strRed)
            local green = tonumber(strGreen)
            local blue = tonumber(strBlue)

            --print (strRed)
            targetArena3Frame[i].t:SetColorTexture(red, green, blue, alphaColor)
        end
    else
        for i = 1, 2 do  
            targetArena3Frame[i].t:SetColorTexture(0, 0, 0, alphaColor)
        end
    end
end

local function updateMyPetBuffs(self, event) 
    
	for _, auraId in pairs(buffs) do
		local auraName = GetSpellInfo(auraId)
		
		if auraName == nil then
			if (lastBuffStatePet [auraId] ~= "BuffOff") then
				petBuffFrames[auraId].t:SetColorTexture(1, 1, 1, alphaColor)
				petBuffFrames[auraId].t:SetAllPoints(false)
				lastBuffStatePet [auraId] = "BuffOff"
				--print("[" .. buff .. "] " .. auraName.. " Off")
			end
			return
		end
		
		local name, count, expirationTime = GetUnitBuff("pet", auraName)		
			
		if (name == auraName) then -- We have Aura up and Aura ID is matching our list	
		
			local getTime = GetTime()
			local remainingTime = 0
			if(expirationTime~=0) then
				remainingTime = math.floor(expirationTime - getTime + 0.5)
			end
			
			if (lastBuffStatePet[auraId] ~= "BuffOn" .. count .. remainingTime) then			
				lastBuffStatePet[auraId] = "BuffOn" .. count 
				remainingTime = string.format("%00.2f",tostring(remainingTime) )
				local green = tonumber(strsub(tostring(remainingTime), 1, 2))/100
				local blue = tonumber(strsub(tostring(remainingTime), -2,-1))/100
				--print("expirationTime:"..expirationTime.." remainingTime:" .. remainingTime .. " blue:" .. blue .. " strbluecount:" ..  strbluecount)
				petBuffFrames[auraId].t:SetColorTexture(count/100, green, blue, alphaColor)
				petBuffFrames[auraId].t:SetAllPoints(false)
				--print("[" .. buff .. "] " .. auraName.. " " .. count .. " Green: " .. green)
			end
		else
			if (lastBuffStatePet[auraId] ~= "BuffOff") then
				petBuffFrames[auraId].t:SetColorTexture(1, 1, 1, alphaColor)
				petBuffFrames[auraId].t:SetAllPoints(false)
				lastBuffStatePet[auraId] = "BuffOff"
				--print("[" .. buff .. "] " .. auraName.. " Off")
			end
		end
	end
end


local function updatePlayerDebuffs(self, event)
    
	for _, auraId in pairs(debuffs) do
        local buff = "UnitDebuff";
		local auraName = GetSpellInfo(auraId)

        if auraName == nil then
            if (lastPlayerDebuffState[auraId] ~= "DebuffOff") then
                playerDebuffFrames[auraId].t:SetColorTexture(1, 1, 1, alphaColor)
                playerDebuffFrames[auraId].t:SetAllPoints(false)
                lastPlayerDebuffState[auraId] = "DebuffOff"               
            end
    
            return
        end
        
		--print("Getting debuff for Id = " .. auraName)
		
        local name, count, expirationTime, spellId =  GetDebuff("player", auraName)

		if (name == auraName) then -- We have Aura up and Aura ID is matching our list					
            local getTime = GetTime()
            local remainingTime = math.floor(expirationTime - getTime + 0.5) 	

			if (lastPlayerDebuffState[auraId] ~= "DebuffOn" .. count .. remainingTime) then
				lastPlayerDebuffState[auraId] = "DebuffOn" .. count .. remainingTime
                remainingTime = string.format("%00.2f",tostring(remainingTime) )
				local green = tonumber(strsub(tostring(remainingTime), 1, 2))/100
				local blue = tonumber(strsub(tostring(remainingTime), -2,-1))/100
                playerDebuffFrames[auraId].t:SetColorTexture(count/100, green, blue, alphaColor)
				playerDebuffFrames[auraId].t:SetAllPoints(false)
                --print("[" .. buff .. "] " .. auraName.. " " .. count .. " Green: " .. green .. " Blue: " .. blue)
            end
        else
            if (lastPlayerDebuffState[auraId] ~= "DebuffOff") then
                playerDebuffFrames[auraId].t:SetColorTexture(1, 1, 1, alphaColor)
                playerDebuffFrames[auraId].t:SetAllPoints(false)
                lastPlayerDebuffState[auraId] = "DebuffOff"
                --print("[" .. buff .. "] " .. auraName.. " Off")
            end
        end
    end
end

local function updateIsSpellOverlayedFrames(self, event)    
	for _, spellId in pairs(cooldowns) do
        if IsSpellOverlayed(spellId) then
            --print('spell' .. spellId .. 'IsOverlayed')
            spellOverlayedFrames[spellId].t:SetColorTexture(1, 0, 0, alphaColor)
            spellOverlayedFrames[spellId].t:SetAllPoints(false)
        else
            spellOverlayedFrames[spellId].t:SetColorTexture(1, 1, 1, alphaColor)
            spellOverlayedFrames[spellId].t:SetAllPoints(false)
        end
    end
end

local function InitializeOne()
    local i = 0

	--print ("Initialising Health Frames")	
	healthFrame = CreateFrame("frame", "", parent)
	healthFrame:SetSize(size, size)
	healthFrame:SetPoint("TOPLEFT", 0, 0)                         -- row 1, column 1 [Player Health]

	healthFrame.t = healthFrame:CreateTexture()        
	healthFrame.t:SetColorTexture(1, 1, 1, alphaColor)
	healthFrame.t:SetAllPoints(healthFrame)	
	healthFrame:Show()
	healthFrame:RegisterEvent("PLAYER_REGEN_ENABLED")
    healthFrame:RegisterEvent("PLAYER_REGEN_DISABLED")
	healthFrame:RegisterUnitEvent("UNIT_HEALTH","player")
	healthFrame:RegisterEvent("PLAYER_TARGET_CHANGED")
	healthFrame:SetScript("OnEvent", updateHealth)
    
	--print ("Initialising Power Frames (Rage, Energy, etc...)")  
	powerFrame = CreateFrame("frame","", parent)
	powerFrame:SetSize(size, size)
	powerFrame:SetPoint("TOPLEFT", 1 * size, 0)                   -- row 1, column 2 [Player Power]
	powerFrame.t = powerFrame:CreateTexture()        
	powerFrame.t:SetColorTexture(1, 1, 1, alphaColor)
	powerFrame.t:SetAllPoints(powerFrame)    
	powerFrame:Show()
	powerFrame:RegisterEvent("PLAYER_ENTERING_WORLD")		
	powerFrame:RegisterEvent("PLAYER_REGEN_ENABLED")
    powerFrame:RegisterEvent("PLAYER_REGEN_DISABLED")
    --powerFrame:RegisterUnitEvent("UNIT_POWER","player")				** TODO **
	powerFrame:SetScript("OnEvent", updatePower)

	--print ("Initialising Target Health Frames")    
	targetHealthFrame = CreateFrame("frame","", parent)
	targetHealthFrame:SetSize(size, size)
	targetHealthFrame:SetPoint("TOPLEFT", 2 * size, 0)            -- row 1, column 3 [Target Health]

	targetHealthFrame.t = targetHealthFrame:CreateTexture()        
	targetHealthFrame.t:SetColorTexture(1, 1, 1, alphaColor)
	targetHealthFrame.t:SetAllPoints(targetHealthFrame)
	targetHealthFrame:Show()
	targetHealthFrame:RegisterEvent("PLAYER_REGEN_ENABLED")
    targetHealthFrame:RegisterEvent("PLAYER_REGEN_DISABLED")
	targetHealthFrame:RegisterEvent("UNIT_HEALTH","target")
	targetHealthFrame:RegisterEvent("PLAYER_TARGET_CHANGED")
	targetHealthFrame:RegisterEvent("PLAYER_ENTER_COMBAT")
	targetHealthFrame:RegisterEvent("PLAYER_LEAVE_COMBAT")
    targetHealthFrame:SetScript("OnEvent", updateTargetHealth)	
 	
    --print ("Initialising InCombat Frames")    
    unitCombatFrame = CreateFrame("frame","", parent);
    unitCombatFrame:SetSize(size, size);
    unitCombatFrame:SetPoint("TOPLEFT", 3 * size, 0)              -- row 1, column 4 [UnitInCombat]
    unitCombatFrame.t = unitCombatFrame:CreateTexture()
    unitCombatFrame.t:SetColorTexture(1, 1, 1, alphaColor)
    unitCombatFrame.t:SetAllPoints(unitCombatFrame)
    unitCombatFrame:Show()
    unitCombatFrame:RegisterEvent("PLAYER_REGEN_ENABLED")
    unitCombatFrame:RegisterEvent("PLAYER_REGEN_DISABLED")
	unitCombatFrame:SetScript("OnEvent", updateCombat)

    --print ("Initialising UnitPower Frames")    
	unitPowerFrame = CreateFrame("frame","", parent);
	unitPowerFrame:SetSize(size, size)
	unitPowerFrame:SetPoint("TOPLEFT", 4 * size, 0)               -- row 1, column 5 [Holy Power, etc...]
	unitPowerFrame.t = unitPowerFrame:CreateTexture()        
	unitPowerFrame.t:SetColorTexture(0, 0, 0, alphaColor)
	unitPowerFrame.t:SetAllPoints(unitPowerFrame)
	unitPowerFrame:Show()
	unitPowerFrame:RegisterEvent("PLAYER_ENTERING_WORLD")		
	unitPowerFrame:RegisterEvent("PLAYER_REGEN_ENABLED")
    unitPowerFrame:RegisterEvent("PLAYER_REGEN_DISABLED")
    --unitPowerFrame:RegisterUnitEvent("UNIT_POWER","player") ** TODO **
	unitPowerFrame:SetScript("OnEvent", updateUnitPower)	
	
    --print ("Initialising IsTargetFriendly Frame")
    isTargetFriendlyFrame = CreateFrame("frame","", parent);
    isTargetFriendlyFrame:SetSize(size, size);
    isTargetFriendlyFrame:SetPoint("TOPLEFT", 5 * size, 0)     -- row 1, column 6 [Target Is Friendly]
    isTargetFriendlyFrame.t = isTargetFriendlyFrame:CreateTexture()        
    isTargetFriendlyFrame.t:SetColorTexture(0, 1, 0, alphaColor)
    isTargetFriendlyFrame.t:SetAllPoints(isTargetFriendlyFrame)
    isTargetFriendlyFrame:Show()
	isTargetFriendlyFrame:RegisterEvent("PLAYER_REGEN_ENABLED")
   	isTargetFriendlyFrame:RegisterEvent("PLAYER_REGEN_DISABLED")
	isTargetFriendlyFrame:RegisterEvent("UNIT_HEALTH","target")
	isTargetFriendlyFrame:RegisterEvent("PLAYER_TARGET_CHANGED")
	isTargetFriendlyFrame:RegisterEvent("PLAYER_ENTER_COMBAT")
	isTargetFriendlyFrame:RegisterEvent("PLAYER_LEAVE_COMBAT")
    isTargetFriendlyFrame:SetScript("OnEvent", updateIsFriendly)

	--print ("Initialising HasTarget Frame")
	hasTargetFrame = CreateFrame("frame","", parent);
	hasTargetFrame:SetSize(size, size);
	hasTargetFrame:SetPoint("TOPLEFT", 6 * size, 0)                           -- row 1, column 7 [Has Target]
	hasTargetFrame.t = hasTargetFrame:CreateTexture()        
	hasTargetFrame.t:SetColorTexture(0, 1, 0, alphaColor)
	hasTargetFrame.t:SetAllPoints(hasTargetFrame)
	hasTargetFrame:Show()				
	hasTargetFrame:RegisterEvent("PLAYER_TARGET_CHANGED")
	hasTargetFrame:RegisterEvent("PLAYER_ENTER_COMBAT")
	hasTargetFrame:RegisterEvent("PLAYER_LEAVE_COMBAT")
	hasTargetFrame:SetScript("OnEvent", hasTarget)
	
	--print ("Initialising PlayerIsCasting Frame")
	playerIsCastingFrame = CreateFrame("frame","", parent);
	playerIsCastingFrame:SetSize(size, size);
	playerIsCastingFrame:SetPoint("TOPLEFT", 7 * size, 0)                     -- row 1, column 8 [Player Is Casting]
	playerIsCastingFrame.t = playerIsCastingFrame:CreateTexture()        
	playerIsCastingFrame.t:SetColorTexture(1, 1, 1, alphaColor)
	playerIsCastingFrame.t:SetAllPoints(playerIsCastingFrame)
	playerIsCastingFrame:Show()				
	playerIsCastingFrame:SetScript("OnUpdate", updatePlayerIsCasting)
	
	--print ("Initialising TargetIsCasting Frame")
	targetIsCastingFrame = CreateFrame("frame","", parent);
	targetIsCastingFrame:SetSize(size, size);
	targetIsCastingFrame:SetPoint("TOPLEFT", 8 * size, 0)                     -- row 1, column 9 [Target Is Casting]
	targetIsCastingFrame.t = targetIsCastingFrame:CreateTexture()        
	targetIsCastingFrame.t:SetColorTexture(1, 1, 1, alphaColor)
	targetIsCastingFrame.t:SetAllPoints(targetIsCastingFrame)
	targetIsCastingFrame:Show()				
	targetIsCastingFrame:SetScript("OnUpdate", updateTargetIsCasting)

	--print ("Initialising Haste Frames")  
	hasteFrame = CreateFrame("frame","", parent)
	hasteFrame:SetSize(size, size)
	hasteFrame:SetPoint("TOPLEFT", 9 * size, 0)                               -- row 1, column 10 [Player Haste]
	hasteFrame.t = hasteFrame:CreateTexture()        
	hasteFrame.t:SetColorTexture(1, 1, 1, alphaColor)
	hasteFrame.t:SetAllPoints(hasteFrame)    
	hasteFrame:Show()				
	hasteFrame:RegisterEvent("PLAYER_ENTERING_WORLD")
	hasteFrame:RegisterUnitEvent("UNIT_SPELL_HASTE","player")
	hasteFrame:SetScript("OnUpdate", updateHaste)

	--print ("Initialising Target Is Visible Frame")
	unitIsVisibleFrame = CreateFrame("frame","", parent);
	unitIsVisibleFrame:SetSize(size, size);
	unitIsVisibleFrame:SetPoint("TOPLEFT", 10 * size, 0)                      -- row 1, column 11 [Target Visible]
	unitIsVisibleFrame.t = unitIsVisibleFrame:CreateTexture()        
	unitIsVisibleFrame.t:SetColorTexture(0, 1, 0, alphaColor)
	unitIsVisibleFrame.t:SetAllPoints(unitIsVisibleFrame)
	unitIsVisibleFrame:Show()			
	unitIsVisibleFrame:SetScript("OnUpdate", updateUnitIsVisible)

    if (classIndex == 6 or                                  -- DeathKnight   
        classIndex == 3 or                                  -- Hunter
        classIndex == 9 or                                  -- Warlock
        classIndex == 8 or                                  -- Mage
        classIndex == 7)                                    -- Shaman (Enh. Needs it for Wolves)
    then
        unitPetFrame = CreateFrame("frame","", parent);
        unitPetFrame:SetSize(size, size);
        unitPetFrame:SetPoint("TOPLEFT", 11 * size, 0)                        -- row 1, column 12 [Has Pet]
	    unitPetFrame.t = unitPetFrame:CreateTexture()    
        unitPetFrame.t:SetColorTexture(0, 1, 0, alphaColor)
        unitPetFrame.t:SetAllPoints(unitPetFrame)
        unitPetFrame:Show()		
        unitPetFrame:SetScript("OnUpdate", updateUnitPet)

        petHealthFrame = CreateFrame("frame","", parent)
	    petHealthFrame:SetSize(size, size)
	    petHealthFrame:SetPoint("TOPLEFT", size * 12, 0)                      -- row 1, column 13 [Pet Health]
	    petHealthFrame.t = petHealthFrame:CreateTexture()        
	    petHealthFrame.t:SetColorTexture(1, 1, 1, alphaColor)
	    petHealthFrame.t:SetAllPoints(petHealthFrame)	
	    petHealthFrame:Show()
		petHealthFrame:RegisterEvent("UNIT_HEALTH","pet")
	    petHealthFrame:SetScript("OnEvent", updatePetHealth)

        wildPetsFrame = CreateFrame("frame","", parent);
        wildPetsFrame:SetSize(size, size);
        wildPetsFrame:SetPoint("TOPLEFT", size * 13, 0)                       -- row 1, column 14 [Wild Pets]
        wildPetsFrame.t = wildPetsFrame:CreateTexture()
        wildPetsFrame.t:SetColorTexture(0, 1, 0, alphaColor)
        wildPetsFrame.t:SetAllPoints(wildPetsFrame)
        wildPetsFrame:Show()
        wildPetsFrame:SetScript("OnUpdate", updateWildPetsFrame)

	    --print ("Initialising Pet Buff Frames")
	    local i = 0
	    for _, buffId in pairs(buffs) do
		    petBuffFrames[buffId] = CreateFrame("frame","", parent)
		    petBuffFrames[buffId]:SetSize(size, size)
		    petBuffFrames[buffId]:SetPoint("TOPLEFT", i * size, -size * 9)                      -- row 10 [Pet Buffs]
		    petBuffFrames[buffId].t = petBuffFrames[buffId]:CreateTexture()        
		    petBuffFrames[buffId].t:SetColorTexture(1, 1, 1, alphaColor)
		    petBuffFrames[buffId].t:SetAllPoints(petBuffFrames[buffId])
		    petBuffFrames[buffId]:Show()		               
			
		i = i + 1
		end
	    petBuffFrames[table.maxn(petBuffFrames)]:SetScript("OnUpdate", updateMyPetBuffs)
    end

    --print ("Initialising Spell Cooldown Frames")
    i = 0
    for _, spellId in pairs(cooldowns) do	
    	cooldownframes[spellId] = CreateFrame("frame","", parent)
    	cooldownframes[spellId]:SetSize(size, size)
    	cooldownframes[spellId]:SetPoint("TOPLEFT", i * size, -size)    -- row 2, column 1+ [Spell Cooldowns]
    	cooldownframes[spellId].t = cooldownframes[spellId]:CreateTexture()        
    	cooldownframes[spellId].t:SetColorTexture(1, 1, 1, alphaColor)
    	cooldownframes[spellId].t:SetAllPoints(cooldownframes[spellId])
    	cooldownframes[spellId]:Show()		               
    	i = i + 1
    end
		cooldownframes[table.maxn (cooldownframes)]:SetScript("OnUpdate", updateSpellCooldowns)
	--print ("Initialising Spell In Range Frames")
	i = 0
	for _, spellId in pairs(cooldowns) do	
		spellInRangeFrames[spellId] = CreateFrame("frame","", parent)
		spellInRangeFrames[spellId]:SetSize(size, size)
		spellInRangeFrames[spellId]:SetPoint("TOPLEFT", i * size, -size * 2)          -- row 3, column 1+ [Spell In Range]
		spellInRangeFrames[spellId].t = spellInRangeFrames[spellId]:CreateTexture()        
		spellInRangeFrames[spellId].t:SetColorTexture(1, 1, 1, alphaColor)
		spellInRangeFrames[spellId].t:SetAllPoints(spellInRangeFrames[spellId])
		spellInRangeFrames[spellId]:Show()		               
		i = i + 1
	end
		spellInRangeFrames[table.maxn (spellInRangeFrames)]:SetScript("OnUpdate", updateSpellInRangeFrames)
	--print ("Initialising Target Debuff Frames")
	i = 0
	for _, debuffId in pairs(debuffs) do
		targetDebuffFrames[debuffId] = CreateFrame("frame","", parent)
		targetDebuffFrames[debuffId]:SetSize(size, size)
		targetDebuffFrames[debuffId]:SetPoint("TOPLEFT", i * size, -size * 3)         -- row 4, column 1+ [Spell In Range]
		targetDebuffFrames[debuffId].t = targetDebuffFrames[debuffId]:CreateTexture()        
		targetDebuffFrames[debuffId].t:SetColorTexture(1, 1, 1, alphaColor)
		targetDebuffFrames[debuffId].t:SetAllPoints(targetDebuffFrames[debuffId])
		targetDebuffFrames[debuffId]:Show()		               
		i = i + 1
	end
		targetDebuffFrames[table.maxn (targetDebuffFrames)]:SetScript("OnUpdate", updateTargetDebuffs)
	--print ("Initialising Spell Charges Frames")
	i = 0
	for _, spellId in pairs(cooldowns) do	
		updateSpellChargesFrame[spellId] = CreateFrame("frame","", parent)
		updateSpellChargesFrame[spellId]:SetSize(size, size)
		updateSpellChargesFrame[spellId]:SetPoint("TOPLEFT", i * size, -size * 4)    -- row 5, column 1+ [Spell Charges]
		updateSpellChargesFrame[spellId].t = updateSpellChargesFrame[spellId]:CreateTexture()        
		updateSpellChargesFrame[spellId].t:SetColorTexture(1, 1, 1, alphaColor)
		updateSpellChargesFrame[spellId].t:SetAllPoints(updateSpellChargesFrame[spellId])
		updateSpellChargesFrame[spellId]:Show()		               
		i = i + 1
	end 
	    updateSpellChargesFrame[table.maxn (updateSpellChargesFrame)]:RegisterEvent("ACTIONBAR_UPDATE_STATE")
		updateSpellChargesFrame[table.maxn (updateSpellChargesFrame)]:SetScript("OnEvent", updateSpellCharges)

    i = 0
	for _, buffId in pairs(buffs) do
		TargetBuffs[buffId] = CreateFrame("frame","", parent)
        TargetBuffs[buffId]:SetSize(size, size)
        TargetBuffs[buffId]:SetPoint("TOPLEFT", i * size, -size * 5)                            -- row 6, column 1+ [Target Buffs]
		TargetBuffs[buffId].t = TargetBuffs[buffId]:CreateTexture()
        TargetBuffs[buffId].t:SetColorTexture(1, 1, 1, alphaColor)
        TargetBuffs[buffId].t:SetAllPoints(TargetBuffs[buffId])
        TargetBuffs[buffId]:Show()
        i = i + 1
	end
	TargetBuffs[table.maxn (TargetBuffs)]:SetScript("OnUpdate", updateTargetBuffs)

    PlayerMovingFrame = CreateFrame("frame","", parent);
    PlayerMovingFrame:SetSize(size, size);
    PlayerMovingFrame:SetPoint("TOPLEFT", 0, -size * 6)                                         -- row 7, column 1 [Player Is Moving]
    PlayerMovingFrame.t = PlayerMovingFrame:CreateTexture()
    PlayerMovingFrame.t:SetColorTexture(1, 1, 1, alphaColor)
    PlayerMovingFrame.t:SetAllPoints(PlayerMovingFrame)
    PlayerMovingFrame:Show()    
    PlayerMovingFrame:SetScript("OnUpdate", PlayerNotMove)

    AutoAtackingFrame = CreateFrame("frame","", parent);
    AutoAtackingFrame:SetSize(size, size);
    AutoAtackingFrame:SetPoint("TOPLEFT", size, -size * 6)                                      -- row 7, column 2 [Auto Attacking]
    AutoAtackingFrame.t = AutoAtackingFrame:CreateTexture()
    AutoAtackingFrame.t:SetColorTexture(1, 1, 1, alphaColor)
    AutoAtackingFrame.t:SetAllPoints(AutoAtackingFrame)
    AutoAtackingFrame:Show()
	AutoAtackingFrame:RegisterEvent("PLAYER_TARGET_CHANGED")
	AutoAtackingFrame:RegisterEvent("PLAYER_ENTER_COMBAT")
	AutoAtackingFrame:RegisterEvent("PLAYER_LEAVE_COMBAT")
    AutoAtackingFrame:SetScript("OnEvent", AutoAtacking)

    --print ("Initialising Is Player Frame")
    targetIsPlayerFrame = CreateFrame("frame","", parent);
    targetIsPlayerFrame:SetSize(size, size);
    targetIsPlayerFrame:SetPoint("TOPLEFT", 2 * size, -size * 6)                                -- row 7, column 3 [Target Is Player]
    targetIsPlayerFrame.t = targetIsPlayerFrame:CreateTexture()
    targetIsPlayerFrame.t:SetColorTexture(1, 1, 1, alphaColor)
    targetIsPlayerFrame.t:SetAllPoints(targetIsPlayerFrame)
    targetIsPlayerFrame:Show()
	targetIsPlayerFrame:RegisterEvent("PLAYER_TARGET_CHANGED")
    targetIsPlayerFrame:SetScript("OnEvent", updateIsPlayer)
	
    flagFrame = CreateFrame("frame","", parent);
    flagFrame:SetSize(size, size);
    flagFrame:SetPoint("TOPLEFT", 3 * size, -size * 6)                                          -- row 7, column 4 [Outdoors] // Was Flag but was not used
    flagFrame.t = flagFrame:CreateTexture()
    flagFrame.t:SetColorTexture(1, 1, 1, alphaColor)
    flagFrame.t:SetAllPoints(flagFrame)
    flagFrame:Show()
    flagFrame:SetScript("OnUpdate", updateOutdoors)
end

local function InitializeTwo()
	--print ("Initialising Player Debuff Frames")
	local i = 0
	    for i = 1, 2 do
        lastSpellFrame[i] = CreateFrame("FRAME", "", parent);
        lastSpellFrame[i]:SetSize(size, size);
        lastSpellFrame[i]:SetPoint("TOPLEFT", (i + 3) * size, -size * 6)                        -- row 7, column 5 & 6 [Last Spell Casted]
        lastSpellFrame[i].t = lastSpellFrame[i]:CreateTexture()
        lastSpellFrame[i].t:SetColorTexture(0, 0, 0, alphaColor)
        lastSpellFrame[i].t:SetAllPoints(lastSpellFrame[i])
        lastSpellFrame[i]:Show()
    end
		lastSpellFrame[table.maxn (lastSpellFrame)]:RegisterEvent("UNIT_SPELLCAST_SUCCEEDED");
        lastSpellFrame[table.maxn (lastSpellFrame)]:SetScript("OnEvent", updateLastSpell)
    

    --print ("Initialising Target Last Spell Frame")
    for i = 1, 2 do
        targetLastSpellFrame[i] = CreateFrame("FRAME", "", parent);
        targetLastSpellFrame[i]:SetSize(size, size);
        targetLastSpellFrame[i]:SetPoint("TOPLEFT", (i + 5) * size, -size * 6)                  -- row 7, column 7 & 8 [Target Spell Casting Id]
        targetLastSpellFrame[i].t = targetLastSpellFrame[i]:CreateTexture()
        targetLastSpellFrame[i].t:SetColorTexture(0, 0, 0, alphaColor)
        targetLastSpellFrame[i].t:SetAllPoints(targetLastSpellFrame[i])
        targetLastSpellFrame[i]:Show()                
    end
		targetLastSpellFrame[table.maxn (targetLastSpellFrame)]:SetScript("OnUpdate", updateTargetCurrentSpell)
    --print ("Initialising Arena1 Frame")
    for i = 1, 2 do
        targetArena1Frame[i] = CreateFrame("FRAME", "", parent);
        targetArena1Frame[i]:SetSize(size, size);
        targetArena1Frame[i]:SetPoint("TOPLEFT", (i + 7) * size, -size * 6)                     -- row 7, column 9 & 10 [Arena1 Spell Casting Id]
        targetArena1Frame[i].t = targetArena1Frame[i]:CreateTexture()
        targetArena1Frame[i].t:SetColorTexture(0, 0, 0, alphaColor)
        targetArena1Frame[i].t:SetAllPoints(targetArena1Frame[i])
        targetArena1Frame[i]:Show()                
	end
		targetArena1Frame[table.maxn (targetArena1Frame)]:SetScript("OnUpdate", updateArena1Spell)
    --print ("Initialising Arena2 Frame")
    for i = 1, 2 do
        targetArena2Frame[i] = CreateFrame("FRAME", "", parent);
        targetArena2Frame[i]:SetSize(size, size);
        targetArena2Frame[i]:SetPoint("TOPLEFT", (i + 9) * size, -size * 6)                     -- row 7, column 10 & 11 [Arena1 Spell Casting Id]
        targetArena2Frame[i].t = targetArena2Frame[i]:CreateTexture()
        targetArena2Frame[i].t:SetColorTexture(0, 0, 0, alphaColor)
        targetArena2Frame[i].t:SetAllPoints(targetArena2Frame[i])
        targetArena2Frame[i]:Show()                
    end
		targetArena2Frame[table.maxn (targetArena2Frame)]:SetScript("OnUpdate", updateArena2Spell)
    --print ("Initialising Arena3 Frame")
    for i = 1, 2 do
        targetArena3Frame[i] = CreateFrame("FRAME", "", parent);
        targetArena3Frame[i]:SetSize(size, size);
        targetArena3Frame[i]:SetPoint("TOPLEFT", (i + 11) * size, -size * 6)                    -- row 7, column 12 & 13 [Arena1 Spell Casting Id]
        targetArena3Frame[i].t = targetArena3Frame[i]:CreateTexture()
        targetArena3Frame[i].t:SetColorTexture(0, 0, 0, alphaColor)
        targetArena3Frame[i].t:SetAllPoints(targetArena3Frame[i])
        targetArena3Frame[i]:Show()                
        
    end
		targetArena3Frame[table.maxn (targetArena3Frame)]:SetScript("OnUpdate", updateArena3Spell)
	--print ("Initialising Player Buff Frames")
	i = 0
	for _, buffId in pairs(buffs) do
		buffFrames[buffId] = CreateFrame("frame","", parent)
		buffFrames[buffId]:SetSize(size, size)
		buffFrames[buffId]:SetPoint("TOPLEFT", i * size, -size * 7)                            -- row 8 [Player Buffs]
		buffFrames[buffId].t = buffFrames[buffId]:CreateTexture()        
		buffFrames[buffId].t:SetColorTexture(1, 1, 1, alphaColor)
		buffFrames[buffId].t:SetAllPoints(buffFrames[buffId])
		buffFrames[buffId]:Show()		               
		i = i + 1
	end
		buffFrames[table.maxn (buffFrames)]:SetScript("OnUpdate", updateMyBuffs)
    i = 0
    for _, itemId in pairs(items) do    
        itemframes[itemId] = CreateFrame("frame","", parent)
        itemframes[itemId]:SetSize(size, size)
        itemframes[itemId]:SetPoint("TOPLEFT", i* size, -(size * 8))                           -- row 9 [Item Cooldowns]
        itemframes[itemId].t = itemframes[itemId]:CreateTexture()
        itemframes[itemId].t:SetColorTexture(1, 1, 1, alphaColor)
        itemframes[itemId].t:SetAllPoints(itemframes[itemId])
        itemframes[itemId]:Show()
        i = i + 1
    end
	if(table.getn(items) ~= 0) then
		itemframes[table.maxn (itemframes)]:SetScript("OnUpdate", updateItemCooldowns)
	end

	for _, debuffId in pairs(debuffs) do
		playerDebuffFrames[debuffId] = CreateFrame("frame","", parent)
		playerDebuffFrames[debuffId]:SetSize(size, size)
		playerDebuffFrames[debuffId]:SetPoint("TOPLEFT", i * size, -size * 10)                 -- row 11, column 1+ [Player Debuff Frames]
		playerDebuffFrames[debuffId].t = playerDebuffFrames[debuffId]:CreateTexture()        
		playerDebuffFrames[debuffId].t:SetColorTexture(1, 1, 1, alphaColor)
		playerDebuffFrames[debuffId].t:SetAllPoints(playerDebuffFrames[debuffId])
		playerDebuffFrames[debuffId]:Show()		               
		playerDebuffFrames[debuffId]:SetScript("OnUpdate", updatePlayerDebuffs)
		i = i + 1
	end	
		playerDebuffFrames[table.maxn (playerDebuffFrames)]:SetScript("OnUpdate", updatePlayerDebuffs)		

    --print ("Initialising Spell Overlayed Frames")
    i = 0
    for _, spellId in pairs(cooldowns) do	
    	spellOverlayedFrames[spellId] = CreateFrame("frame","", parent)
    	spellOverlayedFrames[spellId]:SetSize(size, size)
    	spellOverlayedFrames[spellId]:SetPoint("TOPLEFT", i * size, -size * 11)    -- row 12, column 1+ [Spell IsOverlayed]
    	spellOverlayedFrames[spellId].t = spellOverlayedFrames[spellId]:CreateTexture()        
    	spellOverlayedFrames[spellId].t:SetColorTexture(1, 1, 1, alphaColor)
    	spellOverlayedFrames[spellId].t:SetAllPoints(spellOverlayedFrames[spellId])
    	spellOverlayedFrames[spellId]:Show()		               
    	i = i + 1
    end
	if(table.getn(cooldowns) ~= 0) then
		spellOverlayedFrames[table.maxn (spellOverlayedFrames)]:SetScript("OnUpdate", updateIsSpellOverlayedFrames)
	end
		for i = 1, 5 do
		PlayerStatFrame[i] = CreateFrame("frame", "", parent)
		PlayerStatFrame[i]:SetSize(size, size)
		PlayerStatFrame[i]:SetPoint("TOPLEFT", size*(i-1), -size *23 )   --  row 1-5,  column 24
		PlayerStatFrame[i].t =PlayerStatFrame[i]:CreateTexture()        
		PlayerStatFrame[i].t:SetColorTexture(1, 1, 1, alphaColor)
		PlayerStatFrame[i].t:SetAllPoints(PlayerStatFrame[i])
		PlayerStatFrame[i]:Show()
	end
		PlayerStatFrame[table.maxn (PlayerStatFrame)]:RegisterEvent("PLAYER_ENTERING_WORLD")
		PlayerStatFrame[table.maxn (PlayerStatFrame)]:RegisterEvent("PLAYER_EQUIPMENT_CHANGED")
		PlayerStatFrame[table.maxn (PlayerStatFrame)]:RegisterEvent("PLAYER_REGEN_DISABLED")
		PlayerStatFrame[table.maxn (PlayerStatFrame)]:SetScript("OnEvent",Talents)

		npcDetect.npcCountFrame = CreateFrame("frame", "", parent)
		npcDetect.npcCountFrame:SetSize(size, size)
		npcDetect.npcCountFrame:SetPoint("TOPLEFT", size*(0), -size *22 )   --  row 0,  column 20
		npcDetect.npcCountFrame.t = npcDetect.npcCountFrame:CreateTexture()        
		npcDetect.npcCountFrame.t:SetColorTexture(1, 1, 1, alphaColor)
		npcDetect.npcCountFrame.t:SetAllPoints(npcDetect.npcCountFrame)
		npcDetect.npcCountFrame:Show()
		npcDetect.npcCountFrame:SetScript("OnUpdate",NameplateFrameUPDATE)
end
local function eventHandler(self, event, ...)
	local arg1 = ...
	if event == "ADDON_LOADED" then
		if (arg1 == "[PixelMagic]") then
			InitializeOne()
            InitializeTwo()
		end
	end
		 if event == "NAME_PLATE_UNIT_ADDED" then
			AddNameplate(select(1,...))
			activeEnemies()
    end
		if event == "PLAYER_ENTERING_WORLD"then
		TurnOnPlates()
	end
		if event == "PLAYER_REGEN_DISABLED"then 
		ClearNamePlates()
	end
	if event == "PLAYER_REGEN_ENABLED"then 
		ClearNamePlates()
		activeEnemies()
	end

end	
parent:SetScript("OnEvent", eventHandler)


