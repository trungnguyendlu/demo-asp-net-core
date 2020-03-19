using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using MongoDB.Bson;
using System;

namespace Demo.Infrastructure.Binder
{
    public class ObjectIdBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(ObjectId))
            {
                return new BinderTypeModelBinder(typeof(ObjectIdBinder));
            }

            return null;
        }
    }
}