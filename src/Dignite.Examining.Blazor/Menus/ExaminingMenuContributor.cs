using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace Dignite.Examining.Blazor.Menus
{
    public class ExaminingMenuContributor : IMenuContributor
    {
        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenuAsync(context);
            }
        }

        private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
        {
            //Add main menu items.
            context.Menu.AddItem(new ApplicationMenuItem(ExaminingMenus.Prefix, displayName: "考试", "/examining", icon: "fa fa-globe"));


            context.Menu.AddItem(new ApplicationMenuItem(ExaminingMenus.Prefix, displayName: "消息", "/messages", icon: "fa fa-heartbeat"));
            context.Menu.AddItem(new ApplicationMenuItem(ExaminingMenus.Prefix, displayName: "排行榜", "/rank", icon: "fa fa-cloud"));
            context.Menu.AddItem(new ApplicationMenuItem(ExaminingMenus.Prefix, displayName: "我的", "/my", icon: "fa fa-user"));

            return Task.CompletedTask;
        }
    }
}