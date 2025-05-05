# QBCore methods

> These lists may include methods that are not part of base QBCore, but are intended to provide similar functionality to what is available in Lua.

## [Server](https://docs.qbcore.org/qbcore-documentation/qb-core/server-function-reference)
  - [x] GetCoords (there is a native for it)
  - [ ] GetIdentifier
  - [ ] GetSource
  - [x] GetPlayer (isn't needed, the wrapper does everything under the hood)
  - [ ] GetPlayerByCitizenId
  - [ ] GetPlayerByPhone
  - [ ] GetQBPlayers (untested)
  - [ ] CreateCallback
  - [x] CreateUseableItem
  - [x] CanUseItem
  - [x] UseItem (implemented using the `QBCore:Client:UseItem`) (untested)
  - [x] Kick
  - [x] AddPermission
  - [x] RemovePermission
  - [x] HasPermission
  - [x] GetPermission
  - [x] IsPlayerBanned
  - [x] IsLicenseInUse
  - ### [Player Object](https://docs.qbcore.org/qbcore-documentation/qb-core/player-data#player-object-functions)
    - [x] UpdatePlayerData
    - [x] SetJob
    - [x] SetGang
    - [x] Notify (implemented using the `QBCore:Notify`)
    - [ ] HasItem
    - [x] GetName
    - [ ] SetJobDuty
    - [ ] SetPlayerData
    - [ ] SetMetaData
    - [ ] GetMetaData
    - [ ] AddRep
    - [ ] RemoveRep
    - [ ] GetRep
    - [x] AddMoney
    - [x] RemoveMoney
    - [x] SetMoney
    - [x] GetMoney
    - [x] Save
    - [x] Logout
    - [ ] AddMethod
    - [ ] AddField
  - ### [Events](https://docs.qbcore.org/qbcore-documentation/qb-core/drawtext)
    - [x] `qb-core:client:DrawText` (it works like a broadcast)
    - [x] `qb-core:client:ChangeText` (it works like a broadcast)
    - [x] `qb-core:client:HideText` (it works like a broadcast)
    - [ ] `qb-core:client:KeyPressed`


## [Client](https://docs.qbcore.org/qbcore-documentation/qb-core/client-function-reference)
  - [x] GetPlayerData
  - [x] GetCoords (recreated it natively in NativesPlus)
  - [x] HasItem
  - [x] Notify
  - [ ] TriggerCallback
  - [x] Progressbar
  - [x] GetCoreObject (isn't needed, the wrapper does everything under the hood)
  - [ ] GetPeds
  - [x] GetClosestPed
  - [x] GetClosestVehicle
  - [x] GetClosestObject
  - [x] GetClosestPlayer
  - [x] GetPlayersFromCoords
  - [x] SpawnVehicle (there is a native for it)
  - [x] DeleteVehicle (there is a native for it)
  - [x] GetPlate
  - [ ] GetVehicleProperties
  - [ ] SetVehicleProperties
  - ### [Events](https://docs.qbcore.org/qbcore-documentation/qb-core/client-event-reference)
    - [x] Notify (not in this form, but using the `QBcore.Functions.Notify()`)
    - [x] UseItem (untested)
    - [x] ShowMe3D (untested)
    - [x] UpdateObject (isn't needed, the wrapper does everything under the hood)
  - ### [Exports](https://docs.qbcore.org/qbcore-documentation/qb-core/drawtext)
    - [ ] DrawText
    - [ ] ChangeText
    - [ ] HideText
    - [ ] KeyPressed

## [Shared (tables)](https://docs.qbcore.org/qbcore-documentation/qb-core/shared)
  - [x] QBShared.Items{ }
    - [x] GetItem
    - [ ] GetItems
  - [x] QBShared.Jobs{ }
    - [x] GetJob
    - [ ] GetJobs
  - [x] QBShared.Gangs{ }
    - [x] GetGang
    - [ ] GetGangs
  - [ ] QBShared.Vehicles
    - [ ] GetVehicle
    - [ ] GetVehicles
  - [ ] QBShared.Weapons
    - [ ] AddWeapon
    - [ ] AddWeapons
    - [ ] GetWeapon
    - [ ] GetWeapons
  - ### [Exports](https://docs.qbcore.org/qbcore-documentation/qb-core/shared-exports)
    - [x] AddItem
    - [ ] AddItems
    - [x] AddJob
    - [ ] AddJobs
    - [x] AddGang
    - [ ] AddGangs

> Methods kept for backwards compatibility aren't going to be added!
> - Client > functions
    >   - GetVehicles
>   - GetPlayers

> Nor deprecated ones (obviously)
> - Server > functions
    >   - GetPlayers
