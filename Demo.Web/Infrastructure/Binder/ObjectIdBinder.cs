using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Threading.Tasks;
using Demo.Util;

namespace Demo.Infrastructure.Binder
{
    public class ObjectIdBinder : IModelBinder
    {
        private readonly bool _nullable;

        public ObjectIdBinder(bool nullable = false)
        {
            _nullable = nullable;
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var modelName = bindingContext.ModelName ?? bindingContext.FieldName;
            var result = bindingContext.ValueProvider.GetValue(modelName);

            bindingContext.ModelState.SetModelValue(modelName, result);

            bindingContext.Result = ModelBindingResult.Success(result.FirstValue.ToObjectId());

            return Task.CompletedTask;
        }
    }
}