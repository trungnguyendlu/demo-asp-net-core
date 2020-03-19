using System;
using System.Threading.Tasks;
using Demo.Data;
using Demo.Infrastructure.Utils;
using Demo.Model.Admin.Settings;
using Demo.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    public partial class AdminController
    {
        [Authorize(Policy = nameof(FunctionType.SettingsEdit))]
        public async Task<ActionResult> Settings()
        {
            SetTitle("Thiết lập");

            var settings = await _cacheHelper.GetOrSetAsync(CacheKey.GetSettings,()=> _settingsService.GetByIdAsync(Constants.DefaultSettingsId), TimeSpan.FromDays(30));
            if (settings == null)
            {
                settings = _mapper.Map<Entity.Settings>(Common.Settings);
                settings.Id = Constants.DefaultSettingsId;
                await _settingsService.SaveAsync(new SaveRequest<Entity.Settings>
                {
                    Entity = settings,
                    UserId = _sessionHelper.CurrentUserId
                });
            }
            
            var model = new SettingsEditModel
            {
                Entity = _mapper.Map<SettingsModel>(settings)
            };

            return View("Settings", model);
        }

        [HttpPost]
        [Authorize(Policy = nameof(FunctionType.SettingsEdit))]
        public async Task<ActionResult> SaveSettings(SettingsEditModel model)
        {
            if (model.Entity.IsCommingSoon && !model.Entity.OpeningDate.HasValue)
            {
                ModelState.AddModelError("Entity.OpeningDate", "Chưa nhập Ngày khai trương");
            }

            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = string.Join(", ", GetModelStateErrors()) });
            }

            var entity = _mapper.Map<Entity.Settings>(model.Entity);
            var response = await _settingsService.SaveAsync(new SaveRequest<Entity.Settings>
            {
                Entity = entity,
                IsEdit = true,
                UserId = _sessionHelper.CurrentUserId
            });

            if (response.Success)
            {
                var settings = await _settingsService.GetByIdAsync(Constants.DefaultSettingsId);
                if (settings != null)
                {
                    Common.Settings = settings;
                }
                _cacheHelper.Clear(CacheKey.GetSettings);
            }

            return Json(new { success = response.Success, message = string.Join(", ", response.Messages) });
        }
    }
}