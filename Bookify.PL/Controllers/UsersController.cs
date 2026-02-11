using Bookify.BLL.DTOs.User;
using Bookify.DAL.Common.Consts;
using Bookify.PL.ViewModels.User;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Bookify.PL.Controllers
{
    [Authorize(Roles = AppRoles.Admin)]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _userService.GetAllAsync();
            if (result.HasErrorMessage)
                return StatusCode((int)result.StatusCode, result.ErrorMessage);

            var vm = _mapper.Map<IEnumerable<UserViewModel>>(result.Result);
            return View(vm);
        }

        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> Create()
        {
            var roles = await _userService.GetRolesAsync();

            if (roles.HasErrorMessage)
                return StatusCode((int)roles.StatusCode, roles.ErrorMessage);

            var viewModel = new UserFormViewModel
            {
                Roles = roles.Result!.Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                }).ToList()
            };
            return PartialView("_Form", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserFormViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState[nameof(model.FullName)]?.Errors.First().ErrorMessage);

            var dto = _mapper.Map<UserCreateDTO>(model);
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var result = await _userService.CreateAsync(dto, currentUserId);

            if (result.HasErrorMessage)
                return StatusCode((int)result.StatusCode, result.ErrorMessage);

            var viewModel = _mapper.Map<UserViewModel>(result.Result);
            return PartialView("_UserRow", viewModel);
        }

        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> Edit(string id)
        {
            var userDto = await _userService.GetForEditAsync(id);
            var viewModel = _mapper.Map<UserFormViewModel>(userDto.Result);

            var roles = await _userService.GetRolesAsync();
            viewModel.Roles = roles.Result?.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Name
            }).ToList();

            return PartialView("_Form", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserFormViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState[nameof(model.FullName)]?.Errors.First().ErrorMessage);

            var dto = _mapper.Map<UserUpdateDTO>(model);
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var result = await _userService.UpdateAsync(dto, currentUserId);

            if (result.HasErrorMessage)
                return StatusCode((int)result.StatusCode, result.ErrorMessage);

            var viewModel = _mapper.Map<UserViewModel>(result.Result);
            return PartialView("_UserRow", viewModel);
        }


        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> ResetPassword(string id)
        {
            var userDto = await _userService.GetForResetPasswordAsync(id);
            if (userDto.HasErrorMessage)
                return StatusCode((int)userDto.StatusCode, userDto.ErrorMessage);

            var viewModel = _mapper.Map<ResetPasswordFormViewModel>(userDto.Result);
            return PartialView("_ResetPasswordForm", viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordFormViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState[nameof(model.Password)]?.Errors.First().ErrorMessage);

            var dto = _mapper.Map<UserResetPasswordDTO>(model);
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var result = await _userService.ResetPasswordAsync(dto, currentUserId);

            if (result.HasErrorMessage)
                return StatusCode((int)result.StatusCode, result.ErrorMessage);

            var viewModel = _mapper.Map<UserViewModel>(result.Result);
            return PartialView("_UserRow", viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleStatus(string id)
        {
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var result = await _userService.ToggleStatusAsync(id, currentUserId);
            if (result.HasErrorMessage)
                return StatusCode((int)result.StatusCode, result.ErrorMessage);

            return Ok(result.Result);
        }

        public async Task<IActionResult> AllowUserName(UserFormViewModel model)
        {
            var isAllowed = await _userService.IsUserNameAvailableAsync(model.UserName, model.Id);
            return Json(isAllowed);
        }

        public async Task<IActionResult> AllowEmail(UserFormViewModel model)
        {
            var isAllowed = await _userService.IsEmailAvailableAsync(model.Email, model.Id);
            return Json(isAllowed);
        }
    }
}
