[System.Serializable]
public class Rootobject
{
    public string SceneName;
    public VehicleTester vehicleTester;
    public FollowerTester followerTester;
}
[System.Serializable]
public class VehicleTester
{
    public float Left;
    public float Right;
    public float MoveVehicle;
    public float Up;
    public float Down;
    public bool IsPaused;
    public bool IsPastStop;

}
[System.Serializable]
public class FollowerTester
{
    public float[] currentPosition;
    public float[] lastPosition;
    public float[] testLastSubjectPosition;

}

