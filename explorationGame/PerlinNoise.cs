
using System; 
using UnityEngine; 
using UnityEngine.SocialPlatforms;
 public class PerlinNoise : MonoBehaviour {
    public static float PerlinNoise2D(int x, int y, float FREQUENCY, float GAIN, float LACUNARITY, int OCTAVES) {
        float result = 0;
        for (int Octave = 1; Octave <= OCTAVES; Octave++) {
            float AdjustedFrequency = FREQUENCY * Mathf.Pow(LACUNARITY, Octave - 1);
            result += Mathf.PerlinNoise(x * AdjustedFrequency, y * AdjustedFrequency) * Mathf.Pow(GAIN, Octave); } return Mathf.Clamp(result, 0, 1); }
}
