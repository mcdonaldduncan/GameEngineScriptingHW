[System.Serializable]
public class Rootobject
{
    public string SceneName;
    public PaddleTester paddleTester;
    public SphereTester sphereTester;
}
[System.Serializable]
public class PaddleTester
{
    public float TranslateIncrement;
    public float AngleIncrement;
}
[System.Serializable]
public class SphereTester
{

    public float increment;

}

