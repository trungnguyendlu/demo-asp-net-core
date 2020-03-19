using MongoDB.Bson;

namespace Demo.Util
{
    public static class ObjectIdExtension
    {
        public static bool HasValue(this ObjectId id)
        {
            return id != null && id != ObjectId.Empty;
        }

        public static ObjectId ToObjectId(this string input)
        {
            try
            {
                if (string.IsNullOrEmpty(input))
                {
                    return ObjectId.Empty;
                }
                return new ObjectId(input);
            }
            catch
            {
                return ObjectId.Empty;
            }
        }
    }
}
