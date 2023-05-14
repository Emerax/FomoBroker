using System.Collections.Generic;
using System.Linq;

public class AttractionManager {
    private readonly List<Temple> temples;

    public AttractionManager(List<Temple> temples) {
        this.temples = temples;
    }

    public float[] GetAttractionRatios() {
        float attractionSum = temples.Sum(t => t.Attraction);
        return new float[] {
            temples[0].Attraction / attractionSum,
            temples[1].Attraction / attractionSum,
            temples[2].Attraction / attractionSum,
        };
    }

    public void ChangeAttraction(float value, int target) {
        temples[target].ChangeAttraction(value);
    }

    public void DecayAttraction() {
        //TODO: Implement attraction decay here if we want it.
    }

}
