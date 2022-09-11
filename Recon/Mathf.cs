namespace Recon.Math
{
    public static class Mathf
    {
        public static float lerp(float a, float b, float f)
        {
            return a + f * (b - a);
        }


        public static float ClampA(float value)
        {
            bool flag = value < 0f;
            float result;
            if (flag)
            {
                result = 0f;
            }
            else
            {
                bool flag2 = value > 1f;
                if (flag2)
                {
                    result = 1f;
                }
                else
                {
                    result = value;
                }
            }
            return result;
        }
    }
}