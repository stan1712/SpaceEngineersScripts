/*##############################################################################
# 	Script Name: VelocityLock
# 	Author: stanislasbdx (stan1712.com)
##############################################################################*/

// Minimum Velocity to trigger the door lock.
double minVelocity = 0.05;

// Custom Tag to put on the lockable elements (doors, hatches).
string Doors_NameTag = "[VelocityLock]";

/*############################################################################*/
public Program()
{
    Runtime.UpdateFrequency = UpdateFrequency.Update10;
}

public void Main()
{
	/** GET LOCKABLE DOORS **/
	var DoorsList = new List<IMyTerminalBlock>();						
	GridTerminalSystem.SearchBlocksOfName(Doors_NameTag, DoorsList);
    Echo("==[ INFOS ]==");

    if(DoorsList.Count == 0) {
        Echo("-> Aucun accès ne sera verrouillé.");
    }
    else if(DoorsList.Count == 1) Echo($"-> {DoorsList.Count.ToString()} accès sera verrouillé.");
    else {
        Echo($"-> {DoorsList.Count.ToString()} accès seront verrouillés.");
    }

	/** GET VELOCITY **/
    IMyCockpit cockpit_seat = GridTerminalSystem.GetBlockWithName("Contrôle Principal") as IMyCockpit;
    double velocity = Math.Round(cockpit_seat.GetShipVelocities().LinearVelocity.Sum, 2);
    Echo($"Vélocité : {velocity.ToString()}m/s");

    Echo("\n==[ ACCÈS ]==");
    for (int i=0; i<DoorsList.Count; i++){
        string DoorName = DoorsList[i].CustomName;
        IMyDoor DoorElement = GridTerminalSystem.GetBlockWithName(DoorName) as IMyDoor;

        bool velocityAchieved = velocity >= minVelocity;
        float ORation = DoorElement.OpenRatio;
        string State = velocityAchieved ? "Verrouillé" : "Déverrouillé";
        string OpenState = ORation == 0 ? "Fermé" : "Ouvert";

        if(velocityAchieved) {
            if(ORation == 0) DoorElement.ApplyAction($"OnOff_Off");
            else DoorElement.ApplyAction($"Open_Off");
        }
        else DoorElement.ApplyAction($"OnOff_On");

        if(velocityAchieved) Echo($"-> {DoorName.Replace(Doors_NameTag, "")} : {State}");
        else Echo($"-> {DoorName.Replace(Doors_NameTag, "")} : {State} et {OpenState}");
    }
}