using MongoDB.Bson;
using System;

namespace Demo.Data
{
    [Serializable]
    public class Reference<T> : Reference where T : struct
    {
        public Reference(ObjectId id, string name = "", T data = default(T)) 
            : base(id, name)
        {
            Data = data;
        }

        public T Data { get; set; }
    }

	[Serializable]
	public class Reference
	{
		public Reference(ObjectId id, string name = "")
		{
			Id = id;
			Name = name;
		}

        public ObjectId Id { get; set; }
        
		public string Name { get; set; }
	}
}
