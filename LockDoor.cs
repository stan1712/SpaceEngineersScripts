public Program()
{
    Runtime.UpdateFrequency = UpdateFrequency.Update10;
}

public void Main()
{
	/** GET LOCKABLE DOORS **/
	var DoorsList = new List<IMyTerminalBlock>();						
	GridTerminalSystem.GetBlocksOfType<IMyDoor>(DoorsList);
	Echo(DoorsList.Count.ToString());

	/** GET VELOCITY **/
    var cockpit_seat = GridTerminalSystem.GetBlockWithName("Contrôle Principal") as IMyCockpit;
    var velocity = cockpit_seat.GetShipVelocities().LinearVelocity.Sum;

    Echo("Vélocité : " + velocity.ToString());
}