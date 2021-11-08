using AppGham.Shared;
using FreshMvvm;
using MvvmHelpers.Commands;

namespace AppGham.PageModels
{
    public class UserPageModel : FreshBasePageModel
    {
        public UserPageModel()
        {
            EditCommand = new AsyncCommand(async () => await CoreMethods.PushPageModel<UserEditorPageModel>(User));
            LogoutCommand = new AsyncCommand(async () => await CoreMethods.PushPageModelWithNewNavigation<LoginPageModel>(true, true));
        }

        public AsyncCommand EditCommand { get; private set; }

        public AsyncCommand LogoutCommand { get; private set; }

        public IUser User { get; private set; }

        public override void Init(object initData) =>  User = initData as IUser;
    }
}
