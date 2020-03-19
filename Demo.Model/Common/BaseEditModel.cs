using System;

namespace Demo.Model
{
	[Serializable]
	public class BaseEditModel<T> where T : BaseModel
	{
		public BaseEditModel()
		{
			Entity = Activator.CreateInstance<T>();
		}

		public T Entity { get; set; }

		public bool IsEdit { get; set; }
	}
}
