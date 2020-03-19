using System;
using AutoMapper;
using Demo.Infrastructure.Authorize;
using Demo.Infrastructure.Helper;
using Demo.Model;
using Demo.Model.Dashboard;
using Demo.BusinessLogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Demo.Util;

namespace Demo.Controllers
{
    [Authorize]
    [SessionTimeout]
    public partial class AdminController : BaseController
    {
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;
        private readonly ICategoryService _categoryService;
        private readonly IBlogService _blogService;
        private readonly IMediaService _mediaService;
        private readonly ISettingsService _settingsService;
        private readonly IWidgetService _widgetService;
        private readonly ISessionHelper _sessionHelper;
        private readonly ICacheHelper _cacheHelper;
        private readonly IMapper _mapper;


        public AdminController(IRoleService roleService,
            IUserService userService,
            ICategoryService categoryService,
            IBlogService blogService,
            IMediaService mediaService,
            ISettingsService settingsService,
            IWidgetService widgetService,
            ISessionHelper sessionHelper,
            ICacheHelper cacheHelper,
            IMapper mapper)
        {
            _roleService = roleService;
            _userService = userService;
            _categoryService = categoryService;
            _blogService = blogService;
            _mediaService = mediaService;
            _settingsService = settingsService;
            _widgetService = widgetService;
            _sessionHelper = sessionHelper;
            _cacheHelper = cacheHelper;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            SetTitle("Dashboard");
            return View(new DashboardModel());
        }

        private void UpdateBaseInfo(bool isEdit, BaseModel baseModel)
        {
            if (isEdit)
            {
                baseModel.UpdatedUserId = _sessionHelper.CurrentUserId;
                baseModel.UpdatedDate = DateTime.Now;
            }
            else
            {
                baseModel.CreatedUserId = _sessionHelper.CurrentUserId;
                baseModel.CreatedDate = DateTime.Now;
            }
        }
    }
}