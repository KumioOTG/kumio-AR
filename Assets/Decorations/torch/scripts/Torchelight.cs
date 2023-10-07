using UnityEngine;

public class Torchelight : MonoBehaviour
{
    public Light TorchLight;  // Changed type to Light for direct access
    public ParticleSystem MainFlame;
    public ParticleSystem BaseFlame;
    public ParticleSystem Etincelles;
    public ParticleSystem Fumee;
    public float MaxLightIntensity;
    public float IntensityLight;

    void Start()
    {
        UpdateEffects();
    }

    void Update()
    {
        IntensityLight = Mathf.Clamp(IntensityLight, 0, MaxLightIntensity);
        UpdateEffects();
    }

    private void UpdateEffects()
    {
        if (TorchLight != null)
        {
            TorchLight.intensity = IntensityLight / 2f + Mathf.Lerp(IntensityLight - 0.1f, IntensityLight + 0.1f, Mathf.Cos(Time.time * 30));
            TorchLight.color = new Color(Mathf.Min(IntensityLight / 1.5f, 1f), Mathf.Min(IntensityLight / 2f, 1f), 0f);
        }

        if (MainFlame != null)
            MainFlame.emissionRate = IntensityLight * 20f;

        if (BaseFlame != null)
            BaseFlame.emissionRate = IntensityLight * 15f;

        if (Etincelles != null)
            Etincelles.emissionRate = IntensityLight * 7f;

        if (Fumee != null)
            Fumee.emissionRate = IntensityLight * 12f;
    }
}
