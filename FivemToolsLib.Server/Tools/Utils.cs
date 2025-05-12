namespace FivemToolsLib.Server.Tools
{
    public class Utils
    {
        public static int ComputeJoaat(string input)
        {
            input = input.ToLowerInvariant();
            uint hash = 0;

            foreach (var c in input)
            {
                hash += c;
                hash += hash << 10;
                hash ^= hash >> 6;
            }

            hash += hash << 3;
            hash ^= hash >> 11;
            hash += hash << 15;

            return unchecked((int)hash);
        }
    }
}