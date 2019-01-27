using VRTK;

class Utils2
{

    public static float MapValue(float value, Limits2D inRange, Limits2D outRange)
    {
        return outRange.minimum + (outRange.maximum - outRange.minimum) * ((value - inRange.minimum) / (inRange.maximum - inRange.minimum));
    }
}
